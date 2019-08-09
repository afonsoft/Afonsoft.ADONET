using Afonsoft.ADONET.Interfaces;
using Oracle.ManagedDataAccess.Client;
using System.Data;

namespace Afonsoft.ADONET.Providers
{
    public class Oracle : ISqlProvider
    {
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
    }
}
