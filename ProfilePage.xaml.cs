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
        double applat, applong, appaccuracy;
        string appdate, apptime;
        string preferlanguage;

        DistrictMasterDatabase districtMasterDatabase = new DistrictMasterDatabase();
        List<DistrictMaster> districtMasterslist = new List<DistrictMaster>();

        public ProfilePage()
        {
            InitializeComponent();
            language();

            lbl_navigation_header.Text = App.LableText("lbl_navigation_header");


            MainThread.BeginInvokeOnMainThread(async () =>
            {
                do
                {
                    Loading_activity.IsVisible = true;
                    lbl_PleaseWait.Text = App.LableText("PleaseWait");

                    await App.GetLocation();
                    //   var place = await App.GetmyAddress();

                    applat = App.Latitude;
                    applong = App.Longitude;
                    appaccuracy = App.Accuracy;
                    /*       appdate = App.networkDateTime.ToString("yyyy/MM/dd");
                           apptime = App.networkDateTime.ToString("HH:mm:ss");*/

                    await loaddistrictfromapi(applat.ToString(), applong.ToString());

                    Loading_activity.IsVisible = false;
                }
                while (App.Longitude < 1.00);
            });
        }

        async Task loaddistrictfromapi(string applat, string applong)
        {
            var sevice = new HitServices();
            int responselocation = await sevice.getcuurentdistrict(applat.ToString(), applong.ToString());
            if (responselocation == 200)
            {
                loaddistricts();

                Picker_District.ItemsSource = districtMasterslist;
                Picker_District.ItemDisplayBinding = new Binding("DistrictName");

                int discode = Preferences.Get("Discode", 0);
                DistrictName = Preferences.Get("DistrictName", "");

                int districtindex = districtMasterslist.FindIndex(s => s.DistrictCode == discode);
                if (districtindex != -1)
                {
                    Picker_District.SelectedIndex = districtindex;
                    districtcode = districtMasterslist.ElementAt(districtindex).DistrictCode;
                    DistrictName = districtMasterslist.ElementAt(districtindex).DistrictName;
                    DistrictNameLocal = districtMasterslist.ElementAt(districtindex).DistrictNameLocal;
                }
            }

            lodadata();
            if (Preferences.Get("lan", "").Equals("EN-IN"))
            {
                lbl_gpsdistrict.Text = App.LableText("aspergpsen") + " '" + Preferences.Get("DistrictName", DistrictName) + "'";
            }
            else
            {
                lbl_gpsdistrict.Text = App.LableText("aspergpshi") + " '" + Preferences.Get("DistrictName", DistrictName) + "' " + App.LableText("aspergpshi1");
            }
        }

        void lodadata()
        {
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
            Picker_District.Title = App.LableText("district");
            // Prefer global cache if available, else read from DB
            if (App.Districts != null && App.Districts.Any())
            {
                districtMasterslist = App.Districts;
            }
            else
            {
                districtMasterslist = districtMasterDatabase.GetDistrictMaster("Select * from DistrictMaster").ToList();
            }

            // Fallback: populate local table if empty, then reload
            if (!districtMasterslist.Any())
            {
                DistrictMasterDatabase.insertdistrict();
                districtMasterslist = districtMasterDatabase.GetDistrictMaster("Select * from DistrictMaster").ToList();
                // Update global cache so other pages benefit
                App.Districts = districtMasterslist;
            }

            Picker_District.ItemsSource = districtMasterslist;
            if (Preferences.Get("lan", "EN-IN").Equals("EN-IN"))
            {
                Picker_District.ItemDisplayBinding = new Binding("DistrictName");
            }
            else
            {
                Picker_District.ItemDisplayBinding = new Binding("DistrictNameLocal");
            }

            // Ensure a default selection so the picker shows a value
            if (districtMasterslist.Any() && Picker_District.SelectedIndex == -1)
            {
                Picker_District.SelectedIndex = 0;
            }
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

            //lbl_gpsdistrict.Text = App.LableText("aspergps") + " '" + Preferences.Get("DistrictName", DistrictName) + "'";
            if (Preferences.Get("lan", "").Equals("EN-IN"))
            {
                lbl_gpsdistrict.Text = App.LableText("aspergpsen") + " '" + Preferences.Get("DistrictName", DistrictName) + "'";
            }
            else
            {
                lbl_gpsdistrict.Text = App.LableText("aspergpshi") + " '" + Preferences.Get("DistrictName", DistrictName) + "' " + App.LableText("aspergpshi1");
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
                Application.Current.MainPage = new NavigationPage(new MainPage());
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

                // Save locally instead of remote API to avoid blocking
                if (Picker_District.SelectedIndex >= 0 && districtMasterslist.Any())
                {
                    districtcode = districtMasterslist.ElementAt(Picker_District.SelectedIndex).DistrictCode;
                    DistrictName = districtMasterslist.ElementAt(Picker_District.SelectedIndex).DistrictName;
                    DistrictNameLocal = districtMasterslist.ElementAt(Picker_District.SelectedIndex).DistrictNameLocal;
                    zonecode = districtMasterslist.ElementAt(Picker_District.SelectedIndex).ZoneCode;
                    zonename = districtMasterslist.ElementAt(Picker_District.SelectedIndex).ZoneName;
                }

                // Overwrite any previous prefs and insert fresh
                saveUserPreferencesDatabase.DeleteSaveUserPreferences();
                saveUserPreferencesDatabase.AddSaveUserPreferences(new SaveUserPreferences
                {
                    UserID = UserId,
                    Name = entry_name.Text,
                    Mobile = entry_mobile.Text,
                    email = entry_email.Text,
                    DistrictID = districtcode.ToString(),
                    DistrictName = DistrictName,
                    DistrictNamelocal = DistrictNameLocal,
                    placeofconstruction = entry_place.Text,
                    zonename = zonename,
                    zonecode = zonecode.ToString(),
                    AppInstall = "1",
                    language = Preferences.Get("lan", "EN-IN").Equals("EN-IN") ? 0 : 1
                });

                Application.Current.MainPage = new NavigationPage(new DashboardPage());
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