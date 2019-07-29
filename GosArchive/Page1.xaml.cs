using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Drawing;
using System.IO;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using GosArchive.Model;


namespace GosArchive
{
    /// <summary>
    /// Логика взаимодействия для Page1.xaml
    /// </summary>
    public partial class Page1 : UserControl, INotifyPropertyChanged
    {
        private string MainFolder = AppDomain.CurrentDomain.BaseDirectory;
        public static Page1 page1;
        public FileInfo[] files;
        public FileInfo[] fileImage;
        public static Admin_Page CurrentAdmin;
        public Page1()
        {
            InitializeComponent();
            string filelink = $"{AppDomain.CurrentDomain.BaseDirectory}/PDF";
            if (!Directory.Exists(filelink))
            {
                Directory.CreateDirectory(filelink);
            }
            CurrentListBoxText = new ObservableCollection<MapObject>();
            CurrentMapObjects = new ObservableCollection<MapObject>();
            page1 = this;
            ListBoxContent();
            ShowAllMapObjects();
        }

        public void ListBoxContent()
        {
            var dir = new System.IO.DirectoryInfo(MainFolder + "\\PDF");
            CurrentListBoxText.Clear();
            foreach (var VARIABLE in dir.GetDirectories())
            {
                List <string> list = new List<string>();
                try
                {
                    files = VARIABLE.GetFiles("*.pdf*", SearchOption.TopDirectoryOnly);
                    fileImage = VARIABLE.GetFiles("*.png*", SearchOption.TopDirectoryOnly);

                    foreach (var items in fileImage)
                    {
                        string image = items.FullName;
                        //Image.FromFile(items.FullName);
                        list.Add(image);
                    }
                }
                catch
                {
                }

                foreach (var item in files)
                {
                    MapObject mapObj = new MapObject
                    {
                        FullName = item.FullName,
                        DisplayPath = item.Name,
                        ShortDisplayPath = item.Name.Split('.').First(),
                        listImage = list
                    };
                    CurrentListBoxText.Add(mapObj);
                }
            }
        }

        public void ShowAllMapObjects()
        {
            if (Page1.page1.CurrentMapObjects != null)
            {
               CurrentMapObjects.Clear();
            }
            try
            {
                foreach (var i in Page1.page1.CurrentListBoxText)
                {
                    Page1.page1.CurrentMapObjects.Add(i);
                }
            }
            catch (Exception)
            {
            }
        }

        private ObservableCollection<MapObject> _CurrentListBoxText;
        public ObservableCollection<MapObject> CurrentListBoxText
        {
            get => _CurrentListBoxText;
            set
            {
                if (value != null)
                {
                    _CurrentListBoxText = value;
                    OnPropertyChanged(nameof(CurrentListBoxText));
                }
            }
        }

        private ObservableCollection<MapObject> _CurrentMapObjects;
        public ObservableCollection<MapObject> CurrentMapObjects
        {
            get => _CurrentMapObjects;
            set
            {
                if (value != null)
                {
                    _CurrentMapObjects = value;
                    OnPropertyChanged(nameof(CurrentMapObjects));
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            if (string.IsNullOrWhiteSpace(propertyName)) return;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void List_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            MapObject mapObject = (sender as ListBox).SelectedItem as MapObject;
            MainWindow.mainWindow.controlontentControl.Content = new Page2(mapObject);
            MainWindow.mainWindow.SeachBorder.Visibility = Visibility.Collapsed;
        }
    }
}
