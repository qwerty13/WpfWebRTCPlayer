using System;
using System.Collections.Generic;
using System.Linq;
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

namespace WpfWebRTCPlayer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        PlayerWindow myPW;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void win_main_Loaded(object sender, RoutedEventArgs e)
        {
            myPW = new PlayerWindow();
            //myPW.Width = Properties.Settings.Default.set_playerWidth;
            //myPW.Height = Properties.Settings.Default.set_playerHeight;
            myPW.Top = 0;
            myPW.Left = 0;
            myPW.Show();

            Top = myPW.Top + myPW.Height + 50;
            Left = myPW.Left + myPW.Width + 50;
        }

        private void win_main_Closed(object sender, EventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void btn_play_Click(object sender, RoutedEventArgs e)
        {
            Properties.Settings.Default.Save();
            myPW.playLive();
        }

        private void btn_stop_Click(object sender, RoutedEventArgs e)
        {
            myPW.web_player.NavigateToString("");
        }
    }
}
