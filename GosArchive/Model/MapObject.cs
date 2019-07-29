using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace GosArchive.Model
{
    public class MapObject
    {
        public string FullName { get; set; }
        public string DisplayPath { get; set; }

        public string ShortDisplayPath { get; set; }
        public  List<string> listImage { get; set; }
    }
}
