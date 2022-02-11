using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Codeplex.Data;
using System.Security.Cryptography;
using System.Windows.Media.Animation;
using Newtonsoft.Json;

namespace TensorMine
{

    public partial class LoginWindow : Window
    {
        private static bool Verification = default;
        private const string Version = "0.6";

        private static string Nickname;
        private static string Password;
        private static string UUID;
        private static string Skin;
        private static string Permission;
        private static string Level;
        private static string Money;

        private static string MVersion;
        private static int RAM;
        private static bool ExtraGraphics;
        private static bool Cookie;

        public string VanillaIp;
        public int VanillaPort;
        public string HardcoreIp;
        public int HardcorePort;

        private static string DiscordURL;

        private Visibility VipDiscount = Visibility.Hidden;
        private Visibility PremiumDiscount = Visibility.Hidden;
        private Visibility UltraDiscount = Visibility.Hidden;
        private Visibility SupremeDiscount = Visibility.Hidden;
        private Visibility NexusDiscount = Visibility.Hidden;

        private string VipPrice;
        private string PremiumPrice;
        private string UltraPrice;
        private string SupremePrice;
        private string NexusPrice;

        private string NewsTitle_1;
        private string NewsSubtitle_1;
        private string NewsText_1;
        private string NewsDate_1;
        private string NewsTitle_2;
        private string NewsSubtitle_2;
        private string NewsText_2;
        private string NewsDate_2;
        private string NewsTitle_3;
        private string NewsSubtitle_3;
        private string NewsText_3;
        private string NewsDate_3;

        public class LauncherData
        {
            public string version { get; set; }
            public string cookie { get; set; }
            public string storage { get; set; }
        }

        public class LauncherLocations
        {
            public string vanilla_ip { get; set; }
            public int vanilla_port { get; set; }
            public string hardcore_ip { get; set; }
            public int hardcore_port { get; set; }
        }

        protected class PlayerData
        {
            public string verification { get; set; }
            public string uuid { get; set; }
            public string skin { get; set; }
            public string permission { get; set; }
            public string level { get; set; }
            public string money { get; set; }
        }

        protected class DonateData
        {
            public string vip_price { get; set; }
            public string vip_discount { get; set; }
            public string premium_price { get; set; }
            public string premium_discount { get; set; }
            public string ultra_price { get; set; }
            public string ultra_discount { get; set; }
            public string supreme_price { get; set; }
            public string supreme_discount { get; set; }
            public string nexus_price { get; set; }
            public string nexus_discount { get; set; }
        }

        protected class NewsData
        {
            public string news1_title { get; set; }
            public string news1_subtitle { get; set; }
            public string news1_text { get; set; }
            public string news1_date { get; set; }
            public string news2_title { get; set; }
            public string news2_subtitle { get; set; }
            public string news2_text { get; set; }
            public string news2_date { get; set; }
            public string news3_title { get; set; }
            public string news3_subtitle { get; set; }
            public string news3_text { get; set; }
            public string news3_date { get; set; }
        }

        public LoginWindow()
        {
            InitializeComponent();
        }

        private void Window_Movement(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }

        private void Window_Initialized(object sender, EventArgs e)
        {
            CheckError();

            CheckRelevance();

            CheckResource();

            CheckCookie();

            this.Cursor = ((TextBlock)this.Resources["MainCursor"]).Cursor;
        }

        private void CheckError()
        {
            string process = Process.GetCurrentProcess().ProcessName;

            Process[] processes = Process.GetProcessesByName(process);

            if (processes.Length > 1)
            {
                MessageBox.Show("Лаунчер уже запущен! (" + processes.Length + ")", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error, MessageBoxResult.OK, MessageBoxOptions.ServiceNotification);
                Environment.Exit(1);
            }

            /*try
            {
                using (WebClient client = new WebClient())
                using (Stream stream = client.OpenRead("http://www.google.com")) { }
            }
            catch
            {
                MessageBox.Show("Отсутствует подключение к интернету!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error, MessageBoxResult.OK, MessageBoxOptions.ServiceNotification);
                Environment.Exit(1);
            }*/
        }

        private void CheckRelevance()
        {
           /* try
            {
                string LauncherURL = "https://tensormine.ru/launcher/info/data.php";
                LauncherData JsonData = JsonConvert.DeserializeObject<LauncherData>(new WebClient().DownloadString(LauncherURL));

                if (JsonData.version != Version)
                {
                    MessageBoxResult Question = MessageBox.Show("Обновите лаунчер! Открыть сайт для скачивания?", "Ошибка", MessageBoxButton.YesNo, MessageBoxImage.Error, MessageBoxResult.OK, MessageBoxOptions.ServiceNotification);
                    if (Question == MessageBoxResult.Yes)
                    {
                        Process.Start("https://tensormine.ru/");
                    }

                    if (JsonData.cookie == "1")
                    {
                        if (File.Exists(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\.tensormine\saves\account.dat"))
                        {
                            File.Delete(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\.tensormine\saves\account.dat");
                        }

                        if (File.Exists(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\.tensormine\config\setup.dat"))
                        {
                            File.Delete(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\.tensormine\config\setup.dat");
                        }
                    }

                    if (JsonData.storage == "1")
                    {
                        if (Directory.Exists(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\.tensormine"))
                        {
                            Directory.Delete(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\.tensormine", true);
                        }
                    }

                    Environment.Exit(1);
                }

                string LocationsURL = "https://tensormine.ru/launcher/servers/data.php";
                LauncherLocations LocationsJsonData = JsonConvert.DeserializeObject<LauncherLocations>(new WebClient().DownloadString(LocationsURL));

                VanillaIp = LocationsJsonData.vanilla_ip;
                VanillaPort = LocationsJsonData.vanilla_port;
                HardcoreIp = LocationsJsonData.hardcore_ip;
                HardcorePort = LocationsJsonData.hardcore_port;
            }
            catch
            {
                MessageBox.Show("Что-то пошло не так!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error, MessageBoxResult.OK, MessageBoxOptions.ServiceNotification);
                Environment.Exit(1);
            } */

            if (GetDonateData() == false)
            {
                MessageBox.Show("Что-то пошло не так!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error, MessageBoxResult.OK, MessageBoxOptions.ServiceNotification);
                Environment.Exit(1);
            }

            if (GetNewsData() == false)
            {
                MessageBox.Show("Что-то пошло не так!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error, MessageBoxResult.OK, MessageBoxOptions.ServiceNotification);
                Environment.Exit(1);
            }

            /*try
            {
                using (var webClient = new WebClient())
                {
                    DiscordURL = webClient.DownloadString("https://tensormine.ru/launcher/info/url.php");
                }
            }
            catch
            {
                DiscordURL = "https://tensormine.ru";
            }*/

        }

        private void CheckResource()
        {
            if (!Directory.Exists(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\.tensormine"))
            {
                Directory.CreateDirectory(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\.tensormine");
            }

            if (!Directory.Exists(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\.tensormine\saves"))
            {
                Directory.CreateDirectory(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\.tensormine\saves");
            }

            if (!Directory.Exists(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\.tensormine\config"))
            {
                Directory.CreateDirectory(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\.tensormine\config");
            }

            if (File.Exists(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\.tensormine\config\servers.dat"))
            {
                File.Delete(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\.tensormine\config\servers.dat");

                try
                {
                    WebClient dowloader = new WebClient();
                    dowloader.DownloadFileAsync(new Uri("https://tensormine.ru/launcher/storage/servers.dat"), Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\.tensormine\config\servers.dat");
                }
                catch (WebException)
                {
                    CheckError();
                }
            }
        }

        private void CheckCookie()
        {
            if (File.Exists(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\.tensormine\saves\account.dat"))
            {
                try
                {
                    using (StreamReader reader = new StreamReader(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\.tensormine\saves\account.dat"))
                    {
                        string nickname = reader.ReadLineAsync().Result.Replace("[", "").Replace("]", "");
                        string password = reader.ReadLineAsync().Result.Replace("[", "").Replace("]", "");

                        if (nickname != "" && nickname != null)
                        {
                            NicknameTextBox.Text = nickname;
                        }

                        if (password != "" && password != null)
                        {
                            PasswordTextBox.Password = CookieOpen(password, GetSomething()); ;
                        }

                        reader.Close();
                    }
                }
                catch { }
            }

            if (File.Exists(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\.tensormine\config\setup.dat"))
            {
                try
                {
                    using (StreamReader reader = new StreamReader(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\.tensormine\config\setup.dat"))
                    {
                        string getVersion = reader.ReadLine().Replace("Version: ", "");
                        string getRAM = reader.ReadLine().Replace("RAM: ", "");
                        string getExtraGraphics = reader.ReadLine().Replace("Extra-graphics: ", "");
                        string getCookie = reader.ReadLine().Replace("Cookie: ", "");

                        if (getVersion != "" && getVersion != null)
                        {
                            if (getVersion == "OptiFine")
                            {
                                MVersion = "OptiFine";
                            }
                            else
                            {
                                MVersion = "Classic";
                            }
                        }

                        if (getRAM != "" && getRAM != null)
                        {
                            Int32.TryParse(getRAM, out RAM);

                            if (RAM < 512 || RAM > 8192)
                            {
                                RAM = 1024;
                            }
                        }

                        if (getExtraGraphics != "" && getExtraGraphics != null)
                        {
                            if (getExtraGraphics == "True")
                            {
                                ExtraGraphics = true;
                            }
                            else
                            {
                                ExtraGraphics = false;
                            }
                        }

                        if (getCookie != "" && getCookie != null)
                        {
                            if (getCookie == "True")
                            {
                                Cookie = true;
                            }
                            else
                            {
                                Cookie = false;
                            }
                        }

                        reader.Close();
                    }
                }
                catch
                {
                    MVersion = "OptiFine";
                    RAM = 1024;
                    Cookie = true;
                    ExtraGraphics = false;

                    if (File.Exists(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\.tensormine\config\setup.dat"))
                    {
                        File.Delete(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\.tensormine\config\setup.dat");
                    }
                }
            }
            else
            {
                MVersion = "OptiFine";
                RAM = 1024;
                Cookie = true;
                ExtraGraphics = false;
            }
        }

        private async void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            if (NicknameTextBox.Text != null && NicknameTextBox.Text != "" && PasswordTextBox.Password != null && PasswordTextBox.Password != "")
            {
                LoginButton.IsEnabled = false;
                HintButton.IsEnabled = false;

                if (LoginIcon.Visibility == Visibility.Visible)
                {
                    LoginIcon.Visibility = Visibility.Hidden;
                }

                LoginIconUnchecked.Visibility = Visibility.Hidden;
                LoginIconUpdate.Visibility = Visibility.Visible;

                Nickname = NicknameTextBox.Text;
                Password = PasswordTextBox.Password;

                await Task.Run(() =>
                {
                    try
                    {
                        string Data = "https://tensormine.ru/launcher/profile/user.php?nickname=" + Nickname + "&password=" + Password;
                        PlayerData JsonData = JsonConvert.DeserializeObject<PlayerData>(Encoding.UTF8.GetString(new WebClient().DownloadData(Data)));

                        if (JsonData.verification == "true")
                        {
                            Verification = true;
                            GetPlayerData(JsonData);
                        }
                        else
                        {
                            Verification = false;
                        }
                    }
                    catch
                    {
                        Verification = false;
                    }
                });
            }

            if (Verification == true)
            {
                MainWindow mainWindow = new MainWindow();

                mainWindow.Nickname = Nickname;
                mainWindow.UUID = UUID;
                mainWindow.Skin = Skin;
                mainWindow.Permission = Permission;
                mainWindow.Level = Level;
                mainWindow.Money = Money;

                mainWindow.Version = MVersion;
                mainWindow.RAM = RAM;
                mainWindow.ExtraGraphics = ExtraGraphics;
                mainWindow.Cookie = Cookie;

                mainWindow.VanillaIp = VanillaIp;
                mainWindow.VanillaPort = VanillaPort;
                mainWindow.HardcoreIp = HardcoreIp;
                mainWindow.HardcorePort = HardcorePort;

                mainWindow.DiscordURL = DiscordURL;

                mainWindow.VipPrice = VipPrice;
                mainWindow.PremiumPrice = PremiumPrice;
                mainWindow.UltraPrice = UltraPrice;
                mainWindow.SupremePrice = SupremePrice;
                mainWindow.NexusPrice = NexusPrice;

                mainWindow.VipDiscount = VipDiscount;
                mainWindow.PremiumDiscount = PremiumDiscount;
                mainWindow.UltraDiscount = UltraDiscount;
                mainWindow.SupremeDiscount = SupremeDiscount;
                mainWindow.NexusDiscount = NexusDiscount;

                mainWindow.NewsTitle_1 = NewsTitle_1;
                mainWindow.NewsSubtitle_1 = NewsSubtitle_1;
                mainWindow.NewsText_1 = NewsText_1;
                mainWindow.NewsDate_1 = NewsDate_1;

                mainWindow.NewsTitle_2 = NewsTitle_2;
                mainWindow.NewsSubtitle_2 = NewsSubtitle_2;
                mainWindow.NewsText_2 = NewsText_2;
                mainWindow.NewsDate_2 = NewsDate_2;

                mainWindow.NewsTitle_3 = NewsTitle_3;
                mainWindow.NewsSubtitle_3 = NewsSubtitle_3;
                mainWindow.NewsText_3 = NewsText_3;
                mainWindow.NewsDate_3 = NewsDate_3;

                LoginIconUpdate.Visibility = Visibility.Hidden;
                LoginIconChecked.Visibility = Visibility.Visible;

                await Task.Delay(100);

                DoubleAnimation WindowAnimation = new DoubleAnimation();
                WindowAnimation.From = 1;
                WindowAnimation.To = 0;
                WindowAnimation.Duration = TimeSpan.FromMilliseconds(500);
                this.BeginAnimation(Window.OpacityProperty, WindowAnimation);

                await Task.Delay(510);

                mainWindow.Show();
                this.Close();
            }
            else
            {
                LoginIconUpdate.Visibility = Visibility.Hidden;
                LoginIconUnchecked.Visibility = Visibility.Visible;
                LoginButton.IsEnabled = true;
                HintButton.IsEnabled = true;
            }
        }

        private void GetPlayerData(PlayerData JsonData)
        {
            try
            {
                UUID = JsonData.uuid;

                if (JsonData.skin != null && JsonData.skin != "")
                {
                    using (var client = new WebClient())
                    {
                        string rawJson = Encoding.UTF8.GetString(client.DownloadData(new Uri("https://api.mojang.com/users/profiles/minecraft/" + JsonData.skin)));
                        dynamic json = DynamicJson.Parse(rawJson);

                        Skin = json["id"];
                    }
                }

                Permission = JsonData.permission;

                Level = JsonData.level;

                Money = JsonData.money;
            }
            catch
            {
                UUID = "XXXXXXXX-XXXX-XXXX-XXXX-XXXXXXXXXXXX";
                Skin = "49ec480d39d64b46bf8a6b823c886162";
                Permission = "Игрок";
                Level = "1";
                Money = "1000";
            }
        }

        private bool GetDonateData()
        {
            bool Status = false;

            try
            {
                string Data = "https://tensormine.ru/launcher/donate/data.php";
                DonateData JsonData = JsonConvert.DeserializeObject<DonateData>(new WebClient().DownloadString(Data));

                VipPrice = JsonData.vip_price;
                PremiumPrice = JsonData.premium_price;
                UltraPrice = JsonData.ultra_price;
                SupremePrice = JsonData.supreme_price;
                NexusPrice = JsonData.nexus_price;

                if (JsonData.vip_discount == "1")
                {
                    VipDiscount = Visibility.Visible;
                }
                else
                {
                    VipDiscount = Visibility.Hidden;
                }

                if (JsonData.premium_discount == "1")
                {
                    PremiumDiscount = Visibility.Visible;
                }
                else
                {
                    PremiumDiscount = Visibility.Hidden;
                }

                if (JsonData.ultra_discount == "1")
                {
                    UltraDiscount = Visibility.Visible;
                }
                else
                {
                    UltraDiscount = Visibility.Hidden;
                }

                if (JsonData.supreme_discount == "1")
                {
                    SupremeDiscount = Visibility.Visible;
                }
                else
                {
                    SupremeDiscount = Visibility.Hidden;
                }

                if (JsonData.nexus_discount == "1")
                {
                    NexusDiscount = Visibility.Visible;
                }
                else
                {
                    NexusDiscount = Visibility.Hidden;
                }

                Status = true;
            }
            catch
            {
                Status = false;
            }

            return Status;
        }

        private bool GetNewsData()
        {
            bool Status = false;

            try
            {
                string Data = "https://tensormine.ru/launcher/news/data.php";
                NewsData JsonData = JsonConvert.DeserializeObject<NewsData>(Encoding.UTF8.GetString(new WebClient().DownloadData(Data)));

                NewsTitle_1 = JsonData.news1_title;
                NewsSubtitle_1 = JsonData.news1_subtitle;
                NewsText_1 = JsonData.news1_text;
                NewsDate_1 = JsonData.news1_date;

                NewsTitle_2 = JsonData.news2_title;
                NewsSubtitle_2 = JsonData.news2_subtitle;
                NewsText_2 = JsonData.news2_text;
                NewsDate_2 = JsonData.news2_date;

                NewsTitle_3 = JsonData.news3_title;
                NewsSubtitle_3 = JsonData.news3_subtitle;
                NewsText_3 = JsonData.news3_text;
                NewsDate_3 = JsonData.news3_date;

                Status = true;
            }
            catch
            {
                Status = false;
            }

            return Status;
        }

        private string GetSomething()
        {
            return "albedo";
        }

        private async void Window_Closed(object sender, EventArgs e)
        {
            if (!Directory.Exists(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\.tensormine"))
            {
                Directory.CreateDirectory(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\.tensormine");
            }

            if (!Directory.Exists(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\.tensormine\saves"))
            {
                Directory.CreateDirectory(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\.tensormine\saves");
            }

            if (Cookie == true)
            {
                try
                {
                    using (StreamWriter writer = new StreamWriter(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\.tensormine\saves\account.dat", false, System.Text.Encoding.Default))
                    {
                        await writer.WriteLineAsync("[" + Nickname + "]");
                        await writer.WriteLineAsync("[" + CookieCreate(Password, GetSomething()) + "]");

                        writer.Close();
                    }
                }
                catch { }
            }
            else if (Cookie == false)
            {
                if (File.Exists(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\.tensormine\saves\account.dat"))
                {
                    File.Delete(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\.tensormine\saves\account.dat");
                }
            }
        }

        private void ExitButton_Click(object sender, RoutedEventArgs e)
        {
            Environment.Exit(1);
        }

        private void HintButton_Click(object sender, RoutedEventArgs e)
        {
            Process.Start("http://tensormine.ru");
        }

        private void HintButton_MouseEnter(object sender, MouseEventArgs e)
        {
            HintTextBlock.TextDecorations = TextDecorations.Underline;
        }

        private void HintButton_MouseLeave(object sender, MouseEventArgs e)
        {
            HintTextBlock.TextDecorations = null;
        }

        private static string CookieCreate(string ishText, string pass, string sol = "sometime", string cryptographicAlgorithm = "SHA1", int passIter = 2, string initVec = "a8doSuDitOz1hZe#", int keySize = 256)
        {
            if (string.IsNullOrEmpty(ishText))
                return "";

            byte[] initVecB = Encoding.ASCII.GetBytes(initVec);
            byte[] solB = Encoding.ASCII.GetBytes(sol);
            byte[] ishTextB = Encoding.UTF8.GetBytes(ishText);

            PasswordDeriveBytes derivPass = new PasswordDeriveBytes(pass, solB, cryptographicAlgorithm, passIter);
            byte[] keyBytes = derivPass.GetBytes(keySize / 8);
            RijndaelManaged symmK = new RijndaelManaged();
            symmK.Mode = CipherMode.CBC;

            byte[] cipherTextBytes = null;

            using (ICryptoTransform encryptor = symmK.CreateEncryptor(keyBytes, initVecB))
            {
                using (MemoryStream memStream = new MemoryStream())
                {
                    using (CryptoStream cryptoStream = new CryptoStream(memStream, encryptor, CryptoStreamMode.Write))
                    {
                        cryptoStream.Write(ishTextB, 0, ishTextB.Length);
                        cryptoStream.FlushFinalBlock();
                        cipherTextBytes = memStream.ToArray();
                        memStream.Close();
                        cryptoStream.Close();
                    }
                }
            }

            symmK.Clear();
            return Convert.ToBase64String(cipherTextBytes);
        }


        private static string CookieOpen(string ciphText, string pass, string sol = "sometime", string cryptographicAlgorithm = "SHA1", int passIter = 2, string initVec = "a8doSuDitOz1hZe#", int keySize = 256)
        {
            if (string.IsNullOrEmpty(ciphText))
            {
                return "";
            }

            byte[] initVecB = Encoding.ASCII.GetBytes(initVec);
            byte[] solB = Encoding.ASCII.GetBytes(sol);
            byte[] cipherTextBytes = Convert.FromBase64String(ciphText);

            PasswordDeriveBytes derivPass = new PasswordDeriveBytes(pass, solB, cryptographicAlgorithm, passIter);
            byte[] keyBytes = derivPass.GetBytes(keySize / 8);

            RijndaelManaged symmK = new RijndaelManaged();
            symmK.Mode = CipherMode.CBC;

            byte[] plainTextBytes = new byte[cipherTextBytes.Length];
            int byteCount = 0;

            using (ICryptoTransform decryptor = symmK.CreateDecryptor(keyBytes, initVecB))
            {
                using (MemoryStream mSt = new MemoryStream(cipherTextBytes))
                {
                    using (CryptoStream cryptoStream = new CryptoStream(mSt, decryptor, CryptoStreamMode.Read))
                    {
                        byteCount = cryptoStream.Read(plainTextBytes, 0, plainTextBytes.Length);
                        mSt.Close();
                        cryptoStream.Close();
                    }
                }
            }

            symmK.Clear();
            return Encoding.UTF8.GetString(plainTextBytes, 0, byteCount);
        }
    }
}
