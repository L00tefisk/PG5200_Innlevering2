using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using LevelEditor.Model.Commands;

namespace LevelEditor.Model
{
    public class Model
    {
        private static Model _instance;

        public static Model Instance
        {
            get { return _instance ?? (_instance = new Model()); }
            set { _instance = value; }
        }

        static public List<String> ImgPaths { get; set; }
        public EditorWindow MapView { get; set; }

        public ObservableCollection<Layer> Layers;

        public Model()
        {
            LevelEditorDatabaseDataContext db = new LevelEditorDatabaseDataContext();
            IOrderedQueryable<ImagePath> imagePaths =
                (from a in db.ImagePaths orderby a.Id select a);
            db.Connection.Close();
            
            ImgPaths = new List<string>();

            foreach (ImagePath ip in imagePaths)
            {
                ImgPaths.Add("../../" + ip.Path);
            }
            MapView = new EditorWindow();

            Layers = new ObservableCollection<Layer>();

        }
    }
}
