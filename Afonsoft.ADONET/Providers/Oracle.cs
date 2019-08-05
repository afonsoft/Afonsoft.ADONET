using Afonsoft.ADONET.Interfaces;
#if NET47
using Oracle.ManagedDataAccess.Client;
#endif 
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace Afonsoft.ADONET.Providers
{
    public class Oracle : ISqlProvider
    {
#if NET47
        private OracleConnection sql;
        public Oracle(string conectionString)
        {
            sql = new OracleConnection(conectionString);
        }

        public IDbConnection Connection => sql;

        public IDbCommand CreateCommand()
        {
            return sql?.CreateCommand();
        }

        public IDbDataAdapter CreateDataAdapter(IDbCommand command)
        {
            return new OracleDataAdapter((OracleCommand)command);
        }

        public IDbConnection GetConnection(string conectionString)
        {
            sql = new OracleConnection(conectionString);
            return sql;
        }
#endif
#if NETSTANDARD2_0 
        public IDbConnection Connection => throw new NotImplementedException("Only in Framework >= 4.6.1");

        public Oracle(string conectionString)
        {
            throw new NotImplementedException("Only in Framework >= 4.6.1");
        }

        public IDbCommand CreateCommand()
        {
            throw new NotImplementedException("Only in Framework >= 4.6.1");
        }

        public IDbDataAdapter CreateDataAdapter(IDbCommand command)
        {
            throw new NotImplementedException("Only in Framework >= 4.6.1");
        }

        public IDbConnection GetConnection(string conectionString)
        {
            throw new NotImplementedException("Only in Framework >= 4.6.1");
        }
#endif
    }
}
