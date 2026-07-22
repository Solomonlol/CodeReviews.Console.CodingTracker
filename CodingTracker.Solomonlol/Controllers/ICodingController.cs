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
        public void UpdateData();
        public void DeleteData();
        public void PrintData();
        public void CreateData(CodingSession? codingSession=null);
        public void Exit();
    }
}
