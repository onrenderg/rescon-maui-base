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
	public partial class MaterialsSubMenuPage : ContentPage
	{
        public Label[] Footer_Labels;
        public string[] Footer_Image_Source;
        public Image[] Footer_Images;
        SaveUserPreferencesDatabase saveUserPreferencesDatabase = new SaveUserPreferencesDatabase();
        List<SaveUserPreferences> saveUserPreferenceslist;
        string userzone, districtname, pagettitle;
        string htmlstartpath;
        string htmlendpath = $"\">\n</head>\n</html>";
        public MaterialsSubMenuPage(string _pagenm)
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

            Footer_Labels = new Label[3] { Tab_Home_Label, Tab_Download_Label, Tab_Settings_Label };
            Footer_Images = new Image[3] { Tab_Home_Image, Tab_Download_Image, Tab_Settings_Image };
            //Footer_Image_Source = new string[3] { "ic_stock.png", "ic_add.png", "ic_more.png" };
            Footer_Image_Source = new string[3] { "ic_home.png", "ic_download.png", "ic_more.png" };          

            userzone = saveUserPreferenceslist.ElementAt(0).zonename.ToString();
            lbl_header_submenu.Text = App.LableText("selectsubcatgeory") + "\n" + _pagenm;
        }


        private void Btn_Cement_Clicked(object sender, EventArgs e)
        {
            //Navigation.PushAsync(new ViewWebHtml(pagettitle, "", htmlstartpath + $"{userzone}/Cement.html" + htmlendpath) { Title = "Cement" });
            Navigation.PushAsync(new ViewWebHtml(pagettitle, "", htmlstartpath + $"{userzone}/Cement.html" + htmlendpath) { Title = App.LableText("Cement") });
        }

        private void Btn_Sand_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new ViewWebHtml(pagettitle, "", htmlstartpath + $"{userzone}/Sand.html" + htmlendpath) { Title = App.LableText("Sand") });
        }

        private void Btn_StoneAggregate_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new ViewWebHtml(pagettitle, "", htmlstartpath + $"{userzone}/StoneAggregate.html" + htmlendpath) { Title = App.LableText("StoneAggregate") });
        }

        private void Btn_CementConcreteandMortar_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new ViewWebHtml(pagettitle, "", htmlstartpath + $"{userzone}/CementConcreteandMortar.html" + htmlendpath) { Title = App.LableText("CementConcreteandMortar") });
        }


        private void Btn_Steel_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new ViewWebHtml(pagettitle, "", htmlstartpath + $"{userzone}/steel.html" + htmlendpath) { Title = App.LableText("steel") });            
        }

        private void Btn_Bricks_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new ViewWebHtml(pagettitle, "", htmlstartpath + $"{userzone}/Bricks.html" + htmlendpath) { Title = App.LableText("Bricks") });
        }

        private void Btn_WoodenWork_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new ViewWebHtml(pagettitle, "", htmlstartpath + $"{userzone}/WoodenWork.html" + htmlendpath) { Title = App.LableText("WoodenWork") });
        }

        private void Btn_RoofMaterial_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new ViewWebHtml(pagettitle, "", htmlstartpath + $"{userzone}/Roofmaterial.html" + htmlendpath) { Title = App.LableText("RoofMaterial") });
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
            Footer_Labels[Preferences.Get("Active", 0)].TextColor = Color.FromHex("#0f0f0f");

            // lbl_user_header1.Text = App.LableText("GeneralInformationaboutMaterials");
            lbl_Topheading.Text = saveUserPreferenceslist.ElementAt(0).Name + " (" + districtname + ", " + App.LableText("yourzone") + " - " + saveUserPreferenceslist.ElementAt(0).zonename + ")";

            Btn_Cement.Text = App.LableText("Cement");
            Btn_Sand.Text = App.LableText("Sand");
            Btn_StoneAggregate.Text = App.LableText("StoneAggregate");
            Btn_CementConcreteandMortar.Text = App.LableText("CementConcreteandMortar");
            Btn_Steel.Text = App.LableText("Steel");
            Btn_Bricks.Text = App.LableText("Bricks");
            Btn_WoodenWork.Text = App.LableText("WoodenWork");
            Btn_RoofMaterial.Text = App.LableText("RoofMaterial");
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