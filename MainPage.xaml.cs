using ResillentConstruction.Engineer;
using ResillentConstruction.Models;
using ResillentConstruction.webapi;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Maui;
using Microsoft.Maui.Controls;

namespace ResillentConstruction
{
    public partial class MainPage : ContentPage
    {

        SaveUserPreferencesDatabase saveUserPreferencesDatabase = new SaveUserPreferencesDatabase();

    

        List<SaveUserPreferences> saveUserPreferenceslist;


        public MainPage()
        {
            InitializeComponent();
            saveUserPreferenceslist = saveUserPreferencesDatabase.GetSaveUserPreferences("Select * from SaveUserPreferences").ToList();


            if (!string.IsNullOrEmpty(Preferences.Get("UserType", "")))
            {
                if (Preferences.Get("UserType", "").Equals("Engineer"))
                {
                    //rd_engineer.IsChecked = true;
                   // btn_submit.Text = "View Guidelines";
                }
                else
                {
                   // rd_citizen.IsChecked = true;

                    if (!saveUserPreferenceslist.Any())
                    {
                     //   btn_submit.Text = "Proceed";
                    }
                    else
                    {
                     //   btn_submit.Text = "View Guidelines";

                    }
                }
            }
    

            lbl_navigation_header.Text = "HPSDMA";
            lbl_footer.Text = "Centre Of Competence For Mobile Application Development NIC Himachal Pradesh" + " (v" + VersionTracking.CurrentVersion+")";
        

          
        }
        private void stack_citizen_Tapped(object sender, EventArgs e)
        {
            try
            {
                // Refresh list to avoid null or stale state
                saveUserPreferenceslist = saveUserPreferencesDatabase.GetSaveUserPreferences("Select * from SaveUserPreferences").ToList();
                bool hasPrefs = saveUserPreferenceslist != null && saveUserPreferenceslist.Any();

                var window = Application.Current?.Windows?.FirstOrDefault();
                if (window != null)
                {
                    window.Page = new NavigationPage(hasPrefs ? new DashboardPage() : new ProfilePage());
                }
            }
            catch (Exception ex)
            {
                // Show the error instead of crashing
                _ = DisplayAlert("Resilient Construction H.P.", ex.Message, "Close");
            }
        }
        private void stack_engineer_Tapped(object sender, EventArgs e)
        {
            var window = Application.Current?.Windows?.FirstOrDefault();
            if (window != null)
            {
                window.Page = new NavigationPage(new EngineerDashboardPage());
            }

        }


        private void btn_submit_Clicked(object sender, EventArgs e)
        {
          /*  if (rd_citizen.IsChecked)
            {
                Preferences.Set("UserType", "Citizen");
                if (!saveUserPreferenceslist.Any())
                {
                    Application.Current.MainPage = new NavigationPage(new ProfilePage());
                }
                else
                {
                    Application.Current.MainPage = new NavigationPage(new DashboardPage());
                }
            }
            else
            {
                Preferences.Set("UserType", "Engineer");
                Application.Current.MainPage = new NavigationPage(new EngineerDashboardPage());

            }*/
        }

        /*private void rd_citizen_CheckedChanged(object sender, CheckedChangedEventArgs e)
        {
            if (rd_citizen.IsChecked)
            {
                Preferences.Set("UserType", "Citizen");
                if (!saveUserPreferenceslist.Any())
                {
                    btn_submit.Text = "Proceed";
                }
                else
                {
                    btn_submit.Text = "View Guidelines";

                }
            }
            else
            {
                Preferences.Set("UserType", "Engineer");
                btn_submit.Text = "View Guidelines";
            }
        }*/

    }
}
