using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GosArchive.KeyboardN
{
    /// <summary>
    /// The keyboard model.
    /// </summary>
    public class KeyboardModel : INotifyPropertyChanged
    {
        /// <summary>
        /// Gets or sets the content 1.
        /// </summary>
        protected string[] Content1 { get; set; }

        /// <summary>
        /// Gets or sets the content 1 shift.
        /// </summary>
        protected string[] Content1Shift { get; set; }

        /// <summary>
        /// Gets or sets the content 2.
        /// </summary>
        protected string[] Content2 { get; set; }

        /// <summary>
        /// Gets or sets the content 2 shift.
        /// </summary>
        protected string[] Content2Shift { get; set; }

        /// <summary>
        /// Gets or sets the content RUS.
        /// </summary>
        protected string[] ContentRUS { get; set; }
        /// <summary>
        /// Gets or sets the content RUS.
        /// </summary>

        protected string[] ContentENG { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether is shift.
        /// </summary>
        /// 
        public bool IsShift { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether is eng rus.
        /// </summary>
        public bool IsEngRus { get; set; }

        /// <summary>
        /// Gets the content.
        /// </summary>
        public string[] Content
        {
            get
            {
                if (!this.IsShift && this.IsEngRus) return this.Content1;
                if (this.IsShift && this.IsEngRus) return this.Content1Shift;
                if (!this.IsShift && !this.IsEngRus) return this.Content2;
                if (this.IsShift && !this.IsEngRus) return this.Content2Shift;
                if (this.IsEngRus) return this.ContentRUS;
                if (this.IsEngRus) return this.ContentENG;
                return null;
            }
        }

        /// <summary>
        /// Gets the AS_RUS content.
        /// </summary>
        public string[] AS_RUS_Content
        {
            get
            {
                return this.ContentRUS;               
            }
        }

        /// <summary>
        /// Gets the AS_ENG content.
        /// </summary>
        public string[] AS_ENG_Content
        {
            get
            {
                return this.ContentENG;
            }
        }

        /// <summary>
        /// Gets the buttons raw 1.
        /// </summary>
        public ObservableCollection<ButtonModel> ButtonsRaw1 { get; private set; }

        /// <summary>
        /// Gets the buttons raw 2.
        /// </summary>
        public ObservableCollection<ButtonModel> ButtonsRaw2 { get; private set; }

        /// <summary>
        /// Gets the buttons raw 3.
        /// </summary>
        public ObservableCollection<ButtonModel> ButtonsRaw3 { get; private set; }

        /// <summary>
        /// Gets the buttons raw 4.
        /// </summary>
        public ObservableCollection<ButtonModel> ButtonsRaw4 { get; private set; }

        /// <summary>
        /// Gets the AS_RUS buttons.
        /// </summary>
        public ObservableCollection<ButtonModel> AS_RUS_Buttons { get; private set; }

        /// <summary>
        /// Gets the AS_ENG buttons.
        /// </summary>
        public ObservableCollection<ButtonModel> AS_ENG_Buttons { get; private set; }

        /// <summary>
        /// The text.
        /// </summary>
        private string text;

        /// <summary>
        /// Gets or sets the text.
        /// </summary>
        public string Text
        {
            get
            {
                return this.text;
            }
            set
            {
                this.text = value;
                this.OnPropertyChanged("Text");
            }
        }

        #region Methods

        /// <summary>
        /// The get button content.
        /// </summary>
        /// <param name="btnName">
        /// The btn name.
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        public string GetButtonContent(string btnName)
        {
            var raw = Convert.ToInt32(btnName[1].ToString()) - 1;
            var col = btnName.Length == 3
                          ? Convert.ToInt32(btnName[2].ToString()) - 1
                          : Convert.ToInt32(btnName[2] + btnName[3].ToString()) - 1;
            return this.Content[raw][col].ToString();
        }

        /// <summary>
        /// The get button AS_RUS content.
        /// </summary>
        /// <param name="btnName">
        /// The btn name.
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        public string AS_RUS_GetButtonContent(string btnName)
        {
            var raw = Convert.ToInt32(btnName[1].ToString()) - 1;
            var col = btnName.Length == 3
                          ? Convert.ToInt32(btnName[2].ToString()) - 1
                          : Convert.ToInt32(btnName[2] + btnName[3].ToString()) - 1;
            return this.AS_RUS_Content[raw][col].ToString();
        }

        /// <summary>
        /// The get button AS_ENG content.
        /// </summary>
        /// <param name="btnName">
        /// The btn name.
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        public string AS_ENG_GetButtonContent(string btnName)
        {
            var raw = Convert.ToInt32(btnName[1].ToString()) - 1;
            var col = btnName.Length == 3
                          ? Convert.ToInt32(btnName[2].ToString()) - 1
                          : Convert.ToInt32(btnName[2] + btnName[3].ToString()) - 1;
            return this.AS_ENG_Content[raw][col].ToString();
        }

        /// <summary>
        /// The change buttons content.
        /// </summary>
        public void ChangeButtonsContent()
        {
            this.ChangeButtonsContent(this.ButtonsRaw1, 0);
            this.ChangeButtonsContent(this.ButtonsRaw2, 1);
            this.ChangeButtonsContent(this.ButtonsRaw3, 2);
            this.ChangeButtonsContent(this.ButtonsRaw4, 3);
        }

        /// <summary>
        /// The create buttons.
        /// </summary>
        public void CreateButtons()
        {
            this.ButtonsRaw1 = this.CreateButtons(0);
            this.ButtonsRaw2 = this.CreateButtons(1);
            this.ButtonsRaw3 = this.CreateButtons(2);
            this.ButtonsRaw4 = this.CreateButtons(3);
        }

        /// <summary>
        /// The create AS_RUS buttons.
        /// </summary>
        public void AS_RUS_CreateButtons()
        {
            this.AS_RUS_Buttons = this.AS_RUS_CreateButtons(0);
        }

        /// <summary>
        /// The create AS_ENG buttons.
        /// </summary>
        public void AS_ENG_CreateButtons()
        {
            this.AS_ENG_Buttons = this.AS_ENG_CreateButtons(0);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="KeyboardModel"/> class.
        /// </summary>
        public KeyboardModel()
        {
            this.IsShift = false;
            this.IsEngRus = false;
            this.InitContent();
        }

        /// <summary>
        /// The init content.
        /// </summary>
        public virtual void InitContent()
        {
        }

        /// <summary>
        /// The create buttons.
        /// </summary>
        /// <param name="raw">
        /// The _raw.
        /// </param>
        /// <returns>
        /// The <see cref="ObservableCollection"/>.
        /// </returns>
        private ObservableCollection<ButtonModel> CreateButtons(int raw)
        {
            var buttons = new ObservableCollection<ButtonModel>();
            for (var j = 1; j <= this.Content[raw].Length; j++)
            {
                var name = $"b{raw + 1}{j}";
                buttons.Add(new ButtonModel { Name = name, Column = j - 1, Content = this.GetButtonContent(name) });
            }
            return buttons;
        }

        /// <summary>
        /// The create AS_RUS buttons.
        /// </summary>
        /// <param name="raw">
        /// The _raw.
        /// </param>
        /// <returns>
        /// The <see cref="ObservableCollection"/>.
        /// </returns>
        private ObservableCollection<ButtonModel> AS_RUS_CreateButtons(int raw)
        {
            var buttons = new ObservableCollection<ButtonModel>();
            for (var j = 1; j <= this.AS_RUS_Content[raw].Length; j++)
            {
                var name = $"b{raw + 1}{j}";
                buttons.Add(new ButtonModel { Name = name, Column = j - 1, Content = this.AS_RUS_GetButtonContent(name) });
            }
            return buttons;
        }

        /// <summary>
        /// The create AS_ENG buttons.
        /// </summary>
        /// <param name="raw">
        /// The _raw.
        /// </param>
        /// <returns>
        /// The <see cref="ObservableCollection"/>.
        /// </returns>
        private ObservableCollection<ButtonModel> AS_ENG_CreateButtons(int raw)
        {
            var buttons = new ObservableCollection<ButtonModel>();
            for (var j = 1; j <= this.AS_ENG_Content[raw].Length; j++)
            {
                var name = $"b{raw + 1}{j}";
                buttons.Add(new ButtonModel { Name = name, Column = j - 1, Content = this.AS_ENG_GetButtonContent(name) });
            }
            return buttons;
        }

        /// <summary>
        /// The change buttons content.
        /// </summary>
        /// <param name="buttons">
        /// The _buttons.
        /// </param>
        /// <param name="_raw">
        /// The _raw.
        /// </param>
        private void ChangeButtonsContent(ObservableCollection<ButtonModel> buttons, int _raw)
        {
            for (var j = 1; j <= this.Content[_raw].Length; j++) buttons[j - 1].Content = this.GetButtonContent(buttons[j - 1].Name);
        }

        #endregion

        #region INotifyPropertyChanged Members

        /// <summary>
        /// The property changed.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// The on property changed.
        /// </summary>
        /// <param name="propertyName">
        /// The property name.
        /// </param>
        private void OnPropertyChanged(string propertyName)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion
    }
}
