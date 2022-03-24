using Microsoft.Data.Sqlite;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace derivco.Data.Factories
{
    public class ConnectionFactory : IConnectionFactory
    {
        public string Database { set; get; }

        public ConnectionFactory(IConfiguration iConfig) {
            this.Database = iConfig.GetSection("DatabaseName").Value;
        }

        public SqliteConnection GetConnection()
        {
            var connection = new SqliteConnection(this.Database);
            return connection;
        }
    }
}
