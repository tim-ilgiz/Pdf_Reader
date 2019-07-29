using GosArchive.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
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

namespace GosArchive
{
    /// <summary>
    /// Логика взаимодействия для Page2.xaml
    /// </summary>
    public partial class Page2 : UserControl, INotifyPropertyChanged
    {
        
        private int number { get; set; } = 0; 
        public MapObject mapObject;
        List<string> list = new List<string>();

        private string _Background;
        public Page2(MapObject mapObject)
        {
            _Background = @"../Resources/book.jpg";
            this.mapObject = mapObject;
            try
            {
                list = mapObject.listImage;
            }
            catch { }

            if (list.Count <= 2)
            {
                IsEnablePropertyBack = Visibility.Collapsed;
                IsEnableProperty = Visibility.Collapsed;
            }

            else
            {
                IsEnableProperty = Visibility.Visible;
                IsEnablePropertyBack = Visibility.Collapsed;
            }
            try
            {
                InitializeComponent();
                if (list[number] != null) Image1 = list[number];
                if (list[number] != null) Image2 = list[number + 1];
            } 
            catch  { }

        }

        #region  Property
        private string _Image1;
        public string Image1
        {
            get => _Image1;
            set
            {
                _Image1 = value;
                OnPropertyChanged(nameof(Image1));
            }
        }

        private string _Image2; 
        public string Image2
        {
            get => _Image2;
            set
            {
                _Image2 = value;
                OnPropertyChanged(nameof(Image2));
            }
        }
        private string _Image3;
        public string Image3
        {
            get => _Image3;
            set
            {
                _Image3 = value;
                OnPropertyChanged(nameof(Image3));
            }
        }
        private string _Image4;
        public string Image4
        {
            get => _Image4;
            set
            {
                _Image4 = value;
                OnPropertyChanged(nameof(Image4));
            }
        }
        #endregion

        const double PAGE_WIDTH = 540;
        const double PAGE_HEIGHT = 900;

        private void Storyboard_CurrentTimeInvalidated(object sender, EventArgs e)
        {
            if (((Clock)sender).CurrentState == ClockState.Stopped)
            {
                PageTurn(-PAGE_WIDTH * 2, 0);
            }
            else
            {
                PageTurn(page3Translate.X, page3Translate.Y);
            }
        }

        private void Thumb_DragDelta(object sender, System.Windows.Controls.Primitives.DragDeltaEventArgs e)
        {
            var thumb = (System.Windows.Controls.Primitives.Thumb)sender;
            double x = Math.Min(Math.Max(-(PAGE_WIDTH * 2), e.HorizontalChange), 0);
            double y = Math.Min(e.VerticalChange, 0);
            PageTurn(x, y);
        }

        private void PageTurn(double x, double y)
        {
            if (x == 0)
            {
                this.page3Translate.X = 0;
                this.page3Translate.Y = 0;
                this.page3Rotate.Angle = 0;

                this.page3clip.Points.Clear();
                this.page4clip.Points.Clear();
            }
            else
            {
                double a = Math.Abs((x * x + y * y) / y / 2);
                double b = Math.Abs(a * y / x);

                if (Math.Abs(b) > PAGE_WIDTH)
                {
                    b = PAGE_WIDTH;
                    y = -Math.Sqrt(Math.Pow(PAGE_WIDTH, 2) - Math.Pow(PAGE_WIDTH - Math.Abs(x), 2));
                    a = Math.Abs((x * x + y * y) / y / 2);
                }

                if (double.IsNaN(a) || double.IsInfinity(a))
                {
                    this.page3clip.Points.Clear();
                    this.page3clip.Points.Add(new Point(0, 0));
                    this.page3clip.Points.Add(new Point(Math.Abs(x / 2), 0));
                    this.page3clip.Points.Add(new Point(Math.Abs(x / 2), PAGE_HEIGHT));

                    this.page4clip.Points.Clear();
                    this.page4clip.Points.Add(new Point(PAGE_WIDTH - Math.Abs(x / 2), PAGE_HEIGHT));
                    this.page4clip.Points.Add(new Point(PAGE_WIDTH - Math.Abs(x / 2), 0));
                    this.page4clip.Points.Add(new Point(PAGE_WIDTH, 0));
                }
                else
                {
                    this.page3clip.Points.Clear();
                    this.page3clip.Points.Add(new Point(0, PAGE_HEIGHT - a));
                    this.page3clip.Points.Add(new Point(Math.Abs(b), PAGE_HEIGHT));

                    this.page4clip.Points.Clear();
                    this.page4clip.Points.Add(new Point(PAGE_WIDTH - b, PAGE_HEIGHT));
                    this.page4clip.Points.Add(new Point(PAGE_WIDTH, PAGE_HEIGHT - a));
                }
                this.page3Translate.X = x;
                this.page3Translate.Y = y;
                this.page3Rotate.Angle = 180 - Math.Atan(x / y) * 2 * 180 / Math.PI;
            }
        }


        private Visibility _IsEnableProperty;
        public Visibility IsEnableProperty
        {
            get => _IsEnableProperty;
            set
            {
                _IsEnableProperty = value;
                OnPropertyChanged(nameof(IsEnableProperty));
            }
        }
        private Visibility _IsEnablePropertyBack;
        public Visibility IsEnablePropertyBack
        {
            get => _IsEnablePropertyBack;
            set
            {
                _IsEnablePropertyBack = value;
                OnPropertyChanged(nameof(IsEnablePropertyBack));
            }
        }

        public void ImageLoadTime()
        {
            Thread.Sleep(700);
            Image3 = list[number];
            Image4 = list[number + 1];
        }

        private async void Btn2_OnClickDown (object sender, RoutedEventArgs e)
        {

            IsEnableProperty = Visibility.Visible;
            if (number<=2) IsEnablePropertyBack = Visibility.Hidden;

            if (number >=2)
            {
                number-=2;
                Image1 = list[number];
                Image2 = list[number+1];
                await Task.Run(() => ImageLoadTime());
            }
        }

        private async void Btn2_OnClickNext(object sender, RoutedEventArgs e)
        {
            IsEnablePropertyBack = Visibility.Visible; 
            if (number>=list.Count-4) IsEnableProperty = Visibility.Hidden;
            if (number<list.Count()-1)
            {
                number+=2;
                Image1 = list[number-2];
                Image2 = list[number-1];
                await Task.Run(() => ImageLoadTimeNext());
            }
        }

        public void ImageLoadTimeNext()
        {
            Thread.Sleep(200);
            Image3 = list[number];
            try
            {
                Image4 = list[number + 1];
            }
            catch
            {
                Image4 = _Background;
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            if (string.IsNullOrWhiteSpace(propertyName)) return;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}