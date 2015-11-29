using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Security.RightsManagement;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Xaml;
using Microsoft.Win32;
using System.Windows;

namespace LevelEditor.Model
{
    public class Map
    {
		public string Filename { get; set; }
        public short TileSize { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        //public List<List<Tile>> _level;
        public Tile[] _level;

        public Map(int width, int height)
        {
            Width = width;
            Height = height;
            _level = new Tile[height * width];
            Random randomId = new Random();
            for (int y = 0; y < Height; y++)
            { 
                for (int x = 0; x < Width; x++)
                {
                    _level[y*width + x] =  new Tile(null, new Point(x, y* width)); 
                }
            }
            TileSize = 32;
        }
        /// <summary>
        /// Set tile in the level
        /// </summary>
        /// <param name="t">The tile you want to set</param>
        public void SetTile(Point p, ImageSource image)
        {
            if (_level[(int)p.Y * Width + (int)p.X].Source != image)
            {
                _level[(int)p.Y * Width + (int)p.X].ChangeTile(image);
            }
        }
        /// <summary>
        /// Returns the tile at (x, y)
        /// </summary>
        /// <param name="x">X position</param>
        /// <param name="y">Y position</param>
        /// <returns></returns>
        public Tile GetTile(Point p)
        {
            if (_level.GetLength(0) > 0)
                return _level[(int)p.Y * Width + (int)p.X];
            else
                return null;
        }
        /// <summary>
        /// Saves the map to file in binary format, Filename must be set.
        /// </summary>
        public void Save()
        {
            using (BinaryWriter binaryWriter = new BinaryWriter(File.Open(Filename, FileMode.Create)))
            {
				// Record how many rows and columns there are, then record how many bytes one Tile uses.
				binaryWriter.Write(Height);
                binaryWriter.Write(Width);
				binaryWriter.Write(SerializeToBytes(_level[0]).Length);
                foreach (Tile tile in _level)
                {

                    binaryWriter.Write(SerializeToBytes(tile));
                }
            }
        }
        /// <summary>
        /// Loads the map from file, Filename must be set.
        /// </summary>
        public void Load()
        {
            using (BinaryReader binaryReader = new BinaryReader(File.Open(Filename, FileMode.Open)))
            {
                int rows = binaryReader.ReadInt32();
                int columns = binaryReader.ReadInt32();
                int byteCount = binaryReader.ReadInt32();
                _level = new Tile[rows * columns];
                for (int y = 0; y < rows; y++)
                {
                    for (int x = 0; x < columns; x++)
                    {
                        Byte[] rawData = binaryReader.ReadBytes(byteCount);
                        Tile t = (Tile)DeserializeFromBytes(rawData);

                        _level[y * columns + x] = t;
                    }
                }
            }
        }
        // These are used to Serialize Tile objects so they can be easily written/read to/from file.
        private static byte[] SerializeToBytes(Tile t)
        {
            BinaryFormatter formatter = new BinaryFormatter();
            using (MemoryStream stream = new MemoryStream())
            {
                formatter.Serialize(stream, t);
                stream.Seek(0, SeekOrigin.Begin);
                return stream.ToArray();
            }
        }
        private static object DeserializeFromBytes(byte[] bytes)
        {
            var formatter = new BinaryFormatter();
            using (var stream = new MemoryStream(bytes))
            {
                return formatter.Deserialize(stream);
            }
        }
    }
}
