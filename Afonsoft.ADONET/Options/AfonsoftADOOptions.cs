using System;

namespace Afonsoft.ADONET.Options
{
    public class AfonsoftADOOptions
    {
        /// <summary>
        /// Provider
        /// </summary>
        public EnumProvider Provider { get; set; } = EnumProvider.Unknown;
        /// <summary>
        /// ConnectionString
        /// </summary>
        public string ConnectionString { get; set; } = "";

        /// <summary>
        /// Open and Close Automatic connection befor command
        /// </summary>
        public bool AutoOpenAndCloseConnection { get; set; } = false;

        public int Timeout { get; set; } = 360;
    }

    /// <summary>
    /// EnumProvider
    /// </summary>
    public enum EnumProvider
    {
        /// <summary>
        /// Unknown
        /// </summary>
        Unknown = 9999,
        /// <summary>
        /// MySQL
        /// </summary>
        MySQL = 1,
        /// <summary>
        /// SQLite
        /// </summary>
        SQLite = 2,
        /// <summary>
        /// SQLServer
        /// </summary>
        SQLServer = 3,
        /// <summary>
        /// PostgreSQL
        /// </summary>
        PostgreSQL = 4,
        /// <summary>
        /// Oracle
        /// </summary>
        Oracle = 5
    }
}

