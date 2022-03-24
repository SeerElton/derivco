using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using derivco.Config;
using Microsoft.Data.Sqlite;
using Dapper;

namespace derivco.Data.Database
{
    public class DatabaseBootstrap : IDatabaseBootstrap
    {
        private readonly IConfig databaseConfig;

        public DatabaseBootstrap(IConfig config)
        {
            this.databaseConfig = config;
        }

        public void Setup()
        {
            using var connection = new SqliteConnection($"Data Source={databaseConfig.DatabaseName}");

            var existing = connection.Query<string>("SELECT name FROM sqlite_master WHERE type = 'table'");

            //---> create tables
            if (existing.Any(x => x == "players"))
            {
                connection.Execute(@$"
					CREATE TABLE players (
						[Id] [uniqueidentifier] NOT NULL PRIMARY KEY,
						[CreatedAt] [DateTime] NULL,
						[Name] [nvarchar](50) NOT NULL,
					)
					GO
                ");
            }

            if (existing.Any(x => x == "session"))
            {
                connection.Execute(@$"
					CREATE TABLE session (
						[Id] [uniqueidentifier] NOT NULL PRIMARY KEY,
						[CreatedAt] [DateTime] NULL,
					)
					GO
                ");
            }

            if (!existing.Any(x => x == "payouts"))
            {
                connection.Execute(@$"
					CREATE TABLE payouts(
						[Id] [uniqueidentifier] NOT NULL PRIMARY KEY,
						[PlayerId] [uniqueidentifier] NOT NULL,
						[SessionId] [uniqueidentifier] NOT NULL,
						[payout] [float] not null,
						FOREIGN KEY(PlayerId) REFERENCES [players](Id),
						FOREIGN KEY(SessionId) REFERENCES [session](Id)
					)
					GO
                ");
            }


            if (!existing.Any(x => x == "bets"))
            {
                connection.Execute(@$"
					CREATE TABLE bets (
						[Id] [uniqueidentifier] NOT NULL PRIMARY KEY,
						[PlayerId] [uniqueidentifier] NOT NULL,
						[SessionId] [uniqueidentifier] NOT NULL,
						[CreatedAt] [DateTime] NOT NULL,
						[PositionNumber] int NOT NULL,
						[PositionColor] [nvarchar](50) NOT NULL,
						FOREIGN KEY(PlayerId) REFERENCES [players](Id),
						FOREIGN KEY(SessionId) REFERENCES [session](Id)
					)
					GO
                ");
            }
        }
    }
}
