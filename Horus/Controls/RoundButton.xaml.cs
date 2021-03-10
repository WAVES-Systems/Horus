using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;

namespace Waves.Horus.Controls
{
    /// <summary>
    /// Interaction logic for RoundButton.xaml
    /// </summary>
    public partial class RoundButton : UserControl
    {
        public RoundButton()
        {
            InitializeComponent();
        }

        public Action Action { get; set;}

        private bool isEnabled = true;

        public bool IsEnabled
        {
            get
            {
                return isEnabled;
            }
            set
            {
                isEnabled = value;
                if (isEnabled)
                {
                    this.Opacity = 1;
                }
                else
                {
                    this.Opacity = 0.25;
                }
            }
        }

        private string caption;

        private bool isSmall = false;

        public bool IsSmall
        {
            get
            {
                return isSmall;
            }
            set
            {
                if (value)
                {
                    Txb_caption.FontSize = 14;
                    Brd_border.BorderThickness = new Thickness(2);
                    Brd_background.CornerRadius = Brd_border.CornerRadius = new CornerRadius(15);
                }
                else
                {
                    Txb_caption.FontSize = 17;
                    Brd_border.BorderThickness = new Thickness(3);
                    Brd_background.CornerRadius = Brd_border.CornerRadius = new CornerRadius(22);
                }
            }
        }
        public bool isColored { get; private set; } = false;
        public Color BackgroundColor { get; set; } = Color.FromRgb(255,255,255);
        public void SetBackground(Color color)
        {
            isColored = true;
            BackgroundColor = color;
            Brd_background.Background = new SolidColorBrush(BackgroundColor);
            Txb_caption.Foreground = new SolidColorBrush(Color.FromRgb(255, 255, 255));
            Brd_background.Opacity = 0.8;
            //Brd_background.BorderBrush = new SolidColorBrush(BackgroundColor);
            //Brd_background.BorderThickness = Brd_border.BorderThickness;
            Brd_border.Visibility = Visibility.Collapsed;

        }

        public string Caption {
            get
            {
                return caption;
            }
            set
            {
                Txb_caption.Text = value;
                caption = value;
            }
        }

        private void Uct_round_MouseEnter(object sender, MouseEventArgs e)
        {
            if (isEnabled)
            {
                if (!isColored)
                {
                    //ANIMATION DO TEXTO
                    ColorAnimation colorChangeAnimation = new ColorAnimation();
                    colorChangeAnimation.From = Color.FromRgb(255, 255, 255);
                    colorChangeAnimation.To = Color.FromRgb(0, 0, 0);
                    colorChangeAnimation.Duration = TimeSpan.FromMilliseconds(100);
                    PropertyPath colorTargetPath = new PropertyPath("(Control.Foreground).(SolidColorBrush.Color)");
                    Storyboard CellBackgroundChangeStory = new Storyboard();
                    Storyboard.SetTarget(colorChangeAnimation, Txb_caption);
                    Storyboard.SetTargetProperty(colorChangeAnimation, colorTargetPath);
                    CellBackgroundChangeStory.Children.Add(colorChangeAnimation);
                    CellBackgroundChangeStory.Begin();
                }

                //ANIMATION DO BORDER
                DoubleAnimation animation = new DoubleAnimation();
                if (isColored)
                {
                    animation.To = 0.6;
                }
                else
                {
                    animation.To = 0.8;
                }
                //animation.From = 1;
                animation.Duration = TimeSpan.FromMilliseconds(100);
                //animation.EasingFunction = new QuadraticEase();
                Storyboard sb = new Storyboard();
                sb.Children.Add(animation);
                Storyboard.SetTarget(sb, Brd_background);
                Storyboard.SetTargetProperty(sb, new PropertyPath(Control.OpacityProperty));
                sb.Begin();
                //Brd_background.Opacity = 0.2;
                Brd_border.Opacity = 0;
            }
        }

        private void Uct_round_MouseLeave(object sender, MouseEventArgs e)
        {
            if (isEnabled)
            {
                if (!isColored)
                {
                    ColorAnimation colorChangeAnimation = new ColorAnimation();
                    colorChangeAnimation.From = Color.FromRgb(0, 0, 0);
                    colorChangeAnimation.To = Color.FromRgb(205, 205, 205);
                    colorChangeAnimation.Duration = TimeSpan.FromMilliseconds(100);
                    PropertyPath colorTargetPath = new PropertyPath("(Control.Foreground).(SolidColorBrush.Color)");
                    Storyboard CellBackgroundChangeStory = new Storyboard();
                    Storyboard.SetTarget(colorChangeAnimation, Txb_caption);
                    Storyboard.SetTargetProperty(colorChangeAnimation, colorTargetPath);
                    CellBackgroundChangeStory.Children.Add(colorChangeAnimation);
                    CellBackgroundChangeStory.Begin();
                }

                DoubleAnimation animation = new DoubleAnimation();
                if (!isColored)
                {
                    animation.To = 0;
                    Brd_border.Opacity = 0.8;
                }
                else
                {
                    animation.To = 0.8;
                    Brd_border.Opacity = 0;
                }
                animation.Duration = TimeSpan.FromMilliseconds(100);
                //animation.EasingFunction = new QuadraticEase();
                Storyboard sb = new Storyboard();
                sb.Children.Add(animation);
                Storyboard.SetTarget(sb, Brd_background);
                Storyboard.SetTargetProperty(sb, new PropertyPath(Control.OpacityProperty));
                sb.Begin();
            }
        }

        private void Uct_round_GotFocus(object sender, RoutedEventArgs e)
        {
            if (isEnabled)
            {
                Brd_background.Opacity = 0.5;
                Brd_border.Opacity = 0;
            }
        }

        private void Uct_round_LostFocus(object sender, RoutedEventArgs e)
        {
            if (isEnabled)
            {
                if (!isColored)
                {
                    Brd_background.Opacity = Brd_border.Opacity = 0;
                }
                else
                {
                    Brd_background.Opacity = Brd_border.Opacity = 0.8;
                }
            }
        }

        private void Uct_round_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (isEnabled)
            {
                DoubleAnimation animation = new DoubleAnimation();
                animation.To = Brd_border.Opacity = 1;
                animation.Duration = TimeSpan.FromMilliseconds(100);
                //animation.EasingFunction = new QuadraticEase();
                Storyboard sb = new Storyboard();
                sb.Children.Add(animation);
                Storyboard.SetTarget(sb, Brd_background);
                Storyboard.SetTargetProperty(sb, new PropertyPath(Control.OpacityProperty));
                sb.Begin();
            }
        }
        bool clickController = false;
        private void Uct_round_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (!clickController)
            {
                clickController = true;
                if (Action != null)
                {
                    Action();
                }
            }
        }

        private void Uct_round_MouseUp(object sender, MouseButtonEventArgs e)
        {
            clickController = false;
        }
    }
}
