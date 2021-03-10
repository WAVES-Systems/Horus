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

namespace Waves.Horus.Screens
{
    /// <summary>
    /// Interaction logic for Home.xaml
    /// </summary>
    public partial class Home : Window
    {
        public Home()
        {
            InitializeComponent();
        }

        private void Window_ContentRendered(object sender, EventArgs e)
        {

        }
        private void Cmd_new_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            NewProject newproj = new NewProject();
            newproj.Show();
            this.Close();
        }

        private void Win_home_Loaded(object sender, RoutedEventArgs e)
        {
            Cmd_new.Caption = "Novo...";
            Cmd_open.Caption = "Abrir";
            Cmd_tools.Caption = "Ferramentas";
            Cmd_about.Caption = "Sobre...";
            Cmd_puc.Ies = Controls.IesButton.IES.PucRio;
            Cmd_uerj.Ies = Controls.IesButton.IES.Uerj;
            Cmd_unirio.Ies = Controls.IesButton.IES.Unirio;
        }

        private void Cmd_open_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Microsoft.Win32.OpenFileDialog openFileDialog = new Microsoft.Win32.OpenFileDialog();
            openFileDialog.Filter = "Projeto do Horus |*.hrp";
            openFileDialog.DefaultExt = ".hrp";
            openFileDialog.InitialDirectory = Registry.Get("OpenProjectLocation", "");
            Nullable<bool> openResult = openFileDialog.ShowDialog();
            if (openResult ?? false)
            {
                Registry.Set("OpenProjectLocation", System.IO.Path.GetDirectoryName(openFileDialog.FileName));
                Project novo = Project.Load(openFileDialog.FileName);
                App.currentProject = novo;
                App.currentProjectPath = openFileDialog.FileName;
                OpenProject openproject = new OpenProject();
                openproject.Show();
                this.Close();
            }
        }
    }
}
