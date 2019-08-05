using Afonsoft.ADONET.Interfaces;
using System.Data;
using System.Data.SQLite;

namespace Afonsoft.ADONET.Providers
{
    internal class SQLite : ISqlProvider
    {
        private SQLiteConnection sql;

        public SQLite(string conectionString)
        {
            sql = new SQLiteConnection(conectionString);
        }

        public IDbConnection Connection => sql;

        public IDbCommand CreateCommand()
        {
            return sql?.CreateCommand();
        }

        public IDbConnection GetConnection(string conectionString)
        {
            sql = new SQLiteConnection(conectionString);
            return sql;
        }

        public IDbDataAdapter CreateDataAdapter(IDbCommand command)
        {
            return new SQLiteDataAdapter((SQLiteCommand)command);
        }
    }
}