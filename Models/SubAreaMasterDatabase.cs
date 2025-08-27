using System;
using System.Collections.Generic;
using System.Linq;
using SQLite;
using Microsoft.Maui.Controls;
using System.IO;

namespace ResillentConstruction.Models
{
    public class SubAreaMasterDatabase
    {
        private SQLiteConnection conn;
        public SubAreaMasterDatabase()
        {
            var dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), ResillentConstruction.App.DB_Name);
            conn = new SQLiteConnection(dbPath);
            conn.CreateTable<SubAreaMaster>();
        }
        
        public IEnumerable<SubAreaMaster> GetSubAreaMaster(String Querryhere)
        {
            var list = conn.Query<SubAreaMaster>(Querryhere);
            return list.ToList();
        }
        
        public string AddSubAreaMaster(SubAreaMaster service)
        {
            conn.Insert(service);
            return "success";
        }
        
        public string DeleteSubAreaMaster()
        {
            var del = conn.Query<SubAreaMaster>("delete from SubAreaMaster");
            return "success";
        }

        public string CustomSubAreaMaster(string query)
        {
            conn.Query<SubAreaMaster>(query);
            return "success";
        }
        
        public void ExecuteNonQuery(string query)
        {
            conn.Execute(query);
        }
    }
}
