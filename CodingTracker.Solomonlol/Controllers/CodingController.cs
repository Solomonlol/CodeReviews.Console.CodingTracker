using CodingTracker.Solomonlol.Model;
using Dapper;
using Microsoft.Data.Sqlite;
using Microsoft.Extensions.Configuration;
using Spectre.Console;
using System.Data;
using System.Diagnostics;
using System.Globalization;
using static CodingTracker.Solomonlol.Model.MenuValues;

namespace CodingTracker.Solomonlol.Controllers
{
    internal class CodingController : ICodingController
    {
        public void CreateTableIfNotExists()
        {
            using IDbConnection db = new SqliteConnection(GetConString());
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

        public List<CodingSession> GetData(string? s = null)
        {
            if (s == null) s = "SELECT * FROM CodingSessions";
            using IDbConnection db = new SqliteConnection(GetConString());
            return [.. db.Query<CodingSession>(s)];
        }

        public void CreateData(CodingSession? codingSession=null)
        {
            PrintData();
            CodingSession session = new();
            if (codingSession == null)
            {
                session = NewRecord();
            }
            else session = codingSession;

            using IDbConnection db = new SqliteConnection(GetConString());
            var sqlQuery = "INSERT INTO CodingSessions (Date, StartTime, EndTime, Duration)" +
                "VALUES (@Date, @StartTime, @EndTime, @Duration)";
            db.Execute(sqlQuery, session);
            PrintData();
        }

        public void DeleteData()
        {
            PrintData();
            NumberInput("Write record Id to delete:", out int id);
            using IDbConnection db = new SqliteConnection(GetConString());
            var sqlQuery = "DELETE FROM CodingSessions WHERE Id=@id";
            var check = db.Execute(sqlQuery, new { id });
            if (check == 0)
            {
                AnsiConsole.MarkupLine($"[red]Record whith Id={id} does not exists.[/]");

            }
            else
            {
                PrintData();
                AnsiConsole.MarkupLine($"[green]Record whith Id={id} was deleted.[/]");
            }
            
        }

        public void UpdateData()
        {
            PrintData();
            NumberInput("Write record Id to update:", out int id);
            string sql = "SELECT COUNT(1) FROM CodingSessions WHERE Id = @Id";


            using IDbConnection db = new SqliteConnection(GetConString());
            var checkIfExist = db.ExecuteScalar<bool>(sql, new { Id = id });
            if (checkIfExist)
            {
                var session = NewRecord(id);

                var sqlQuery = "UPDATE CodingSessions SET Date=@Date," +
                    "StartTime=@StartTime," +
                    "EndTime=@EndTime," +
                    "Duration=@Duration " +
                    "WHERE Id=@Id";
                db.Execute(sqlQuery, session);
                PrintData();
                AnsiConsole.MarkupLine($"[green]Record whith Id={id} was upadted.[/]");
            }
            else
            {
                PrintData();
                AnsiConsole.MarkupLine($"[red]Record whith Id={id} does not exists.[/]");
            }
            
        }

        public void PrintData(string? s = null)
        {
            AnsiConsole.Clear();
            List<CodingSession> toPrint = new List<CodingSession>();
            if (s != null)
            {
                toPrint = GetData(s);
            }
            else toPrint = GetData();
            if (toPrint.Count>0)
            {
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
            else AnsiConsole.MarkupLine("[red]Database is empty.[/]");
        }

        private static string GetConString()
        {
            IConfiguration config = new ConfigurationBuilder()
                .AddJsonFile("appsetings.json")
                .Build();

            var connectionString = config.GetConnectionString("SQLiteConnection");
            return connectionString;
        }

        private CodingSession NewRecord(int? id = null)
        {
            DateTime startTime, endTime;
            do
            {
                PrintData();
                DateInput("Write start time in 'dd.MM.yyyy H:m' format:", out startTime);
                DateInput("Write end time in 'dd.MM.yyyy H:m' format:", out endTime);
                if (!(endTime > startTime))
                {
                    AnsiConsole.MarkupLine("[red]The end time cannot be earlier than or the same as the start time.[/]" +
                        "\nPress any key to continue");
                    AnsiConsole.Console.Input.ReadKey(true);
                }
            } while (!(endTime > startTime));
            CodingSession session = new(startTime, endTime, id);
            return session;

        }
        private static void DateInput(string s, out DateTime date)
        {
            Console.WriteLine(s);
            while (!DateTime.TryParseExact(Console.ReadLine(), "dd.MM.yyyy H:m", CultureInfo.InvariantCulture, DateTimeStyles.None, out date))
            {
                AnsiConsole.MarkupLine("[red]Wrong data format. Write it like 'dd.MM.yyyy H:m' and try again.[/]");
            }
        }
        private static void NumberInput(string s, out int id)
        {
            Console.WriteLine(s);
            while (!int.TryParse(Console.ReadLine(), out id) || id <= 0)
            {
                AnsiConsole.MarkupLine("[red]Wrong data format. The string must contain only digits above zero. Try again.[/]");
            }
        }
        public void StartTimer()
        {
            Stopwatch stopwatch = new Stopwatch();
            DateTime start = DateTime.Now;
            DateTime end = new();
            stopwatch.Start();
            while(true)
            {
                if(Console.KeyAvailable)
                {
                    var key = Console.ReadKey(true).Key;
                    if(key==ConsoleKey.Enter)
                    {
                        stopwatch.Stop();
                        end = start+stopwatch.Elapsed;
                        CreateData(new CodingSession(start, end));
                        break;
                    }
                }
                
                Console.Clear();
                AnsiConsole.MarkupLine("[green]IT'S CODING TIME![/]\nPress Enter key to stop this session...");
                AnsiConsole.MarkupLine($"Current session: {stopwatch.Elapsed.ToString(@"mm\:ss")}");
                Thread.Sleep(1000);
            }
        }
        public void PrintOrderby()
        {
            var choice = AnsiConsole.Prompt(new SelectionPrompt<string>()
                    .Title("Select an [green]option[/]:")
                    .AddChoices(printValues.Keys));

            printValues[choice]("");
              
        }

        public void Exit()
        {
            Environment.Exit(0);
        }
    }
}
