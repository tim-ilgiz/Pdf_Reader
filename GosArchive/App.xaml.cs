using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace GosArchive
{
    /// <summary>
    /// Логика взаимодействия для App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            EventManager.RegisterClassHandler(typeof(Window), Window.PreviewMouseDownEvent, new MouseButtonEventHandler(OnPreviewMouseDown));

            EventManager.RegisterClassHandler(typeof(Window), Window.PreviewMouseUpEvent, new MouseButtonEventHandler(OnPreviewMouseDown));

            EventManager.RegisterClassHandler(typeof(Window), Window.TouchUpEvent, new RoutedEventHandler(xOnTouchDownEvent));

            EventManager.RegisterClassHandler(typeof(Window), Window.TouchDownEvent, new RoutedEventHandler(xOnTouchDownEvent));

            EventManager.RegisterClassHandler(typeof(Window), Window.KeyDownEvent, new RoutedEventHandler(xOnTouchDownEvent));
            base.OnStartup(e);
        }


        static void OnPreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            GosArchive.MainWindow.mainWindow.ScreenSaver();
        }
        static void xOnTouchDownEvent(object sender, EventArgs e)
        {
            GosArchive.MainWindow.mainWindow.ScreenSaver();
        }
    }
}
