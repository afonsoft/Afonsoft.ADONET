using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace Afonsoft.ADONET.Interfaces
{
    public interface ISqlProvider
    {
        IDbConnection GetConnection(string conectionString);
        IDbConnection Connection { get; }
        IDbCommand CreateCommand();
        IDbDataAdapter CreateDataAdapter(IDbCommand command);
        
    }
}
