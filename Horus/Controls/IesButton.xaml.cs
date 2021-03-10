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
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Waves.Horus.Controls
{
    /// <summary>
    /// Interaction logic for IesButton.xaml
    /// </summary>
    public partial class IesButton : UserControl
    {
        public IesButton()
        {
            InitializeComponent();
        }

        public enum IES
        {
            Ufpa,
            PucRio,
            Uerj,
            Unirio
        }
        private IES currentIes = IES.Ufpa;
        public static string[] IesPath { get; } = { "/horus;component/Images/LogoGrayUFPA.png", "/horus;component/Images/LogoGrayPUCRIO.png", "/horus;component/Images/LogoGrayUERJ.png", "/horus;component/Images/LogoGrayUNIRIO.png" };

        public IES Ies { get
            {
                return currentIes;
            }
            set
            {
                currentIes = value;
                Img_ies.Source = new BitmapImage(new Uri(IesPath[(int)value], UriKind.RelativeOrAbsolute));
            }
        }

        private void Uct_ies_MouseEnter(object sender, MouseEventArgs e)
        {
            DoubleAnimation animation = new DoubleAnimation();
            animation.To = 0.75;
            //animation.From = 1;
            animation.Duration = TimeSpan.FromMilliseconds(100);
            //animation.EasingFunction = new QuadraticEase();
            Storyboard sb = new Storyboard();
            sb.Children.Add(animation);
            Storyboard.SetTarget(sb, Img_ies);
            Storyboard.SetTargetProperty(sb, new PropertyPath(Control.OpacityProperty));
            sb.Begin();
        }

        private void Uct_ies_MouseLeave(object sender, MouseEventArgs e)
        {
            DoubleAnimation animation = new DoubleAnimation();
            animation.To = 1;
            //animation.From = 1;
            animation.Duration = TimeSpan.FromMilliseconds(500);
            //animation.EasingFunction = new QuadraticEase();
            Storyboard sb = new Storyboard();
            sb.Children.Add(animation);
            Storyboard.SetTarget(sb, Img_ies);
            Storyboard.SetTargetProperty(sb, new PropertyPath(Control.OpacityProperty));
            sb.Begin();
        }

        private void Uct_ies_MouseDown(object sender, MouseButtonEventArgs e)
        {
            DoubleAnimation animation = new DoubleAnimation();
            animation.To = 0.5;
            animation.From = 1;
            animation.Duration = TimeSpan.FromMilliseconds(500);
            //animation.EasingFunction = new QuadraticEase();
            Storyboard sb = new Storyboard();
            sb.Children.Add(animation);
            Storyboard.SetTarget(sb, Img_ies);
            Storyboard.SetTargetProperty(sb, new PropertyPath(Control.OpacityProperty));
            sb.Begin();
        }
    }
}
