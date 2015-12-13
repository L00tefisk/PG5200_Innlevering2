using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Windows.Media;
using System.Windows;

namespace LevelEditor.Model
{
    public class Map
    {
        public int Width { get; set; }
        public int Height { get; set; }

        public Tile[] Tiles { get; set; }

        private readonly int _tileSize;
        public Map(int width, int height)
        {
            Width = width;
            Height = height;
            Tiles = new Tile[height * width];

            for (int i = 0; i < height * width; i++)
                Tiles[i] = new Tile(null, (i - (i / width) * width) , i / width, int.MaxValue);

            _tileSize = 32;
        }
        public void SetTile(int x, int y, int id, ImageSource image)
        {
            int index = y*Width + x;
            if (index >= 0 && index < Tiles.Length && Tiles[index] != null)
            {
                Tiles[index].Source = image;
                Tiles[index].Id = id;
            }

        }
        public Tile GetTile(int x, int y)
        {
            return Tiles[y * Width + x];
        }

        public int GetTileSize()
        {
            return _tileSize;
        }

        public Tile[] GetLevel()
        {
            return Tiles;;
        }

    }
}
