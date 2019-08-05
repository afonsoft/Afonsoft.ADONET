using Afonsoft.ADONET.Interfaces;
using Afonsoft.ADONET.Options;
using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using Afonsoft.Extensions;

namespace Afonsoft.ADONET
{
    public class Connection : ISqlConn
    {
        private readonly ISqlProvider _sqlProvider;
        private readonly IDbConnection _conn;
        private readonly AfonsoftADOOptions _options;
        private IDbTransaction _transaction;

        private static AfonsoftADOOptions Build(Action<AfonsoftADOOptions> options)
        {
            var opt = new AfonsoftADOOptions();
            options.Invoke(opt);
            return opt;
        }

        public Connection(Action<AfonsoftADOOptions> options)
        {
            _options = Build(options);
            _sqlProvider = GetProvider(_options.Provider);
            _conn = _sqlProvider.Connection;
            ExistConnection();
        }

        #region GetProvider
        private ISqlProvider GetProvider(EnumProvider provider)
        {
            ISqlProvider rt;
            switch (provider)
            {
                case EnumProvider.SQLServer:
                    rt = new Providers.SQLServer(_options.ConnectionString);
                    break;
                case EnumProvider.MySQL:
                    rt = new Providers.MySQL(_options.ConnectionString);
                    break;
                case EnumProvider.SQLite:
                    rt = new Providers.SQLite(_options.ConnectionString);
                    break;
                case EnumProvider.PostgreSQL:
                    rt = new Providers.PostGreSql(_options.ConnectionString);
                    break;
                case EnumProvider.Oracle:
                    rt = new Providers.Oracle(_options.ConnectionString);
                    break;
                case EnumProvider.Unknown:
                    throw new Exception("Unknown Provider");
                default:
                    rt = new Providers.SQLite(_options.ConnectionString);
                    break;
            }

            return rt;
        }
        #endregion

        #region AttachParameters
        private void AttachParameters(IDbCommand command, IDataParameter[] parameters)
        {
            foreach (IDataParameter idbParameter in parameters)
            {
                if (idbParameter.Direction == ParameterDirection.Input || idbParameter.Direction == ParameterDirection.InputOutput)
                {
                    try
                    {
                        
                        if (idbParameter.Value == null)
                            idbParameter.Value = DBNull.Value;
                        else if (string.IsNullOrEmpty(Convert.ToString(idbParameter.Value)))
                            idbParameter.Value = DBNull.Value;
                    }
                    catch
                    {
                        // ignored
                    }
                }

                command.Parameters.Add(idbParameter);
            }
        }

        #endregion


        private void ExistConnection()
        {
            if (_conn == null || string.IsNullOrEmpty(_options.ConnectionString))
            {
                throw new Exception("Não existe uma string de conexão. (There is a no connection string.)");
            }
        }

        public EnumProvider Provider => _options.Provider;

        public string ConectionString => _options.ConnectionString;

        public bool IsOpen
        {
            get
            {
                ExistConnection();
                return _conn.State == ConnectionState.Open;
            }
        }

        public bool IsClose
        {
            get
            {
                ExistConnection();
                return _conn.State == ConnectionState.Closed;
            }
        }

        public ConnectionState State
        {
            get
            {
                ExistConnection();
                return _conn.State;
            }
        }

        public string DatabaseName
        {
            get
            {
                if (IsOpen)
                    return _conn.Database;
                else
                    return "";
            }
        }

        public IDbConnection DbConnection => _conn;

        public IDbCommand CreateCommand()
        {
            return _sqlProvider.CreateCommand();
        }

        public IDataAdapter CreateDataAdapter(IDbCommand command)
        {
            return _sqlProvider.CreateDataAdapter(command);
        }

        #region Transaction

        public bool ExistTransaction
        {
            get
            {
                if (_transaction != null && IsOpen)
                    return true;
                else
                    return false;
            }
        }
        public bool BeginTransaction()
        {
            if (_transaction == null && IsOpen)
            {
                _transaction = _conn.BeginTransaction(IsolationLevel.ReadCommitted);
                return true;
            }
            else
                return false;
        }

        public bool BeginTransaction(IsolationLevel level)
        {
            if (_transaction == null && IsOpen)
            {
                _transaction = _conn.BeginTransaction(level);
                return true;
            }
            else
                return false;
        }

        public bool CloseConnection()
        {

            if (_conn != null)
                if (_conn.State != ConnectionState.Closed)
                {
                    CommitTransaction();
                    _conn.Close();
                }
            return true;

        }

        public bool CommitTransaction()
        {
            if (_transaction != null && IsOpen)
            {
                _transaction.Commit();
                _transaction = null;
                return true;
            }
            else
                return false;
        }

        public bool OpenConnection()
        {
            if (_conn.State == ConnectionState.Closed)
                _conn.Open();
            return true;
        }

        public bool RollbackTransaction()
        {
            if (_transaction != null && IsOpen)
            {
                _transaction.Rollback();
                _transaction = null;
                return true;
            }
            else
                return false;
        }

        private void IsCloseOpenConnection()
        {
            if (IsClose && _transaction == null && _options.AutoOpenAndCloseConnection)
                OpenConnection();
        }

        private void IsNoTransCloseConnection()
        {
            if (_transaction == null && _options.AutoOpenAndCloseConnection)
                CloseConnection();
        }

        #endregion

        public void Dispose()
        {
            try
            {
                if (_conn != null)
                {
                    CommitTransaction();
                    CloseConnection();
                    _conn.Dispose();
                }
            }
            catch (Exception)
            {
                // ignored
            }
            finally
            {
                GC.Collect();
                GC.SuppressFinalize(this);
            }
        }

        #region ExecuteQuery

        public DataSet ExecuteQuery(string query, CommandType commandType, params IDataParameter[] param)
        {
            try
            {
                DataSet ds = new DataSet();

                using (IDbCommand cd = _sqlProvider.CreateCommand())
                {
                    ExistConnection();
                    cd.Connection = _conn;
                    cd.CommandText = query;
                    cd.CommandType = commandType;
                    cd.CommandTimeout = _options.Timeout;
                    //Adicionar a Transação
                    if (_transaction != null)
                        cd.Transaction = _transaction;
                    //Adicionar os paramentros
                    if (param != null)
                        AttachParameters(cd, param);
                    IDataAdapter da = _sqlProvider.CreateDataAdapter(cd);
                    IsCloseOpenConnection();
                    da.Fill(ds);
                }
                return ds;
            }
            catch (Exception) { RollbackTransaction(); throw; }
            finally { IsNoTransCloseConnection(); }
        }

        public DataSet ExecuteQuery(string query, CommandType commandType)
        {
            return ExecuteQuery(query, commandType, null);
        }

        public DataSet ExecuteQuery(string query)
        {
            return ExecuteQuery(query, CommandType.Text, null);
        }
        #endregion

        #region ExecuteNoQuery
        public void ExecuteNoQuery(string query, CommandType commandType, params IDataParameter[] param)
        {
            try
            {
                using (IDbCommand cd = _sqlProvider.CreateCommand())
                {
                    ExistConnection();
                    cd.Connection = _conn;
                    cd.CommandText = query;
                    cd.CommandType = commandType;
                    cd.CommandTimeout = _options.Timeout;
                    //Adicionar a Transação
                    if (_transaction != null)
                        cd.Transaction = _transaction;
                    //Adicionar os paramentros
                    if (param != null)
                        AttachParameters(cd, param);

                    IsCloseOpenConnection();
                    cd.ExecuteNonQuery();
                }
            }
            catch (Exception) { RollbackTransaction(); CloseConnection(); throw; }
            finally { IsNoTransCloseConnection(); }
        }

        public void ExecuteNoQuery(string query, CommandType commandType)
        {
            ExecuteNoQuery(query, commandType, null);
        }

        public void ExecuteNoQuery(string query)
        {
            ExecuteNoQuery(query, CommandType.Text, null);
        }
        #endregion

        #region ExecuteReader
        public IDataReader ExecuteReader(string query, CommandType commandType, params IDataParameter[] param)
        {
            try
            {
                using (IDbCommand cd = _sqlProvider.CreateCommand())
                {
                    ExistConnection();
                    cd.Connection = _conn;
                    cd.CommandText = query;
                    cd.CommandType = commandType;
                    cd.CommandTimeout = _options.Timeout;
                    //Adicionar a Transação
                    if (_transaction != null)
                        cd.Transaction = _transaction;
                    //Adicionar os paramentros
                    if (param != null)
                        AttachParameters(cd, param);

                    IsCloseOpenConnection();
                    return cd.ExecuteReader(CommandBehavior.Default);
                }
            }
            catch (Exception) { RollbackTransaction(); CloseConnection(); throw; }
            finally { }
        }

        public IDataReader ExecuteReader(string query, CommandType commandType)
        {
            return ExecuteReader(query, commandType, null);
        }

        public IDataReader ExecuteReader(string query)
        {
            return ExecuteReader(query, CommandType.Text, null);
        }
        #endregion

        #region ExecuteScalar

        public object ExecuteScalar(string query, CommandType commandType, params IDataParameter[] param)
        {
            try
            {
                using (IDbCommand cd = _sqlProvider.CreateCommand())
                {
                    ExistConnection();
                    cd.Connection = _conn;
                    cd.CommandText = query;
                    cd.CommandType = commandType;
                    cd.CommandTimeout = _options.Timeout;
                    //Adicionar a Transação
                    if (_transaction != null)
                        cd.Transaction = _transaction;
                    //Adicionar os paramentros
                    if (param != null)
                        AttachParameters(cd, param);

                    IsCloseOpenConnection();
                    return cd.ExecuteScalar();
                }
            }
            catch (Exception) { RollbackTransaction(); CloseConnection(); throw; }
            finally { IsNoTransCloseConnection(); }
        }

        public object ExecuteScalar(string query, CommandType commandType)
        {
            return ExecuteScalar(query, commandType, null);
        }

        public object ExecuteScalar(string query)
        {
            return ExecuteScalar(query, CommandType.Text, null);
        }


        #endregion

        #region  ExecuteQuery<T>
        public IEnumerator<T> ExecuteQuery<T>(string query, CommandType commandType, IDataParameter[] param)
        {
            IDataReader reader = ExecuteReader(query, commandType, param);

            int nuReg = 0;

            IList<T> listOfT = new List<T>();

            while (reader.Read())
            {
                nuReg++;
                T obj = Activator.CreateInstance<T>();
                foreach (PropertyInfo prop in obj.GetType().GetProperties())
                {
                    if (!object.Equals(reader[prop.Name], DBNull.Value))
                    {
                        prop.SetValue(obj, reader[prop.Name], null);
                    }
                }

                listOfT.Add(obj);
            }

            return listOfT.GetEnumerator();
        }

        public IEnumerator<T> ExecuteQuery<T>(string query, CommandType commandType)
        {
            return ExecuteQuery<T>(query, commandType, null);
        }

        public IEnumerator<T> ExecuteQuery<T>(string query)
        {
            return ExecuteQuery<T>(query, CommandType.Text, null);
        }
        #endregion

    }
}
