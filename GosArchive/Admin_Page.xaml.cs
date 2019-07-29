using GosArchive.Model;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
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
    /// Логика взаимодействия для Admin_Page.xaml
    /// </summary>
    public partial class Admin_Page : UserControl, INotifyPropertyChanged
    {
        private MapObject thisMapObject { get; set; }

        public Admin_Page(ObservableCollection<MapObject> AdminList)
        {
            this.CurrentListBoxText = AdminList;
            InitializeComponent();
        }

        private void Admin_List_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            MapObject thisMapObject = (sender as ListBox).SelectedItem as MapObject;
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

        MainWindow _CurrentWindow;
        public MainWindow CurrentWindow
        {
            get => _CurrentWindow;
            set
            {
                _CurrentWindow = value;
                OnPropertyChanged(nameof(CurrentWindow));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged(string propertyName)
        {
            if (string.IsNullOrWhiteSpace(propertyName)) return;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        string safeFileName  { get; set; }
        string CurrentNameDirrectory { get; set; }
        string linkpath { get; set; }

        private string MainFolder = AppDomain.CurrentDomain.BaseDirectory;

        private async void Add_PdfFile(object sender, RoutedEventArgs e)
        {

            string[] result = new string[2];
            {
                OpenFileDialog openFileDialog = new OpenFileDialog();
                openFileDialog.Filter = " PDF(.pdf) |*.pdf";
                var show = openFileDialog.ShowDialog();
                if (show == true)
                {
                    MainWindow.mainWindow.button_All.IsEnabled = false;
                    MainWindow.mainWindow.LoadGrid.Visibility = Visibility.Visible;
                    //Имя файла выбранного файла (полное)
                    string fullName = openFileDialog.FileName;
                    //Имя файла с форматом .pdf
                    safeFileName = openFileDialog.SafeFileName;
                    //Актуальное имя папки 
                    string actualNameFolder = safeFileName.Substring(0, safeFileName.Length - 4);
                    FileInfo fileinfo = new FileInfo(fullName);
                    CurrentNameDirrectory = $"{MainFolder}/PDF/{actualNameFolder}";

                    if (!File.Exists(CurrentNameDirrectory))
                    {
                        Directory.CreateDirectory(CurrentNameDirrectory);
                        linkpath = $"{CurrentNameDirrectory}/{safeFileName}";
                        if (!File.Exists(linkpath))
                        {
                            fileinfo.CopyTo(linkpath);
                            Converter2 convert = new Converter2();
                            await convert.ConvertToImage(linkpath);
                            MainWindow.mainWindow.LoadGrid.Visibility = Visibility.Collapsed;
                            MainWindow.mainWindow.button_All.IsEnabled = true;
                            Page1.page1.ListBoxContent();
                        }

                        else
                        {
                            MessageBox.Show("Такой файл уже есть");
                            MainWindow.mainWindow.LoadGrid.Visibility = Visibility.Collapsed;
                            MainWindow.mainWindow.button_All.IsEnabled = true;
                        }
                    }
                }
            }

            AutoCompleteTextBox.autoCompleteTextBox.autoCompletionList.Clear();
            MainWindow.mainWindow.AddText();
        }
        private void ToggleButton_OnChecked(object sender, RoutedEventArgs e)
        {
            if (e.Source is RadioButton b)
            {
                b.Tag = true.ToString();
            }
        }

        private void ToggleButton_OnUnchecked(object sender, RoutedEventArgs e)
        {
            if (e.Source is RadioButton b)
            {
                b.Tag = false.ToString();
            }
        }

        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {

            if ((sender as RadioButton)?.DataContext is MapObject mapObject)
            {
                thisMapObject = mapObject; 
            }
        }

        private void Delete_PdfFile(object sender, RoutedEventArgs e)
        {
            try
            {
                var files =  Directory.GetFiles($"{thisMapObject.FullName.Substring(0, (thisMapObject.FullName.Length - thisMapObject.DisplayPath.Length))}").ToList();
                foreach (var item in files)
                {
                    try
                    {
                        File.Delete(item);
                    }
                    catch (IOException)
                    {

                    }
                }
                Directory.Delete($"{thisMapObject.FullName.Substring(0, (thisMapObject.FullName.Length - thisMapObject.DisplayPath.Length))}");
                //Page1.page1.ListBoxContent();
                CurrentListBoxText.Remove(thisMapObject);
            }
            catch
            {
                MessageBox.Show("Не выбран файл для удаления");
            }

            AutoCompleteTextBox.autoCompleteTextBox.autoCompletionList.Clear();
            MainWindow.mainWindow.AddText();
        }
    }
}
