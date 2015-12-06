using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Windows.Media;
using System.Windows;

namespace LevelEditor.Model
{
    public class Map
    {
        public string Filename { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        private Tile[] _level;
        private int _tileSize;
        public Map(int width, int height)
        {
            Width = width;
            Height = height;
            _level = new Tile[height * width];
            Random randomId = new Random();
            for (int i = 0; i < height * width; i++)
            {
                _level[i] = new Tile(null, (i - (i / width) * width) , i / width, int.MaxValue);
            }
            _tileSize = 32;

            // index = y * width + x
            // x = index / y ' width
        }
        /// <summary>
        /// Set tile in the level
        /// </summary>
        /// <param name="p">The position of the tile you want to set</param>
        /// <param name="image">TODO</param>
        public void SetTile(int x, int y, int id, ImageSource image) //Set tile burde kanskje ha samme rekkefølge for parameterene? ikke at det har noe å si
        {
            int index = y*Width + x;
            if (index >= 0 && index < _level.Length && _level[index] != null)
            {
                _level[index].Source = image;
                _level[index].Id = id;
            }

        }
        /// <summary>
        /// Returns the tile at the specified point.x
        /// </summary>
        /// <param name="p">TODO</param>
        /// <returns></returns>
        public Tile GetTile(int x, int y)
        {
            return _level[y * Width + x];
        }

        public int GetTileSize()
        {
            return _tileSize;
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
