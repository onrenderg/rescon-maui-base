using ResillentConstruction.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Maui;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Controls.Xaml;

namespace ResillentConstruction.submenus
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class RoofConstructionSubMenuPage : ContentPage
	{
        public Label[] Footer_Labels;
        public string[] Footer_Image_Source;
        public Image[] Footer_Images;
        SaveUserPreferencesDatabase saveUserPreferencesDatabase = new SaveUserPreferencesDatabase();
        List<SaveUserPreferences> saveUserPreferenceslist;
        string userzone, districtname, pagettitle;
        string htmlstartpath;
        string htmlendpath = $"\">\n</head>\n</html>";
        public RoofConstructionSubMenuPage (string _pagenm)
		{
			InitializeComponent ();
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

            Footer_Labels = new Label[3] { Tab_Home_Label, Tab_Download_Label, Tab_Settings_Label };
            Footer_Images = new Image[3] { Tab_Home_Image, Tab_Download_Image, Tab_Settings_Image };
            //Footer_Image_Source = new string[3] { "ic_stock.png", "ic_add.png", "ic_more.png" };
            Footer_Image_Source = new string[3] { "ic_home.png", "ic_download.png", "ic_more.png" };

         
            userzone = saveUserPreferenceslist.ElementAt(0).zonename.ToString();
            lbl_header_submenu.Text = App.LableText("selectsubcatgeory") + "\n" + _pagenm;

        }


        private void Btn_GeneralInstructions_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new ViewWebHtml(pagettitle, "", htmlstartpath + $"{userzone}/GeneralInstructions.html" + htmlendpath) { Title = App.LableText("GeneralInstructions") });
        }

        private void Btn_RoofSlab_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new ViewWebHtml(pagettitle, "", htmlstartpath + $"{userzone}/RoofSlab.html" + htmlendpath) { Title = App.LableText("RoofSlab") });

        }

        private void Btn_WoodenRoof_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new ViewWebHtml(pagettitle, "", htmlstartpath + $"{userzone}/Woodenroof.html" + htmlendpath) { Title = App.LableText("WoodenRoof") });
        }

        private void Btn_RoofInsulation_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new ViewWebHtml(pagettitle, "", htmlstartpath + $"{userzone}/RoofInsulation.html" + htmlendpath) { Title = App.LableText("RoofInsulation") });
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

            // lbl_user_header1.Text = App.LableText("RoofConstruction");
            lbl_Topheading.Text = saveUserPreferenceslist.ElementAt(0).Name + " (" + districtname + ", " + App.LableText("yourzone") + " - " + saveUserPreferenceslist.ElementAt(0).zonename + ")";

            Btn_GeneralInstructions.Text = App.LableText("GeneralInstructions");
            Btn_RoofSlab.Text = App.LableText("RoofSlab");
            Btn_WoodenRoof.Text = App.LableText("WoodenRoof");
            Btn_RoofInsulation.Text = App.LableText("RoofInsulation");
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