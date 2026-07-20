using CodingTracker.Solomonlol.Model;
using Dapper;
using Microsoft.Data.Sqlite;
using Microsoft.Extensions.Configuration;
using Spectre.Console;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace CodingTracker.Solomonlol.Controllers
{
    internal class CodingController : ICodingController
    {
        public List<CodingSession> GetData()
        {
            using(IDbConnection db = new SqliteConnection(GetConString()))
            {
                return db.Query<CodingSession>("SELECT * FROM CodingSessions").ToList();
            }
        }
        public void DeleteData(int id)
        {
            using (IDbConnection db = new SqliteConnection(GetConString()))
            {
                var sqlQuery="DELETE FROM CodingSessions WHERE Id=@id";
                var check=db.Execute(sqlQuery, new { id });
                if (check == 0)
                {
                    AnsiConsole.MarkupLine($"[red]Cannot found record whith Id={id} to delete.[/]");

                }
                else AnsiConsole.MarkupLine($"[green]Record whith Id={id} was deleted.[/]");
            }
        }

        public void UpdateData(int id)
        {
            throw new NotImplementedException();
        }

        public void PrintData()
        {
            throw new NotImplementedException();
        }

        public string GetConString()
        {
            IConfiguration config = new ConfigurationBuilder()
                .AddJsonFile("appsetings.json")
                .Build();

            var connectionString = config.GetConnectionString("SQLiteConnection");
            return connectionString;
        }

        public void CreateTableIfNotExists()
        {
            using (IDbConnection db = new SqliteConnection(GetConString()))
            {
                db.Open();
                var sqlQuery = "CREATE TABLE IF NOT EXISTS CodingSessions" +
                    "(Id INTEGER PRIMARY KEY AUTOINCREMENT," +
                    "Date TEXT NOT NULL," +
                    "StartTime TEXT NOT NULL," +
                    "EndTime TEXT NOT NULL," +
                    "Duration TEXT NOT NULL)";
                db.Execute(sqlQuery);
                db.Close();
            }
        }
    }
}
