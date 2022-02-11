using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Effects;
using System.Windows.Threading;
using CmlLib.Core;
using CmlLib.Core.Auth;
using CmlLib.Core.Downloader;
using Codeplex.Data;
using MCQuery;
using Newtonsoft.Json;

namespace TensorMine
{
    public partial class MainWindow : Window
    {
        [DllImport("kernel32.dll")]
        static extern bool SetProcessWorkingSetSize(IntPtr hProcess, int dwMinimumWorkingSetSize, int dwMaximumWorkingSetSize);

        private static MinecraftPath MinecraftPATH = new MinecraftPath();

        private static CMLauncher launcher = new CMLauncher(MinecraftPATH);

        private Process minecraftProcess;

        private bool OnlineCheckerStatus = false;

        public string Nickname;
        public string Permission;
        public string Level;
        public string Money;
        public string UUID;
        public string Skin;

        public string Version = "Classic";
        public int RAM = 1024;
        public bool ExtraGraphics = default;
        public bool Cookie = true;

        public string VanillaIp;
        public int VanillaPort;
        public string HardcoreIp;
        public int HardcorePort;

        public string DiscordURL;

        public string VipPrice;
        public string PremiumPrice;
        public string UltraPrice;
        public string SupremePrice;
        public string NexusPrice;

        public Visibility VipDiscount;
        public Visibility PremiumDiscount;
        public Visibility UltraDiscount;
        public Visibility SupremeDiscount;
        public Visibility NexusDiscount;

        public string NewsTitle_1;
        public string NewsSubtitle_1;
        public string NewsText_1;
        public string NewsDate_1;
        public string NewsTitle_2;
        public string NewsSubtitle_2;
        public string NewsText_2;
        public string NewsDate_2;
        public string NewsTitle_3;
        public string NewsSubtitle_3;
        public string NewsText_3;
        public string NewsDate_3;

        public MainWindow()
        {
            InitializeComponent();
        }

        private async void Window_Initialized(object sender, EventArgs e)
        {
            bool checker = false;

            this.Cursor = ((TextBlock)this.Resources["MainCursor"]).Cursor;

            if (checker == false)
            {
                checker = true;
                await Task.Run(() => OnlineChecker());
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            ProfileAvatarImage.Uuid = Skin;
            ControlAvatarImage.Uuid = Skin;
            ProfileNicknameTextBlock.Text = Nickname;
            ProfileDonateTextBlock.Text = Permission;
            ProfileLevelTextBlock.Text = Level + "★";
            ProfileMoneyTextBlock.Text = Money + "$";
            ProfileUUIDTextBlock.Text = UUID;

            ShopVipPrice.Content = VipPrice + "₽";
            ShopPremiumPrice.Content = PremiumPrice + "₽";
            ShopUltraPrice.Content = UltraPrice + "₽";
            ShopSupremePrice.Content = SupremePrice + "₽";
            ShopNexusPrice.Content = NexusPrice + "₽";

            ShopVipDiscount.Visibility = VipDiscount;
            ShopPremiumDiscount.Visibility = PremiumDiscount;
            ShopUltraDiscount.Visibility = UltraDiscount;
            ShopSupremeDiscount.Visibility = SupremeDiscount;
            ShopNexusDiscount.Visibility = NexusDiscount;

            News1TitleTextBlock.Text = NewsTitle_1;
            News1SubtitleTextBlock.Text = NewsSubtitle_1;
            News1TextTextBlock.Text = NewsText_1;
            News1DateTextBlock.Text = NewsDate_1;

            News2TitleTextBlock.Text = NewsTitle_2;
            News2SubtitleTextBlock.Text = NewsSubtitle_2;
            News2TextTextBlock.Text = NewsText_2;
            News2DateTextBlock.Text = NewsDate_2;

            News3TitleTextBlock.Text = NewsTitle_3;
            News3SubtitleTextBlock.Text = NewsSubtitle_3;
            News3TextTextBlock.Text = NewsText_3;
            News3DateTextBlock.Text = NewsDate_3;

            this.Visibility = Visibility.Visible;
            this.IsEnabled = true;

            SetProcessWorkingSetSize(Process.GetCurrentProcess().Handle, -1, -1);
        }

        private void OnlineChecker()
        {
            OnlineCheckerStatus = true;

            try                                                                 // Vanilla
            {
                MCServer server = new MCServer(VanillaIp, VanillaPort);
                ServerStatus status = server.Status();
                Dispatcher.BeginInvoke(DispatcherPriority.Background, new Action(() => ServersVanillaMask.Opacity = 0));
                Dispatcher.BeginInvoke(DispatcherPriority.Background, new Action(() => ServersVanillaPlayers.Content = status.Players.Online + "/200"));
                Dispatcher.BeginInvoke(DispatcherPriority.Background, new Action(() => ServersVanillaButton.IsEnabled = true));
            }
            catch
            {
                Dispatcher.BeginInvoke(DispatcherPriority.Background, new Action(() => ServersVanillaMask.Opacity = 0.7));
                Dispatcher.BeginInvoke(DispatcherPriority.Background, new Action(() => ServersVanillaPlayers.Content = "Выкл."));
                Dispatcher.BeginInvoke(DispatcherPriority.Background, new Action(() => ServersVanillaButton.IsEnabled = false));
            }
            try                                                                 // Hardcore
            {
                MCServer server = new MCServer(HardcoreIp, HardcorePort);
                ServerStatus status = server.Status();
                Dispatcher.BeginInvoke(DispatcherPriority.Background, new Action(() => ServersHardcoreMask.Opacity = 0));
                Dispatcher.BeginInvoke(DispatcherPriority.Background, new Action(() => ServersHardcorePlayers.Content = status.Players.Online + "/200"));
                Dispatcher.BeginInvoke(DispatcherPriority.Background, new Action(() => ServersHardcoreButton.IsEnabled = true));
            }
            catch
            {
                Dispatcher.BeginInvoke(DispatcherPriority.Background, new Action(() => ServersHardcoreMask.Opacity = 0.7));
                Dispatcher.BeginInvoke(DispatcherPriority.Background, new Action(() => ServersHardcorePlayers.Content = "Выкл."));
                Dispatcher.BeginInvoke(DispatcherPriority.Background, new Action(() => ServersHardcoreButton.IsEnabled = false));
            }

            /* try                                                                 // Minigames
             {
                 MCServer server = new MCServer("mellowcraft.ru", 25737);
                 ServerStatus status = server.Status();
                 Dispatcher.BeginInvoke(DispatcherPriority.Background, new Action(() => ServersMinigamesMask.Opacity = 0));
                 Dispatcher.BeginInvoke(DispatcherPriority.Background, new Action(() => ServersMinigamesPlayers.Content = status.Players.Online + "/1000"));
                 Dispatcher.BeginInvoke(DispatcherPriority.Background, new Action(() => ServersMinigamesButton.IsEnabled = true));
             }
             catch
             {
                 Dispatcher.BeginInvoke(DispatcherPriority.Background, new Action(() => ServersMinigamesMask.Opacity = 0.7));
                 Dispatcher.BeginInvoke(DispatcherPriority.Background, new Action(() => ServersMinigamesPlayers.Content = "Выкл."));
                 Dispatcher.BeginInvoke(DispatcherPriority.Background, new Action(() => ServersMinigamesButton.IsEnabled = false));
             }
             try                                                                 // Roleplay
             {
                 MCServer server = new MCServer("mellowcraft.ru", 22737);
                 ServerStatus status = server.Status();
                 Dispatcher.BeginInvoke(DispatcherPriority.Background, new Action(() => ServersRoleplayMask.Opacity = 0));
                 Dispatcher.BeginInvoke(DispatcherPriority.Background, new Action(() => ServersRoleplayPlayers.Content = status.Players.Online + "/1000"));
                 Dispatcher.BeginInvoke(DispatcherPriority.Background, new Action(() => ServersRoleplayButton.IsEnabled = true));
             }
             catch
             {
                 Dispatcher.BeginInvoke(DispatcherPriority.Background, new Action(() => ServersRoleplayMask.Opacity = 0.7));
                 Dispatcher.BeginInvoke(DispatcherPriority.Background, new Action(() => ServersRoleplayPlayers.Content = "Выкл."));
                 Dispatcher.BeginInvoke(DispatcherPriority.Background, new Action(() => ServersRoleplayButton.IsEnabled = false));
             } */

            OnlineCheckerStatus = false;
        }

        private async void Window_Closed(object sender, EventArgs e)
        {
            if (!Directory.Exists(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\.tensormine\config"))
            {
                Directory.CreateDirectory(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\.tensormine\config");
            }

            try
            {
                using (StreamWriter writer = new StreamWriter(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\.tensormine\config\setup.dat", false, System.Text.Encoding.Default))
                {
                    await writer.WriteLineAsync("Version: " + Version);
                    await writer.WriteLineAsync("RAM: " + RAM.ToString());
                    await writer.WriteLineAsync("Extra-graphics: " + ExtraGraphics.ToString());
                    await writer.WriteLineAsync("Cookie: " + Cookie.ToString());

                    writer.Close();
                }
            }
            catch { }

            if (Cookie == false)
            {
                if (File.Exists(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\.tensormine\saves\account.dat"))
                {
                    File.Delete(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\.tensormine\saves\account.dat");
                }
            }
            else
            {
                if (!File.Exists(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\.tensormine\saves\account.dat"))
                {
                    try
                    {
                        using (StreamWriter writer = new StreamWriter(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\.tensormine\saves\account.dat", false, System.Text.Encoding.Default))
                        {
                            await writer.WriteLineAsync("[" + Nickname + "]");
                            await writer.WriteLineAsync("[" + "" + "]");

                            writer.Close();
                        }
                    }
                    catch { }
                }
            }

            GetSession(false);

            Environment.Exit(0);
        }

        private void Window_Movement(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }

        private async void PageChanger(string Page)
        {
            DoubleAnimation ServersOpacityAnimation = new DoubleAnimation();
            DoubleAnimation ProfileOpacityAnimation = new DoubleAnimation();
            DoubleAnimation ShopOpacityAnimation = new DoubleAnimation();
            DoubleAnimation NewsOpacityAnimation = new DoubleAnimation();

            SetProcessWorkingSetSize(Process.GetCurrentProcess().Handle, -1, -1);

            switch (Page)
            {
                case "Servers":

                    if (Servers.Visibility == Visibility.Hidden)
                    {
                        ServersOpacityAnimation.From = Servers.Opacity;
                        ServersOpacityAnimation.To = 1;
                        ServersOpacityAnimation.Duration = TimeSpan.FromSeconds(0.3);
                        Servers.BeginAnimation(Border.OpacityProperty, ServersOpacityAnimation);
                    }
                    if (Profile.Visibility == Visibility.Visible)
                    {
                        ProfileOpacityAnimation.From = Profile.Opacity;
                        ProfileOpacityAnimation.To = 0;
                        ProfileOpacityAnimation.Duration = TimeSpan.FromSeconds(0.3);
                        Profile.BeginAnimation(Border.OpacityProperty, ProfileOpacityAnimation);
                    }
                    if (Shop.Visibility == Visibility.Visible)
                    {
                        ShopOpacityAnimation.From = Shop.Opacity;
                        ShopOpacityAnimation.To = 0;
                        ShopOpacityAnimation.Duration = TimeSpan.FromSeconds(0.3);
                        Shop.BeginAnimation(Border.OpacityProperty, ShopOpacityAnimation);
                    }
                    if (News.Visibility == Visibility.Visible)
                    {
                        NewsOpacityAnimation.From = Shop.Opacity;
                        NewsOpacityAnimation.To = 0;
                        NewsOpacityAnimation.Duration = TimeSpan.FromSeconds(0.3);
                        News.BeginAnimation(Border.OpacityProperty, NewsOpacityAnimation);
                    }

                    Servers.Visibility = Visibility.Visible;
                    Profile.Visibility = Visibility.Hidden;
                    Shop.Visibility = Visibility.Hidden;
                    News.Visibility = Visibility.Hidden;

                    Servers.IsEnabled = true;
                    Profile.IsEnabled = false;
                    Shop.IsEnabled = false;
                    News.IsEnabled = false;

                    if (OnlineCheckerStatus == false)
                    {
                        await Task.Run(() => OnlineChecker());
                    }

                    break;

                case "Profile":

                    if (Servers.Visibility == Visibility.Visible)
                    {
                        ServersOpacityAnimation.From = Servers.Opacity;
                        ServersOpacityAnimation.To = 0;
                        ServersOpacityAnimation.Duration = TimeSpan.FromSeconds(0.3);
                        Servers.BeginAnimation(Border.OpacityProperty, ServersOpacityAnimation);
                    }
                    if (Profile.Visibility == Visibility.Hidden)
                    {
                        ProfileOpacityAnimation.From = Profile.Opacity;
                        ProfileOpacityAnimation.To = 1;
                        ProfileOpacityAnimation.Duration = TimeSpan.FromSeconds(0.3);
                        Profile.BeginAnimation(Border.OpacityProperty, ProfileOpacityAnimation);
                    }
                    if (Shop.Visibility == Visibility.Visible)
                    {
                        ShopOpacityAnimation.From = Shop.Opacity;
                        ShopOpacityAnimation.To = 0;
                        ShopOpacityAnimation.Duration = TimeSpan.FromSeconds(0.3);
                        Shop.BeginAnimation(Border.OpacityProperty, ShopOpacityAnimation);
                    }
                    if (News.Visibility == Visibility.Visible)
                    {
                        NewsOpacityAnimation.From = Shop.Opacity;
                        NewsOpacityAnimation.To = 0;
                        NewsOpacityAnimation.Duration = TimeSpan.FromSeconds(0.3);
                        News.BeginAnimation(Border.OpacityProperty, NewsOpacityAnimation);
                    }

                    Servers.Visibility = Visibility.Hidden;
                    Profile.Visibility = Visibility.Visible;
                    Shop.Visibility = Visibility.Hidden;
                    News.Visibility = Visibility.Hidden;

                    Servers.IsEnabled = false;
                    Profile.IsEnabled = true;
                    Shop.IsEnabled = false;
                    News.IsEnabled = false;

                    break;

                case "Shop":
                    if (Servers.Visibility == Visibility.Visible)
                    {
                        ServersOpacityAnimation.From = Servers.Opacity;
                        ServersOpacityAnimation.To = 0;
                        ServersOpacityAnimation.Duration = TimeSpan.FromSeconds(0.3);
                        Servers.BeginAnimation(Border.OpacityProperty, ServersOpacityAnimation);
                    }
                    if (Profile.Visibility == Visibility.Visible)
                    {
                        ProfileOpacityAnimation.From = Profile.Opacity;
                        ProfileOpacityAnimation.To = 0;
                        ProfileOpacityAnimation.Duration = TimeSpan.FromSeconds(0.3);
                        Profile.BeginAnimation(Border.OpacityProperty, ProfileOpacityAnimation);
                    }
                    if (Shop.Visibility == Visibility.Hidden)
                    {
                        ShopOpacityAnimation.From = Shop.Opacity;
                        ShopOpacityAnimation.To = 1;
                        ShopOpacityAnimation.Duration = TimeSpan.FromSeconds(0.3);
                        Shop.BeginAnimation(Border.OpacityProperty, ShopOpacityAnimation);
                    }
                    if (News.Visibility == Visibility.Visible)
                    {
                        NewsOpacityAnimation.From = Shop.Opacity;
                        NewsOpacityAnimation.To = 0;
                        NewsOpacityAnimation.Duration = TimeSpan.FromSeconds(0.3);
                        News.BeginAnimation(Border.OpacityProperty, NewsOpacityAnimation);
                    }

                    Servers.Visibility = Visibility.Hidden;
                    Profile.Visibility = Visibility.Hidden;
                    Shop.Visibility = Visibility.Visible;
                    News.Visibility = Visibility.Hidden;

                    Servers.IsEnabled = false;
                    Profile.IsEnabled = false;
                    Shop.IsEnabled = true;
                    News.IsEnabled = false;

                    break;

                case "News":
                    if (Servers.Visibility == Visibility.Visible)
                    {
                        ServersOpacityAnimation.From = Servers.Opacity;
                        ServersOpacityAnimation.To = 0;
                        ServersOpacityAnimation.Duration = TimeSpan.FromSeconds(0.3);
                        Servers.BeginAnimation(Border.OpacityProperty, ServersOpacityAnimation);
                    }
                    if (Profile.Visibility == Visibility.Visible)
                    {
                        ProfileOpacityAnimation.From = Profile.Opacity;
                        ProfileOpacityAnimation.To = 0;
                        ProfileOpacityAnimation.Duration = TimeSpan.FromSeconds(0.3);
                        Profile.BeginAnimation(Border.OpacityProperty, ProfileOpacityAnimation);
                    }
                    if (Shop.Visibility == Visibility.Visible)
                    {
                        ShopOpacityAnimation.From = Shop.Opacity;
                        ShopOpacityAnimation.To = 0;
                        ShopOpacityAnimation.Duration = TimeSpan.FromSeconds(0.3);
                        Shop.BeginAnimation(Border.OpacityProperty, ShopOpacityAnimation);
                    }
                    if (News.Visibility == Visibility.Hidden)
                    {
                        NewsOpacityAnimation.From = Shop.Opacity;
                        NewsOpacityAnimation.To = 1;
                        NewsOpacityAnimation.Duration = TimeSpan.FromSeconds(0.3);
                        News.BeginAnimation(Border.OpacityProperty, NewsOpacityAnimation);
                    }

                    Servers.Visibility = Visibility.Hidden;
                    Profile.Visibility = Visibility.Hidden;
                    Shop.Visibility = Visibility.Hidden;
                    News.Visibility = Visibility.Visible;

                    Servers.IsEnabled = false;
                    Profile.IsEnabled = false;
                    Shop.IsEnabled = false;
                    News.IsEnabled = true;

                    break;
            }
        }

        private void Launcher_ProgressChanged(DownloadFileChangedEventArgs e)
        {
            if (e.ProgressedFileCount == 0 && e.TotalFileCount == 0)
            {
                Dispatcher.BeginInvoke(DispatcherPriority.Background, new Action(() => ProgressMinecraftTextBlock.Text = "Загрузка..."));
            }
            else if (e.ProgressedFileCount == 0 && e.TotalFileCount == 1)
            {
                Dispatcher.BeginInvoke(DispatcherPriority.Background, new Action(() => ProgressMinecraftTextBlock.Text = "Загрузка..."));
            }
            else if (e.TotalFileCount / 100 != 0)
            {
                Dispatcher.BeginInvoke(DispatcherPriority.Background, new Action(() => ProgressMinecraftTextBlock.Text = (e.ProgressedFileCount / (e.TotalFileCount / 100)).ToString() + "%"));

                if (e.ProgressedFileCount / (e.TotalFileCount / 100) > 100)
                {
                    Dispatcher.BeginInvoke(DispatcherPriority.Background, new Action(() => ProgressMinecraftTextBlock.Text = "Загрузка..."));
                }
            }
        }

        private void NavigationServersButton_Click(object sender, RoutedEventArgs e)
        {
            PageChanger("Servers");
        }

        private void NavigationProfileButton_Click(object sender, RoutedEventArgs e)
        {
            PageChanger("Profile");
        }

        private void NavigationShopButton_Click(object sender, RoutedEventArgs e)
        {
            PageChanger("Shop");
        }

        private void NavigationNewsButton_Click(object sender, RoutedEventArgs e)
        {
            PageChanger("News");
        }

        private void ServersVanillaButton_Click(object sender, RoutedEventArgs e)
        {
            StartClient("Vanilla");
        }

        private void ServersHardcoreButton_Click(object sender, RoutedEventArgs e)
        {
            StartClient("Hardcore");
        }

        private void ServersMinigamesButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void NavigationExitButton_Click(object sender, RoutedEventArgs e)
        {
            BlurEffect blur = new BlurEffect();
            blur.Radius = 5;
            this.Effect = blur;

            ExitWindow exitWindow = new ExitWindow();
            exitWindow.Owner = this;
            exitWindow.ShowDialog();
        }

        private void SettingsButton_Click(object sender, RoutedEventArgs e)
        {
            BlurEffect blur = new BlurEffect();
            blur.Radius = 5;
            this.Effect = blur;

            SettingsWindow settingsWindow = new SettingsWindow();
            settingsWindow.Owner = this;


            if (Version.ToLower() == "optifine")
            {
                settingsWindow.VersionButton.IsChecked = true;
            }
            else
            {
                settingsWindow.VersionButton.IsChecked = false;
            }

            settingsWindow.RAMTextBox.Text = RAM.ToString();
            settingsWindow.ExtraGraphicsButton.IsChecked = ExtraGraphics;
            settingsWindow.CookieButton.IsChecked = Cookie;
            settingsWindow.ShowDialog();
            Int32.TryParse(settingsWindow.RAMTextBox.Text, out RAM);

            if (settingsWindow.VersionButton.IsChecked == true)
            {
                Version = "OptiFine";
            }
            else
            {
                Version = "Classic";
            }

            if (RAM > 4096 || RAM < 512)
            {
                RAM = 1024;
            }

            if (settingsWindow.ExtraGraphicsButton.IsChecked == true)
            {
                ExtraGraphics = true;
            }
            else
            {
                ExtraGraphics = false;
            }

            if (settingsWindow.CookieButton.IsChecked == true)
            {
                Cookie = true;
            }
            else
            {
                Cookie = false;
            }

        }

        private void DiscordButton_Click(object sender, RoutedEventArgs e)
        {
            Process.Start(DiscordURL);
        }

        private bool GetSession(bool TYPE)
        {
            bool Status = false;

            try
            {
                using (var webClient = new WebClient())
                {
                    Status = Convert.ToBoolean(webClient.DownloadString("https://tensormine.ru/launcher/profile/session.php?nickname=" + Nickname + "&type=" + TYPE));
                }
            }
            catch
            {
                Status = false;
            }

            return Status;
        }

        private async void StartClient(string Location)
        {
            if (Nickname == "" || Nickname == null || VanillaIp == "" || VanillaIp == null || HardcoreIp == "" || HardcoreIp == null)
            {
                Environment.Exit(0);
            }
            else
            {
                if (Location == "" || Location == null)
                {
                    Environment.Exit(0);
                }
                else
                {
                    bool Session = GetSession(true);

                    if (Session == true)
                    {
                        this.IsEnabled = false;

                        if (File.Exists(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\.tensormine\config\servers.dat"))
                        {
                            File.Copy(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\.tensormine\config\servers.dat", MinecraftPATH + @"\servers.dat", true);
                        }

                        ServicePointManager.DefaultConnectionLimit = 1000;

                        var launchOption = new MLaunchOption();

                        if (Location == "Vanilla")
                        {
                            launchOption = new MLaunchOption
                            {
                                ServerIp = VanillaIp,
                                ServerPort = VanillaPort,
                                MaximumRamMb = RAM,
                                Session = MSession.GetOfflineSession(Nickname),
                                VersionType = "TensorMine",
                                GameLauncherName = "TensorMine"
                            };
                        }
                        else if (Location == "Hardcore")
                        {
                            launchOption = new MLaunchOption
                            {
                                ServerIp = HardcoreIp,
                                ServerPort = HardcorePort,
                                MaximumRamMb = RAM,
                                Session = MSession.GetOfflineSession(Nickname),
                                VersionType = "TensorMine",
                                GameLauncherName = "TensorMine"
                            };
                        }
                        else
                        {
                            Environment.Exit(0);
                        }

                        launcher.FileChanged += Launcher_ProgressChanged;

                        BlurEffect blur = new BlurEffect();
                        blur.Radius = 5;
                        Servers.Effect = blur;

                        DoubleAnimation disappearance = new DoubleAnimation();
                        disappearance.From = 0;
                        disappearance.To = 1;
                        disappearance.Duration = TimeSpan.FromSeconds(0.5);
                        ProgressMinecraft.BeginAnimation(Border.OpacityProperty, disappearance);

                        ProgressMinecraft.Visibility = Visibility.Visible;

                        if (Version.ToLower() == "optifine")
                        {
                            minecraftProcess = await launcher.CreateProcessAsync("OptiFine 1.16.5", launchOption);
                        }
                        else
                        {
                            minecraftProcess = await launcher.CreateProcessAsync("1.16.5", launchOption);
                        }

                        DoubleAnimation disappearanceWindow = new DoubleAnimation();
                        disappearanceWindow.From = 1;
                        disappearanceWindow.To = 0;
                        disappearanceWindow.Duration = TimeSpan.FromSeconds(0.5);
                        this.BeginAnimation(Window.OpacityProperty, disappearanceWindow);

                        await Task.Delay(500);

                        minecraftProcess.Start();

                        ProgressMinecraft.Opacity = 0;
                        ProgressMinecraft.Visibility = Visibility.Hidden;
                        this.WindowState = WindowState.Minimized;
                        this.ShowInTaskbar = false;

                        while (true)
                        {
                            await Task.Delay(50);

                            if (minecraftProcess.HasExited)
                            {
                                break;
                            }
                        }

                        Servers.Effect = null;

                        this.WindowState = WindowState.Normal;
                        this.ShowInTaskbar = true;

                        disappearanceWindow.From = 0;
                        disappearanceWindow.To = 1;
                        disappearanceWindow.Duration = TimeSpan.FromSeconds(0.1);
                        this.BeginAnimation(Window.OpacityProperty, disappearanceWindow);


                        if (File.Exists(MinecraftPATH + @"\servers.dat"))
                        {
                            File.Delete(MinecraftPATH + @"\servers.dat");
                        }

                        GetSession(false);

                        this.IsEnabled = true;
                    }
                }
            }
        }

        private void ServersVanillaButton_MouseEnter(object sender, MouseEventArgs e)
        {
            this.Cursor = ((TextBlock)this.Resources["FocusCursor"]).Cursor;
        }

        private void ServersVanillaButton_MouseLeave(object sender, MouseEventArgs e)
        {
            this.Cursor = ((TextBlock)this.Resources["MainCursor"]).Cursor;
        }

        private void ServersHardcoreButton_MouseEnter(object sender, MouseEventArgs e)
        {
            this.Cursor = ((TextBlock)this.Resources["FocusCursor"]).Cursor;
        }

        private void ServersHardcoreButton_MouseLeave(object sender, MouseEventArgs e)
        {
            this.Cursor = ((TextBlock)this.Resources["MainCursor"]).Cursor;
        }

    }
}
