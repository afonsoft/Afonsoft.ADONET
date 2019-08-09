using Afonsoft.ADONET.Interfaces;
using Npgsql;
using System.Data;

namespace Afonsoft.ADONET.Providers
{
    public class PostGreSql : ISqlProvider
    {
        private NpgsqlConnection sql;
        public IDbConnection Connection => sql;

        public PostGreSql(string conectionString)
        {
            sql = new NpgsqlConnection(conectionString);
        }

        public IDbCommand CreateCommand()
        {
            return sql?.CreateCommand();
        }

        public IDbDataAdapter CreateDataAdapter(IDbCommand command)
        {
            return new NpgsqlDataAdapter((NpgsqlCommand)command);
        }

        public IDbConnection GetConnection(string conectionString)
        {
            sql = new NpgsqlConnection(conectionString);
            return sql;
        }
    }
}
