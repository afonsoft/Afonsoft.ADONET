using Afonsoft.ADONET.Options;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace Afonsoft.ADONET.Interfaces
{
    /// <summary>
    /// Interface do SQLConn
    /// </summary>
    public interface ISqlConn : IDisposable
    {
        #region Interface

        EnumProvider Provider { get; }

        /// <summary>
        /// ConectionString
        /// </summary>
        string ConectionString { get; }

        IDbConnection DbConnection { get; }
        IDbCommand CreateCommand();
        IDataAdapter CreateDataAdapter(IDbCommand command);

        /// <summary>
        /// isOpen
        /// </summary>
        bool IsOpen { get; }
        /// <summary>
        /// isClose
        /// </summary>
        bool IsClose { get; }

        /// <summary>
        /// State
        /// </summary>
        ConnectionState State { get; }
        /// <summary>
        /// DatabaseName
        /// </summary>
        string DatabaseName { get; }

        /// <summary>
        /// OpenConnection
        /// </summary>
        bool OpenConnection();
        /// <summary>
        /// CloseConnection
        /// </summary>
        bool CloseConnection();

        /// <summary>
        /// ExistTransaction
        /// </summary>
        bool ExistTransaction { get; }
        /// <summary>
        /// BeginTransaction
        /// </summary>
        bool BeginTransaction();
        /// <summary>
        /// BeginTransaction
        /// </summary>
        bool BeginTransaction(IsolationLevel level);
        /// <summary>
        /// CommitTransaction
        /// </summary>
        bool CommitTransaction();
        /// <summary>
        /// RollbackTransaction
        /// </summary>
        bool RollbackTransaction();
        
        #endregion

        #region Execute

        /// <summary>
        /// ExecuteNoQuery
        /// </summary>
        void ExecuteNoQuery(string query, CommandType commandType, IDataParameter[] param);
        /// <summary>
        /// ExecuteNoQuery
        /// </summary>
        void ExecuteNoQuery(string query, CommandType commandType);
        /// <summary>
        /// ExecuteNoQuery
        /// </summary>
        void ExecuteNoQuery(string query);


        /// <summary>
        /// ExecuteQuery
        /// </summary>
        DataSet ExecuteQuery(string query, CommandType commandType, IDataParameter[] param);
        /// <summary>
        /// ExecuteQuery
        /// </summary>
        DataSet ExecuteQuery(string query, CommandType commandType);
        /// <summary>
        /// ExecuteQuery
        /// </summary>
        DataSet ExecuteQuery(string query);

        /// <summary>
        /// ExecuteQuery
        /// </summary>
        IEnumerator<T> ExecuteQuery<T>(string query, CommandType commandType, IDataParameter[] param);
        /// <summary>
        /// ExecuteQuery
        /// </summary>
        IEnumerator<T> ExecuteQuery<T>(string query, CommandType commandType);
        /// <summary>
        /// ExecuteQuery
        /// </summary>
        IEnumerator<T> ExecuteQuery<T>(string query);

        /// <summary>
        /// ExecuteQuery
        /// </summary>
        IDataReader ExecuteReader(string query, CommandType commandType, IDataParameter[] param);
        /// <summary>
        /// ExecuteQuery
        /// </summary>
        IDataReader ExecuteReader(string query, CommandType commandType);
        /// <summary>
        /// ExecuteQuery
        /// </summary>
        IDataReader ExecuteReader(string query);

        /// <summary>
        /// ExecuteScalar
        /// </summary>
        object ExecuteScalar(string query, CommandType commandType, IDataParameter[] param);
        /// <summary>
        /// ExecuteScalar
        /// </summary>
        object ExecuteScalar(string query, CommandType commandType);
        /// <summary>
        /// ExecuteScalar
        /// </summary>
        object ExecuteScalar(string query);
        #endregion

        #region Async
        /// <summary>
        /// ExecuteNoQuery
        /// </summary>
        Task ExecuteNoQueryAsync(string query, CommandType commandType, IDataParameter[] param);
        /// <summary>
        /// ExecuteNoQuery
        /// </summary>
        Task ExecuteNoQueryAsync(string query, CommandType commandType);
        /// <summary>
        /// ExecuteNoQuery
        /// </summary>
        Task ExecuteNoQueryAsync(string query);

        /// <summary>
        /// ExecuteQuery
        /// </summary>
        Task<IEnumerator<T>> ExecuteQueryAsync<T>(string query, CommandType commandType, IDataParameter[] param);
        /// <summary>
        /// ExecuteQuery
        /// </summary>
        Task<IEnumerator<T>> ExecuteQueryAsync<T>(string query, CommandType commandType);
        /// <summary>
        /// ExecuteQuery
        /// </summary>
        Task<IEnumerator<T>> ExecuteQueryAsync<T>(string query);


        /// <summary>
        /// ExecuteQuery
        /// </summary>
        Task<DataSet> ExecuteQueryAsync(string query, CommandType commandType, IDataParameter[] param);
        /// <summary>
        /// ExecuteQuery
        /// </summary>
        Task<DataSet> ExecuteQueryAsync(string query, CommandType commandType);
        /// <summary>
        /// ExecuteQuery
        /// </summary>
        Task<DataSet> ExecuteQueryAsync(string query);

        #endregion

    }

}
