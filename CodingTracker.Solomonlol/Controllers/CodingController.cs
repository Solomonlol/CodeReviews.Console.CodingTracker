using CodingTracker.Solomonlol.Model;
using Dapper;
using Microsoft.Data.Sqlite;
using Microsoft.Extensions.Configuration;
using Spectre.Console;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Text;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace CodingTracker.Solomonlol.Controllers
{
    internal class CodingController : ICodingController
    {
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

        public List<CodingSession> GetData()
        {
            using(IDbConnection db = new SqliteConnection(GetConString()))
            {
                return db.Query<CodingSession>("SELECT * FROM CodingSessions").ToList();
            }
        }

        public void CreateData()
        {
            var session = NewRecord();
            using (IDbConnection db = new SqliteConnection(GetConString()))
            {
                var sqlQuery = "INSERT INTO CodingSessions (Date, StartTime, EndTime, Duration)" +
                    "VALUES (@Date, @StartTime, @EndTime, @Duration)";
                db.Execute(sqlQuery, session);
            }
        }

        public void DeleteData()
        {
            NumberInput("Write record Id to delete:", out int id);
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

        public void UpdateData()
        {
            NumberInput("Write record Id to update:", out int id);
            var session = NewRecord(id);
            using (IDbConnection db = new SqliteConnection(GetConString()))
            {
                var sqlQuery= "UPDATE CodingSessions SET Date=@Date," +
                    "StartTime=@StartTime," +
                    "EndTime=@EndTime," +
                    "Duration=@Duration " +
                    "WHERE Id=@Id";
                db.Execute(sqlQuery, session);
            }
        }

        public void PrintData()
        {
            var toPrint = GetData();
            var table = new Table();
            table.AddColumns("ID", "Date", "Start time", "End time", "Duration");

            foreach (var c in toPrint)
            {
                table.AddRow($"{c.Id}",
                             $"{c.Date}",
                             $"{c.StartTime}",
                             $"{c.EndTime}",
                             $"{c.Duration}");
            }
            AnsiConsole.Write(table);
        }

        public string GetConString()
        {
            IConfiguration config = new ConfigurationBuilder()
                .AddJsonFile("appsetings.json")
                .Build();

            var connectionString = config.GetConnectionString("SQLiteConnection");
            return connectionString;
        }

        private CodingSession NewRecord(int? id = null)
        {
            DateInput("Write start time in 'dd.MM.yyyy HH:mm' format:", out DateTime startTime);
            DateInput("Write start time in 'dd.MM.yyyy HH:mm' format:", out DateTime endTime);
            CodingSession session = new(startTime, endTime, id);
            return session;

        }
        private static void DateInput(string s, out DateTime date)
        {
            Console.WriteLine(s);
            while (!DateTime.TryParseExact(Console.ReadLine(), "dd.MM.yyyy H:m", CultureInfo.InvariantCulture, DateTimeStyles.None, out date))
            {
                Console.WriteLine("Wrong data format. Write it like 'dd.MM.yyyy H:m' and try again.");
            }
        }
        private static void NumberInput(string s, out int id)
        {
            Console.WriteLine(s);
            while (!int.TryParse(Console.ReadLine(), out id) || id <= 0)
            {
                Console.WriteLine("Wrong data format. The string must contain only digits above zero. Try again.");
            }
        }
        public void Exit()
        {
            Environment.Exit(0);
        }
    }
}
