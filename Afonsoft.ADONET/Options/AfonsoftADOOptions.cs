using System;
using System.Data;

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
        /// Default: false
        /// </summary>
        public bool AutoOpenAndCloseConnection { get; set; } = false;

        /// <summary>
        /// Timeout
        /// Default: 360
        /// </summary>
        public int Timeout { get; set; } = 360;

        /// <summary>
        /// TransactionLevel
        /// Default: Snapshot
        /// </summary>
        public IsolationLevel TransactionLevel { get; set; } = IsolationLevel.Snapshot;

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
        /// Oracle Only in FW >= 4.6.1 Not working in Core.Net
        /// </summary>
        Oracle = 5
    }
}

