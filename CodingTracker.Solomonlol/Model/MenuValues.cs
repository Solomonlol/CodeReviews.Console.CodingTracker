using CodingTracker.Solomonlol.Controllers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using static CodingTracker.Solomonlol.Controllers.CodingController;

namespace CodingTracker.Solomonlol.Model
{
    public class MenuValues
    {
        private static CodingController cod = new();
        public static Dictionary<string, Action> menuValues = new()
        {
            { "Print all records", () => cod.PrintData() },
            { "New record", () => cod.CreateData() },
            { "Update record", () => cod.UpdateData() },
            { "Delete record", () => cod.DeleteData() },
            { "Exit", () => cod.Exit() }
        };
    }
}
