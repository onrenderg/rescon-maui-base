
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Maui.Controls;
using ResillentConstruction.Models;

namespace ResillentConstruction.Models
{
    public class EngineerResponseDetailsDatabase
    {
        private SQLiteConnection conn;
        public EngineerResponseDetailsDatabase()
        {
            conn = DependencyService.Get<ISQLite>().GetConnection();
            conn.CreateTable<EngineerResponseDetails>();
        }
        public IEnumerable<EngineerResponseDetails> GetEngineerResponseDetails(String Querryhere)
        {
            var list = conn.Query<EngineerResponseDetails>(Querryhere);
            return list.ToList();
        }
        public string AddEngineerResponseDetails(EngineerResponseDetails service)
        {
            conn.Insert(service);
            return "success";
        }
        public string DeleteEngineerResponseDetails()
        {
            var del = conn.Query<EngineerResponseDetails>("delete from EngineerResponseDetails");
            return "success";
        }


        public string CustomEngineerResponseDetails(string query)
        {
            conn.Query<EngineerResponseDetails>(query);
            return "success";
        }

    }
}
