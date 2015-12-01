using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace LevelEditor.Model
{
    [Serializable]
    public class Tile : Image
    {
        public int X;
        public int Y;
        public int Id;
        //public ImageSource ImgSrc;
        public Tile(ImageSource image, int x, int y, int id)
        {
            Y = y;
            X = x;
            Id = id;
            Width = 32;
            Height = 32;
            Source = image;
        }

        public void ChangeTile(ImageSource newImage)
        {
            Source = newImage;
        }
    }
}