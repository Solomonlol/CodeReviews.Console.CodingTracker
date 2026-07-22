using CodingTracker.Solomonlol.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace CodingTracker.Solomonlol.Controllers
{
    internal interface ICodingController
    {
        public void CreateTableIfNotExists();
        public void UpdateData();
        public void DeleteData();
        public void PrintData(string? s=null);
        public void CreateData(CodingSession? codingSession=null);
        public void Exit();
    }
}
