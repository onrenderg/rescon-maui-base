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
    public partial class DownloadPage : ContentPage
    {
        public Label[] Footer_Labels;
        public string[] Footer_Image_Source;
        public Image[] Footer_Images;
        SaveUserPreferencesDatabase saveUserPreferencesDatabase = new SaveUserPreferencesDatabase();
        List<SaveUserPreferences> saveUserPreferenceslist;
        string userzone, districtname;

        public DownloadPage()
        {
            InitializeComponent();
            saveUserPreferenceslist = saveUserPreferencesDatabase.GetSaveUserPreferences("Select * from SaveUserPreferences").ToList();
            string language = Preferences.Get("lan", "EN-IN");
            if (language.Equals("EN-IN"))
            {
                districtname = saveUserPreferenceslist.ElementAt(0).DistrictName.ToString();
            }
            else
            {
                districtname = saveUserPreferenceslist.ElementAt(0).DistrictNamelocal.ToString();
            }

            lbl_Topheading.Text = saveUserPreferenceslist.ElementAt(0).Name + " (" + districtname + ", " + App.LableText("yourzone") + " - " + saveUserPreferenceslist.ElementAt(0).zonename + ")";

            Footer_Labels = new Label[3] { Tab_Home_Label, Tab_Download_Label, Tab_Settings_Label };
            Footer_Images = new Image[3] { Tab_Home_Image, Tab_Download_Image, Tab_Settings_Image };            
            Footer_Image_Source = new string[3] { "ic_home.png", "ic_download.png", "ic_more.png" };          

            lbl_navigation_header.Text = App.LableText("lbl_navigation_header");
            userzone = saveUserPreferenceslist.ElementAt(0).zonename.ToString();
            lbl_mapzone.Text = App.LableText("mapforzone") + userzone;
            Btn_video.Text = App.LableText("videoResilientConstructionGuidelines");
            Btn_userzone.Text = App.LableText("yourzoneguidelines") + " '" + userzone + "'";
            Btn_otherzone.Text = App.LableText("otherzoneguidelines");
            Btn_consprofpri.Text = App.LableText("priguidelines");
            lbl_user_header1.Text = App.LableText("guidelines");
            lbl_iecgudelines.Text = App.LableText("IECMaterial");
            lbl_moregudelines.Text = App.LableText("moreguidelines");
            lbl_SafetyTips.Text = App.LableText("SafetyTips");

            if (userzone.Equals("A"))
            {
                img_zoneimage.Source = "zonea.png";
            }
            else if (userzone.Equals("B"))
            {
                img_zoneimage.Source = "zoneb.png";
            }
            else if (userzone.Equals("C"))
            {
                img_zoneimage.Source = "zonec.png";
            }

        }

        private void ToolbarItem_Clicked(object sender, EventArgs e)
        {
            //await DownloadPDF("", "");
        }
        private async void Btn_video_Clicked(object sender, EventArgs e)
        {
            var service = new HitServices();
            await Launcher.OpenAsync(service.Constructionpriurl);
        }

        private async void Btn_userzone_Clicked(object sender, EventArgs e)
        {
            string url = "";
            var service = new HitServices();           

            if (userzone.Equals("A"))
            {
                url = service.zoneAurl;
            }
            else if (userzone.Equals("B"))
            {
                url = service.zoneBurl;
            }
            else if (userzone.Equals("C"))
            {
                url = service.zoneCurl;
            }
            await Launcher.OpenAsync(url);
        }

        private void Btn_otherzone_Clicked(object sender, EventArgs e)
        {
            ShowActionSheet();
        }

        private async void Btn_Constructionpri_Clicked(object sender, EventArgs e)
        {
            var service = new HitServices();
            string url = service.Constructionpriurl;
            await Launcher.OpenAsync(url);
        }
        async void ShowActionSheet()
        {
            string action = "";
            if (userzone.Equals("A"))
            {
                action = await DisplayActionSheet(App.LableText("ChooseZone"), "cancel", null, "Zone B", "Zone C");
            }
            else if (userzone.Equals("B"))
            {
                action = await DisplayActionSheet(App.LableText("ChooseZone"), "cancel", null, "Zone A", "Zone C");
            }
            else if (userzone.Equals("C"))
            {
                action = await DisplayActionSheet(App.LableText("ChooseZone"), "cancel", null, "Zone A", "Zone B");
            }

            var service = new HitServices();                      
            switch (action)
            {
                case "Zone A":
                    string url = service.zoneAurl;
                    await Launcher.OpenAsync(url);
                    break;
                case "Zone B":
                    url = service.zoneBurl;
                    await Launcher.OpenAsync(url);
                    break;
                case "Zone C":
                    url = service.zoneCurl;
                    await Launcher.OpenAsync(url);
                    break;
                case "cancel":
                    // Optionally handle cancel action
                    break;
            }
        }

        private void imgbtn_profile_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new ProfilePage());
        } 
        
        private void Btn_iecguidelines_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new IECGuidelinesPage());
        } 
        
        private void Btn_moreguidelines_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new MoreGuidelinesPage(App.LableText("moreguidelines")));
        } 
        
        private void Btn_SafetyTips_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new SafetyTipsPage(App.LableText("SafetyTips")));
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            Tab_Home_Label.Text = App.LableText("Home");
            Tab_Download_Label.Text = App.LableText("Download");
            Tab_Settings_Label.Text = App.LableText("More");
            Footer_Image_Source = new string[3] { "ic_homewhite.png", "ic_download.png", "ic_morewhite.png" };
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
            var window = Application.Current?.Windows?.FirstOrDefault();
            if (window != null)
            {
                window.Page = new NavigationPage(new DownloadPage());
            }
        }
        private void Tab_Settings_Tapped(object sender, EventArgs e)
        {
            Preferences.Set("Active", 2);
            var window = Application.Current?.Windows?.FirstOrDefault();
            if (window != null)
            {
                window.Page = new NavigationPage(new MorePage());
            }
        }
    }
}