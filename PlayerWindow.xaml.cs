using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Threading;
using Microsoft.Web.WebView2.Core;

namespace WpfWebRTCPlayer
{
    /// <summary>
    /// Interaction logic for PlayerWindow.xaml
    /// </summary>
    public partial class PlayerWindow : Window
    {
        public PlayerWindow()
        {
            //Environment.SetEnvironmentVariable("WEBVIEW2_ADDITIONAL_BROWSER_ARGUMENTS", "--autoplay-policy=no-user-gesture-required --disable-gpu-driver-bug-workarounds --ignore-gpu-blocklist");
            InitializeComponent();
        }

        private async void web_player_Initialized(object sender, EventArgs e)
        {
            var options = new CoreWebView2EnvironmentOptions("--autoplay-policy=no-user-gesture-required --disable-gpu-driver-bug-workarounds --ignore-gpu-blocklist");
            var env = await CoreWebView2Environment.CreateAsync(null, null, options);
            try
            {
                await web_player.EnsureCoreWebView2Async(env);
            }
            catch (Exception) { }

            if (Properties.Settings.Default.set_playAuto)
            {
                playLive();
            }
        }

        private async void web_player_Loaded(object sender, RoutedEventArgs e)
        {
            
        }

        public async void playLive()
        {
            var html = @"
            <!DOCTYPE html>
            <html lang=""en"">
            <head>
                <meta charset=""UTF-8"">
                <meta http-equiv=""X-UA-Compatible"" content=""IE=edge"">
                <meta name=""viewport"" content=""width=device-width, initial-scale=1.0"">
                <title>OvenPlayer</title>
                <style>* {cursor: none; direction: rtl; font-family: segoe ui;}</style>
            </head>
            <body>
                <div id=""player_id""></div>

                <script>" + Properties.Resources.ovenplayer + @"</script>

                <script>
                    // Initialize OvenPlayer
                    var playSettings = {
                        sources: [
                            {
                                label: 'label_for_webrtc',
                                // Set the type to 'webrtc'
                                type: 'webrtc',
                                // Set the file to WebRTC Signaling URL with OvenMediaEngine 
                                file: '" + Properties.Settings.Default.set_serverAddress + @"'
                            }
                        ],
                        expandFullScreenUI: false,
                        controls: false,
                        showBigPlayButton: false,
                        autoStart: true,
                    };
                    var player = OvenPlayer.create('player_id', playSettings);
                    player.toggleFullScreen();


                    setInterval(function () {
                        if (OvenPlayer.getPlayerByIndex(0).getState() == 'error') {
                            if(OvenPlayer.getPlayerByIndex(0)){
                                OvenPlayer.getPlayerByIndex(0).remove();
                            }
                            player = OvenPlayer.create('player_id', playSettings);
                            player.toggleFullScreen();
                        }
                    }, 2000);

                </script>
            </body>
            </html>
            ";

            web_player.NavigateToString(html);
            web_player.CoreWebView2.Settings.AreBrowserAcceleratorKeysEnabled = false;
            web_player.CoreWebView2.Settings.AreDefaultContextMenusEnabled = false;
        }

        public async void stopLive()
        {
            web_player.NavigateToString("");
        }

        private void win_player_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            Dispatcher.Invoke(() =>
            {
                Properties.Settings.Default.set_playerWidth = (int)this.Width - 10;
                Properties.Settings.Default.set_playerHeight = (int)this.Height - 10;
            });
        }

        private void ResizeGripper_H_DragDelta(object sender, DragDeltaEventArgs e)
        {
            Width += e.HorizontalChange;
        }
        private void ResizeGripper_V_DragDelta(object sender, DragDeltaEventArgs e)
        {
            Height += e.VerticalChange;
        }
    }
}
