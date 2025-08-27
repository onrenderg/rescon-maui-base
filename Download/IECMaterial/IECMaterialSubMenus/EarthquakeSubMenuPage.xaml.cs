using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Maui;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Controls.Xaml;

namespace ResillentConstruction.IECMaterial.IECMaterialSubMenus
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EarthquakeSubMenuPage : ContentPage
    {
        public Label[] Footer_Labels;
        public string[] Footer_Image_Source;
        public Image[] Footer_Images;

        public EarthquakeSubMenuPage(string menuname)
        {
            InitializeComponent();
            Footer_Labels = new Label[3] { Tab_Home_Label, Tab_Download_Label, Tab_Settings_Label };
            Footer_Images = new Image[3] { Tab_Home_Image, Tab_Download_Image, Tab_Settings_Image };
            Footer_Image_Source = new string[3] { "ic_home.png", "ic_download.png", "ic_more.png" };

            lbl_navigation_header.Text = App.LableText("lbl_navigation_header");
            lbl_Topheading.Text = menuname;

            Btn_EarthquakeAwarenessforworkplace.Text = App.LableText("EarthquakeAwarenessforworkplace");
            Btn_EarthquakePoster.Text = App.LableText("EarthquakePoster");
            Btn_EarthQuakeSafety.Text = App.LableText("EarthQuakeSafety");
            Btn_EarthquakeSafetyTrifold.Text = App.LableText("EarthquakeSafetyTrifold");
            Btn_EarthQuakeSurvivalGuide.Text = App.LableText("EarthQuakeSurvivalGuide");
            Btn_EarthquakePreparednessGuide.Text = App.LableText("EarthquakePreparednessGuide");
            Btn_EarthquakeSafetyHangUp.Text = App.LableText("EarthquakeSafetyHangUp");
          
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

        private void Btn_EarthquakeAwarenessforworkplace_Clicked(object sender, EventArgs e)
        {
            Launcher.OpenAsync(new Uri("https://hpsdma.nic.in/WriteReadData/LINKS/showimg%20(6)c2712ab0-1bcb-4e1f-826c-fef26b3c87c9.pdf"));
        }

        private void Btn_EarthquakePoster_Clicked(object sender, EventArgs e)
        {
            Launcher.OpenAsync(new Uri("https://hpsdma.nic.in/WriteReadData/LINKS/showimg%20(7)b7985512-e859-4ab4-835c-63513fa41db8.pdf"));
        }

        private void Btn_EarthQuakeSafety_Clicked(object sender, EventArgs e)
        {
            Launcher.OpenAsync(new Uri("https://hpsdma.nic.in/WriteReadData/LINKS/showimg%20(8)56dca7da-b35e-4672-9dd3-9c8ee739ccda.pdf"));
        }

        private void Btn_EarthquakeSafetyTrifold_Clicked(object sender, EventArgs e)
        {
            Launcher.OpenAsync(new Uri("https://hpsdma.nic.in/WriteReadData/LINKS/showimg%20(13)dc0ab9b2-d5cf-4bc3-9f49-49627eb06902.pdf"));

        }
        private void Btn_EarthQuakeSurvivalGuide_Clicked(object sender, EventArgs e)
        {
            Launcher.OpenAsync(new Uri("https://hpsdma.nic.in/WriteReadData/LINKS/showimg%20(18)4f5920e6-eedf-4540-9270-5a381b3ce983.pdf"));
        }

        private void Btn_EarthquakePreparednessGuide_Clicked(object sender, EventArgs e)
        {
            Launcher.OpenAsync(new Uri("https://hpsdma.nic.in/WriteReadData/LINKS/showimg%20(19)38d64f95-41b9-4b76-b4be-bdaae1718df9.pdf"));
        }

        private void Btn_EarthquakeSafetyHangUp_Clicked(object sender, EventArgs e)
        {
            Launcher.OpenAsync(new Uri("https://hpsdma.nic.in/WriteReadData/LINKS/showimg%20(20)42e6f546-bcce-4df5-b976-1f7cee1a5b1b.pdf"));
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