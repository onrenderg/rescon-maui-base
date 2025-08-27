using System;
using Microsoft.Maui;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Controls.Xaml;

namespace ResillentConstruction
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SafetyTipsPage : ContentPage
    {

        public Label[] Footer_Labels;
        public string[] Footer_Image_Source;
        public Image[] Footer_Images;
        public SafetyTipsPage(string title)
        {
            InitializeComponent();
            Footer_Labels = new Label[3] { Tab_Home_Label, Tab_Download_Label, Tab_Settings_Label };
            Footer_Images = new Image[3] { Tab_Home_Image, Tab_Download_Image, Tab_Settings_Image };
            Footer_Image_Source = new string[3] { "ic_home.png", "ic_download.png", "ic_more.png" };
            lbl_navigation_header.Text = App.LableText("lbl_navigation_header");

            lbl_Topheading.Text = title;

            Btn_EarthQuakeSafetyTips.Text = App.LableText("EarthQuakeSafetyTips");
            Btn_FireAwarenessSafetyTips.Text = App.LableText("FireAwarenessSafetyTips");
            Btn_FloodSafetyTips.Text = App.LableText("FloodSafetyTips");
            Btn_LandSlideReadyReckonerSafetyTips.Text = App.LableText("LandSlideReadyReckonerSafetyTips");
            Btn_LandSlidesSafetyTips.Text = App.LableText("LandSlidesSafetyTips");
            Btn_ColdWaveSafetyTips.Text = App.LableText("ColdWaveSafetyTips");
            Btn_ColdWaveFrost.Text = App.LableText("ColdWaveFrost");         

        }

        private void Btn_EarthQuakeSafetyTips_Clicked(object sender, EventArgs e)
        {
            Launcher.OpenAsync(new Uri("https://hpsdma.nic.in//admnis/admin/showimg.aspx?ID=262"));
        }

        private void Btn_FireAwarenessSafetyTips_Clicked(object sender, EventArgs e)
        {
            Launcher.OpenAsync(new Uri("https://hpsdma.nic.in//admnis/admin/showimg.aspx?ID=263"));
        }

        private void Btn_FloodSafetyTips_Clicked(object sender, EventArgs e)
        {
            Launcher.OpenAsync(new Uri("https://hpsdma.nic.in//admnis/admin/showimg.aspx?ID=264"));
        }

        private void Btn_LandSlideReadyReckonerSafetyTips_Clicked(object sender, EventArgs e)
        {
            Launcher.OpenAsync(new Uri("https://hpsdma.nic.in//admnis/admin/showimg.aspx?ID=265"));
        }

        private void Btn_LandSlidesSafetyTips_Clicked(object sender, EventArgs e)
        {
            Launcher.OpenAsync(new Uri("https://hpsdma.nic.in//admnis/admin/showimg.aspx?ID=266"));
        }

        private void Btn_ColdWaveSafetyTips_Clicked(object sender, EventArgs e)
        {
            Launcher.OpenAsync(new Uri("https://hpsdma.nic.in//admnis/admin/showimg.aspx?ID=267"));
        }

        private void Btn_ColdWaveFrost_Clicked(object sender, EventArgs e)
        {
            Launcher.OpenAsync(new Uri("https://hpsdma.nic.in//admnis/admin/showimg.aspx?ID=3375"));
        }



        protected override void OnAppearing()
        {
            base.OnAppearing();
            Tab_Home_Label.Text = App.LableText("Home");
            Tab_Download_Label.Text = App.LableText("Download");
            Tab_Settings_Label.Text = App.LableText("More");
            Footer_Image_Source = new string[3] { "ic_homewhite.png", "ic_download.png", "ic_morewhite.png" };
            Footer_Images[Preferences.Get("Active", 0)].Source = Footer_Image_Source[Preferences.Get("Active", 0)];
            Footer_Labels[Preferences.Get("Active", 0)].TextColor = Color.FromHex("#0f0f0f");
        }
        private void Tab_Home_Tapped(object sender, EventArgs e)
        {
            Preferences.Set("Active", 0);
            Application.Current.MainPage = new NavigationPage(new DashboardPage());
        }
        private void Tab_Download_Tapped(object sender, EventArgs e)
        {
            Preferences.Set("Active", 1);
            Application.Current.MainPage = new NavigationPage(new DownloadPage());
        }
        private void Tab_Settings_Tapped(object sender, EventArgs e)
        {
            Preferences.Set("Active", 2);
            Application.Current.MainPage = new NavigationPage(new MorePage());
        }
    }
}