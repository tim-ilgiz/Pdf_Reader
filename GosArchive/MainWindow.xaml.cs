using GosArchive.Model;
using System;
using System.Diagnostics;
using System.IO;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Threading;

namespace GosArchive
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static MainWindow mainWindow;
        private Page1 page1 = new Page1();
        private DispatcherTimer timer = new DispatcherTimer();
        Thread thr;
        public MainWindow()
        {
            InitializeComponent();
            
            controlontentControl.Content = page1;
            mainWindow = this; 
            AddText();
            textBox1.PreviewMouseDown += Search_Keyboard_Down;
            DelayedExecute();
            Start();
            dispatcherTimer.Tick += new EventHandler(dispatcherTimer_Tick);
            dispatcherTimer.Interval = new TimeSpan(0, 0, ExitSeconds);
            dispatcherTimer_Admin.Tick += new EventHandler(dispatcherTimer_Tick_Admin);
            dispatcherTimer_Admin.Interval = new TimeSpan(0, 0, ExitSeconds_Admin);
           
        }

        public void AddText()
        {
            try
            {
                foreach (var i in page1.CurrentListBoxText)
                {
                    {
                        textBox1.AddItem(new AutoCompleteEntry(i.DisplayPath.ToLower(), i.DisplayPath.ToLower()));
                    }
                }
            }
            catch (Exception)
            {
            }
        }

        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            AutoCompleteTextBox.autoCompleteTextBox.ResultSearch(AutoCompleteTextBox.autoCompleteTextBox.Text,
                Page1.page1.CurrentListBoxText);
        }

        private void GoToMainClick(object sender, RoutedEventArgs e)
        {

            page1.CurrentMapObjects.Clear();
            controlontentControl.Content = page1;
            AutoCompleteTextBox.autoCompleteTextBox.Text = null;
            page1.CurrentMapObjects.Clear();
            SeachBorder.Visibility = Visibility.Visible;
            Page1.page1.ShowAllMapObjects();
        }

        private void Search_Keyboard_Down(object sender, MouseButtonEventArgs e)
        {
            Process[] oskProcessArray = Process.GetProcessesByName("TabTip");
            foreach (Process onscreenProcess in oskProcessArray)
            {
                onscreenProcess.Kill();
            }
            string progFiles = @"C:\Program Files\Common Files\Microsoft Shared\ink";
            string onScreenKeyboardPath = System.IO.Path.Combine(progFiles, "TabTip.exe");
            var onScreenKeyboardProc = Process.Start(onScreenKeyboardPath);
        }

        #region Таймер заставки 

        public void DelayedExecute()
        {
            timer.Tick += DispatcherTimer_Tick;
            timer.Interval = new TimeSpan(0, 0, 120);
            timer.Start();
        }

        void DispatcherTimer_Tick(object sender, EventArgs e)
        {
            ScreenCanvas.Visibility = Visibility.Visible;
            ScreenCanvas.Background = new ImageBrush(new BitmapImage(new Uri(@"pack://application:,,,/GosArchive;component/Resources/screensaver.png", UriKind.Absolute)));
        }

        public void ScreenSaver()
        {
            ScreenCanvas.Visibility = Visibility.Collapsed;
            timer.Stop();
            timer.Start();
        }
        #endregion

        #region Скрытый выход по таймеру (+ блок инициализации и приввязка обработчика на закрытие окна)

        DispatcherTimer dispatcherTimer = new DispatcherTimer();
        private const int ExitSeconds = 3;
        public static Thread explorerThread;

        private void XMainWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            explorerThread?.Abort();
            RunExplorer();
        }

        public void RunExplorer()
        {
            Process[] processInfo = Process.GetProcessesByName("explorer");
            if (processInfo?.Length == 0)
            {
                //Process.Start("explorer.exe");
                Process proc = new Process();
                thr.Abort();
                thr.Join(2);
                proc.StartInfo.FileName = "C:\\Windows\\explorer.exe";
                proc.StartInfo.UseShellExecute = true;
                proc.Start();
            }
        }

        private void Start()
        {
            thr = new Thread(new ThreadStart(KillExplorer));
            thr.Priority = ThreadPriority.AboveNormal;
            thr.IsBackground = false;
            thr.Start();
        }

        public void KillExplorer()
        {
            Process[] processInfo = Process.GetProcessesByName("explorer");
            foreach (Process onscreenProcess in processInfo)
            {
                onscreenProcess.Kill();
            }
        }

        private void dispatcherTimer_Tick(object sender, EventArgs e)
        {
            dispatcherTimer.Stop();
            Window.Close();
        }

        private void Button_TouchDown(object sender, TouchEventArgs e)
        {
            dispatcherTimer.Start();
        }

        private void Button_MouseDown(object sender, MouseButtonEventArgs e)
        {
            dispatcherTimer.Start();
        }


        private void Button_TouchUp(object sender, TouchEventArgs e)
        {
            dispatcherTimer.Stop();
        }

        private void Button_MouseUp(object sender, MouseButtonEventArgs e)
        {
            dispatcherTimer.Stop();
        }

        private void Button_TouchLeave(object sender, TouchEventArgs e)
        {
            dispatcherTimer.Stop();
        }

        private void Button_MouseLeave(object sender, MouseEventArgs e)
        {
            dispatcherTimer.Stop();
        }
        #endregion

        #region Вход в Админку

        DispatcherTimer dispatcherTimer_Admin = new DispatcherTimer();
        private const int ExitSeconds_Admin = 3;

        private void dispatcherTimer_Tick_Admin(object sender, EventArgs e)
        {
            dispatcherTimer_Admin.Stop();
            controlontentControl.Content = new Admin_Page(Page1.page1.CurrentListBoxText);
            SeachBorder.Visibility = Visibility.Collapsed;
        }

        private void Button_TouchDown_Admin(object sender, TouchEventArgs e)
        {
            dispatcherTimer_Admin.Start();
        }

        private void Button_MouseDown_Admin(object sender, MouseButtonEventArgs e)
        {
            dispatcherTimer_Admin.Start();
        }


        private void Button_TouchUp_Admin(object sender, TouchEventArgs e)
        {
            dispatcherTimer_Admin.Stop();
        }

        private void Button_MouseUp_Admin(object sender, MouseButtonEventArgs e)
        {
            dispatcherTimer_Admin.Stop();
        }

        private void Button_TouchLeave_Admin(object sender, TouchEventArgs e)
        {
            dispatcherTimer_Admin.Stop();
        }

        private void Button_MouseLeave_Admin(object sender, MouseEventArgs e)
        {
            dispatcherTimer_Admin.Stop();
        }


        #endregion

        private void InitZoom()
        {
            VisualBrush b = (VisualBrush)magnifierEllipse.Fill;
            b.Visual = mainUI;
            visualbrush.Viewbox = new Rect(0, 0, 300, 300);
        }

        private void ZoomChanged(object sender, EventArgs e)
        {
            if (magnifierEllipse != null)
            {

                VisualBrush b = (VisualBrush)magnifierEllipse.Fill;
                Rect viewBox = b.Viewbox;
                double zoomedit = 1;
                viewBox.Width = zoomedit;
                viewBox.Height = zoomedit;
                b.Viewbox = viewBox;
            }
        }

        private void OnMoveOverMainUI(object sender, MouseEventArgs e)
        {
            VisualBrush b = (VisualBrush)magnifierEllipse.Fill;
            Point pos = e.MouseDevice.GetPosition(www);
            Rect viewBox = b.Viewbox;
            double xoffset = viewBox.Width / 2.0;
            double yoffset = viewBox.Height / 2.0;
            viewBox.X = pos.X - xoffset;
            viewBox.Y = pos.Y - yoffset;
            b.Viewbox = viewBox;
            Canvas.SetLeft(magnifierCanvas, pos.X - magnifierEllipse.Width / 2);
            Canvas.SetTop(magnifierCanvas, pos.Y - magnifierEllipse.Height / 2);
        }

        int number = 0;

        private void Seach_down(object sender, RoutedEventArgs e)
        {
            switch (number)
            {
                case 0:
                    magnifierCanvas.Visibility = Visibility.Visible;
                    number++;
                    break;
                case 1:
                    magnifierCanvas.Visibility = Visibility.Collapsed;
                    number--;
                    break;
            }
        }

        private void MainWindow_OnLoaded(object sender, RoutedEventArgs e)
        {
            InitZoom(); 
        }
    }
}
