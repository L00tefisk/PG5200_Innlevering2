using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace LevelEditor.Model
{
    [Serializable]
    class Tile
    {
        public byte BitmapID { get; set; }
        public Image Img { get; set; }
        public bool Collidable { get; set; }

        public Tile(byte bitmapid, bool collidable)
        {
            string pathToImage;
            BitmapID = bitmapid;
            BitmapImage bi = new BitmapImage();
            bi.BeginInit();
            //bi.UriSource = new Uri(pathToImage, UriKind.RelativeOrAbsolute);
            bi.EndInit();
            Img.Source = bi;
            Collidable = collidable;
        }
    }
}
