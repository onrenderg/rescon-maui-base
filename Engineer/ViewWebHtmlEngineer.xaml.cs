using ResillentConstruction.Engineer;
using Microsoft.Maui;
using Microsoft.Maui.Controls;
using System.IO;
using System;
using System.Threading.Tasks;

namespace ResillentConstruction
{
    public partial class ViewWebHtmlEngineer : ContentPage
    {
        string fileaddress; // Store the fileaddress parameter
        public interface IBaseUrl { string Get(); }
        public ViewWebHtmlEngineer(string mainpg_Name, string footerlbl, string fileaddress)
        {
            InitializeComponent();
            // Store the fileaddress parameter
            this.fileaddress = fileaddress;
              string language = Preferences.Get("lan", "EN-IN");
            lbl_navigation_header.Text = App.LableText("lbl_navigation_header");
            lbl_Topheading.Text = mainpg_Name;

            // Configure WebView for better rendering
            ConfigureWebView();

            // Check if the fileaddress contains HTML content or is a file path
            if (fileaddress.Contains("<html>") && fileaddress.Contains("meta http-equiv=\"Refresh\""))
            {
                // Extract the actual file path from the meta refresh
                var startIndex = fileaddress.IndexOf("content=\"0;url=") + 15;
                var endIndex = fileaddress.IndexOf("\"", startIndex);
                var filePath = fileaddress.Substring(startIndex, endIndex - startIndex);
                
                // Load the actual HTML file content
                LoadHtmlFile(filePath);
            }
            else
            {
                // Direct HTML content
                var htmlSource = new HtmlWebViewSource();
                htmlSource.Html = fileaddress;
                SetBaseUrl(htmlSource);
                browser.Source = htmlSource;
            }
        }

        private async void LoadHtmlFile(string filePath)
        {
            try
            {
                // For MAUI, we need to construct the proper logical name
                // The LogicalName in the project file removes the "Resources\Raw" prefix
                var logicalName = filePath.Replace("Resources\\Raw\\", "").Replace("Resources/Raw/", "");
                
                // Read the HTML file content
                var htmlContent = await FileSystem.OpenAppPackageFileAsync(logicalName);
                using var reader = new StreamReader(htmlContent);
                var html = await reader.ReadToEndAsync();
                
                // Try to embed images directly into the HTML
                html = await EmbedImagesInHtml(html, logicalName);
                
                // Create HtmlWebViewSource with proper base URL
                var htmlSource = new HtmlWebViewSource();
                htmlSource.Html = html;
                
                // Set the base URL to the directory containing the HTML file
                // This should make relative paths like "img/Figure3.1.1.JPG" work correctly
                var directoryPath = Path.GetDirectoryName(logicalName)?.Replace('\\', '/') ?? string.Empty;
                htmlSource.BaseUrl = $"ms-appx:///{directoryPath}/";
                
                System.Diagnostics.Debug.WriteLine($"Setting base URL to: ms-appx:///{directoryPath}/");
                System.Diagnostics.Debug.WriteLine($"HTML content length: {html.Length}");
                System.Diagnostics.Debug.WriteLine($"HTML contains img tag: {html.Contains("<img")}");
                
                browser.Source = htmlSource;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error loading HTML file: {ex.Message}");
                // Fallback: try to load as HTML content with proper base URL
                var htmlSource = new HtmlWebViewSource();
                htmlSource.Html = fileaddress;
                SetBaseUrl(htmlSource);
                browser.Source = htmlSource;
            }
        }

        private async Task<string> EmbedImagesInHtml(string html, string logicalName)
        {
            try
            {
                // Find all img tags in the HTML
                var imgPattern = @"<img[^>]+src=""([^""]+)""[^>]*>";
                var matches = System.Text.RegularExpressions.Regex.Matches(html, imgPattern);
                
                foreach (System.Text.RegularExpressions.Match match in matches)
                {
                    var imgSrc = match.Groups[1].Value;
                    var fullImgPath = Path.Combine(Path.GetDirectoryName(logicalName) ?? string.Empty, imgSrc).Replace('\\', '/');
                    
                    try
                    {
                        // Try to read the image file
                        var imageStream = await FileSystem.OpenAppPackageFileAsync(fullImgPath);
                        if (imageStream != null)
                        {
                            // Convert image to base64
                            using var memoryStream = new MemoryStream();
                            await imageStream.CopyToAsync(memoryStream);
                            var imageBytes = memoryStream.ToArray();
                            var base64String = Convert.ToBase64String(imageBytes);
                            
                            // Get the image MIME type based on file extension
                            var extension = Path.GetExtension(imgSrc).ToLower();
                            var mimeType = extension switch
                            {
                                ".jpg" or ".jpeg" => "image/jpeg",
                                ".png" => "image/png",
                                ".gif" => "image/gif",
                                _ => "image/jpeg"
                            };
                            
                            // Replace the img src with base64 data
                            var newImgSrc = $"data:{mimeType};base64,{base64String}";
                            html = html.Replace($"src=\"{imgSrc}\"", $"src=\"{newImgSrc}\"");
                            
                            System.Diagnostics.Debug.WriteLine($"Embedded image: {imgSrc} -> {fullImgPath}");
                        }
                    }
                    catch (Exception ex)
                    {
                        System.Diagnostics.Debug.WriteLine($"Failed to embed image {imgSrc}: {ex.Message}");
                    }
                }
                
                return html;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error embedding images: {ex.Message}");
                return html;
            }
        }

        private void ConfigureWebView()
        {
            // Configure WebView for better rendering and to fix zoom issues
            browser.Navigated += (sender, e) =>
            {
                // Inject CSS and JavaScript to fix zoom and layout issues
                var css = @"
                    <style>
                        html, body { 
                            margin: 0; 
                            padding: 10px; 
                            font-size: 16px; 
                            line-height: 1.4;
                            -webkit-text-size-adjust: 100%;
                            -ms-text-size-adjust: 100%;
                            width: 100%;
                            height: 100%;
                            overflow-x: hidden;
                        }
                        body { 
                            transform: none !important;
                            zoom: 1 !important;
                            -webkit-transform: none !important;
                            -moz-transform: none !important;
                        }
                        img { 
                            max-width: 100%; 
                            height: auto; 
                        }
                        .container { 
                            max-width: 100%; 
                            margin: 0 auto; 
                            padding: 0 10px;
                        }
                        * {
                            box-sizing: border-box;
                        }
                    </style>";
                
                // Inject the CSS and JavaScript into the loaded page
                browser.EvaluateJavaScriptAsync($@"
                    // Remove any existing zoom styles
                    var existingStyle = document.getElementById('maui-zoom-fix');
                    if (existingStyle) existingStyle.remove();
                    
                    // Add our CSS
                    var style = document.createElement('style');
                    style.id = 'maui-zoom-fix';
                    style.innerHTML = `{css}`;
                    document.head.appendChild(style);
                    
                    // Force viewport settings
                    var viewport = document.querySelector('meta[name=""viewport""]');
                    if (!viewport) {{
                        viewport = document.createElement('meta');
                        viewport.name = 'viewport';
                        document.head.appendChild(viewport);
                    }}
                    viewport.content = 'width=device-width, initial-scale=1.0, maximum-scale=1.0, user-scalable=no';
                    
                    // Reset any zoom transformations
                    document.body.style.transform = 'none';
                    document.body.style.zoom = '1';
                    document.documentElement.style.zoom = '1';
                ");
            };

            // Additional WebView configuration
            browser.Loaded += (sender, e) =>
            {
                // Ensure WebView is properly configured
                browser.Scale = 1;
                browser.ScaleX = 1;
                browser.ScaleY = 1;
            };
        }

        private void SetBaseUrl(HtmlWebViewSource htmlSource)
        {
            // Set appropriate base URL for different platforms
            if (DeviceInfo.Platform == DevicePlatform.Android)
            {
                htmlSource.BaseUrl = "file:///android_asset/";
            }
            else if (DeviceInfo.Platform == DevicePlatform.iOS)
            {
                htmlSource.BaseUrl = DependencyService.Get<IBaseUrl>().Get();
            }
            else if (DeviceInfo.Platform == DevicePlatform.WinUI)
            {
                // For Windows, use the app package directory
                htmlSource.BaseUrl = "ms-appx:///";
            }
            else
            {
                // Default fallback
                htmlSource.BaseUrl = "ms-appx:///";
            }
        }

        void SettingBtn(object sender, System.EventArgs e)
        {
           /* if (Footer.Text.Contains("National"))
            {
                Navigation.PushAsync(new NavigationPage(new SettingsPage(Footer.Text)) { Title = "Preferences" };
            } else
            {
                Navigation.PushAsync(new NavigationPage(new SettingsPage(Footer.Text)) { Title = "पसंद" };
            }*/
        }
        void HomeBtn(object sender, System.EventArgs e)
        {
            var window = Application.Current?.Windows?.FirstOrDefault();
            if (window != null)
            {
                window.Page = new EngineerDashboardPage();
            }
        }
    }
}
