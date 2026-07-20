using System;
using System.Collections.Generic;
using System.Text;

namespace CodingTracker.Solomonlol.Model
{
    internal class CodingSession
    {
        private static List<CodingSession> session = new();
        private int Id {  get; set; }
        private DateTime StartTime { get; set; }
        private DateTime EndTime { get; set; }
        private TimeOnly Duration { get; set; }
    }
}
