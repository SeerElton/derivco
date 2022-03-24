using derivco.Data.Factories;
using derivco.Data.Models;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using Dapper;
using System.Linq;
using System.Threading.Tasks;

namespace derivco.Data.Repositories
{
    public class PlayerRepository : RepositoryBase, IPlayerRepository
    {
        private ILogger<BetRepository> logger;

        public PlayerRepository(ILogger<BetRepository> logger, IConnectionFactory connectionFactory) : base(connectionFactory)
        {
            this.logger = logger;
        }

        public async Task<List<Player>> GetAll(Guid? id = null)
        {
            {
                try
                {
                    var builder = new SqlBuilder();
                    var template = builder.AddTemplate(@"SELECT * FROM players /**where**/");

                    if (id.HasValue)
                    {
                        builder.Where($"Id = @Id", new { Id = id });
                    }

                    var rows = await this.connection.QueryAsync<Player>(template.RawSql, template.Parameters);
                    return rows.ToList();
                }
                catch (Exception e)
                {
                    this.logger.LogError(e, nameof(this.GetAll));
                    throw;
                }
            }
        }

        public async Task Insert(Player player)
        {
            {
                try
                {
                    var sql = $@"INSERT INTO players (Id, Name, CreatedAt)
                                 values (@Id, @Name, @CreatedAt)";

                    await this.connection.ExecuteAsync(sql, new { player.Id, player.Name, player.CreatedAt });
                }
                catch (Exception e)
                {
                    this.logger.LogError(e, nameof(this.Insert));
                    throw;
                }
            }
        }
    }
}
