using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
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
using System.Windows.Threading;

namespace Waves.Horus.Screens
{
    /// <summary>
    /// Interaction logic for Collect.xaml
    /// </summary>
    public partial class Collect : Window
    {
        public Collect()
        {
            InitializeComponent();
        }

        public string XamppLocation { get; private set; } = "";

        private DispatcherTimer checkStatus = new DispatcherTimer();

        public void StartApache()
        {
            Cmd_fire.Caption = ". . .";
            Process process = new Process();
            process.StartInfo.FileName = XamppLocation+"\\apache\\bin\\httpd.exe";
            process.StartInfo.Arguments = "";
            process.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
            process.Start();
        }

        public void StopApache()
        {
            Cmd_fire.Caption = ". . .";
            string path = "taskkill.exe";
            string toKill = "-im \"httpd.exe\" -t -f";
            var killer = new ProcessStartInfo();
            killer.WorkingDirectory = Environment.GetFolderPath(Environment.SpecialFolder.System);
            killer.Arguments = toKill;
            killer.FileName = path;
            killer.UseShellExecute = false;

            Process killP = new Process();
            killP.StartInfo = killer;
            killP.Start();
            killP.WaitForExit();
        }

        private void Win_collect_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                Microsoft.Win32.RegistryKey loadReg = Microsoft.Win32.Registry.LocalMachine.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Uninstall\\xampp");
                XamppLocation = (string)loadReg.GetValue("InstallLocation", "");
            }
            catch { }

            //STOP 255,25,25
            //START 25,255,90
            Cmd_fire.IsSmall = true;
            Cmd_fire.SetBackground(Colors.Start);
            Cmd_fire.Caption = "INICIAR";
            Cmd_fire.Action = StartApache;

            Cmd_panel.IsSmall = true;
            Cmd_panel.Caption = "Painel";

            Cmd_apacheExplore.IsSmall = true;
            Cmd_apacheExplore.Caption = "Explorar";

            Cmd_appInstall.IsSmall = true;
            Cmd_appInstall.Caption = "Instalar";
            Cmd_appInstall.SetBackground(Colors.Start);

            Cmd_appModify.IsSmall = true;
            Cmd_appModify.Caption = "Modify";

            Cmd_appExplore.IsSmall = true;
            Cmd_appExplore.Caption = "Explore";

            /*--------------*/
            Cmd_fireChrome .IsSmall = true;
            Cmd_fireChrome.SetBackground(Colors.Stop);
            Cmd_fireChrome.Caption = "STOP";

            Cmd_clear.IsSmall = true;
            Cmd_clear.Caption = "Clear";

            Cmd_chromeExplore.IsSmall = true;
            Cmd_chromeExplore.Caption = "Explore";

            Cmd_fireClient.IsSmall = true;
            Cmd_fireClient.SetBackground(Colors.Stop);
            Cmd_fireClient.Caption = "UNISTALL";

            Cmd_clientModify .IsSmall = true;
            Cmd_clientModify.Caption = "Modify";

            Cmd_clientExplore.IsSmall = true;
            Cmd_clientExplore.Caption = "Explore";

            Cmd_done.IsSmall = true;
            Cmd_done.Caption = "DONE!";
            Cmd_done.SetBackground(Color.FromRgb(25, 143, 255));

                        Cmd_explore.IsSmall = true;
            Cmd_explore.Caption = "Explore";

            Cmd_move.IsSmall = true;
            Cmd_move.Caption = "Move";
            Cmd_move.IsEnabled = false;

            Cmd_cancel.IsSmall = true;
            Cmd_cancel.Caption = "Cancel";
            Cmd_cancel.SetBackground(Colors.Stop);

            //Define Timer de verificações de status
            checkStatus.Interval = TimeSpan.FromSeconds(3);
            checkStatus.Tick += CheckStatus_Tick;
            checkStatus.Start();
        }

        private void CheckStatus_Tick(object sender, EventArgs e)
        {
            WebRequest requestServer = WebRequest.Create("http://127.0.0.1");
            try
            {
                HttpWebResponse response = (HttpWebResponse)requestServer.GetResponse();
                //STOP 255,25,25
                //START 25,255,90
                //ONLINE 0,160,0
                //OFFLINE 160,0,0
                Txb_serverStatus.Content = "ONLINE";
                Brd_serverStatus.Background = new SolidColorBrush(Colors.Online);
                Cmd_fire.SetBackground(Colors.Stop);
                Cmd_fire.Caption = "PARAR";
                Cmd_fire.Action = StopApache;
                string serverTech = response.Headers.Get("Server");
                Txb_serverVersion.Content = serverTech;
                try
                {
                    WebRequest request = WebRequest.Create("http://127.0.0.1/horus/itamarati/verify.php");
                    response = (HttpWebResponse)request.GetResponse();
                    if (response.StatusCode == HttpStatusCode.OK)
                    {
                        //read request
                        string serverData = new StreamReader(response.GetResponseStream()).ReadToEnd();
                        //set horus status
                        Txb_appVersion.Content = serverData.Substring(serverData.IndexOf("Horus"));
                        Txb_appStatus.Content = "ONLINE";
                        Brd_appStatus.Background = new SolidColorBrush(Colors.Online);
                        Cmd_appInstall.SetBackground(Colors.Stop);
                        Cmd_appInstall.Caption = "Desinstalar";
                        WebRequest requestPath = WebRequest.Create("http://127.0.0.1/horus/itamarati/pathfinder.php");
                        response = (HttpWebResponse)request.GetResponse();
                        if (response.StatusCode == HttpStatusCode.OK)
                        {
                        }
                    }
                }
                catch
                {
                    Txb_appStatus.Content = "OFFLINE";
                    Brd_appStatus.Background = new SolidColorBrush(Colors.Offline);
                    Cmd_appInstall.SetBackground(Colors.Start);
                    Cmd_appInstall.Caption = "Instalar";
                }
            }
            catch
            {
                //SERVER IS OFFLINE

                //APACHE
                Txb_serverStatus.Content = "OFFLINE";
                Brd_serverStatus.Background = new SolidColorBrush(Colors.Offline);
                Cmd_fire.SetBackground(Colors.Start);
                Cmd_fire.Caption = "INICIAR";
                Cmd_fire.Action = StartApache;

                //Horus
                Txb_appStatus.Content = "OFFLINE";
                Brd_appStatus.Background = new SolidColorBrush(Colors.Offline);
                Cmd_appInstall.SetBackground(Colors.Start);
                Cmd_appInstall.Caption = "Instalar";

            }
        }
    }
}
