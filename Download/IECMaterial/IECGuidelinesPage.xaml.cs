using ResillentConstruction.IECMaterial.IECMaterialSubMenus;
using System;
using Microsoft.Maui;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Controls.Xaml;

namespace ResillentConstruction
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class IECGuidelinesPage : ContentPage
    {
        public Label[] Footer_Labels;
        public string[] Footer_Image_Source;
        public Image[] Footer_Images;

        public IECGuidelinesPage()
        {
            InitializeComponent();
            Footer_Labels = new Label[3] { Tab_Home_Label, Tab_Download_Label, Tab_Settings_Label };
            Footer_Images = new Image[3] { Tab_Home_Image, Tab_Download_Image, Tab_Settings_Image };
            Footer_Image_Source = new string[3] { "ic_home.png", "ic_download.png", "ic_more.png" };
           lbl_navigation_header.Text = App.LableText("lbl_navigation_header");

            lbl_Topheading.Text = App.LableText("IECMaterial");      
            Btn_SnowAvalanche.Text = App.LableText("SnowAvalanche");
            Btn_ColdWave.Text = App.LableText("ColdWave");
            Btn_Earthquake.Text = App.LableText("Earthquake");
            Btn_Fire.Text = App.LableText("Fire");
            Btn_Flood.Text = App.LableText("Flood");
            Btn_Lightining.Text = App.LableText("Lightining");
            Btn_RoadAccident.Text = App.LableText("RoadAccident");
            Btn_SamarthIEC.Text = App.LableText("SamarthIEC");
            Btn_LocustAttack.Text = App.LableText("LocustAttack");
            Btn_IEConSearchandRescueOperations.Text = App.LableText("IEConSearchandRescueOperations");
            Btn_AwarenessMaterial.Text = App.LableText("AwarenessMaterial");
        }

        private void Btn_SnowAvalanche_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new SnowAvalancheSubMenuPage(App.LableText("SnowAvalanche")));
        }
        private void Btn_ColdWave_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new ColdWaveSubMenuPage(App.LableText("ColdWave")));

        }
        private void Btn_Earthquake_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new EarthquakeSubMenuPage(App.LableText("Earthquake")));

        }
        private void Btn_Fire_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new FireSubMenuPage(App.LableText("Fire")));

        }
        private void Btn_Flood_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new FloodSubMenuPage(App.LableText("Flood")));

        }
        private void Btn_Lightining_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new LightiningSubMenuPage(App.LableText("Lightining")));

        }
        private void Btn_RoadAccident_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new RoadAccidentSubMenuPage(App.LableText("RoadAccident")));

        }
        private void Btn_SamarthIEC_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new SamarthIecSubMenuPage(App.LableText("SamarthIEC")));

        }
        private void Btn_LocustAttack_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new LocustAttackSubMenuPage(App.LableText("LocustAttack")));

        }
        private void Btn_IEConSearchandRescueOperations_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new IEConSearchandRescueOperationsSubMenuPage(App.LableText("IEConSearchandRescueOperations")));

        }
        private void Btn_AwarenessMaterial_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new AwarenessMaterialSubMenuPage(App.LableText("AwarenessMaterial")));

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