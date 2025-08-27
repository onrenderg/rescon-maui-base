using System.Collections.Generic;
using System.Linq;
using SQLite;
using Microsoft.Maui.Controls;

namespace ResillentConstruction.Models
{
   public class EngineerMasterDatabase
    {
        private SQLiteConnection conn;
        public EngineerMasterDatabase()
        {
            conn = DependencyService.Get<ISQLite>().GetConnection();
            conn.CreateTable<EngineerMaster>();
        }

        public IEnumerable<EngineerMaster> GetEngineerMaster(string Querryhere)
        {
            var list = conn.Query<EngineerMaster>(Querryhere);
            return list.ToList();
        }
        public string AddEngineerMaster(EngineerMaster service)
        {
            conn.Insert(service);
            return "success";
        }
        public string DeleteEngineerMaster()
        {
            var del = conn.Query<EngineerMaster>("delete from EngineerMaster");
            return "success";
        }
    }

}