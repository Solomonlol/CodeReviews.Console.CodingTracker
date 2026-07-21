using Spectre.Console;
using System;
using System.Collections.Generic;
using System.Text;

namespace CodingTracker.Solomonlol.Model
{
    internal class CodingSession
    {
        public int? Id { get; set; } = null;
        public string Date { get;  set; }
        public string StartTime { get;  set; }
        public string EndTime { get;  set; }
        public string Duration { get;  set; }

        public CodingSession()
        { }

        public CodingSession(DateTime startTime, DateTime endTime, int? id = null)
        {
            Id = id;
            Date = DateOnly.FromDateTime(startTime).ToString();
            StartTime = TimeOnly.FromDateTime(startTime).ToString();
            EndTime = TimeOnly.FromDateTime(endTime).ToString();
            Duration = ((int)endTime.Subtract(startTime).TotalMinutes) + " minutes";
        }
    }
}
