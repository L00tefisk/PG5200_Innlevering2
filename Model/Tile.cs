using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace LevelEditor.Model
{
    [Serializable]
    public class Tile : Image, IEquatable<Tile>
    {
        public readonly int X;
        public readonly int Y;
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

        public bool Equals(Tile other)
        {
            return X == other.X && Y == other.Y;
        }
    }
}