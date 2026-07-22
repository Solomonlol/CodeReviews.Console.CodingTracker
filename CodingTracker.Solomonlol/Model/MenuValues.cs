using CodingTracker.Solomonlol.Controllers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace CodingTracker.Solomonlol.Model
{
    public static class MenuValues
    {
        internal static CodingController cod = new();
        public static Dictionary<string, Action> menuValues = new()
        {
            { "Print all records", () => cod.PrintData() },
            { "New record", () => cod.CreateData() },
            { "Update record", () => cod.UpdateData() },
            { "Delete record", () => cod.DeleteData() },
            { "Timer", ()=>cod.StartTimer()  },
            { "Exit", () => cod.Exit() }
        };

        
    }
}
