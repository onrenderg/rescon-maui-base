using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Maui.Controls;
using System.IO; 
namespace ResillentConstruction.Models
{
   public class SaveUserPreferencesDatabase
    {
        private SQLiteConnection conn;
        public SaveUserPreferencesDatabase()
        {
            var dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), ResillentConstruction.App.DB_Name);
            conn = new SQLiteConnection(dbPath);
            conn.CreateTable<SaveUserPreferences>();
        }
        public IEnumerable<SaveUserPreferences> GetSaveUserPreferences(String Querryhere)
        {
            var list = conn.Query<SaveUserPreferences>(Querryhere);
            return list.ToList();
        }
        public string AddSaveUserPreferences(SaveUserPreferences service)
        {
            conn.Insert(service);
            return "success";
        }
        public string DeleteSaveUserPreferences()
        {
            var del = conn.Query<SaveUserPreferences>("delete from SaveUserPreferences");
            return "success";
        }


        public string CustomSaveUserPreferences(string query)
        {
            conn.Query<SaveUserPreferences>(query);
            return "success";
        }

        public void ExecuteNonQuery(string query)
        {
            conn.Execute(query);
        }
    }
}
