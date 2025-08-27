using Newtonsoft.Json.Linq;
using ResillentConstruction.Models;
using ResillentConstruction.webapi;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography.X509Certificates;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.Maui;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Controls.Xaml;

namespace ResillentConstruction
{
    public partial class App : Application
    {
        public static string AppName = "Resilient Construction H.P.";
        public static string DB_Name = "ResilientConstruction.db";
        LanguageMasterDatabase languageMasterDatabase=new LanguageMasterDatabase();
        public static int Language = 0;
        public static List<LanguageMaster> MyLanguage;
        // Global cache for districts similar to MyLanguage
        public static List<DistrictMaster> Districts;
        //public static List<DistrictMaster> districtMasterslist;
        SaveUserPreferencesDatabase saveUserPreferencesDatabase=new SaveUserPreferencesDatabase();
        List<SaveUserPreferences> saveUserPreferenceslist;

        public static double Latitude;
        public static double Longitude;
        public static double Accuracy;
        public static DateTime networkDateTime;

        public App()
        {
            InitializeComponent();
            InitializeLocalData();
            MainPage = new NavigationPage(new MainPage());

        }
        public static string LableText(string key)
        {
            try//Preferences.Get("lan", "EN-IN")
            {
                if (Preferences.Get("lan", "EN-IN") == "EN-IN")
                {
                    return App.MyLanguage.FindAll(x => x.ResourceKey.Trim().ToLower() == key.Trim().ToLower()).FirstOrDefault().ResourceValue;
                }
                else
                {
                    return App.MyLanguage.FindAll(x => x.ResourceKey.Trim().ToLower() == key.Trim().ToLower()).FirstOrDefault().LocalResourceValue;
                }
            }
            catch (Exception ex)
            {
                // return ex.Message;
                return key;
            }
        }

       

        public static string GetLableByMultipleKey(string Key)
        {
            string Lable_Name = "No Value";
            try
            {
                Lable_Name = MyLanguage.FindAll(s => s.MultipleResourceKey == Key).ElementAt(0).ResourceValue;
            }
            catch
            {
                Lable_Name = Key;
            }
            return Lable_Name;
        }

       
        public static async Task GetLocation()
        {
            try
            {
                var request = new GeolocationRequest(GeolocationAccuracy.High);

                var location_Exact = await Geolocation.GetLocationAsync(request);
                if (location_Exact != null)
                {
                    Latitude = location_Exact.Latitude;
                    Longitude = location_Exact.Longitude;
                    Accuracy = (double)location_Exact.Accuracy;
                    DateTime locationtime = location_Exact.Timestamp.UtcDateTime.AddHours(5.5);
                    var placemarks = await Geocoding.GetPlacemarksAsync(location_Exact.Latitude, location_Exact.Longitude);
                    var placemark = placemarks?.FirstOrDefault();
                    //radius = sCoord.GetDistanceTo(eCoord).ToString();
                    //App.locaddress = placemark.SubLocality;
                    networkDateTime = locationtime;
                    // networkDateTime = locationtime;
                }
            }
            catch (FeatureNotSupportedException fnsEx)
            {
                await Current.MainPage.DisplayAlert(AppName, fnsEx.Message + "\n" + "App Cannot be used without Location", "Close");
                System.Diagnostics.Process.GetCurrentProcess().CloseMainWindow();

                Latitude = 0.00;
                Longitude = 0.00;
                Accuracy = 0;
                return;
            }
            catch (FeatureNotEnabledException fneEx)
            {
                bool m = await Current.MainPage.DisplayAlert(AppName, fneEx.Message + "\n" + "App Cannot be used without Location.",
                    "Close", "Settings");
                if (m)
                {
                    System.Diagnostics.Process.GetCurrentProcess().CloseMainWindow();
                }
                else
                {
                    AppInfo.ShowSettingsUI();//.OpenSettings();
                                             // System.Diagnostics.Process.GetCurrentProcess().CloseMainWindow();
                }
                Latitude = 0.00;
                Longitude = Latitude;
                Accuracy = 0;
                return;
            }
            catch (PermissionException pEx)
            {
                bool m = await Current.MainPage.DisplayAlert(AppName, pEx.Message + "\n" + "App Cannot be used without Location Permission.",
                    "No Thanks",
                    "Settings");
                if (m)
                {
                    System.Diagnostics.Process.GetCurrentProcess().CloseMainWindow();
                }
                else
                {

                    AppInfo.ShowSettingsUI();//.OpenSettings();
                }
                Latitude = 0.00;
                Longitude = 0.00;
                Accuracy = 0;
                return;
            }
            catch (Exception)
            {

                Latitude = 0.00;
                Longitude = Latitude;
                Accuracy = 0;
                return;
            }
        }


        public static async Task<Placemark> GetmyAddress()
        {
            try
            {
                var placeholder = await Geocoding.GetPlacemarksAsync(App.Latitude, App.Longitude);
                return placeholder?.FirstOrDefault();
            }
            catch
            {
                return null;
            }
        }
        public static bool isAlphabetonly(string strtocheck)
        {
            Regex rg = new Regex(@"^[a-zA-Z \s]+$");
            return rg.IsMatch(strtocheck);
        }
        public static bool isAlphaNumeric(string strToCheck)
        {
            Regex rg = new Regex(@"^[a-zA-Z0-9\s,./_@()]*$");
            return rg.IsMatch(strToCheck);
        }

        public static bool isNumeric(string strToCheck)
        {
            Regex rg = new Regex("^[0-9]+$");
            return rg.IsMatch(strToCheck);
        }

        public static bool isvalidpassword(string strToCheck)
        {
            Regex rg = new Regex(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,}$");
            return rg.IsMatch(strToCheck);
        }

        public static bool isvalidemail(string strToCheck)
        {
            Regex rg = new Regex(@"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z");
            return rg.IsMatch(strToCheck);
        }
        


        protected override void OnStart()
        {
           
        }

        private void InitializeLocalData()
        {
            try
            {
                // Initialize language data
                LanguageMasterDatabase.insertlanguageleys1();
                
                // Initialize district data
                DistrictMasterDatabase.insertdistrict();
                
                // Load language data into memory
                MyLanguage = languageMasterDatabase.GetLanguageMaster("SELECT * FROM LanguageMaster").ToList();

                // Load districts into global cache
                Districts = new DistrictMasterDatabase()
                    .GetDistrictMaster("SELECT * FROM DistrictMaster").ToList();
            }
            catch (Exception ex)
            {
                // Handle any initialization errors
                System.Diagnostics.Debug.WriteLine($"Error initializing local data: {ex.Message}");
            }
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
