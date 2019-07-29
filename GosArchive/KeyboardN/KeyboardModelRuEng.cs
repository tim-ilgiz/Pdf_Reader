using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GosArchive.KeyboardN
{
    /// <summary>
    /// The keyboard model russian english.
    /// </summary>
    public class KeyboardModelRuEng : KeyboardModel
    {
        public static KeyboardModelRuEng keyboardModelRuEng;
        /// <summary>
        /// Initializes a new instance of the <see cref="KeyboardModelRuEng"/> class.
        /// </summary>
        public KeyboardModelRuEng()
            : base()
        {
            keyboardModelRuEng = this;
        }

        /// <summary>
        /// The initialize content.
        /// </summary>
        public override void InitContent()
        {
            this.Content1 = new[] { "1234567890-", "йцукенгшщзхъ", "фывапролджэ", "ячсмитьбю,.?" };
            this.Content1Shift = new[] { "!\"№;%:?*()-", "ЙЦУКЕНГШЩЗХЪ", "ФЫВАПРОЛДЖЭ", "ЯЧСМИТЬБЮ,.?" };
            this.Content2 = new[] { "1234567890-", "qwertyuiop[]", "asdfghjkl;'", "zxcvbnm<>,.?" };
            this.Content2Shift = new[] { "!@#$%^&*()-", "QWERTYUIOP{}", "ASDFGHJKL:\"", "ZXCVBNM<>,.?" };

            this.ContentRUS = new[] { "АБВГДЕЁЖЗИЙКЛМНОПРСТУФХЦЧШЩЭЮЯ" };
            this.ContentENG = new[] { "ABCDEFGHIJKLMNOPQRSTUVWXYZ" };
        }
    }
}
