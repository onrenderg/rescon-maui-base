using ResillentConstruction.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Maui;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Controls.Xaml;

namespace ResillentConstruction.submenus
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PlanningGuidelinesSubMenuPage : ContentPage
    {
        public Label[] Footer_Labels;
        public string[] Footer_Image_Source;
        public Image[] Footer_Images;
        SaveUserPreferencesDatabase saveUserPreferencesDatabase = new SaveUserPreferencesDatabase();
        List<SaveUserPreferences> saveUserPreferenceslist;
        string userzone, districtname;
        string htmlstartpath ;
        string htmlendpath = $"\">\n</head>\n</html>";
        string pagettitle;
        public PlanningGuidelinesSubMenuPage(string _pagenm)
        {
            InitializeComponent();
            pagettitle = _pagenm;
            saveUserPreferenceslist = saveUserPreferencesDatabase.GetSaveUserPreferences("Select * from SaveUserPreferences").ToList();

            string language = Preferences.Get("lan", "EN-IN");
            if (language.Equals("EN-IN"))
            {

                htmlstartpath = $"<html>\n<head>\n<meta http-equiv=\"Refresh\" content=\"0;url=2024/English/HTMLs/Zone/";
                districtname = saveUserPreferenceslist.ElementAt(0).DistrictName.ToString();

            }
            else
            {
                htmlstartpath = $"<html>\n<head>\n<meta http-equiv=\"Refresh\" content=\"0;url=2024/Hindi/HTMLs/Zone/";
                districtname = saveUserPreferenceslist.ElementAt(0).DistrictNamelocal.ToString();


            }
        


            userzone = saveUserPreferenceslist.ElementAt(0).zonename.ToString();
            Footer_Labels = new Label[3] { Tab_Home_Label, Tab_Download_Label, Tab_Settings_Label };
            Footer_Images = new Image[3] { Tab_Home_Image, Tab_Download_Image, Tab_Settings_Image };
            //Footer_Image_Source = new string[3] { "ic_stock.png", "ic_add.png", "ic_more.png" };
            Footer_Image_Source = new string[3] { "ic_home.png", "ic_download.png", "ic_more.png" };
            lbl_header_submenu.Text = App.LableText("selectsubcatgeory") + "\n" + _pagenm;
        }


        private void Btn_FormandOrientation_Clicked(object sender, EventArgs e)
        {
            //Navigation.PushAsync(new ViewWebHtml(pagettitle, "", htmlstartpath + $"{userzone}/SitePreparation.html" + htmlendpath) { Title = "Site Preparation" });
            Navigation.PushAsync(new ViewWebHtml(pagettitle, "", htmlstartpath + $"{userzone}/FormandOrientation.html" + htmlendpath) { Title = App.LableText("FormandOrientation") });

        }

        private void Btn_InternalPlanning_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new ViewWebHtml(pagettitle, "", htmlstartpath + $"{userzone}/InternalPlanning.html" + htmlendpath) { Title = App.LableText("InternalPlanning") });
        }

        private void Btn_requirementsofthebuilding_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new ViewWebHtml(pagettitle, "", htmlstartpath + $"{userzone}/Generalrequirementsofthebuilding.html" + htmlendpath) { Title = App.LableText("requirementsofthebuilding") });
        }

        private void Btn_DoorsandWindows_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new ViewWebHtml(pagettitle, "", htmlstartpath + $"{userzone}/DoorsandWindows.html" + htmlendpath) { Title = App.LableText("DoorsandWindows") });
        }
        private void Btn_PracticesForConstruction_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new ViewWebHtml(pagettitle, "", htmlstartpath + $"{userzone}/CommonPraticesforConstruction.html" + htmlendpath) { Title = App.LableText("PracticesForConstruction") });
        }

        private void Btn_BuildingLayout_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new ViewWebHtml(pagettitle, "", htmlstartpath + $"{userzone}/Buldinglayout.html" + htmlendpath) { Title = App.LableText("BuildingLayout") });
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            lbl_navigation_header.Text = App.LableText("lbl_navigation_header");
            Tab_Home_Label.Text = App.LableText("Home");
            Tab_Download_Label.Text = App.LableText("Download");
            Tab_Settings_Label.Text = App.LableText("More");
            Footer_Image_Source = new string[3] { "ic_home.png", "ic_downloadwhite.png", "ic_morewhite.png" };
            Footer_Images[Preferences.Get("Active", 0)].Source = Footer_Image_Source[Preferences.Get("Active", 0)];
            Footer_Labels[Preferences.Get("Active", 0)].TextColor = Color.FromArgb("#FF0F0F0F");

            // lbl_user_header1.Text = App.LableText("PlanningGuidelines");
            lbl_Topheading.Text = saveUserPreferenceslist.ElementAt(0).Name + " (" + districtname + ", " + App.LableText("yourzone") + " - " + saveUserPreferenceslist.ElementAt(0).zonename + ")";

            Btn_FormandOrientation.Text = App.LableText("FormandOrientation");
            Btn_InternalPlanning.Text = App.LableText("InternalPlanning");
            Btn_requirementsofthebuilding.Text = App.LableText("requirementsofthebuilding");
            Btn_DoorsandWindows.Text = App.LableText("DoorsandWindows");
            Btn_PracticesForConstruction.Text = App.LableText("PracticesForConstruction");
            Btn_BuildingLayout.Text = App.LableText("BuildingLayout");
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