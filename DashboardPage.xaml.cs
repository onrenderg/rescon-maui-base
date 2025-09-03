using ResillentConstruction.Models;
using ResillentConstruction.submenus;
using ResillentConstruction.webapi;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Maui;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Controls.Xaml;

namespace ResillentConstruction
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DashboardPage : ContentPage
    {
        public Label[] Footer_Labels;
        public string[] Footer_Image_Source;
        public Image[] Footer_Images;

        SaveUserPreferencesDatabase saveUserPreferencesDatabase = new SaveUserPreferencesDatabase();
        List<SaveUserPreferences> saveUserPreferenceslist;
        string userzone;
        string htmlstartpath ;
        string htmlendpath = $"\">\n</head>\n</html>";
        string districtname;

        public DashboardPage()
        {
            InitializeComponent();
            saveUserPreferenceslist = saveUserPreferencesDatabase.GetSaveUserPreferences("Select * from SaveUserPreferences").ToList();
            userzone = saveUserPreferenceslist.ElementAt(0).zonename?.ToString() ?? string.Empty;
            string language = Preferences.Get("lan",  "EN-IN");

            if (language.Equals("EN-IN"))
            {
                htmlstartpath = $"<html>\n<head>\n<meta http-equiv=\"Refresh\" content=\"0;url=2024/English/HTMLs/Zone/";
                districtname = saveUserPreferenceslist.ElementAt(0).DistrictName?.ToString() ?? string.Empty;
            }
            else
            {
                htmlstartpath = $"<html>\n<head>\n<meta http-equiv=\"Refresh\" content=\"0;url=2024/Hindi/HTMLs/Zone/";
                districtname = saveUserPreferenceslist.ElementAt(0).DistrictNamelocal?.ToString() ?? string.Empty;

            }

            lbl_user_header1.Text = App.LableText("welcome") + " " + (saveUserPreferenceslist.ElementAt(0).Name?.ToString() ?? string.Empty) + "\n" +
             App.LableText("yourdistrict") + " : " + districtname + "\n" + App.LableText("yourzone") + " : " + userzone;


            Footer_Labels = new Label[3] { Tab_Home_Label, Tab_Download_Label, Tab_Settings_Label };
            Footer_Images = new Image[3] { Tab_Home_Image, Tab_Download_Image, Tab_Settings_Image };
            Footer_Image_Source = new string[3] { "ic_home.png", "ic_download.png", "ic_more.png" };         
          
            lbl_navigation_header.Text = App.LableText("lbl_navigation_header");                  

           /* Device.BeginInvokeOnMainThread( async() =>
            {
                var service = new HitServices();
                int response_EngineerMaster_Get = await service.EngineerMaster_Get();
            });*/
        }
        private void imgbtn_profile_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new ProfilePage());
        }

        private void Btn_SitePreparationandSelection_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new SitePreparationandSelectionSubMenu(App.LableText("SitePreparationandSelection")));
        }

        private void Btn_SitePreparation_Clicked(object sender, EventArgs e)
        {
            // Navigation.PushAsync(new ViewWebHtml("", "", $"<html>\n<head>\n<meta http-equiv=\"Refresh\" content=\"0;url=2024/English/HTMLs/agriculture_and_horticulture.html\">\n</head>\n</html>") { Title = "agriculture_and_horticulture" });
            Navigation.PushAsync(new ViewWebHtml(App.LableText("SitePreparation"), "", htmlstartpath + $"{userzone}/SitePreparation.html" + htmlendpath) { Title = App.LableText("SitePreparation") });
        }

        private void Btn_PlanningGuidelines_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new PlanningGuidelinesSubMenuPage(App.LableText("PlanningGuidelines")));
        }

        private void Btn_ExcavationandConstructionofFoundation_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new ViewWebHtml(App.LableText("ExcavationandConstructionofFoundation"), "", htmlstartpath + $"{userzone}/ExcavationofFoundationTrenches.html" + htmlendpath) { Title = App.LableText("ExcavationandConstructionofFoundation") });
        }

        private void Btn_ConstructionofPlinthBand_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new ViewWebHtml(App.LableText("ConstructionofPlinthBand"), "", htmlstartpath + $"{userzone}/ConstructionofPlinthBand.html" + htmlendpath) { Title = App.LableText("ConstructionofPlinthBand") });
        }

        private void Btn_MasonryofSuperStructure_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new ViewWebHtml(App.LableText("MasonryofSuperStructure"), "", htmlstartpath + $"{userzone}/MasonryofSuperStructure.html" + htmlendpath) { Title = App.LableText("MasonryofSuperStructure") });
        }
        private void Btn_FixingofDoorsandWindowsFrames_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new ViewWebHtml(App.LableText("FixingofDoorsandWindowsFrames"), "", htmlstartpath + $"{userzone}/FixingofDoorsandWindowsFrames.html" + htmlendpath) { Title = App.LableText("FixingofDoorsandWindowsFrames") });
        }

        private void Btn_LintelBandandSunshades_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new ViewWebHtml(App.LableText("LintelBandandSunshades"), "", htmlstartpath + $"{userzone}/LintelBandandSunshades.html" + htmlendpath) { Title = App.LableText("LintelBandandSunshades") });
        }

        private void Btn_RoofConstruction_Clicked(object sender, EventArgs e)
        {
            if (userzone.Equals("A"))
            {
                Navigation.PushAsync(new RoofConstructionSubMenuPage(App.LableText("RoofConstruction")));
            }
            else
            {
                Navigation.PushAsync(new ViewWebHtml(App.LableText("RoofConstruction"), "", htmlstartpath + $"{userzone}/RoofConstruction.html" + htmlendpath) { Title = App.LableText("RoofConstruction") });
            }
        }

        private void Btn_GeneralInformationaboutMaterials_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new MaterialsSubMenuPage(App.LableText("GeneralInformationaboutMaterials")));
        }

        private void Btn_AverageConstructionCost_Clicked(object sender, EventArgs e)
        {
            // Navigation.PushAsync(new MaterialsSubMenuPage());
            Navigation.PushAsync(new ViewWebHtml(App.LableText("AverageConstructionCost"), "", htmlstartpath + $"{userzone}/AverageConstructionCost.html" + htmlendpath) { Title = App.LableText("AverageConstructionCost") });

        }  
        
        protected override  void OnAppearing()
        {
            base.OnAppearing();
            Tab_Home_Label.Text = App.LableText("Home");
            Tab_Download_Label.Text = App.LableText("Download");
            Tab_Settings_Label.Text = App.LableText("More");
            lbl_raisequery.Text = App.LableText("RaiseQuery");
            Preferences.Set("Active", 0);
            Footer_Image_Source = new string[3] { "ic_home.png", "ic_downloadwhite.png", "ic_morewhite.png" };
            Footer_Images[Preferences.Get("Active", 0)].Source = Footer_Image_Source[Preferences.Get("Active", 0)];
            Footer_Labels[Preferences.Get("Active", 0)].TextColor = Color.FromArgb("#FF0F0F0F");

            lbl_header_menu.Text = App.LableText("SelectContructionCategory");
            Btn_SitePreparationandSelection.Text = App.LableText("SitePreparationandSelection");
            Btn_SitePreparation.Text = App.LableText("SitePreparation");
            Btn_PlanningGuidelines.Text = App.LableText("PlanningGuidelines");
            Btn_ExcavationandConstructionofFoundation.Text = App.LableText("ExcavationandConstructionofFoundation");
            Btn_ConstructionofPlinthBand.Text = App.LableText("ConstructionofPlinthBand");
            Btn_MasonryofSuperStructure.Text = App.LableText("MasonryofSuperStructure");
            Btn_FixingofDoorsandWindowsFrames.Text = App.LableText("FixingofDoorsandWindowsFrames");
            Btn_LintelBandandSunshades.Text = App.LableText("LintelBandandSunshades");
            Btn_RoofConstruction.Text = App.LableText("RoofConstruction");
            Btn_GeneralInformationaboutMaterials.Text = App.LableText("GeneralInformationaboutMaterials");
            Btn_AverageConstructionCost.Text = App.LableText("AverageConstructionCost");
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
        private void img_raisequery_Clicked(object sender, EventArgs e)
        {
            //Navigation.PushAsync(new RaiseQueryPage());
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
            var window = Application.Current?.Windows?.FirstOrDefault();
            if (window != null)
            {
                window.Page = new NavigationPage(new DashboardPage());
            }
        }

        private void ToolbarItem_Clicked(object sender, EventArgs e)
        {
            var window2 = Application.Current?.Windows?.FirstOrDefault();
            if (window2 != null)
            {
                window2.Page = new NavigationPage(new MainPage());
            }
        }
    }
}