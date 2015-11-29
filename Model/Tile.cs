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
    public class Tile : Image
    {
        public int X { get; set; }
        public int Y { get; set; }
        public Tile(ushort id, int x, int y)
        {
            X = x;
            Y = y;
            Width = 32;
            Height = 32;
        }

        public void ChangeTile(ImageSource newImage)
        {
            Source = newImage;
        }
    }
}