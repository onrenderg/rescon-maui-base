using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Maui;
using Microsoft.Maui.Controls;
using ResillentConstruction;
using System.Linq;
using System.Web;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using ResillentConstruction.Models;
//using static Android.Content.ClipData;

namespace ResillentConstruction.webapi
{
    public class HitServices
    {
        public string AppName = "Resilient Construction HP";
        public string NoInternet_ = "No Internet Connection Found.";
        string BasicAuth = $"{HttpUtility.UrlEncode(AESCryptography.EncryptAES("ResilientConstruction"))}:{HttpUtility.UrlEncode(AESCryptography.EncryptAES("9kO9E3CP7P8F0823"))}";


// mgogo
        // public string PrivacyPolicyUrl = "http://10.146.2.8/ResilientConstructionAPI/PrivacyPolicy.aspx";// for login and fetching departments       
       public string PrivacyPolicyUrl = "https://mobileappshp.nic.in/assets/pdf/mobile-app-privacy-policy/HimKavach.html"

       
       
       // public string baseurl = "http://10.146.2.8/ResilientConstructionAPI/";       
        public string currentLocationUrl = "https://mobileappshp.nic.in/shereshthhimachal/Initilisation.svc/location?";

        public string zoneAurl = "https://hpsdma.nic.in//admnis/admin/showimg.aspx?ID=3667";
        public string zoneBurl = "https://hpsdma.nic.in//admnis/admin/showimg.aspx?ID=3668";
        public string zoneCurl = "https://hpsdma.nic.in//admnis/admin/showimg.aspx?ID=3670";  
        public string kawach2url = "https://hpsdma.nic.in//admnis/admin/showimg.aspx?ID=3671";  
        public static string GetAppVersionDetailsUrl = "https://mobileappshp.nic.in/MyDiary/MobileAppVersions.svc/GetAppVersion?";//App version check

        public string guidebookpriurl = "https://hpsdma.nic.in//admnis/admin/showimg.aspx?ID=3671";

// mgogo
        // public string Constructionpriurl = "https://drive.google.com/file/d/1HC0QfxRdXhdIksQ_Rd86fQb4W5SGEJXB/view?usp=sharing";
        public string Constructionpriurl = "https://youtu.be/kEJCc2OMZvQ"
        DistrictMasterDatabase districtMasterDatabase = new DistrictMasterDatabase();
        List<DistrictMaster> districtMasters = new List<DistrictMaster>();
        LanguageMasterDatabase languageMasterDatabase = new LanguageMasterDatabase();   
        SaveUserPreferencesDatabase saveUserPreferencesDatabase = new SaveUserPreferencesDatabase();


        public async void update()
        {
            try
            {
                var current = Connectivity.NetworkAccess;
                if (current == NetworkAccess.Internet)
                {
                    double installedVersionNumber = double.Parse(VersionTracking.CurrentVersion);


                    var client = new HttpClient();
                    string CurrentPlateform = "A";

                    if (DeviceInfo.Platform == DevicePlatform.iOS)
                    {
                        CurrentPlateform = "I";
                    }
                    if (DeviceInfo.Platform == DevicePlatform.macOS)
                    {
                        CurrentPlateform = "I";
                    }
                  
                    else
                    {
                        CurrentPlateform = "A";
                    }

                    var responce = await client.GetAsync(GetAppVersionDetailsUrl + $"&Platform={CurrentPlateform}&packageid={AppInfo.PackageName}");
                    var MyJson = await responce.Content.ReadAsStringAsync();

                    JObject parsed = JObject.Parse(MyJson);
                    var ServiceStatusCode = parsed?["message"]?["status"]?.ToString();
                    if (ServiceStatusCode == "200")
                    {
                        if (MyJson.Contains("Mandatory"))
                        {
                            double latestVersionNumber = double.Parse(parsed?["appVersionDetails"]?[0]?["VersionNumber"]?.ToString() ?? "1.0");
                            string isMandatory = parsed?["appVersionDetails"]?[0]?["Mandatory"]?.ToString() ?? "N";
                            string whatsNew = parsed?["appVersionDetails"]?[0]?["WhatsNew"]?.ToString() ?? "";
                            string url = parsed?["appVersionDetails"]?[0]?["Url"]?.ToString() ?? "https://play.google.com/";

                            if (installedVersionNumber < latestVersionNumber)
                            {
                                if (isMandatory == "Y")
                                {
                                    var window = Application.Current?.Windows?.FirstOrDefault();
                                    if (window?.Page != null)
                        await window.Page.DisplayAlert("New Version", $"There is a new version (v{latestVersionNumber}) of this app available.\nWhatsNew: {whatsNew}", "Update","Close");
                                    await Launcher.OpenAsync(url);
                                    return;
                                }
                                else
                                {

                                    var window2 = Application.Current?.Windows?.FirstOrDefault();
                                    bool updat = false;
                    if (window2?.Page != null)
                        updat = await window2.Page.DisplayAlert("New Version", $"There is a new version (v{latestVersionNumber}) of this app available.\nWhatsNew: {whatsNew}\nWould you like to update now?", "Yes", "No");
                                            if (updat)
                                            {
                                                await Launcher.OpenAsync(url);
                                            }
                                       
                                    

                                }
                            }
                        }
                    }
                }
            }
            catch
            {
            }
        }

        /*  public async void AppVersion()
          {
              var current = Connectivity.NetworkAccess;
              if (current == NetworkAccess.Internet)
              {
                  try
                  {
                      var client = new HttpClient();
                      var byteArray = Encoding.ASCII.GetBytes(Preferences.Get("BasicAuth", "xx:xx"));
                      client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(byteArray));
                      string _Plateform = "A";
                      if (DevicePlatform.Android == DevicePlatform.Android)
                      {
                          _Plateform = "A";
                      }
                      else if (DevicePlatform.iOS == DevicePlatform.iOS)
                      {
                          _Plateform = "I";
                      }
                      double installedVersionNumber = double.Parse(VersionTracking.CurrentVersion);
                      double latestVersionNumber = installedVersionNumber;

                      string parameters = GetAppVersionDetailsUrl + $"api/AppVersion?" +
                      $"Platform={HttpUtility.UrlEncode(AESCryptography.EncryptAES(_Plateform))}" +
                      $"&packageid={HttpUtility.UrlEncode(AESCryptography.EncryptAES(AppInfo.PackageName))}";

                      HttpResponseMessage response = await client.GetAsync(parameters);
                      var result = await response.Content.ReadAsStringAsync();
                      var parsed = JObject.Parse(result);

                      if ((int)response.StatusCode == 200)
                      {
                          if (result.Contains("Mandatory"))
                          {
                              var m = parsed["data"][0]["VersionNumber"].ToString();
                              latestVersionNumber = double.Parse(AESCryptography.DecryptAES(parsed["data"][0]["VersionNumber"].ToString()));
                              if (installedVersionNumber < latestVersionNumber)
                              {
                                  if (AESCryptography.DecryptAES(parsed["data"][0]["Mandatory"].ToString()) == "Y")
                                  {
                                      await Application.Current.MainPage.DisplayAlert("New Version", $"There is a new version (v{AESCryptography.DecryptAES(parsed["data"][0]["VersionNumber"].ToString())}) of this app available.\nWhatsNew: {AESCryptography.DecryptAES(parsed["data"][0]["WhatsNew"].ToString())}", "Update");
                                      await Launcher.OpenAsync(AESCryptography.DecryptAES(parsed["data"][0]["StoreLink"].ToString()));
                                  }
                                  else
                                  {
                                      var update = await Application.Current.MainPage.DisplayAlert("New Version", $"There is a new version (v{AESCryptography.DecryptAES(parsed["data"][0]["VersionNumber"].ToString())}) of this app available.\nWhatsNew: {AESCryptography.DecryptAES(parsed["data"][0]["WhatsNew"].ToString())}\nWould you like to update now?", "Yes", "No");
                                      if (update)
                                      {
                                          await Launcher.OpenAsync(AESCryptography.DecryptAES(parsed["data"][0]["StoreLink"].ToString()));
                                      }
                                  }
                              }
                          }
                      }
                      //else if ((int)response.StatusCode != 404)
                      //{
                      //    await App.Current.MainPage.DisplayAlert(AppName, parsed["Message"].ToString(), App.GetLabelByKey("close"));
                      //}
                      //return (int)response.StatusCode;
                  }
                  catch (Exception)
                  {
                      //await App.Current.MainPage.DisplayAlert("Exception", "Something went wrong. Please try again!", "OK");
                      //return 500;
                  }
              }
              else
              {
                  //await App.Current.MainPage.DisplayAlert(AppName, App.NoInternet_, App.GetLabelByKey("close"));
                  //return 101;
              }
          }*/

        /*   public async Task<int> LocalResources_Get()
           {
               var current = Connectivity.NetworkAccess;
               if (current == NetworkAccess.Internet)
               {
                   try
                   {
                       var client = new HttpClient();
                       var byteArray = Encoding.ASCII.GetBytes(BasicAuth);
                       client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(byteArray));

                       HttpResponseMessage response = await client.GetAsync(baseurl + $"api/LocalResources");

                       var result = await response.Content.ReadAsStringAsync();
                       var parsed = JObject.Parse(result);
                       languageMasterDatabase.DeleteLanguageMaster();
                       if ((int)response.StatusCode == 200)
                       {
                           foreach (var pair in parsed)
                           {
                               if (pair.Key == "data")
                               {
                                   var nodes = pair.Value;
                                   foreach (var node in nodes)
                                   {
                                       var item = new LanguageMaster();
                                       item.MultipleResourceKey = AESCryptography.DecryptAES(node["MultipleResourceKey"].ToString());
                                       item.ResourceKey = AESCryptography.DecryptAES(node["ResourceKey"].ToString());
                                       item.ResourceValue = AESCryptography.DecryptAES(node["ResourceValue"].ToString());
                                       item.LocalResourceValue = AESCryptography.DecryptAES(node["LocalResourceValue"].ToString());
                                       item.Sequence = AESCryptography.DecryptAES(node["Sequence"].ToString());
                                       languageMasterDatabase.AddLanguageMaster(item);
                                   }
                               }
                           }
                           App.MyLanguage = languageMasterDatabase.GetLanguageMaster($"select * from  LanguageMaster").ToList();
                           //App.MyLanguage = languageMasterDatabase.GetLanguageMaster($"select ResourceKey, (case when ({App.Language} = 0) then ResourceValue else LocalResourceValue end)ResourceValue from  LanguageMaster").ToList();

                       }
                       else if ((int)response.StatusCode == 404)
                       {
                           await Application.Current.MainPage.DisplayAlert(AppName, parsed["Message"].ToString(), "Close");
                       }
                       return (int)response.StatusCode;
                   }
                   catch
                   {
                       await Application.Current.MainPage.DisplayAlert("Exception", "Something went wrong. Please try again!", "OK");
                       return 500;
                   }
               }
               else
               {
                   await Application.Current.MainPage.DisplayAlert(AppName, NoInternet_, "Close");
                   return 101;
               }
           }*/

        //get current district from shreshth himachal
        public async Task<int> getcuurentdistrict(string latitu, string longitu)
        {
            var current = Connectivity.NetworkAccess;
            if (current == NetworkAccess.Internet)
            {
                try
                {
                    var client = new HttpClient();
                    string url = currentLocationUrl
                        + "log=" + HttpUtility.UrlEncode(AESEncryptionforcurrentlocation(longitu))
                        + "&lat=" + HttpUtility.UrlEncode(AESEncryptionforcurrentlocation(latitu));

                    HttpResponseMessage response = await client.GetAsync(url);
                    var result = await response.Content.ReadAsStringAsync();
                    var parsed = JObject.Parse(result);
                    if ((int)response.StatusCode == 200)
                    {
                        string DistrictName = parsed["location"]?["DistrictName"]?.ToString() ?? string.Empty;
                        int districtcode = int.Parse(parsed["location"]?["DistrictCode"]?.ToString() ?? "0");
                        Preferences.Set("Discode", districtcode);
                        Preferences.Set("DistrictName", DistrictName);
                        return 200;
                    }
                    else
                    {
                        var window = Application.Current?.Windows?.FirstOrDefault();
                        if (window?.Page != null)
                            await window.Page.DisplayAlert(AppName, "Unable to fetch Department.", "close");

                    }
                    return (int)response.StatusCode;
                }
                catch
                {
                    //await Application.Current.MainPage.DisplayAlert("Exception", "Something went wrong. Please try again!", "OK");
                    return 500;
                }
            }
            else
            {
                var window3 = Application.Current?.Windows?.FirstOrDefault();
                if (window3?.Page != null)
                    await window3.Page.DisplayAlert(AppName, NoInternet_, "close");
                return 101;
            }
        }
        protected string AESEncryptionforcurrentlocation(string plainText)
        {
            string encryptText;
            try
            {
                // Define the key and IV
                string key = "e8ffc7e56311679f12b6fc91aa77a5eb";
                byte[] keyBytes = Encoding.UTF8.GetBytes(key); // UTF-8 encoding
                byte[] ivBytes = new byte[16]; // IV is initialized to all zeros (16 bytes for AES)

                // Encrypt the data
                using (Aes aes = Aes.Create())
                {
                    aes.Key = keyBytes;
                    aes.IV = ivBytes;
                    aes.Mode = CipherMode.CBC;
                    aes.Padding = PaddingMode.PKCS7;

                    using (ICryptoTransform encryptor = aes.CreateEncryptor(aes.Key, aes.IV))
                    {
                        byte[] plainTextBytes = Encoding.UTF8.GetBytes(plainText);
                        byte[] cipherData = encryptor.TransformFinalBlock(plainTextBytes, 0, plainTextBytes.Length);

                        // Convert to base64 string
                        encryptText = Convert.ToBase64String(cipherData);
                    }
                }
            }
            catch (Exception)
            {
                encryptText = string.Empty;
            }
            return encryptText;
        }



       /* public async Task<int> SaveUserDetails(string UserId, string UserName, string Mobile, string Email, string DistrictCode, string zonecode,
            string DistrictName, string zonename, string placeofconstruction, string DistrictNameLocal)
        {
            int statusCode;         

            var current = Connectivity.NetworkAccess;
            if (current == NetworkAccess.Internet)
            {
                try
                {
                    var client = new HttpClient();
                    var byteArray = Encoding.ASCII.GetBytes(BasicAuth);
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(byteArray));
                    string jsonData = JsonConvert.SerializeObject(new
                    {
                        UserId = AESCryptography.EncryptAES(UserId),
                        UserName = AESCryptography.EncryptAES(UserName),
                        Mobile = AESCryptography.EncryptAES(Mobile),
                        Email = AESCryptography.EncryptAES(Email),
                        DistrictCode = AESCryptography.EncryptAES(DistrictCode),
                        zonecode = AESCryptography.EncryptAES(zonecode),
                        DistrictName = AESCryptography.EncryptAES(DistrictName),
                        zoneName = AESCryptography.EncryptAES(zonename),
                        placeofconstruction = AESCryptography.EncryptAES(placeofconstruction),

                    });

                    StringContent content = new StringContent(jsonData, Encoding.UTF8, "application/json");
                    HttpResponseMessage response = await client.PostAsync(baseurl + "api/UserDetailsInsUpd", content);
                    var result = await response.Content.ReadAsStringAsync();
                    var parsed = JObject.Parse(result);                  
                    statusCode = (int)parsed["status_code"];
                    string userid = parsed["userid"].ToString();                  
                    if (statusCode == 200)
                    {
                        saveUserPreferencesDatabase.DeleteSaveUserPreferences();
                        var item = new SaveUserPreferences();
                        item.DistrictID = DistrictCode;
                        item.DistrictName = DistrictName;
                        item.DistrictNamelocal = DistrictNameLocal;                      
                        item.Name = UserName;
                        item.Mobile = Mobile;
                        item.email = Email;
                        item.placeofconstruction = placeofconstruction;
                        item.zonecode = zonecode;
                        item.zonename = zonename;
                        item.UserID = userid;
                        saveUserPreferencesDatabase.AddSaveUserPreferences(item);

                        // string query = saveUserPreferencesDatabase.CustomSaveUserPreferences($"update SaveUserPreferences set UserId='{userid}'");
                    }

                    else if (statusCode == 409)
                    {
                        await Application.Current.MainPage.DisplayAlert(AppName, parsed["Message"].ToString(), "close");

                    }

                    return statusCode;
                }
                catch 
                {
                    await Application.Current.MainPage.DisplayAlert("Exception", "Something went wrong. Please try again!", "OK");
                    return 500;
                }
            }
            else
            {
                await Application.Current.MainPage.DisplayAlert(AppName, NoInternet_, "close");
                return 101;
            }
        }

         public async Task<int> getDistrictMaster()
        {
            var current = Connectivity.NetworkAccess;
            if (current == NetworkAccess.Internet)
            {
                try
                {
                    var client = new HttpClient();
                    var byteArray = Encoding.ASCII.GetBytes(BasicAuth);
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(byteArray));

                    HttpResponseMessage response = await client.GetAsync(baseurl + $"api/DistrictMaster");

                    var result = await response.Content.ReadAsStringAsync();
                    var parsed = JObject.Parse(result);
                    districtMasterDatabase.DeleteDistrictMaster();
                    if ((int)response.StatusCode == 200)
                    {
                        foreach (var pair in parsed)
                        {
                            if (pair.Key == "data")
                            {
                                var nodes = pair.Value;
                                foreach (var node in nodes)
                                {
                                    var item = new DistrictMaster();
                                    item.DistrictCode = int.Parse(AESCryptography.DecryptAES(node["districtid"].ToString()));
                                    item.DistrictName = AESCryptography.DecryptAES(node["districtname"].ToString());
                                    item.DistrictNameLocal = AESCryptography.DecryptAES(node["districtnamelocal"].ToString());
                                    item.ZoneName = AESCryptography.DecryptAES(node["districtzonename"].ToString());
                                    item.ZoneCode = 1;                                  
                                    districtMasterDatabase.AddDistrictMaster(item);
                                }
                            }
                        }
                    }
                    else if ((int)response.StatusCode == 404)
                    {
                        await Application.Current.MainPage.DisplayAlert(AppName, parsed["Message"].ToString(), ("close"));
                    }
                    return (int)response.StatusCode;
                }
                catch
                {
                    await Application.Current.MainPage.DisplayAlert("Exception", "Something went wrong. Please try again!", "OK");
                    return 500;
                }
            }
            else
            {
                await Application.Current.MainPage.DisplayAlert(AppName, NoInternet_, "close");
                return 101;
            }
        }
*/

    }
}
