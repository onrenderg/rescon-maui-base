using System.Collections.Generic;
using System.Linq;
using SQLite;
using Microsoft.Maui.Controls;
using System;
using System.IO;

namespace ResillentConstruction.Models
{
    public class DistrictMasterDatabase
    {
        private SQLiteConnection conn;
        public DistrictMasterDatabase()
        {
            var dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), ResillentConstruction.App.DB_Name);
            conn = new SQLiteConnection(dbPath);
            conn.CreateTable<DistrictMaster>();
        }

        public IEnumerable<DistrictMaster> GetDistrictMaster(string Querryhere)
        {
            var list = conn.Query<DistrictMaster>(Querryhere).ToList();
            return list;
        }
        public string AddDistrictMaster(DistrictMaster service)
        {
            conn.Insert(service);
            return "success";
        }
        public string DeleteDistrictMaster()
        {
            var del = conn.Query<DistrictMaster>("delete from DistrictMaster");
            return "success";
        }

        public static void insertdistrict()
        {
            try
            {
                DistrictMasterDatabase db = new DistrictMasterDatabase();
                db.DeleteDistrictMaster();

                db.ExecuteNonQuery("INSERT INTO DistrictMaster(DistrictCode, DistrictName, DistrictNameLocal, ZoneName) VALUES(1, 'Bilaspur', N'बिलासपुर', 'C');");
                db.ExecuteNonQuery("INSERT INTO DistrictMaster(DistrictCode, DistrictName, DistrictNameLocal, ZoneName) VALUES(2, 'Chamba', N'चम्बा', 'B');");
                db.ExecuteNonQuery("INSERT INTO DistrictMaster(DistrictCode, DistrictName, DistrictNameLocal, ZoneName) VALUES(3, 'Hamirpur', N'हमीरपुर', 'C');");
                db.ExecuteNonQuery("INSERT INTO DistrictMaster(DistrictCode, DistrictName, DistrictNameLocal, ZoneName) VALUES(4, 'Kangra', N'काँगड़ा', 'B');");
                db.ExecuteNonQuery("INSERT INTO DistrictMaster(DistrictCode, DistrictName, DistrictNameLocal, ZoneName) VALUES(5, 'Kinnaur', N'किन्नौर', 'A');");
                db.ExecuteNonQuery("INSERT INTO DistrictMaster(DistrictCode, DistrictName, DistrictNameLocal, ZoneName) VALUES(6, 'Kullu', N'कुल्लू', 'B');");
                db.ExecuteNonQuery("INSERT INTO DistrictMaster(DistrictCode, DistrictName, DistrictNameLocal, ZoneName) VALUES(7, 'LAHAUL - SPITI', N'लाहौल -स्पीति ', 'A');");
                db.ExecuteNonQuery("INSERT INTO DistrictMaster(DistrictCode, DistrictName, DistrictNameLocal, ZoneName) VALUES(8, 'MANDI', N'मंडी', 'B');");
                db.ExecuteNonQuery("INSERT INTO DistrictMaster(DistrictCode, DistrictName, DistrictNameLocal, ZoneName) VALUES(9, 'Shimla', N'शिमला', 'B');");
                db.ExecuteNonQuery("INSERT INTO DistrictMaster(DistrictCode, DistrictName, DistrictNameLocal, ZoneName) VALUES(10, 'Sirmaur', N'सिरमौर', 'C');");
                db.ExecuteNonQuery("INSERT INTO DistrictMaster(DistrictCode, DistrictName, DistrictNameLocal, ZoneName) VALUES(11, 'SOLAN', N'सोलन', 'C');");
                db.ExecuteNonQuery("INSERT INTO DistrictMaster(DistrictCode, DistrictName, DistrictNameLocal, ZoneName) VALUES(12, 'Una', N'ऊना', 'C');");
            }
            catch
            {
                // Handle any errors during district insertion
            }
        }

        public void ExecuteNonQuery(string query)
        {
            conn.Execute(query);
        }

        
        

        
    }

}
