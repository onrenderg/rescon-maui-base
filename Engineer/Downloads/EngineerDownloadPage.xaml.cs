using ResillentConstruction.Engineer;
using ResillentConstruction.webapi;
using System;
using Microsoft.Maui;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Controls.Xaml;

namespace ResillentConstruction
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EngineerDownloadPage : ContentPage
    {
        public Label[] Footer_Labels;
        public string[] Footer_Image_Source;
        public Image[] Footer_Images;
  


        public EngineerDownloadPage()
        {
            InitializeComponent();
            
            string language = Preferences.Get("lan", "EN-IN");
           
            Footer_Labels = new Label[3] { Tab_Home_Label, Tab_Download_Label, Tab_Settings_Label };
            Footer_Images = new Image[3] { Tab_Home_Image, Tab_Download_Image, Tab_Settings_Image };            
            Footer_Image_Source = new string[3] { "ic_home.png", "ic_download.png", "ic_more.png" };          

            lbl_navigation_header.Text = App.LableText("lbl_navigation_header");
            lbl_user_header1.Text = App.LableText("guidelines");
            Btn_guideliensengineer.Text = App.LableText("engineerguidelines");
          
          
        }

        private void ToolbarItem_Clicked(object sender, EventArgs e)
        {
            //await DownloadPDF("", "");
        }
     
        private async void Btn_guideliensengineer_Clicked(object sender, EventArgs e)
        {
            var service = new HitServices();
            string url = service.kawach2url;
            await Launcher.OpenAsync(url);
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
            Application.Current.MainPage = new NavigationPage(new EngineerDashboardPage());
        }
        private void Tab_Download_Tapped(object sender, EventArgs e)
        {
            Preferences.Set("Active", 1);
            Application.Current.MainPage = new NavigationPage(new EngineerDownloadPage());
        }
        private void Tab_Settings_Tapped(object sender, EventArgs e)
        {
            Preferences.Set("Active", 2);
            Application.Current.MainPage = new NavigationPage(new MorePage());
        }
    }
}