using Afonsoft.ADONET.Interfaces;
using System.Data;
using System.Data.SqlClient;

namespace Afonsoft.ADONET.Providers
{
    internal class SQLServer : ISqlProvider
    {
        private SqlConnection sql;

        public IDbConnection Connection => sql;
        public IDbCommand CreateCommand()
        {
            return sql?.CreateCommand();
        }

        public SQLServer(string conectionString)
        {
            sql = new SqlConnection(conectionString);
        }

        public IDbConnection GetConnection(string conectionString)
        {
            sql = new SqlConnection(conectionString);
            return sql;
        }

        public IDbDataAdapter CreateDataAdapter(IDbCommand command)
        {
            return new SqlDataAdapter((SqlCommand)command);
        }
    }
}