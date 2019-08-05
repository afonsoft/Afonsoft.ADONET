using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ADOTest
{
    [TestClass]
    public class ADOTest
    {
        [TestMethod]
        public void AdoTestConection()
        {
            try
            {
                Afonsoft.ADONET.Connection conn = new Afonsoft.ADONET.Connection(o =>
                {
                    o.Provider = Afonsoft.ADONET.Options.EnumProvider.SQLite;
                    o.ConnectionString = "Data Source=SQLite.db";
                    o.AutoOpenAndCloseConnection = false;
                    o.Timeout = 360;
                   
                });

                string query = "CREATE TABLE IF NOT EXISTS contacts(" +
                                " contact_id INTEGER PRIMARY KEY,   " +
                                " first_name TEXT NOT NULL,         " +
                                " last_name TEXT NOT NULL,          " +
                                " email text NOT NULL UNIQUE,       " +
                                " phone text NOT NULL UNIQUE        " +
                                ");                                 ";

                conn.OpenConnection();
                conn.ExecuteNoQuery(query);

                query = "SELECT * FROM contacts;";

                var reader = conn.ExecuteReader(query);

                while (reader.Read())
                {

                }
                conn.CloseConnection();

            }
            catch (Exception ex)
            {

            }
        }
    }
}
