using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Security.RightsManagement;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Xaml;
using Microsoft.Win32;

namespace LevelEditor.Model
{
    class Map
    {
		string Filename { get; set; }
		List<List<Tile>> level { get; set; }
		public Map() { }

        public void Save()
        {
            using (BinaryWriter binaryWriter = new BinaryWriter(File.Open(Filename, FileMode.Create)))
            {
				// Record how many rows and columns there are, then record how many bytes one Image uses.
				binaryWriter.Write(level.Count);
                binaryWriter.Write(level[0].Count);
				binaryWriter.Write(SerializeToBytes(level[0][0]).Length);
                foreach (List<Tile> tList in level)
                {
                    foreach (Tile t in tList)
                    {
                        binaryWriter.Write(SerializeToBytes(t));
                    }
                }
            }
			
        }

        public void Load()
        {
            using (BinaryReader binaryReader = new BinaryReader(File.Open(Filename, FileMode.Open)))
            {
                int rows = binaryReader.ReadInt32();
                int columns = binaryReader.ReadInt32();
                int byteCount = binaryReader.ReadInt32();
				level = new List<List<Tile>>();
                for (int y = 0; y < rows; y++)
                {
					level.Add(new List<Tile>());
                    for (int x = 0; x < columns; x++)
                    {
                        Byte[] rawData = binaryReader.ReadBytes(byteCount);
                        Tile t = (Tile)DeserializeFromBytes(rawData);

                        level[y].Add(t);
                    }
                }
            }
        }

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
