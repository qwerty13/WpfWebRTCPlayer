using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace WpfWebRTCPlayer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        PlayerWindow myPW;
        DispatcherTimer closeTimer = new DispatcherTimer();
        

        public MainWindow()
        {
            InitializeComponent();
            lbl_version.Text = $"(Build { new FileInfo(Assembly.GetExecutingAssembly().Location).LastWriteTime.ToString("dd/MM/yyyy HH:mm:ss")})";
        }

        private void win_main_Loaded(object sender, RoutedEventArgs e)
        {
            myPW = new PlayerWindow();
            //myPW.Width = Properties.Settings.Default.set_playerWidth;
            //myPW.Height = Properties.Settings.Default.set_playerHeight;
            myPW.Top = 0;
            myPW.Left = 0;
            myPW.Show();

            Top = SystemParameters.PrimaryScreenHeight - Height - 50;
            Left = (SystemParameters.PrimaryScreenWidth/2) - (Width/2);
            //Top = myPW.Top + myPW.Height + 50;
            //Left = myPW.Left + myPW.Width + 50;

            closeTimer.Tick += new EventHandler(closeTimer_Tick);
            closeTimer.Interval = new TimeSpan(0, 0, 0, 1);
            closeTimer.Start();
        }

        private void win_main_Closed(object sender, EventArgs e)
        {
            Properties.Settings.Default.Save();
            Application.Current.Shutdown();
        }

        void closeTimer_Tick(object sender, EventArgs e)
        {
            if ((bool)chk_autoClose.IsChecked)
            {
                string originTime = String.Format("{0:00}:{1:00}", Properties.Settings.Default.set_closeHour, Properties.Settings.Default.set_closeMinute);
                DateTime nowDate = DateTime.Now;
                string nowTime = String.Format("{0:00}:{1:00}", nowDate.Hour, nowDate.Minute);
                if (originTime == nowTime)
                    Application.Current.Shutdown();
            }
        }

        private void btn_play_Click(object sender, RoutedEventArgs e)
        {
            Properties.Settings.Default.Save();
            myPW.playLive();
        }

        private void btn_stop_Click(object sender, RoutedEventArgs e)
        {
            myPW.stopLive();
        }

        private void chk_displayGrip_Click(object sender, RoutedEventArgs e)
        {
            myPW.win_player.ResizeMode = (bool)chk_displayGrip.IsChecked ? ResizeMode.CanResizeWithGrip : ResizeMode.NoResize;
            myPW.web_player.Margin = (bool)chk_displayGrip.IsChecked ? new Thickness(0, 0, 10, 10) : new Thickness(0, 0, 0, 0);
        }

        private void chk_autoClose_Click(object sender, RoutedEventArgs e)
        {
            txt_autoCloseHour.IsEnabled = (bool)!chk_autoClose.IsChecked;
            txt_autoCloseMinute.IsEnabled = (bool)!chk_autoClose.IsChecked;

            if ((bool)chk_autoClose.IsChecked)
            {

            }
        }

        private void chk_autoClose_PreviewMouseUp(object sender, MouseButtonEventArgs e)
        {
            if (!int.TryParse(txt_autoCloseHour.Text, out _) || !int.TryParse(txt_autoCloseMinute.Text, out _))
            {
                MessageBox.Show("Check clock parameters!");
                e.Handled = false;
                return;
            }
            if (int.Parse(txt_autoCloseHour.Text) < 0 || int.Parse(txt_autoCloseHour.Text) > 23)
            {
                MessageBox.Show("Clock hour is incorrect!\nEnter it between 0 and 23");
                e.Handled = false;
                return;
            }
            if (int.Parse(txt_autoCloseMinute.Text) < 0 || int.Parse(txt_autoCloseMinute.Text) > 59)
            {
                MessageBox.Show("Clock hour is incorrect!\nEnter it between 0 and 59");
                e.Handled = false;
                return;
            }
        }
    }
}
