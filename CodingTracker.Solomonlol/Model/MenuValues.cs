using CodingTracker.Solomonlol.Controllers;


namespace CodingTracker.Solomonlol.Model
{
    public static class MenuValues
    {
        internal static CodingController cod = new();
        public static Dictionary<string, Action> menuValues = new()
        {
            { "Print all records", () => cod.PrintOrderby() },
            { "New record", () => cod.CreateData() },
            { "Update record", () => cod.UpdateData() },
            { "Delete record", () => cod.DeleteData() },
            { "Timer", ()=>cod.StartTimer()  },
            { "Exit", () => cod.Exit() }
        };

        public static Dictionary<string, Action<string>> printValues = new()
        {
            { "Print all records", (s) => cod.PrintData("SELECT * FROM CodingSessions") },
            { "Order by Date Ascending", (s) => cod.PrintData("SELECT * FROM CodingSessions ORDER BY Date ASC") },
            { "Order by Date Descending", (s) => cod.PrintData("SELECT * FROM CodingSessions ORDER BY Date DESC") },
            { "Order by Duration Ascending", (s) => cod.PrintData("SELECT * FROM CodingSessions ORDER BY Duration ASC") },
            { "Order by Duration Descending", (s) => cod.PrintData("SELECT * FROM CodingSessions ORDER BY Duration DESC") },
        };

        //public static Dictionary<string, Action<string>> printOrderByValuesColumn = new()
        //{
        //    { "Date Ascending", (s) => cod.PrintData(s) },
        //    { "Date Descending", (s) => cod.PrintData(s) },
        //    { "Duration Ascending", (s) => cod.PrintData(s) },
        //    { "Duration Descending", (s) => cod.PrintData(s) }
        //};


    }
}
