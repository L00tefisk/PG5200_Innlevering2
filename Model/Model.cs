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

        public List<ImagePath> ImagePaths; 
        public ObservableCollection<Layer> Layers;
        public Editor _editor;

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
    }
}
