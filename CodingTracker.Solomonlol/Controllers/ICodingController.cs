using CodingTracker.Solomonlol.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace CodingTracker.Solomonlol.Controllers
{
    internal interface ICodingController
    {
        public List<CodingSession> GetData();
        public void UpdateData();
        public void CreateTableIfNotExists();
        public void DeleteData();
        public void WriteData();
        public string GetConString();
    }
}
