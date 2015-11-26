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
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace LevelEditor.Model
{
    [Serializable]
    public class Tile
    {
        public ushort TileId{ get; set; }
        public bool Collidable { get; set; }
        public string FilePath { get; set; }
        public ImageSource ImgSrc { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
        public Tile(ushort id, int x, int y)
        {
            TileId = id;
            X = x;
            Y = y;
            FilePath = Model.ImgPaths[id];
            ImgSrc = new BitmapImage(new Uri(FilePath, UriKind.Relative));
            Collidable = false;
        }
    }
}