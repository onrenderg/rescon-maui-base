using System;
using Microsoft.Maui;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Controls.Xaml;

namespace ResillentConstruction.IECMaterial.IECMaterialSubMenus
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RoadAccidentSubMenuPage : ContentPage
    {
        public Label[] Footer_Labels;
        public string[] Footer_Image_Source;
        public Image[] Footer_Images;
        public RoadAccidentSubMenuPage(string menuname)
        {
            InitializeComponent();
            Footer_Labels = new Label[3] { Tab_Home_Label, Tab_Download_Label, Tab_Settings_Label };
            Footer_Images = new Image[3] { Tab_Home_Image, Tab_Download_Image, Tab_Settings_Image };
            Footer_Image_Source = new string[3] { "ic_home.png", "ic_download.png", "ic_more.png" };

            lbl_navigation_header.Text = App.LableText("lbl_navigation_header");
            lbl_Topheading.Text = menuname;
            Btn_PosteronRoadSafety1.Text = App.LableText("PosteronRoadSafety1");
            Btn_PosteronRoadSafety2.Text = App.LableText("PosteronRoadSafety2");
            Btn_RoadSafetyTrifold.Text = App.LableText("RoadSafetyTrifold");
        }

        private void Btn_PosteronRoadSafety1_Clicked(object sender, EventArgs e)
        {
            Launcher.OpenAsync(new Uri("https://hpsdma.nic.in/WriteReadData/LINKS/showimg%20(26)97aa8e93-7541-430f-8f5d-8b860d9567d2.pdf"));

        } 
        private void Btn_PosteronRoadSafety2_Clicked(object sender, EventArgs e)
        {
            Launcher.OpenAsync(new Uri("https://hpsdma.nic.in/WriteReadData/LINKS/showimg%20(27)e7808500-3746-4962-a1b1-a1461646fe37.pdf"));

        } 
        private void Btn_RoadSafetyTrifold_Clicked(object sender, EventArgs e)
        {
            Launcher.OpenAsync(new Uri("https://hpsdma.nic.in/WriteReadData/LINKS/showimg%20(25)6e07477d-f34a-4148-a172-d2d8ce70d422.pdf"));

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