

using ResillentConstruction.Engineer;
using ResillentConstruction.Models;
using ResillentConstruction.webapi;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Maui;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Controls.Xaml;


namespace ResillentConstruction
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MorePage : ContentPage
    {
        public Label[] Footer_Labels;
        public string[] Footer_Image_Source;
        public Image[] Footer_Images;
       
       
        public MorePage()
        {
            InitializeComponent();
            lbl_navigation_header.Text = App.LableText("lbl_navigation_header");
            VersionTracking.Track();
            var currentVersion = VersionTracking.CurrentVersion;
            lbl_appversion.Text = App.LableText("Version") + " " + currentVersion;
            lbl_appname.Text = App.LableText("AppName");
            //lbl_title.Text = App.LableText("deptt");
            lbl_dept.Text = App.LableText("deptt");
            lbl_call.Text = App.LableText("callus");
            lbl_website.Text = App.LableText("Website");
            lbl_email.Text = App.LableText("Email");
            lbl_policy.Text = App.LableText("PrivacyPolicy");
            lbl_raisequery.Text = App.LableText("RaiseQuery");
            lbl_language.Text = App.LableText("language");
            lbl_profile.Text = App.LableText("profile");
            if (Preferences.Get("UserType", "").Equals("Engineer"))
            {
                stack_profile.IsVisible = false; 
            }


            Footer_Labels = new Label[3] { Tab_Home_Label, Tab_Download_Label, Tab_Settings_Label };
            Footer_Images = new Image[3] { Tab_Home_Image, Tab_Download_Image, Tab_Settings_Image };
            //Footer_Image_Source = new string[3] { "ic_stock.png", "ic_add.png", "ic_more.png" };
            Footer_Image_Source = new string[3] { "ic_Home.png", "ic_download.png", "ic_more.png" };
          
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            Tab_Home_Label.Text = App.LableText("Home");
            Tab_Download_Label.Text = App.LableText("Download");
            Tab_Settings_Label.Text = App.LableText("More");
            Footer_Image_Source = new string[3] { "ic_Homewhite.png", "ic_downloadehite.png", "ic_more.png" };
            Footer_Images[Preferences.Get("Active", 0)].Source = Footer_Image_Source[Preferences.Get("Active", 0)];
            Footer_Labels[Preferences.Get("Active", 0)].TextColor = Color.FromArgb("#FF0F0F0F");
        }

        private void Tab_Home_Tapped(object sender, EventArgs e)
        {
            Preferences.Set("Active", 0);
            var window = Application.Current?.Windows?.FirstOrDefault();
            if (window != null)
            {
                window.Page = new NavigationPage(new DashboardPage());
            }
        }
        private void Tab_Download_Tapped(object sender, EventArgs e)
        {
            Preferences.Set("Active", 1);
            var window2 = Application.Current?.Windows?.FirstOrDefault();
            if (window2 != null)
            {
                window2.Page = new NavigationPage(new DownloadPage());
            }
        }
        private void Tab_Settings_Tapped(object sender, EventArgs e)
        {
            Preferences.Set("Active", 2);
            var window3 = Application.Current?.Windows?.FirstOrDefault();
            if (window3 != null)
            {
                window3.Page = new NavigationPage(new MorePage());
            }
        }

        private void Deptt_Call(object sender, EventArgs e)
        {

            Launcher.OpenAsync("tel:+911772620152");
        }

        private void deptt_WebSite(object sender, EventArgs e)
        {
            Launcher.OpenAsync("https://hpsdma.nic.in/");
        }

        private async void Deptt_email(object sender, EventArgs e)
        {            
            string address = "sdma-hp@nic.in";
            await Launcher.OpenAsync(new Uri($"mailto:{address}"));
        }

        private void policytapped(object sender, EventArgs e)
        {
            var servicve = new HitServices();
            Navigation.PushAsync(new LoadWebViewPage(servicve.PrivacyPolicyUrl));
        } 
        
        
        private void profiletapped(object sender, EventArgs e)
        {
            var servicve = new HitServices();
            Navigation.PushAsync(new ProfilePage());
        }

        private void raisequerytapped(object sender, EventArgs e)
        {           
           // Application.Current.MainPage=new NavigationPage(new RaiseQueryPage());
        } 
        
        private void languagetapped(object sender, EventArgs e)
        {
            if (Preferences.Get("lan", "EN-IN") == "EN-IN")
            {
                Preferences.Set("lan", "HI-IN");
            }
            else
            {
                Preferences.Set("lan", "EN-IN");
            }
            if (Preferences.Get("UserType", "").Equals("Engineer"))
            {
                var window4 = Application.Current?.Windows?.FirstOrDefault();
                if (window4 != null)
                {
                    window4.Page = new NavigationPage(new EngineerDashboardPage());
                }

            }
            else
            {
                var window5 = Application.Current?.Windows?.FirstOrDefault();
                if (window5 != null)
                {
                    window5.Page = new NavigationPage(new DashboardPage());
                }

            }
        }


        private async void logoutTapped(object sender, EventArgs e)
        {
            SaveUserPreferencesDatabase saveUserPreferencesDatabase = new SaveUserPreferencesDatabase();
            List<SaveUserPreferences> saveUserPreferenceslist;

            saveUserPreferenceslist = saveUserPreferencesDatabase.GetSaveUserPreferences("Select * from SaveUserPreferences").ToList();
            string loggedinuser = saveUserPreferenceslist.ElementAt(0).Name ?? string.Empty;
            bool m = await DisplayAlert(App.AppName, App.LableText("areyousure") + " '" + loggedinuser
               + "' " + App.LableText("youlogout"), App.LableText("Logout"), App.LableText("Cancel"));
           
            if (m)
            {              
                saveUserPreferencesDatabase.DeleteSaveUserPreferences();            
                var window6 = Application.Current?.Windows?.FirstOrDefault();
                if (window6 != null)
                {
                    window6.Page = new NavigationPage(new MainPage());
                }
            }
        }

       
    }
}