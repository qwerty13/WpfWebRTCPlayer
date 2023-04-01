using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace WpfWebRTCPlayer
{
    /// <summary>
    /// Interaction logic for settingsWindow.xaml
    /// </summary>
    public partial class settingsWindow : Window
    {
        public settingsWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, EventArgs e)
        {
            Top = SystemParameters.PrimaryScreenHeight - Height - 50;
            Left = (SystemParameters.PrimaryScreenWidth / 2) - (Width / 2);
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            Properties.Settings.Default.Save();
        }

        private void autorunCheck(object sender, RoutedEventArgs e)
        {
            try
            {
                Microsoft.Win32.RegistryKey key = Microsoft.Win32.Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true);
                Assembly curAssembly = Assembly.GetExecutingAssembly();
                if ((bool)chk_autorunEnable.IsOn) 
                    key.SetValue(curAssembly.GetName().Name, curAssembly.Location);
                else
                    key.DeleteValue(curAssembly.GetName().Name);
            }
            catch(Exception) { }
        }
    }
}
