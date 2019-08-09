using Afonsoft.ADONET.Interfaces;
using System.Data;

namespace Afonsoft.ADONET.Providers
{
    internal class MySQL : ISqlProvider
    {
        private MySql.Data.MySqlClient.MySqlConnection sql;

        public IDbConnection Connection => sql;

        public MySQL(string conectionString)
        {
            sql = new MySql.Data.MySqlClient.MySqlConnection(conectionString);
        }

        public IDbCommand CreateCommand()
        {
            return sql?.CreateCommand();
        }

        public IDbConnection GetConnection(string conectionString)
        {
            sql = new MySql.Data.MySqlClient.MySqlConnection(conectionString);
            return sql;
        }

        public IDbDataAdapter CreateDataAdapter(IDbCommand command)
        {
            return new MySql.Data.MySqlClient.MySqlDataAdapter((MySql.Data.MySqlClient.MySqlCommand)command);
        }
    }
}
