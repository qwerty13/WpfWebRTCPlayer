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
            if (Properties.Settings.Default.set_closeEnable)
            {
                string originTime = String.Format("{0:00}:{1:00}:00", Properties.Settings.Default.set_closeHour, Properties.Settings.Default.set_closeMinute);
                DateTime nowDate = DateTime.Now;
                string nowTime = String.Format("{0:00}:{1:00}:{2:00}", nowDate.Hour, nowDate.Minute, nowDate.Second);
                if (originTime == nowTime)
                {
                    this.WindowState = WindowState.Minimized;
                    myPW.stopLive();
                    myPW.WindowState = WindowState.Minimized;
                    if (Properties.Settings.Default.set_closeExit) Application.Current.Shutdown();
                }
            }

            if (Properties.Settings.Default.set_openEnable)
            {
                string originTime = String.Format("{0:00}:{1:00}:00", Properties.Settings.Default.set_openHour, Properties.Settings.Default.set_openMinute);
                DateTime nowDate = DateTime.Now;
                string nowTime = String.Format("{0:00}:{1:00}:{2:00}", nowDate.Hour, nowDate.Minute, nowDate.Second);
                if (originTime == nowTime)
                {
                    this.WindowState = WindowState.Normal;
                    this.Show();
                    this.Focus();
                    myPW.WindowState = WindowState.Normal;
                    myPW.Show();
                    myPW.Focus();
                    myPW.playLive();
                }
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

        private void btn_schedule_Click(object sender, RoutedEventArgs e)
        {
            var win_schedule = new scheduleWindow();
            win_schedule.ShowDialog();
        }
    }
}
