using System;
using System.Collections.Generic;
using System.Linq;
using SQLite;
using Microsoft.Maui.Controls;

namespace ResillentConstruction.Models
{
    public class AreaMasterDatabase
    {
        private SQLiteConnection conn;
        public AreaMasterDatabase()
        {
            conn = DependencyService.Get<ISQLite>().GetConnection();
            conn.CreateTable<AreaMaster>();
        }
        public IEnumerable<AreaMaster> GetAreaMaster(String Querryhere)
        {
            var list = conn.Query<AreaMaster>(Querryhere);
            return list.ToList();
        }
        public string AddAreaMaster(AreaMaster service)
        {
            conn.Insert(service);
            return "success";
        }
        public string DeleteAreaMaster()
        {
            var del = conn.Query<AreaMaster>("delete from AreaMaster");
            return "success";
        }


        public string CustomAreaMaster(string query)
        {
            conn.Query<AreaMaster>(query);
            return "success";
        }
    }
}
