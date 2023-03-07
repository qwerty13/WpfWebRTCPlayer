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
    /// Interaction logic for scheduleWindow.xaml
    /// </summary>
    public partial class scheduleWindow : Window
    {
        public scheduleWindow()
        {
            InitializeComponent();
        }

        private void chk_autoClose_Click(object sender, RoutedEventArgs e)
        {
            //txt_autoCloseHour.IsEnabled = (bool)!chk_autoCloseEnable.IsChecked;
            //txt_autoCloseMinute.IsEnabled = (bool)!chk_autoCloseEnable.IsChecked;

            //if ((bool)chk_autoCloseEnable.IsChecked)
            //{

            //}
        }

        private void chk_autoClose_PreviewMouseUp(object sender, MouseButtonEventArgs e)
        {
            //if (!int.TryParse(txt_autoCloseHour.Text, out _) || !int.TryParse(txt_autoCloseMinute.Text, out _))
            //{
            //    MessageBox.Show("Check clock parameters!");
            //    e.Handled = false;
            //    return;
            //}
            //if (int.Parse(txt_autoCloseHour.Text) < 0 || int.Parse(txt_autoCloseHour.Text) > 23)
            //{
            //    MessageBox.Show("Clock hour is incorrect!\nEnter it between 0 and 23");
            //    e.Handled = false;
            //    return;
            //}
            //if (int.Parse(txt_autoCloseMinute.Text) < 0 || int.Parse(txt_autoCloseMinute.Text) > 59)
            //{
            //    MessageBox.Show("Clock hour is incorrect!\nEnter it between 0 and 59");
            //    e.Handled = false;
            //    return;
            //}
        }

        private void hourChecker(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            TextBox t1 = (TextBox)sender;
            Regex regex2 = new Regex("^([0-5]?[0-9])$");
            if (t1.Text.Length == 1)
            {
                string time = string.Format("{0}{1}", t1.Text, e.Text);
                e.Handled = !regex2.IsMatch(time);
                return;
            }
            if (t1.Text.Length > 1)
            {
                e.Handled = true;
                return;
            }
            e.Handled = regex.IsMatch(e.Text);
        }
        private void minuteChecker(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            TextBox t1 = (TextBox)sender;
            Regex regex2 = new Regex("^([0-5]?[0-9])$");
            if (t1.Text.Length == 1)
            {
                string time = string.Format("{0}{1}", t1.Text, e.Text);
                e.Handled = !regex2.IsMatch(time);
                return;
            }
            if (t1.Text.Length > 1)
            {
                e.Handled = true;
                return;
            }
            e.Handled = regex.IsMatch(e.Text);
        }

        private void autorunCheck(object sender, RoutedEventArgs e)
        {
            try
            {
                Microsoft.Win32.RegistryKey key = Microsoft.Win32.Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true);
                Assembly curAssembly = Assembly.GetExecutingAssembly();
                if ((bool)chk_autorunEnable.IsChecked) 
                    key.SetValue(curAssembly.GetName().Name, curAssembly.Location);
                else
                    key.DeleteValue(curAssembly.GetName().Name);
            }
            catch(Exception ex) { }
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            Properties.Settings.Default.Save();
        }
    }
}
