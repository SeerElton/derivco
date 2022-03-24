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
    public class BetRepository : RepositoryBase, IBetRepository
    {
        private ILogger<BetRepository> logger;

        public BetRepository(ILogger<BetRepository> logger, IConnectionFactory connectionFactory) : base(connectionFactory)
        {
            this.logger = logger;
        }

        public async Task<List<Bet>> GetAll(Guid? id = null)
        {
            {
                try
                {
                    var builder = new SqlBuilder();
                    var template = builder.AddTemplate(@"SELECT * FROM bets /**where**/");

                    if (id.HasValue)
                    {
                        builder.Where($"Id = @Id", new { Id = id });
                    }

                    var rows = await this.connection.QueryAsync<Bet>(template.RawSql, template.Parameters);
                    return rows.ToList();
                }
                catch (Exception e)
                {
                    this.logger.LogError(e, nameof(this.GetAll));
                    throw;
                }
            }
        }

        public async Task Insert(Bet bet)
        {
            {
                try
                {
                    var sql = $@"INSERT INTO bets (Id, PlayerId, SessionId, CreatedAt, PositionNumber, PositionColor)
                                    values (@Id, @PlayerId, SessionId, @CreatedAt, @PositionNumber,  @PositionColor)";

                    await this.connection.ExecuteAsync(sql, new { bet.Id, bet.SessionId, bet.CreatedAt, PositionNumber = bet.Position.Number, PositionColor = bet.Position.Color });
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
