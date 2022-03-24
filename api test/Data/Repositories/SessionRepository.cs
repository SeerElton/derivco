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
    public class SessionRepository : RepositoryBase, ISessionRepository
    {
        private ILogger<SessionRepository> logger;

        public SessionRepository(ILogger<SessionRepository> logger, IConnectionFactory connectionFactory) : base(connectionFactory)
        {
            this.logger = logger;
        }

        public async Task<List<Session>> GetAll(Guid? id = null)
        {
            {
                try
                {
                    var builder = new SqlBuilder();
                    var template = builder.AddTemplate(@"SELECT * FROM session /**where**/");

                    if (id.HasValue)
                    {
                        builder.Where($"Id = @Id", new { Id = id });
                    }

                    var rows = await this.connection.QueryAsync<Session>(template.RawSql, template.Parameters);

                    return rows.ToList();
                }
                catch (Exception e)
                {
                    this.logger.LogError(e, nameof(this.GetAll));
                    throw;
                }
            }
        }

        public async Task Insert(Session session)
        {
            {
                try
                {
                    var sql = @$"INSERT INTO session (Id, CreatedAt) values (@Id, @CreatedAt)";
                    await this.connection.ExecuteAsync(sql, new { session.Id, session.CreatedAt });
                }
                catch (Exception e)
                {
                    this.logger.LogError(e, nameof(this.Insert));
                    throw;
                }
            }
        }

        public async Task<List<Payout>> Payouts(Guid? id = null)
        {
            {
                try
                {
                    var builder = new SqlBuilder();
                    var template = builder.AddTemplate(@"select pa.Payout, pl.Id PlayerId, pl.CreatedAt PlayerCreatedAt, pl.Name PlayerName, s.Id SessionId, s.CreatedAt SessionCreatedAt from payouts pa
                                                            join player pl on pa.PlayerId = pl.Id
                                                            join Session s on s.Id = pa.SessionId /**where**/");

                    if (id.HasValue)
                    {
                        builder.Where($"pa.Id = @Id", new { Id = id });
                    }

                    var rows = await this.connection.QueryAsync(template.RawSql, template.Parameters);

                    var payouts = rows.Select(x => new Payout()
                    {
                        Player = new Player()
                        {
                            Id = x.PlayerId,
                            Name = x.PlayerName,
                            CreatedAt = x.PlayerCreatedAt,
                        },
                        Session = new Session()
                        {
                            CreatedAt = x.SessionCreatedAt,
                            Id = x.SessionId,
                        }
                    });

                    return payouts.ToList();
                }
                catch (Exception e)
                {
                    this.logger.LogError(e, nameof(this.Payouts));
                    throw;
                }
            }
        }
    }
}
