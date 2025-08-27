
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Controls.Xaml;

namespace ResillentConstruction
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoadWebViewPage : ContentPage
    {
        public LoadWebViewPage(string url)
        {
            InitializeComponent();
            lbl_navigation_header.Text = App.LableText("lbl_navigation_header");
           // lbl_navigation_header.Text = App.AppName;
            lbl_heading.Text = App.LableText("PrivacyPolicy");
            Loading_activity.IsVisible = true;
            webview_loaddata.Source = url;

            Loading_activity.IsVisible = false;
           

        }

        private void ToolbarItem_Clicked(object sender, EventArgs e)
        {
            var window = Application.Current?.Windows?.FirstOrDefault();
            if (window != null)
            {
                window.Page = new NavigationPage(new DashboardPage());
            }
        }
    }
}