using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Xml.Serialization;
using LevelEditor.Model.Commands;
using Newtonsoft.Json;

namespace LevelEditor.Model
{
    public class Model
    {
        private static Model _instance;
        public static Model Instance
        {
            get
            {
                if (_instance != null)
                    return _instance;

                _instance = new Model();
                _instance.Init();
                return _instance;
            }
            set
            {
                _instance = value;
            }
        }

        [JsonIgnore]
        public List<ImagePath> ImagePaths;
        [JsonIgnore]
        public Editor _editor;

        public ObservableCollection<Layer> Layers;
        

        public Model()
        {
            ImagePaths = new List<ImagePath>();
            LevelEditorDatabaseDataContext db = new LevelEditorDatabaseDataContext();
            IOrderedQueryable<ImagePath> imagePaths = (from a in db.ImagePaths orderby a.Id select a);

            ImagePaths.AddRange(imagePaths);
            db.Connection.Close();
        }

        private void Init()
        {
            _editor = new Editor();
            Layers = new ObservableCollection<Layer>();
        }

        private JsonSerializerSettings _config = new JsonSerializerSettings
        {
            //Formatting = Formatting.Insented,
            NullValueHandling = NullValueHandling.Ignore
        };

        
        public SaveModel Load(string value)
        {
            try
            {
                return JsonConvert.DeserializeObject<SaveModel>(value, _config);
            }
            catch (Exception e)
            {
                MessageBox.Show("ERROR: " + e.Message);
                return null;
            }
        }

        public string Save()
        {
            SaveModel saveModel  = new SaveModel();
            saveModel.GenerateSaveModel(this);
            return JsonConvert.SerializeObject(saveModel, _config);


            /*
            using (BinaryWriter binaryWriter = new BinaryWriter(File.Open(Filename, FileMode.Create)))
            {
                // Record how many rows and columns there are, then record how many bytes one Tile uses.
                binaryWriter.Write(Height);
                binaryWriter.Write(Width);
                binaryWriter.Write(SerializeToBytes(Tiles[0]).Length);
                foreach (Tile tile in Tiles)
                {

                    binaryWriter.Write(SerializeToBytes(tile));
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
                Tiles = new Tile[rows * columns];
                for (int y = 0; y < rows; y++)
                {
                    for (int x = 0; x < columns; x++)
                    {
                        Byte[] rawData = binaryReader.ReadBytes(byteCount);
                        Tile t = (Tile)DeserializeFromBytes(rawData);

                        Tiles[y * columns + x] = t;
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
            }*/
        }
    }
}
