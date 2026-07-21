using CodingTracker.Solomonlol.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace CodingTracker.Solomonlol.Controllers
{
    internal interface ICodingController
    {
        public void CreateTableIfNotExists();
        public List<CodingSession> GetData();
        public string GetConString();
        public void UpdateData();
        public void DeleteData();
        public void PrintData();
        public void CreateData();
        public void Exit();
    }
}
