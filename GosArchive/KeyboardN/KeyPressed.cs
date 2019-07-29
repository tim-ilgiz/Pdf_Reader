﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace GosArchive.KeyboardN
{
    /// <summary>
    /// The key pressed.
    /// </summary>
    public class KeyPressed
    {
        /// <summary>
        /// The image property.
        /// </summary>
        public static readonly DependencyProperty ImageProperty;

        /// <summary>
        /// Initializes static members of the <see cref="KeyPressed"/> class.
        /// </summary>
        static KeyPressed()
        {
            var metadata = new FrameworkPropertyMetadata((ImageSource)null);
            ImageProperty = DependencyProperty.RegisterAttached(
                "Image",
                typeof(ImageSource),
                typeof(KeyPressed),
                metadata);
        }

        /// <summary>
        /// The get image.
        /// </summary>
        /// <param name="obj">
        /// The object.
        /// </param>
        /// <returns>
        /// The <see cref="ImageSource"/>.
        /// </returns>
        public static ImageSource GetImage(DependencyObject obj)
        {
            return (ImageSource)obj.GetValue(ImageProperty);
        }

        /// <summary>
        /// The set image.
        /// </summary>
        /// <param name="obj">
        /// The object.
        /// </param>
        /// <param name="value">
        /// The value.
        /// </param>
        public static void SetImage(DependencyObject obj, ImageSource value)
        {
            obj.SetValue(ImageProperty, value);
        }
    }
}
