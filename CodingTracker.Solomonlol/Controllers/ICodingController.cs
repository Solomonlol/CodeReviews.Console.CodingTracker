using CodingTracker.Solomonlol.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace CodingTracker.Solomonlol.Controllers
{
    internal interface ICodingController
    {
        public List<CodingSession> GetData();
        public void UpdateData(int id);
        public void CreateTableIfNotExists();
        public void DeleteData(int id);
        public void PrintData();
        public string GetConString();
    }
}
