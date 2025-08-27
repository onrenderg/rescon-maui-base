using System;
using Microsoft.Maui;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Controls.Xaml;

namespace ResillentConstruction.IECMaterial.IECMaterialSubMenus
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SnowAvalancheSubMenuPage : ContentPage
    {
        public Label[] Footer_Labels;
        public string[] Footer_Image_Source;
        public Image[] Footer_Images;

        public SnowAvalancheSubMenuPage(string menuname)
        {
            InitializeComponent();
            Footer_Labels = new Label[3] { Tab_Home_Label, Tab_Download_Label, Tab_Settings_Label };
            Footer_Images = new Image[3] { Tab_Home_Image, Tab_Download_Image, Tab_Settings_Image };
            Footer_Image_Source = new string[3] { "ic_home.png", "ic_download.png", "ic_more.png" };

            lbl_navigation_header.Text = App.LableText("lbl_navigation_header");
            lbl_Topheading.Text = menuname;

            Btn_AvadhavAvalanche.Text = App.LableText("AvadhavAvalanche");
            Btn_AvalancheAUsersGuide.Text = App.LableText("AvalancheAUsersGuide");
            Btn_AvdhavHimskhalanNirdeshikaHindi.Text = App.LableText("AvdhavHimskhalanNirdeshikaHindi");
            Btn_ControlledReleaseOfAvalanche.Text = App.LableText("ControlledReleaseOfAvalanche");
            Btn_VisualAvalancheSafetyBooklet.Text = App.LableText("VisualAvalancheSafetyBooklet");
           
        }

        private void Btn_AvadhavAvalanche_Clicked(object sender, EventArgs e)
        {
            Launcher.OpenAsync(new Uri("https://hpsdma.nic.in/WriteReadData/LINKS/showimgc4f396c7-e81d-4dad-b63f-689098ad2620.pdf"));
        }
        
        private void Btn_AvalancheAUsersGuide_Clicked(object sender, EventArgs e)
        {
            Launcher.OpenAsync(new Uri("https://hpsdma.nic.in/WriteReadData/LINKS/showimg%20(1)edc3dcbc-1912-4584-9c0b-e3227b66f3cb.pdf"));

        }

        private void Btn_AvdhavHimskhalanNirdeshikaHindi_Clicked(object sender, EventArgs e)
        {
            Launcher.OpenAsync(new Uri("https://hpsdma.nic.in/WriteReadData/LINKS/showimg%20(2)2d3cc717-f46b-4e46-bffb-9948aaf77b7f.pdf"));

        }

        private void Btn_ControlledReleaseOfAvalanche_Clicked(object sender, EventArgs e)
        {
            Launcher.OpenAsync(new Uri("https://hpsdma.nic.in/WriteReadData/LINKS/showimg%20(3)27461680-c3cf-42fd-9e7a-f013a7cac817.pdf"));

        }

        private void Btn_VisualAvalancheSafetyBooklet_Clicked(object sender, EventArgs e)
        {
            Launcher.OpenAsync(new Uri("https://hpsdma.nic.in/WriteReadData/LINKS/showimg%20(4)9eafc2bc-c6a9-4b1f-9d45-cfb92fe8453e.pdf"));

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
    }
}