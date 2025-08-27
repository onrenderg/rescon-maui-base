using System;

using Microsoft.Maui;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Controls.Xaml;

namespace ResillentConstruction.IECMaterial.IECMaterialSubMenus.AwarenessMaterialSubSubMenu
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AwarenessMaterialSchoolPage : ContentPage
    {
        public Label[] Footer_Labels;
        public string[] Footer_Image_Source;
        public Image[] Footer_Images;
        public AwarenessMaterialSchoolPage(string menuname)
        {
            InitializeComponent();
            Footer_Labels = new Label[3] { Tab_Home_Label, Tab_Download_Label, Tab_Settings_Label };
            Footer_Images = new Image[3] { Tab_Home_Image, Tab_Download_Image, Tab_Settings_Image };
            Footer_Image_Source = new string[3] { "ic_home.png", "ic_download.png", "ic_more.png" };

            lbl_navigation_header.Text = App.LableText("lbl_navigation_header");
            lbl_Topheading.Text = menuname;

            Btn_SchoolFireSafety.Text = App.LableText("SchoolFireSafety");
            Btn_SchoolSafety.Text = App.LableText("SchoolSafety");
            Btn_StudentsRoadSafety.Text = App.LableText("StudentsRoadSafety");
          

        }
        private void Btn_SchoolFireSafety_Clicked(object sender, EventArgs e)
        {
            Launcher.OpenAsync(new Uri("https://hpsdma.nic.in/WriteReadData/LINKS/School%20Fire%20safety981748ab-74f7-4d6c-9b04-bd0b76149cf6.pdf"));
        }

        private void Btn_SchoolSafety_Clicked(object sender, EventArgs e)
        {
            Launcher.OpenAsync(new Uri("https://hpsdma.nic.in/WriteReadData/LINKS/School%20Safety%20Poster834cbd5a-f752-4870-86c3-1cf2c1b17393.pdf"));
        }

        private void Btn_StudentsRoadSafety_Clicked(object sender, EventArgs e)
        {
            Launcher.OpenAsync(new Uri("https://hpsdma.nic.in/WriteReadData/LINKS/School%20Students%20Road%20Safety%20Posterc50d7a33-04c9-4e62-acff-edd71defc37b.pdf"));
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