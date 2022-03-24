using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.Text;

namespace derivco.Data.Factories
{
    public interface IConnectionFactory
    {
        SqliteConnection GetConnection();
    }
}
