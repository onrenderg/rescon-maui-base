using ResillentConstruction.IECMaterial.IECMaterialSubMenus.AwarenessMaterialSubSubMenu;
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
    public partial class AwarenessMaterialSubMenuPage : ContentPage
    {
        public Label[] Footer_Labels;
        public string[] Footer_Image_Source;
        public Image[] Footer_Images;
        public AwarenessMaterialSubMenuPage(string menuname)
        {
            InitializeComponent();
            Footer_Labels = new Label[3] { Tab_Home_Label, Tab_Download_Label, Tab_Settings_Label };
            Footer_Images = new Image[3] { Tab_Home_Image, Tab_Download_Image, Tab_Settings_Image };
            Footer_Image_Source = new string[3] { "ic_home.png", "ic_download.png", "ic_more.png" };

            lbl_navigation_header.Text = App.LableText("lbl_navigation_header");
            lbl_Topheading.Text = menuname;

            Btn_School.Text = App.LableText("School");
            Btn_UrbanRiskReduction.Text = App.LableText("UrbanRiskReduction");
            Btn_HouseSafety.Text = App.LableText("HouseSafety");
            Btn_LandslideSafety.Text = App.LableText("LandslideSafety");           
       
        }
        private void Btn_School_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new AwarenessMaterialSchoolPage(App.LableText("School")));
        } 
        
        
        private void Btn_UrbanRiskReduction_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new AwarenessMaterialUrbanRiskReductionPage(App.LableText("UrbanRiskReduction")));
        }

        private void Btn_HouseSafety_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new AwarenessMaterialHouseSafetyPage(App.LableText("HouseSafety")));
        }

       
        private void Btn_LandslideSafety_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new AwarenessMaterialLandslideSafetyPage(App.LableText("LandslideSafety")));

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