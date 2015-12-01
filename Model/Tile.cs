using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace LevelEditor.Model
{
    [Serializable]
    public class Tile : Image
    {
        public Point Position;
        public Tile(ImageSource image, Point pos)
        {
            Position = pos;
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