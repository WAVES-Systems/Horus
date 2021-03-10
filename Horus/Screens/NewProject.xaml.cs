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
    /// Interaction logic for NewProject.xaml
    /// </summary>
    public partial class NewProject : Window
    {
        public NewProject()
        {
            InitializeComponent();
        }
        bool showHome = true;
        private void Window_ContentRendered(object sender, EventArgs e)
        {
            this.WindowState = WindowState.Normal;
        }

        private void Win_newProject_Closed(object sender, EventArgs e)
        {

        }

        private void Win_newProject_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (showHome)
            {
                Screens.Home home = new Screens.Home();
                home.Show();
            }
        }

        private void Win_newProject_Loaded(object sender, RoutedEventArgs e)
        {
            Cmd_Criar.Caption = "Criar";
            Cmd_Cancelar.Caption = "Cancelar";
            Cmd_Criar.IsSmall = true;
            Cmd_Cancelar.IsSmall = true;
            Cmd_Criar.SetBackground(Colors.Blue);
        }

        private void Cmd_Cancelar_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            showHome = true;
            this.Close();
        }

        private void Txb_projectName_TextChanged(object sender, TextChangedEventArgs e)
        {
            string hashCode = Utils.StringToMd5(Txb_projectName.Text + DateTime.Now.ToString("h:mm:ss tt")).Substring(0, 16);
            string name = Utils.RemoveSpecialCharacters(Utils.AsciiCleaning(Txb_projectName.Text));
            if (name.Length > 16)
            {
                name = name.Substring(0, 16);
            }
            Txb_projectId.Text = (name +"-"+ hashCode).ToUpper();
        }

        private void Cmd_Criar_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (!(Utils.RemoveNonPathCharacters(Utils.AsciiCleaning(Txb_projectId.Text)).Equals(Txb_projectId.Text)) || Txb_projectId.Text.Equals(""))
            {
                MessageBox.Show("Ivalid ID!", "New project", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                Microsoft.Win32.SaveFileDialog saveFileDialog = new Microsoft.Win32.SaveFileDialog();
                saveFileDialog.Filter = "Projeto do Horus |*.hrp";
                saveFileDialog.FileName = Txb_projectId.Text;
                saveFileDialog.DefaultExt = ".hrp";
                saveFileDialog.InitialDirectory = Registry.Get("NewProjectLocation","");
                Nullable<bool> saveResult = saveFileDialog.ShowDialog();
                if (saveResult ?? false)
                {
                    Registry.Set("NewProjectLocation", System.IO.Path.GetDirectoryName(saveFileDialog.FileName));
                    Project novo = new Project(Txb_projectName.Text,Txb_projectDescription.Text, Txb_projectId.Text);
                    if(Project.Write(novo, saveFileDialog.FileName))
                    {
                        App.currentProject = novo;
                        App.currentProjectPath = saveFileDialog.FileName;
                        showHome = false;
                        OpenProject openproject = new OpenProject();
                        openproject.Show();
                        this.Close();
                    }
                }
            }
        }
    }
}
