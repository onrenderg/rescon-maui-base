using System;
using Microsoft.Maui;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Controls.Xaml;

namespace ResillentConstruction.Engineer
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EngineerDashboardPage : ContentPage
    {
        public Label[] Footer_Labels;
        public string[] Footer_Image_Source;
        public Image[] Footer_Images;       
       
        string htmlstartpath;
        string htmlendpath = $"\">\n</head>\n</html>";
        public EngineerDashboardPage()
        {
            InitializeComponent();
          
            string language = Preferences.Get("lan", "EN-IN");
            if (language.Equals("EN-IN"))
            {
                htmlstartpath = $"<html>\n<head>\n<meta http-equiv=\"Refresh\" content=\"0;url=Engineer/2024/English/HTMLs/";
            }
            else
            {
                htmlstartpath = $"<html>\n<head>\n<meta http-equiv=\"Refresh\" content=\"0;url=Engineer/2024/Hindi/HTMLs/";
            }

            Footer_Labels = new Label[3] { Tab_Home_Label, Tab_Download_Label, Tab_Settings_Label };
            Footer_Images = new Image[3] { Tab_Home_Image, Tab_Download_Image, Tab_Settings_Image };
            Footer_Image_Source = new string[3] { "ic_home.png", "ic_download.png", "ic_more.png" };

            lbl_navigation_header.Text = App.LableText("lbl_navigation_header");
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            Tab_Home_Label.Text = App.LableText("Home");
            Tab_Download_Label.Text = App.LableText("Download");
            Tab_Settings_Label.Text = App.LableText("More");

            Preferences.Set("Active", 0);
            Footer_Image_Source = new string[3] { "ic_home.png", "ic_downloadwhite.png", "ic_morewhite.png" };
            Footer_Images[Preferences.Get("Active", 0)].Source = Footer_Image_Source[Preferences.Get("Active", 0)];
            Footer_Labels[Preferences.Get("Active", 0)].TextColor = Color.FromHex("#0f0f0f");

            lbl_header_menu.Text = App.LableText("SelectContructionCategory");
            Btn_ConfinedMasonry.Text = App.LableText("ConfinedMasonry");
            Btn_MaterialSpecificationsinConfinedMasonry.Text = App.LableText("MaterialSpecificationsinConfinedMasonry");
            Btn_EarthquakePerformanceofConfinedMasonryStructures.Text = App.LableText("EarthquakePerformanceofConfinedMasonryStructures");
            Btn_ImprovisedplansforCommunityBuildings.Text = App.LableText("ImprovisedplansforCommunityBuildings");
            Btn_StepsToConfinedMasonry.Text = App.LableText("StepsToConfinedMasonry");
            Btn_SiteSelection.Text = App.LableText("SiteSelection");
            Btn_SitePreparation.Text = App.LableText("SitePreparation");


            Btn_Quality.Text = App.LableText("Quality");
            Btn_Dos.Text = App.LableText("Dos");
            Btn_Annexure.Text = App.LableText("Annexure");

        }


        private void Btn_ConfinedMasonry_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new ViewWebHtmlEngineer(App.LableText("ConfinedMasonry"), "", htmlstartpath + $"ConfinedMasonry.html" + htmlendpath) { Title = App.LableText("ConfinedMasonry") });
        }
        
        private void Btn_MaterialSpecificationsinConfinedMasonry_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new ViewWebHtmlEngineer(App.LableText("MaterialSpecificationsinConfinedMasonry"), "", htmlstartpath + $"MaterialSpecificationsinConfinedMasonry.html" + htmlendpath) { Title = App.LableText("MaterialSpecificationsinConfinedMasonry") });
        }  
        
        private void Btn_EarthquakePerformanceofConfinedMasonryStructures_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new ViewWebHtmlEngineer(App.LableText("EarthquakePerformanceofConfinedMasonryStructures"), "", htmlstartpath + $"EarthquakePerformanceofConfinedMasonryStructures.html" + htmlendpath) { Title = App.LableText("EarthquakePerformanceofConfinedMasonryStructures") });
        } 
        
        
        private void Btn_ImprovisedplansforCommunityBuildings_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new ViewWebHtmlEngineer(App.LableText("ImprovisedplansforCommunityBuildings"), "", htmlstartpath + $"ImprovisedplansforCommunityBuildings.html" + htmlendpath) { Title = App.LableText("ImprovisedplansforCommunityBuildings") });
        }  
        private void Btn_StepsToConfinedMasonry_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new ViewWebHtmlEngineer(App.LableText("StepsToConfinedMasonry"), "", htmlstartpath + $"StepsToConfinedMasonry.html" + htmlendpath) { Title = App.LableText("StepsToConfinedMasonry") });
        } 
        
        private void Btn_SiteSelection_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new ViewWebHtmlEngineer(App.LableText("SiteSelection"), "", htmlstartpath + $"SiteSelection.html" + htmlendpath) { Title = App.LableText("SiteSelection") });
        }  
        
        private void Btn_SitePreparation_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new ViewWebHtmlEngineer(App.LableText("SitePreparation"), "", htmlstartpath + $"SitePreparation.html" + htmlendpath) { Title = App.LableText("SitePreparation") });
        }
             
        private void Btn_Quality_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new ViewWebHtmlEngineer(App.LableText("Quality"), "", htmlstartpath + $"Quality.html" + htmlendpath) { Title = App.LableText("Quality") });
        } 
        
        private void Btn_Dos_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new ViewWebHtmlEngineer(App.LableText("Dos"), "", htmlstartpath + $"Dos.html" + htmlendpath) { Title = App.LableText("Dos") });
        }
         private void Btn_Annexure_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new ViewWebHtmlEngineer(App.LableText("Annexure"), "", htmlstartpath + $"Annexure.html" + htmlendpath) { Title = App.LableText("Annexure") });
        }

        private void img_language_Clicked(object sender, EventArgs e)
        {
            if (Preferences.Get("lan", "EN-IN") == "EN-IN")
            {
                Preferences.Set("lan", "HI-IN");
            }
            else
            {
                Preferences.Set("lan", "EN-IN");
            }
            Application.Current.MainPage = new NavigationPage(new EngineerDashboardPage());
        }

        private void ToolbarItem_Clicked(object sender, EventArgs e)
        {
            Application.Current.MainPage = new NavigationPage(new MainPage());
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