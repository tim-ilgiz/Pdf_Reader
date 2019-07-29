using System;
using System.Collections.Generic;
using System.ComponentModel;
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

namespace GosArchive.KeyboardN
{
    /// <summary>
    ///     The on screen keyboard.
    /// </summary>
    public partial class OnScreenKeyboard : UserControl
    {
        public static OnScreenKeyboard onScreenKeyboard;
        /// <summary>
        ///     The text property.
        /// </summary>
        public static readonly DependencyProperty TextProperty = DependencyProperty.Register(
            "Text",
            typeof(string),
            typeof(OnScreenKeyboard),
            new UIPropertyMetadata(null));

        /// <summary>
        ///     The command property.
        /// </summary>
        public static readonly DependencyProperty CommandProperty = DependencyProperty.Register(
            "Command",
            typeof(ICommand),
            typeof(OnScreenKeyboard));

        /// <summary>
        /// Initializes a new instance of the <see cref="OnScreenKeyboard"/> class. 
        /// </summary>
        public OnScreenKeyboard()
        {
            this.InitializeComponent();
            onScreenKeyboard = this;
        }

        /// <summary>
        ///     Gets or sets the text.
        /// </summary>
        public string Text
        {
            get
            {
                return (string)this.GetValue(TextProperty);
            }

            set
            {
                this.SetValue(TextProperty, value);
            }
        }

        /// <summary>
        ///     Gets or sets the command.
        /// </summary>
        public ICommand Command
        {
            get
            {
                return (ICommand)this.GetValue(CommandProperty);
            }

            set
            {
                this.SetValue(CommandProperty, value);
            }
        }

    }
}
