using ResillentConstruction.IECMaterial.IECMaterialSubMenus;
using System;
using Microsoft.Maui;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Controls.Xaml;

namespace ResillentConstruction
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MoreGuidelinesPage : ContentPage
    {
        public Label[] Footer_Labels;
        public string[] Footer_Image_Source;
        public Image[] Footer_Images;
        public MoreGuidelinesPage(string title)
        {
            
            InitializeComponent();
            Footer_Labels = new Label[3] { Tab_Home_Label, Tab_Download_Label, Tab_Settings_Label };
            Footer_Images = new Image[3] { Tab_Home_Image, Tab_Download_Image, Tab_Settings_Image };
            Footer_Image_Source = new string[3] { "ic_home.png", "ic_download.png", "ic_more.png" };
            lbl_navigation_header.Text = App.LableText("lbl_navigation_header");

            lbl_Topheading.Text = title;

            Btn_masonguide.Text = App.LableText("masonguide");
            if (Preferences.Get("lan", "") == "EN-IN")
            {
                Btn_Ownersguide.Text = App.LableText("Ownersguideeng");
            }
            else
            {
                Btn_Ownersguide.Text = App.LableText("Ownersguidehindi");
            }
            Btn_dwellingunitforSeismiczone.Text = App.LableText("dwellingunitforSeismiczone");
            Btn_EarthQuakeResistantConstructionIV.Text = App.LableText("EarthQuakeResistantConstructionIV");
            Btn_GuideForEarthquakeRetrofitting.Text = App.LableText("GuideForEarthquakeRetrofitting");
            Btn_RetrofittingGuidelines.Text = App.LableText("RetrofittingGuidelines");
            Btn_SimplifiedGuidelineZoneV.Text = App.LableText("SimplifiedGuidelineZoneV");
            Btn_EvolvingStrategyPanchayatlevel.Text = App.LableText("EvolvingStrategyPanchayatlevel");
           
        }

        private void Btn_masonguide_Clicked(object sender, EventArgs e)
        {
            Launcher.OpenAsync(new Uri("https://hpsdma.nic.in//admnis/admin/showimg.aspx?ID=342"));
        }

        private void Btn_Ownersguide_Clicked(object sender, EventArgs e)
        {
            if (Preferences.Get("lan", "") == "EN-IN")
            {
                Launcher.OpenAsync(new Uri("https://hpsdma.nic.in//admnis/admin/showimg.aspx?ID=343"));
            }
            else
            {
                Launcher.OpenAsync(new Uri("https://hpsdma.nic.in//admnis/admin/showimg.aspx?ID=344"));
            }
        }

        private void Btn_dwellingunitforSeismiczone_Clicked(object sender, EventArgs e)
        {
            Launcher.OpenAsync(new Uri("https://hpsdma.nic.in//admnis/admin/showimg.aspx?ID=345"));
        }

        private void Btn_EarthQuakeResistantConstructionIV_Clicked(object sender, EventArgs e)
        {
            Launcher.OpenAsync(new Uri("https://hpsdma.nic.in//admnis/admin/showimg.aspx?ID=346"));
        }

        private void Btn_GuideForEarthquakeRetrofitting_Clicked(object sender, EventArgs e)
        {
            Launcher.OpenAsync(new Uri("https://hpsdma.nic.in//admnis/admin/showimg.aspx?ID=347"));
        }

        private void Btn_RetrofittingGuidelines_Clicked(object sender, EventArgs e)
        {
            Launcher.OpenAsync(new Uri("https://hpsdma.nic.in//admnis/admin/showimg.aspx?ID=348"));
        }

        private void Btn_SimplifiedGuidelineZoneV_Clicked(object sender, EventArgs e)
        {
            Launcher.OpenAsync(new Uri("https://hpsdma.nic.in//admnis/admin/showimg.aspx?ID=349"));
        }

        private void Btn_EvolvingStrategyPanchayatlevel_Clicked(object sender, EventArgs e)
        {
            Launcher.OpenAsync(new Uri("https://hpsdma.nic.in//admnis/admin/showimg.aspx?ID=350"));
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