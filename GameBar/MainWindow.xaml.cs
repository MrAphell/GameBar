using MaterialDesignThemes.Wpf;
using Microsoft.Win32;
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Gamebar
{
    public partial class MainWindow : Window
    {
        private Dictionary<string, string> gameLaunchers = new Dictionary<string, string>();

        private readonly PaletteHelper paletteHelper = new PaletteHelper();

        private Dictionary<string, StackPanel> iconPanels = new Dictionary<string, StackPanel>();


        public MainWindow()
        {
            InitializeComponent();
            ApplyTheme();
            UpdateGridLayout();
            SourceInitialized += (s, e) => ApplyDarkTitleBar();
        }

        private void ApplyDarkTitleBar()
        {
            var hwnd = new WindowInteropHelper(this).Handle;

            if (IsWindows10OrGreater(17763))
            {
                int darkMode = 1;
                DwmSetWindowAttribute(hwnd, DWMWA_USE_IMMERSIVE_DARK_MODE, ref darkMode, sizeof(int));
            }
        }

        [DllImport("dwmapi.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern int DwmSetWindowAttribute(IntPtr hwnd, int dwAttribute, ref int pvAttribute, int cbAttribute);

        private const int DWMWA_USE_IMMERSIVE_DARK_MODE = 20;

        private static bool IsWindows10OrGreater(int build = 17763)
        {
            return Environment.OSVersion.Platform == PlatformID.Win32NT &&
                   Environment.OSVersion.Version.Major >= 10 &&
                   Environment.OSVersion.Version.Build >= build;
        }

        private void UpdateGridLayout()
        {
            int totalPanels = 6;
            int columns = 3;
            int rows = (int)Math.Ceiling((double)totalPanels / columns);

            IconGrid.ColumnDefinitions.Clear();
            IconGrid.RowDefinitions.Clear();
            IconGrid.Children.Clear();
            iconPanels.Clear();

            for (int i = 0; i < columns; i++)
            {
                IconGrid.ColumnDefinitions.Add(new ColumnDefinition());
            }

            for (int i = 0; i < rows; i++)
            {
                IconGrid.RowDefinitions.Add(new RowDefinition());
            }

            List<string> launcherNames = new List<string>
            {
                "Steam", "Epic Games", "Xbox", "GOG Galaxy", "Ubisoft", "Battle.net"
            };

            int index = 0;
            foreach (var launcher in launcherNames)
            {
                StackPanel panel = new StackPanel
                {
                    Orientation = Orientation.Vertical,
                    HorizontalAlignment = HorizontalAlignment.Center
                };

                int row = index / columns;
                int col = index % columns;
                Grid.SetRow(panel, row);
                Grid.SetColumn(panel, col);
                IconGrid.Children.Add(panel);

                iconPanels[launcher] = panel;

                index++;
            }
        }

        private void btnDarkMode_Click(object sender, RoutedEventArgs e)
        {
            var theme = paletteHelper.GetTheme();
            var isDark = theme.GetBaseTheme() == BaseTheme.Dark;

            theme.SetBaseTheme(isDark ? BaseTheme.Light : BaseTheme.Dark);
            paletteHelper.SetTheme(theme);

            btnDarkMode.Content = isDark ? "🌙 Dark Mode" : "☀️ Light Mode";
        }

        private void ApplyTheme()
        {
            var theme = paletteHelper.GetTheme();
            var isDark = theme.GetBaseTheme() == BaseTheme.Dark;
            btnDarkMode.Content = isDark ? "☀️ Light Mode" : "🌙 Dark Mode";
        }

        private void btnFelderites_Click(object sender, RoutedEventArgs e)
        {
            Felderites();
        }

        private string GetLauncherPathFromRegistry(string registryKey, string valueName)
        {
            try
            {
                using (RegistryKey key = Registry.LocalMachine.OpenSubKey(registryKey))
                {
                    if (key != null)
                    {
                        object value = key.GetValue(valueName);
                        if (value != null)
                        {
                            return value.ToString();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Hiba a registry olvasásakor: {ex.Message}");
            }
            return null;
        }

        private string GetEpicGamesLauncherPath()
        {
            string[] possiblePaths =
            {
                @"C:\Program Files (x86)\Epic Games\Launcher\Portal\Binaries\Win64\EpicGamesLauncher.exe",
                @"D:\Program Files (x86)\Epic Games\Launcher\Portal\Binaries\Win64\EpicGamesLauncher.exe"
            };

            foreach (string path in possiblePaths)
            {
                if (File.Exists(path))
                {
                    return path;
                }
            }
            return null;
        }

        private void Felderites()
        {
            gameLaunchers.Clear();

            gameLaunchers["Steam"] = GetLauncherPathFromRegistry(@"SOFTWARE\WOW6432Node\Valve\Steam", "InstallPath") + @"\steam.exe";
            gameLaunchers["Epic Games"] = GetEpicGamesLauncherPath();
            gameLaunchers["GOG Galaxy"] = GetLauncherPathFromRegistry(@"SOFTWARE\WOW6432Node\GOG.com\GalaxyClient", "clientExe");
            gameLaunchers["Ubisoft"] = GetLauncherPathFromRegistry(@"SOFTWARE\WOW6432Node\Ubisoft\Launcher", "InstallDir") + @"\upc.exe";
            gameLaunchers["Battle.net"] = GetLauncherPathFromRegistry(@"SOFTWARE\WOW6432Node\Blizzard Entertainment\Battle.net", "InstallPath") + @"\Battle.net.exe";
            gameLaunchers["Xbox"] = GetLauncherPathFromRegistry(@"SOFTWARE\Microsoft\Windows\CurrentVersion\App Paths\XboxApp.exe", "");

            UpdateIcons();
        }

        private void UpdateIcons()
        {
            foreach (var panel in iconPanels.Values)
            {
                panel.Children.Clear();
            }

            foreach (var launcher in gameLaunchers)
            {
                string name = launcher.Key;
                string path = launcher.Value;

                bool isInstalled = !string.IsNullOrEmpty(path) && File.Exists(path);

                Border border = new Border()
                {
                    Background = Brushes.Transparent,
                    CornerRadius = new CornerRadius(5),
                    Padding = new Thickness(5)
                };

                StackPanel panel = new StackPanel()
                {
                    Orientation = Orientation.Vertical,
                    HorizontalAlignment = HorizontalAlignment.Center
                };

                Image icon = new Image()
                {
                    Width = 64,
                    Height = 64,
                    Margin = new Thickness(5),
                    Opacity = isInstalled ? 1.0 : 0.4
                };

                string baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
                string iconPath = Path.Combine(baseDirectory, @"Assets", $"{name}.png");

                if (!File.Exists(iconPath))
                {
                    iconPath = Path.Combine(baseDirectory, @"Assets", $"{name}.jpg");
                }

                if (File.Exists(iconPath))
                {
                    BitmapImage originalBitmap = new BitmapImage(new Uri(iconPath, UriKind.Absolute));

                    if (!isInstalled)
                    {
                        FormatConvertedBitmap grayBitmap = new FormatConvertedBitmap(originalBitmap, PixelFormats.Gray8, null, 0);
                        icon.Source = grayBitmap;
                    }
                    else
                    {
                        icon.Source = originalBitmap;
                    }
                }

                TextBlock label = new TextBlock()
                {
                    Text = name,
                    HorizontalAlignment = HorizontalAlignment.Center,
                    Foreground = isInstalled ? Brushes.Black : Brushes.Gray
                };

                if (isInstalled)
                {
                    icon.MouseDown += (sender, e) => { Process.Start(name == "Xbox" ? "explorer.exe" : path); };
                }

                panel.MouseEnter += (s, e) =>
                {
                    border.Background = Brushes.DimGray;
                    label.Foreground = Brushes.White;
                };

                panel.MouseLeave += (s, e) =>
                {
                    border.Background = Brushes.Transparent;
                    label.Foreground = Brushes.Black;
                };

                panel.Children.Add(icon);
                panel.Children.Add(label);
                border.Child = panel;

                if (iconPanels.ContainsKey(name))
                {
                    StackPanel targetPanel = iconPanels[name];
                    targetPanel.Children.Add(border);
                }
            }
        }

    }
}

