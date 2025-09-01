using ResillentConstruction.Models;
using ResillentConstruction.webapi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.Maui;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Controls.Xaml;
using Microsoft.Maui.ApplicationModel;
namespace ResillentConstruction
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ProfilePage : ContentPage
    {
        int districtcode;
        string DistrictName, DistrictNameLocal;

        SaveUserPreferencesDatabase saveUserPreferencesDatabase = new SaveUserPreferencesDatabase();
        List<SaveUserPreferences> saveUserPreferenceslist;
        int zonecode;
        string zonename;

        DistrictMasterDatabase districtMasterDatabase = new DistrictMasterDatabase();
        List<DistrictMaster> districtMasterslist = new List<DistrictMaster>();

        public ProfilePage()
        {
            InitializeComponent();
            // Ensure language has a sane default on first run
            if (string.IsNullOrWhiteSpace(Preferences.Get("lan", string.Empty)))
            {
                Preferences.Set("lan", "EN-IN");
            }
            language();

            lbl_navigation_header.Text = App.LableText("lbl_navigation_header");

            Dispatcher.Dispatch(async () =>
            {
                language();
                Loading_activity.IsVisible = true;

                // Ensure districts are loaded and bind Picker once
                insertdistrict();
                districtMasterslist = districtMasterDatabase.GetDistrictMaster("Select * from districtMaster").ToList();
                Picker_District.ItemsSource = districtMasterslist;
                Picker_District.Title = App.LableText("district");
                Picker_District.ItemDisplayBinding = new Binding("DistrictName");

                // Load any saved profile
                saveUserPreferenceslist = saveUserPreferencesDatabase.GetSaveUserPreferences("Select * from SaveUserPreferences").ToList();

                if (!saveUserPreferenceslist.Any())
                {
                    // First profile: hide Get District button (will be shown after first save)
                    Btn_getdistrict.IsVisible = false;
                    // First visit: get GPS and resolve current district ONCE
                    await App.GetLocation();
                    var sevice = new HitServices();
                    int responselocation = await sevice.getcuurentdistrict(App.Latitude.ToString(), App.Longitude.ToString());
                    if (responselocation == 200)
                    {
                        int discode = Preferences.Get("Discode", 0);
                        int districtindex = districtMasterslist.FindIndex(s => s.DistrictCode == discode);
                        if (districtindex != -1)
                        {
                            Picker_District.SelectedIndex = districtindex; // triggers SelectedIndexChanged to set zone
                        }
                    }
                }
                else
                {
                    // Subsequent visits: show Get District button for on-demand refresh
                    Btn_getdistrict.IsVisible = true;
                    // Subsequent visits: prefer saved profile district
                    if (int.TryParse(saveUserPreferenceslist.ElementAt(0).DistrictID, out var savedDisCode))
                    {
                        int idx = districtMasterslist.FindIndex(d => d.DistrictCode == savedDisCode);
                        if (idx != -1)
                        {
                            Picker_District.SelectedIndex = idx; // triggers SelectedIndexChanged to set zone
                        }
                    }
                }

                loaddata();

                // Update the GPS label using saved user preferences data
                string districtname;
                if (saveUserPreferenceslist.Any())
                {
                    if (Preferences.Get("lan", "EN-IN").Equals("EN-IN"))
                    {
                        districtname = saveUserPreferenceslist.ElementAt(0).DistrictName.ToString();
                        lbl_gpsdistrict.Text = App.LableText("aspergpsen") + " '" + districtname + "'";
                    }
                    else
                    {
                        districtname = saveUserPreferenceslist.ElementAt(0).DistrictNamelocal.ToString();
                        lbl_gpsdistrict.Text = App.LableText("aspergpshi") + " '" + districtname + "' " + App.LableText("aspergpshi1");
                    }
                }
                else
                {
                    // Fallback to Preferences if no saved data
                    if (Preferences.Get("lan", "EN-IN").Equals("EN-IN"))
                    {
                        lbl_gpsdistrict.Text = App.LableText("aspergpsen") + " '" + Preferences.Get("DistrictName", DistrictName) + "'";
                    }
                    else
                    {
                        lbl_gpsdistrict.Text = App.LableText("aspergpshi") + " '" + Preferences.Get("DistrictName", DistrictName) + "' " + App.LableText("aspergpshi1");
                    }
                }

                Loading_activity.IsVisible = false;
            });
            // ... rest of your code remains the same ...


           

           
        }

        public void insertdistrict()
        {
            try
            {
                DistrictMasterDatabase db = new DistrictMasterDatabase();
                var existing = db.GetDistrictMaster("Select * from DistrictMaster").ToList();
                if (existing.Any())
                {
                    return; // Already seeded
                }

                db.ExecuteNonQuery("INSERT INTO DistrictMaster(DistrictCode, DistrictName, DistrictNameLocal,ZoneName,ZoneCode) VALUES(1, 'Bilaspur', 'बिलासपुर', 'C', 'C');");
                db.ExecuteNonQuery("INSERT INTO DistrictMaster(DistrictCode, DistrictName, DistrictNameLocal,ZoneName,ZoneCode) VALUES(2, 'Chamba', 'चम्बा', 'B', 'B');");
                db.ExecuteNonQuery("INSERT INTO DistrictMaster(DistrictCode, DistrictName, DistrictNameLocal,ZoneName,ZoneCode) VALUES(3, 'Hamirpur', 'हमीरपुर', 'C', 'C');");
                db.ExecuteNonQuery("INSERT INTO DistrictMaster(DistrictCode, DistrictName, DistrictNameLocal,ZoneName,ZoneCode) VALUES(4, 'Kangra', 'काँगड़ा', 'B', 'B');");
                db.ExecuteNonQuery("INSERT INTO DistrictMaster(DistrictCode, DistrictName, DistrictNameLocal,ZoneName,ZoneCode) VALUES(5, 'Kinnaur', 'किन्नौर', 'A', 'A');");
                db.ExecuteNonQuery("INSERT INTO DistrictMaster(DistrictCode, DistrictName, DistrictNameLocal,ZoneName,ZoneCode) VALUES(6, 'Kullu', 'कुल्लू', 'B', 'B');");
                db.ExecuteNonQuery("INSERT INTO DistrictMaster(DistrictCode, DistrictName, DistrictNameLocal,ZoneName,ZoneCode) VALUES(7, 'LAHAUL - SPITI', 'लाहौल -स्पीति ', 'A', 'A');");
                db.ExecuteNonQuery("INSERT INTO DistrictMaster(DistrictCode, DistrictName, DistrictNameLocal,ZoneName,ZoneCode) VALUES(8, 'MANDI', 'मंडी', 'B', 'B');");
                db.ExecuteNonQuery("INSERT INTO DistrictMaster(DistrictCode, DistrictName, DistrictNameLocal,ZoneName,ZoneCode) VALUES(9, 'Shimla', 'शिमला', 'B', 'B');");
                db.ExecuteNonQuery("INSERT INTO DistrictMaster(DistrictCode, DistrictName, DistrictNameLocal,ZoneName,ZoneCode) VALUES(10, 'Sirmaur', 'सिरमौर', 'C', 'C');");
                db.ExecuteNonQuery("INSERT INTO DistrictMaster(DistrictCode, DistrictName, DistrictNameLocal,ZoneName,ZoneCode) VALUES(11, 'SOLAN', 'सोलन', 'C', 'C');");
                db.ExecuteNonQuery("INSERT INTO DistrictMaster(DistrictCode, DistrictName, DistrictNameLocal,ZoneName,ZoneCode) VALUES(12, 'Una', 'ऊना', 'C', 'C');");
            }
            catch (Exception)
            {
            }
        }

        void loaddata() {

            saveUserPreferenceslist = saveUserPreferencesDatabase.GetSaveUserPreferences("Select * from SaveUserPreferences").ToList();
            if (saveUserPreferenceslist.Any())
            {
                entry_mobile.Text = saveUserPreferenceslist.ElementAt(0).Mobile;
                entry_email.Text = saveUserPreferenceslist.ElementAt(0).email;
                entry_name.Text = saveUserPreferenceslist.ElementAt(0).Name;
                entry_place.Text = saveUserPreferenceslist.ElementAt(0).placeofconstruction;

                string zone = saveUserPreferenceslist.ElementAt(0).zonename;
                lbl_fallinzone.Text = App.LableText("underzone") + zone;               
            }
            


        }
        void loaddistricts()
        {
          

            }
        private void Picker_District_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (Picker_District.SelectedIndex == -1)
            {
                return;
            }
            districtcode = districtMasterslist.ElementAt(Picker_District.SelectedIndex).DistrictCode;
            DistrictName = districtMasterslist.ElementAt(Picker_District.SelectedIndex).DistrictName;
            DistrictNameLocal = districtMasterslist.ElementAt(Picker_District.SelectedIndex).DistrictNameLocal;
            zonecode = districtMasterslist.ElementAt(Picker_District.SelectedIndex).ZoneCode;
            zonename = districtMasterslist.ElementAt(Picker_District.SelectedIndex).ZoneName;

            lbl_fallinzone.Text = App.LableText("underzone") + zonename;
        }
        void language()
        { 
            loaddistricts();

            // Update GPS district label using saved user preferences data
            saveUserPreferenceslist = saveUserPreferencesDatabase.GetSaveUserPreferences("Select * from SaveUserPreferences").ToList();
            string districtname;
            if (saveUserPreferenceslist.Any())
            {
                if (Preferences.Get("lan", "").Equals("EN-IN"))
                {
                    districtname = saveUserPreferenceslist.ElementAt(0).DistrictName.ToString();
                    lbl_gpsdistrict.Text = App.LableText("aspergpsen") + " '" + districtname + "'";
                }
                else
                {
                    districtname = saveUserPreferenceslist.ElementAt(0).DistrictNamelocal.ToString();
                    lbl_gpsdistrict.Text = App.LableText("aspergpshi") + " '" + districtname + "' " + App.LableText("aspergpshi1");
                }
            }
            else
            {
                // Fallback to Preferences if no saved data
                if (Preferences.Get("lan", "").Equals("EN-IN"))
                {
                    lbl_gpsdistrict.Text = App.LableText("aspergpsen") + " '" + Preferences.Get("DistrictName", DistrictName) + "'";
                }
                else
                {
                    lbl_gpsdistrict.Text = App.LableText("aspergpshi") + " '" + Preferences.Get("DistrictName", DistrictName) + "' " + App.LableText("aspergpshi1");
                }
            }
           
            lbl_mandatory.Text = App.LableText("mandatory");
            lbl_user_header1.Text = App.LableText("profile");
            lbl_District.Text = App.LableText("districthouse") + "*";
            lbl_name.Text = App.LableText("name") + "*";
            entry_name.Placeholder = App.LableText("entname");

            lbl_mobile.Text = App.LableText("mobileno");
            entry_mobile.Placeholder = App.LableText("entmobileno");

            lbl_email.Text = App.LableText("email");
            entry_email.Placeholder = App.LableText("entemail");

            lbl_place.Text = App.LableText("Placeofconstruction") + "*";
            entry_place.Placeholder = App.LableText("enter") + App.LableText("Placeofconstruction");



            lbl_fallinzone.Text = App.LableText("underzone") + zonename;
            Btn_save.Text = App.LableText("save");
            Btn_cancel.Text = App.LableText("Cancel");
        }


        private void ToolbarItem_Clicked(object sender, EventArgs e)
        {
            if (Preferences.Get("lan", "EN-IN") == "EN-IN")
            {
                Preferences.Set("lan", "HI-IN");
            }
            else
            {
                Preferences.Set("lan", "EN-IN");
            }
            language();
        }



        private void Btn_cancel_Clicked(object sender, EventArgs e)
        {
            if (saveUserPreferenceslist.Any())
            {
                Navigation.PopAsync();
            }
            else
            {
                var window = Application.Current?.Windows?.FirstOrDefault();
                if (window != null)
                {
                    window.Page = new NavigationPage(new MainPage());
                }
            }
        }

        private async void Btn_save_Clicked(object sender, EventArgs e)
        {
            if (await checkvalidtion())
            {

                string UserId;
                saveUserPreferenceslist = saveUserPreferencesDatabase.GetSaveUserPreferences("Select UserID from SaveUserPreferences").ToList();
                if (saveUserPreferenceslist.Any())
                {
                    UserId = saveUserPreferenceslist.ElementAt(0).UserID;
                }
                else
                {
                    UserId = string.Empty;
                }

                Loading_activity.IsVisible = true;
                
                // Store user data in local database
                saveUserPreferencesDatabase.DeleteSaveUserPreferences();
                var item = new SaveUserPreferences();
                item.DistrictID = districtcode.ToString();
                item.DistrictName = DistrictName;
                item.DistrictNamelocal = DistrictNameLocal;                      
                item.Name = entry_name.Text;
                item.Mobile = entry_mobile.Text;
                item.email = entry_email.Text;
                item.placeofconstruction = entry_place.Text;
                item.zonecode = zonecode.ToString();
                item.zonename = zonename;
                item.UserID = UserId;
                saveUserPreferencesDatabase.AddSaveUserPreferences(item);
                
                Loading_activity.IsVisible = false;
                
                // Navigate to dashboard
                var window = Application.Current?.Windows?.FirstOrDefault();
                if (window != null)
                {
                    window.Page = new NavigationPage(new DashboardPage());
                }

            }

        }

        private async void Btn_getdistrict_Clicked(object sender, EventArgs e)
        {
            try
            {
                lbl_PleaseWait.Text = "Getting district as per your location...";
                Loading_activity.IsVisible = true;
                await App.GetLocation();
                lbl_PleaseWait.Text = "Resolving district...";
                var sevice = new HitServices();
                int responselocation = await sevice.getcuurentdistrict(App.Latitude.ToString(), App.Longitude.ToString());
                if (responselocation == 200)
                {
                    int discode = Preferences.Get("Discode", 0);
                    int idx = districtMasterslist.FindIndex(d => d.DistrictCode == discode);
                    if (idx != -1)
                    {
                        Picker_District.SelectedIndex = idx; // updates zone and labels via SelectedIndexChanged
                    }

                    if (Preferences.Get("lan", "").Equals("EN-IN"))
                    {
                        lbl_gpsdistrict.Text = App.LableText("aspergpsen") + " '" + Preferences.Get("DistrictName", DistrictName) + "'";
                    }
                    else
                    {
                        lbl_gpsdistrict.Text = App.LableText("aspergpshi") + " '" + Preferences.Get("DistrictName", DistrictName) + "' " + App.LableText("aspergpshi1");
                    }
                }
            }
            finally
            {
                lbl_PleaseWait.Text = "Please Wait ...";
                Loading_activity.IsVisible = false;
            }
        }

        private async Task<bool> checkvalidtion()
        {
            try
            {


                if (string.IsNullOrEmpty(entry_name.Text))
                {
                    await DisplayAlert("Resilient Construction H.P.", App.LableText("enter") + App.LableText("name"), App.LableText("close"));
                    return false;
                }

                if (string.IsNullOrEmpty(entry_mobile.Text) && string.IsNullOrEmpty(entry_email.Text))
                {
                    await DisplayAlert("Resilient Construction H.P.", App.LableText("entemailormobile"), App.LableText("close"));
                    return false;
                }


                if (Picker_District.SelectedIndex == -1)
                {
                    await DisplayAlert("Resilient Construction H.P.", App.LableText("enter") + App.LableText("district"), App.LableText("close"));
                    return false;
                }


            }
            catch (Exception ex)
            {
                await DisplayAlert("Resilient Construction H.P.", ex.Message, App.LableText("close"));
                return false;
            }
            return true;
        }
    }
}