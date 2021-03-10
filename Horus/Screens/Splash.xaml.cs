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
    /// Interaction logic for Splash.xaml
    /// </summary>
    public partial class Splash : Window
    {
        public Splash()
        {
            InitializeComponent();
        }

        private void Win_Splash_ContentRendered(object sender, EventArgs e)
        {
            System.Threading.Thread.Sleep(4000);
            Registry.Initialize("Software\\WAVES Systems\\AIT2-UXT");
            Home home = new Home();
            home.Show();
            this.Close();
        }
    }
}
