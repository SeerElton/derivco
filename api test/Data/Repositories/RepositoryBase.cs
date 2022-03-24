using derivco.Data.Factories;
using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace derivco.Data.Repositories
{
    public class RepositoryBase : IRepositoryBase
    {
        public SqliteConnection connection;

        public RepositoryBase(IConnectionFactory connectionFactory)
        {
            this.connection = connectionFactory.GetConnection();
        }
    }
}
