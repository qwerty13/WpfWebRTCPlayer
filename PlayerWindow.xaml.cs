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
using System.Windows.Shapes;
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

        private async void web_player_Loaded(object sender, RoutedEventArgs e)
        {
            if (Properties.Settings.Default.set_playAuto)
            {
                playLive();
            }
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
            </head>
            <body>
                <div id=""player_id""></div>

                <script>" + Properties.Resources.ovenplayer + @"</script>

                <script>
                    // Initialize OvenPlayer
                    const player = OvenPlayer.create('player_id', {
                        sources: [
                            {
                                label: 'label_for_webrtc',
                                // Set the type to 'webrtc'
                                type: 'webrtc',
                                // Set the file to WebRTC Signaling URL with OvenMediaEngine 
                                file: '" + Properties.Settings.Default.set_serverAddress + @"'
                            }
                        ],
                        expandFullScreenUI: true,
                        controls: false,
                        showBigPlayButton: false,
                        autoStart: true,
                    });
                    //setTimeout(player.play(), 1000);
                    player.toggleFullScreen();
                </script>
            </body>
            </html>
            ";

            var options = new CoreWebView2EnvironmentOptions("--autoplay-policy=no-user-gesture-required --disable-gpu-driver-bug-workarounds --ignore-gpu-blocklist");
            var env = await CoreWebView2Environment.CreateAsync(null, null, options);
            await web_player.EnsureCoreWebView2Async(env);
            web_player.NavigateToString(html);
        }
    }
}
