﻿using ModernWpf;
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
        DispatcherTimer closeTimer = new DispatcherTimer();

        public MainWindow()
        {
            InitializeComponent();
            //ThemeManager.Current.ApplicationTheme = ApplicationTheme.Light;
            lbl_version.Text = $"(Build { new FileInfo(Assembly.GetExecutingAssembly().Location).LastWriteTime.ToString("dd/MM/yyyy HH:mm:ss")})";
        }

        private void win_main_Loaded(object sender, RoutedEventArgs e)
        {
            Top = SystemParameters.PrimaryScreenHeight - Height - 50;
            Left = (SystemParameters.PrimaryScreenWidth/2) - (Width/2);

            GlobalValues.PlayerWindow = new PlayerWindow();
            GlobalValues.PlayerWindow.Top = 0;
            GlobalValues.PlayerWindow.Left = 0;

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
            DateTime nowDate = DateTime.Now;
            string nowTime = String.Format("{0:00}:{1:00}:{2:00}", nowDate.Hour, nowDate.Minute, nowDate.Second);

            if (Properties.Settings.Default.set_closeEnable)
            {
                string originTime = String.Format("{0:00}:{1:00}:00", Properties.Settings.Default.set_closeHour, Properties.Settings.Default.set_closeMinute);
                if (originTime == nowTime)
                {
                    if (Properties.Settings.Default.set_closeExit) Application.Current.Shutdown();
                    this.WindowState = WindowState.Minimized;
                    GlobalValues.PlayerWindow.stopLive();
                    GlobalValues.PlayerWindow.Visibility = Visibility.Hidden;
                }
            }
            if (Properties.Settings.Default.set_close2Enable)
            {
                string originTime = String.Format("{0:00}:{1:00}:00", Properties.Settings.Default.set_close2Hour, Properties.Settings.Default.set_close2Minute);
                if (originTime == nowTime)
                {
                    if (Properties.Settings.Default.set_close2Exit) Application.Current.Shutdown();
                    this.WindowState = WindowState.Minimized;
                    GlobalValues.PlayerWindow.stopLive();
                    GlobalValues.PlayerWindow.Visibility = Visibility.Hidden;
                }
            }

            if (Properties.Settings.Default.set_openEnable)
            {
                string originTime = String.Format("{0:00}:{1:00}:00", Properties.Settings.Default.set_openHour, Properties.Settings.Default.set_openMinute);
                if (originTime == nowTime)
                {
                    if (!Properties.Settings.Default.set_openJustPlayer)
                    {
                        this.Show();
                        this.WindowState = WindowState.Normal;
                    }
                    GlobalValues.PlayerWindow.Show();
                    GlobalValues.PlayerWindow.Visibility = Visibility.Visible;
                    GlobalValues.PlayerWindow.playLive();
                }
            }
            if (Properties.Settings.Default.set_open2Enable)
            {
                string originTime = String.Format("{0:00}:{1:00}:00", Properties.Settings.Default.set_open2Hour, Properties.Settings.Default.set_open2Minute);
                if (originTime == nowTime)
                {
                    if (!Properties.Settings.Default.set_open2JustPlayer)
                    {
                        this.Show();
                        this.WindowState = WindowState.Normal;
                    }
                    GlobalValues.PlayerWindow.Show();
                    GlobalValues.PlayerWindow.Visibility = Visibility.Visible;
                    GlobalValues.PlayerWindow.playLive();
                }
            }
        }

        private void btn_play_Click(object sender, RoutedEventArgs e)
        {
            Properties.Settings.Default.Save();
            GlobalValues.PlayerWindow.playLive();
        }

        private void btn_stop_Click(object sender, RoutedEventArgs e)
        {
            GlobalValues.PlayerWindow.stopLive();
        }

        private void btn_settings_Click(object sender, RoutedEventArgs e)
        {
            var win_settings = new settingsWindow();
            win_settings.ShowDialog();
        }

    }
}
