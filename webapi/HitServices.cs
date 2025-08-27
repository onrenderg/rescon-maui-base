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

        public string PrivacyPolicyUrl = "http://10.146.2.8/ResilientConstructionAPI/PrivacyPolicy.aspx";// for login and fetching departments       
      //  public string baseurl = "http://10.146.2.8/ResilientConstructionAPI/";
   //     public string queryimageurl = "http://10.146.2.8/ResilientConstructionAPI/QueryImageViewer?";
        public string currentLocationUrl = "https://mobileappshp.nic.in/shereshthhimachal/Initilisation.svc/location?";

        public string zoneAurl = "https://hpsdma.nic.in//admnis/admin/showimg.aspx?ID=3667";
        public string zoneBurl = "https://hpsdma.nic.in//admnis/admin/showimg.aspx?ID=3668";
        public string zoneCurl = "https://hpsdma.nic.in//admnis/admin/showimg.aspx?ID=3670";  
        public string kawach2url = "https://hpsdma.nic.in//admnis/admin/showimg.aspx?ID=3671";  
        public string Constructionpriurl = "https://drive.google.com/file/d/1HC0QfxRdXhdIksQ_Rd86fQb4W5SGEJXB/view?usp=sharing";

        public string guidebookpriurl = "https://hpsdma.nic.in//admnis/admin/showimg.aspx?ID=3671";
     
        
        /* public string zoneAurl = "https://drive.google.com/file/d/1Hu7SD15GszAdXJdGsVQltkLaV_jYVDPI/view?usp=drive_link";
        public string zoneBurl = "https://drive.google.com/file/d/1hrU_3R7pebN9eXbFlVtSvpRVr7YGFieu/view?usp=drive_link";
        public string zoneCurl = "https://drive.google.com/file/d/1Q_DWsdkmmhyXN3pcl95k5gU4HiDChkiZ/view?usp=drive_link";
        public string Constructionpriurl = "https://drive.google.com/file/d/1HC0QfxRdXhdIksQ_Rd86fQb4W5SGEJXB/view?usp=sharing";*/


        SaveUserPreferencesDatabase saveUserPreferencesDatabase = new SaveUserPreferencesDatabase();
        List<SaveUserPreferences> saveUserPreferenceslist;
        EngineerMasterDatabase engineerMasterDatabase = new EngineerMasterDatabase();
        EngineerResponseDetailsDatabase engineerResponseDetailsDatabase = new EngineerResponseDetailsDatabase();
        AreaMasterDatabase areaMasterDatabase = new AreaMasterDatabase();
        SubAreaMasterDatabase subAreaMasterDatabase = new SubAreaMasterDatabase();
        DistrictMasterDatabase districtMasterDatabase = new DistrictMasterDatabase();
        List<DistrictMaster> districtMasters = new List<DistrictMaster>();
        LanguageMasterDatabase languageMasterDatabase = new LanguageMasterDatabase();   

        public async void AppVersion()
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

                    string parameters = "" + $"api/AppVersion?" +
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
        }

     

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
                        string DistrictName = parsed["location"]["DistrictName"].ToString();
                        int districtcode = int.Parse(parsed["location"]["DistrictCode"].ToString());
                        Preferences.Set("Discode", districtcode);
                        Preferences.Set("DistrictName", DistrictName);
                        return 200;
                    }
                    else
                    {
                        await Application.Current.MainPage.DisplayAlert(AppName, "Unable to fetch Department.", ("close"));

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
                await Application.Current.MainPage.DisplayAlert(AppName, NoInternet_, ("close"));
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
/*
        public async Task<int> SaveUserDetails(string UserId, string UserName, string Mobile, string Email, string DistrictCode, string zonecode,
            string DistrictName, string zonename, string placeofconstruction, string DistrictNameLocal)
        {
            int statusCode;
            *//*  saveUserPreferenceslist = saveUserPreferencesDatabase.GetSaveUserPreferences("Select * from SaveUserPreferences").ToList();

              string UserId = saveUserPreferenceslist.ElementAt(0).UserID;
              string UserName = saveUserPreferenceslist.ElementAt(0).Name;
              string Mobile = saveUserPreferenceslist.ElementAt(0).Mobile;
              string Email = saveUserPreferenceslist.ElementAt(0).email;
              string DistrictCode = saveUserPreferenceslist.ElementAt(0).DistrictID;
              string zonecode = saveUserPreferenceslist.ElementAt(0).zonecode;
              string DistrictName = saveUserPreferenceslist.ElementAt(0).DistrictName;
              string zonename = saveUserPreferenceslist.ElementAt(0).zonename;
              string placeofconstruction = saveUserPreferenceslist.ElementAt(0).placeofconstruction;*//*



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

        public async Task<int> Saveuserquery(string engineerid, string expertisearea, string subarea, string userremarks, Stream _file)
        {
            saveUserPreferenceslist = saveUserPreferencesDatabase.GetSaveUserPreferences("Select * from SaveUserPreferences").ToList();
            string UserId = saveUserPreferenceslist.ElementAt(0).UserID;

            var current = Connectivity.NetworkAccess;
            if (current == NetworkAccess.Internet)
            {
                try
                {
                    var client = new HttpClient();
                    var request = new HttpRequestMessage(HttpMethod.Post, baseurl + "api/UserquerywithImage");
                    var byteArray = Encoding.ASCII.GetBytes(BasicAuth);

                    client.DefaultRequestHeaders.Add("Accept", "application/json");
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(byteArray));

                    var content = new MultipartFormDataContent();

                    content.Add(new StringContent(AESCryptography.EncryptAES(UserId)), "UserId");
                    content.Add(new StringContent(AESCryptography.EncryptAES(engineerid)), "engineerid");
                    content.Add(new StringContent(AESCryptography.EncryptAES(expertisearea)), "expertisearea");
                    content.Add(new StringContent(AESCryptography.EncryptAES(subarea)), "subarea");
                    content.Add(new StringContent(AESCryptography.EncryptAES(userremarks)), "userremarks");

                    if (_file != null)
                    {
                        content.Add(new StreamContent(_file), "file", "mepj");
                    }

                    string b = "UserId: " + UserId + "\n" +
                            "engineerid:" + engineerid + "\n" +
                            "expertisearea:" + expertisearea + "\n" +
                              "subarea:" + subarea + "\n" +
                            "userremarks:" + userremarks + "\n" +
                            "file:" + _file ?? "";
                    request.Content = content;
                    var response = await client.SendAsync(request);
                    response.EnsureSuccessStatusCode();


                    var result = await response.Content.ReadAsStringAsync();
                    var parsed = JObject.Parse(result);
                    if ((int)response.StatusCode == 200)
                    {
                        Preferences.Set("queryid", parsed["queryid"].ToString());
                    }
                    await Application.Current.MainPage.DisplayAlert(AppName, parsed["Message"].ToString(), "Close");
                    return (int)response.StatusCode;
                }
                catch
                {
                    await App.Current.MainPage.DisplayAlert("Exception", "Something went wrong. Please try again!", "OK");
                    return 500;
                }
            }
            else
            {
                await App.Current.MainPage.DisplayAlert(AppName, NoInternet_, "Close");
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


        public async Task<int> EngineerMaster_Get()
        {
            var current = Connectivity.NetworkAccess;
            if (current == NetworkAccess.Internet)
            {
                try
                {
                    var client = new HttpClient();
                    var byteArray = Encoding.ASCII.GetBytes(BasicAuth);
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(byteArray));

                    HttpResponseMessage response = await client.GetAsync(baseurl + $"api/EngineerMaster");

                    var result = await response.Content.ReadAsStringAsync();
                    var parsed = JObject.Parse(result);
                    engineerMasterDatabase.DeleteEngineerMaster();
                    if ((int)response.StatusCode == 200)
                    {
                        foreach (var pair in parsed)
                        {
                            if (pair.Key == "data")
                            {
                                var nodes = pair.Value;
                                foreach (var node in nodes)
                                {
                                    var item = new EngineerMaster();
                                    item.engineerid = AESCryptography.DecryptAES(node["engineerid"].ToString());
                                    item.engineername = AESCryptography.DecryptAES(node["engineername"].ToString());
                                    item.email = AESCryptography.DecryptAES(node["email"].ToString());
                                    item.mobile = AESCryptography.DecryptAES(node["mobile"].ToString());
                                    item.expertisearea = AESCryptography.DecryptAES(node["expertisearea"].ToString());
                                    item.enginnerzone = AESCryptography.DecryptAES(node["enginnerzone"].ToString());
                                    engineerMasterDatabase.AddEngineerMaster(item);
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

        public async Task<int> EngineerResponseDetails_Get()
        {
            saveUserPreferenceslist = saveUserPreferencesDatabase.GetSaveUserPreferences("Select * from SaveUserPreferences").ToList();

            string UserId = saveUserPreferenceslist.ElementAt(0).UserID;

            var current = Connectivity.NetworkAccess;
            if (current == NetworkAccess.Internet)
            {
                try
                {
                    var client = new HttpClient();
                    var byteArray = Encoding.ASCII.GetBytes(BasicAuth);
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(byteArray));
                    string url = baseurl + $"api/EngineerResponse?userid=" + HttpUtility.UrlEncode(AESCryptography.EncryptAES(UserId));
                    HttpResponseMessage response = await client.GetAsync(url);

                    var result = await response.Content.ReadAsStringAsync();
                    var parsed = JObject.Parse(result);
                    engineerResponseDetailsDatabase.DeleteEngineerResponseDetails();
                    if ((int)response.StatusCode == 200)
                    {
                        foreach (var pair in parsed)
                        {
                            if (pair.Key == "data")
                            {
                                var nodes = pair.Value;
                                foreach (var node in nodes)
                                {
                                    var item = new EngineerResponseDetails();

                                    item.responseid = AESCryptography.DecryptAES(node["responseid"].ToString());
                                    item.queryid = AESCryptography.DecryptAES(node["queryid"].ToString());
                                    item.engineerid = AESCryptography.DecryptAES(node["engineerid"].ToString());
                                    item.engineername = AESCryptography.DecryptAES(node["engineername"].ToString());
                                    item.engineerremarks = AESCryptography.DecryptAES(node["engineerremarks"].ToString());
                                    item.engineerresponsedate = AESCryptography.DecryptAES(node["engineerresponsedate"].ToString());
                                    item.userid = AESCryptography.DecryptAES(node["userid"].ToString());
                                    item.userremarks = AESCryptography.DecryptAES(node["userremarks"].ToString());
                                    item.userquerydate = AESCryptography.DecryptAES(node["userquerydate"].ToString());
                                    item.engineeremail = AESCryptography.DecryptAES(node["engineeremail"].ToString());
                                    item.engineermobile = AESCryptography.DecryptAES(node["engineermobile"].ToString());
                                    item.hasimage = AESCryptography.DecryptAES(node["hasimage"].ToString());
                                    item.areaid = AESCryptography.DecryptAES(node["areaid"].ToString());
                                    item.expertisearea = AESCryptography.DecryptAES(node["expertisearea"].ToString());
                                    item.expertisearealocal = AESCryptography.DecryptAES(node["expertisearealocal"].ToString());
                                    item.subareaid = AESCryptography.DecryptAES(node["subareaid"].ToString());
                                    item.subarename = AESCryptography.DecryptAES(node["subarename"].ToString());
                                    item.subarenamelocal = AESCryptography.DecryptAES(node["subarenamelocal"].ToString());

                                    engineerResponseDetailsDatabase.AddEngineerResponseDetails(item);
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

        public async Task<int> AreaMaster_Get()
        {

            var current = Connectivity.NetworkAccess;
            if (current == NetworkAccess.Internet)
            {
                try
                {
                    var client = new HttpClient();
                    var byteArray = Encoding.ASCII.GetBytes(BasicAuth);
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(byteArray));
                    string url = baseurl + $"api/RelateToAreaMaster?";
                    HttpResponseMessage response = await client.GetAsync(url);

                    var result = await response.Content.ReadAsStringAsync();
                    var parsed = JObject.Parse(result);
                    areaMasterDatabase.DeleteAreaMaster();
                    if ((int)response.StatusCode == 200)
                    {
                        foreach (var pair in parsed)
                        {
                            if (pair.Key == "data")
                            {
                                var nodes = pair.Value;
                                foreach (var node in nodes)
                                {
                                    var item = new AreaMaster();

                                    item.areaid = AESCryptography.DecryptAES(node["areaid"].ToString());
                                    item.areaname = AESCryptography.DecryptAES(node["areaname"].ToString());
                                    item.areanamelocal = AESCryptography.DecryptAES(node["areanamelocal"].ToString());

                                    areaMasterDatabase.AddAreaMaster(item);
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

        public async Task<int> SubAreaMaster_Get()
        {

            var current = Connectivity.NetworkAccess;
            if (current == NetworkAccess.Internet)
            {
                try
                {
                    var client = new HttpClient();
                    var byteArray = Encoding.ASCII.GetBytes(BasicAuth);
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(byteArray));
                    string url = baseurl + $"api/RelateToSubAreaMaster?";
                    HttpResponseMessage response = await client.GetAsync(url);

                    var result = await response.Content.ReadAsStringAsync();
                    var parsed = JObject.Parse(result);
                    subAreaMasterDatabase.DeleteSubAreaMaster();
                    if ((int)response.StatusCode == 200)
                    {
                        foreach (var pair in parsed)
                        {
                            if (pair.Key == "data")
                            {
                                var nodes = pair.Value;
                                foreach (var node in nodes)
                                {
                                    var item = new SubAreaMaster();

                                    item.areaid = AESCryptography.DecryptAES(node["areaid"].ToString());
                                    item.subareaid = AESCryptography.DecryptAES(node["subareaid"].ToString());
                                    item.subareaname = AESCryptography.DecryptAES(node["subareaname"].ToString());
                                    item.subareanamelocal = AESCryptography.DecryptAES(node["subareanamelocal"].ToString());

                                    subAreaMasterDatabase.AddSubAreaMaster(item);
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
