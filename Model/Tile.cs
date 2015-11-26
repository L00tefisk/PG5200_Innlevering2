using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace LevelEditor.Model
{
    [Serializable]
    public class Tile : Image
    {
        public ushort BitmapID { get; set; }
        public bool Collidable { get; set; }
        public int X { get; set; }
        public int Y { get; set; }

        public Tile(ushort bitmapid, string path, bool collidable)
        {
            BitmapID = bitmapid;

            Source = new BitmapImage(new Uri(path, UriKind.Relative));

            Collidable = collidable;
        }
    }
}