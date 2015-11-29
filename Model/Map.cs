using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Security.RightsManagement;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Xaml;
using Microsoft.Win32;

namespace LevelEditor.Model
{
    public class Map
    {
		public string Filename { get; set; }
        public short TileSize { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public List<List<Tile>> _level;

        public Map(int width, int height)
        {
            Width = width;
            Height = height;
            _level = new List<List<Tile>>();
            Random randomId = new Random();
            for (int y = 0; y < Height; y++)
            { 
                _level.Add(new List<Tile>());
                for (int x = 0; x < Width; x++)
                {
                    try
                    {
                        Tile t = new Tile(0, y, x);
                        _level[y].Add( new Tile((ushort)randomId.Next(0, Model.ImgPaths.Count), y, x));

                    }
                    catch (Exception e )
                    {
                        MessageBox.Show(e.ToString());
                        throw;
                    }
                    
                }
            }

        TileSize = 32;
        }
        /// <summary>
        /// Set tile in the level
        /// </summary>
        /// <param name="t">The tile you want to set</param>
        public void SetTile(ushort x, ushort y, ImageSource image)
        {
            if (_level[y][x].Source != image)
            {
                _level[y][x].ChangeTile(image);
            }
        }
        /// <summary>
        /// Returns the tile at (x, y)
        /// </summary>
        /// <param name="x">X position</param>
        /// <param name="y">Y position</param>
        /// <returns></returns>
        public Tile GetTile(int x, int y)
        {
            if (_level[y].Count > 0)
                return _level[y][x];
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
				binaryWriter.Write(SerializeToBytes(_level[0][0]).Length);
                foreach (List<Tile> tList in _level)
                {
                    foreach (Tile t in tList)
                    {
                        binaryWriter.Write(SerializeToBytes(t));
                    }
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
                _level = new List<List<Tile>>();
                for (int y = 0; y < rows; y++)
                {
                    _level.Add(new List<Tile>());
                    for (int x = 0; x < columns; x++)
                    {
                        Byte[] rawData = binaryReader.ReadBytes(byteCount);
                        Tile t = (Tile)DeserializeFromBytes(rawData);

                        _level[y].Add(t);
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
