using System.Windows.Input;
using GalaSoft.MvvmLight;
using System.Collections.Generic;
using System;
using System.IO.Packaging;
using System.Linq;
using System.Windows.Controls;
using GalaSoft.MvvmLight.Command;
using LevelEditor.Model;
using LevelEditor;

namespace LevelEditor.ViewModel
{
    /// <summary>
    /// This class contains properties that the main View can data bind to.
    /// <para>
    /// See http://www.galasoft.ch/mvvm
    /// </para>
    /// </summary>
    public class MainViewModel : ViewModelBase
    {
        #region Properties
        private ModelClass _model;

        public IEnumerable<ModelClass.Shapes> Shapes
        {
            get
            {
                return Enum.GetValues(typeof(ModelClass.Shapes)) as IEnumerable<ModelClass.Shapes>;
            }
        }

        public WrapPanel DynamicGrid
        {
            get
            {
                return _model.TilePanel;
            }
            set
            {
                if (_model.TilePanel != value)
                {
                    _model.TilePanel = value;
                    RaisePropertyChanged(() => DynamicGrid);
                }
            }

        }

        public string Name
        {
            get
            {
                return _model.Name;
            }

            set
            {
                if (_model.Name != value)
                {
                    _model.Name = value;
                    RaisePropertyChanged(() => Name);
                }
            }
        }


        #endregion

        #region Commands

        public ICommand NewCommand
        {
            get; private set;
        }
        private void CreateCommands()
        {
            NewCommand = new RelayCommand(NewModel, CanPerform);
        }

        #endregion

        /// <summary>
        /// Initializes a new instance of the MainViewModel class.
        /// </summary>
        public MainViewModel()
        {
            CreateCommands();
            _model = new ModelClass();

            //exportToDatabase();
        }

        private bool CanPerform()
        {
            //TODO: Additional validation
            return true;
        }

        public void NewModel()
        {
            PopulateView(new ModelClass());
        }

        /// <summary>
        /// Populates View with the specified data.
        /// </summary>
        public void PopulateView(ModelClass model)
        {
            Name = model.Name;

            
        }

        private void exportToDatabase()
        {
            LevelEditorDatabaseDataContext db = new LevelEditorDatabaseDataContext();

            IOrderedQueryable<ImagePath> toDelete =
                (from a in db.ImagePaths orderby a.Id select a);
            db.ImagePaths.DeleteAllOnSubmit(toDelete);
            db.SubmitChanges();

            string path = "../../Sprites/Tiles/";
            int id = 0;

            foreach (string f in System.IO.Directory.GetFiles(path))
            {
                //string filename = f.Substring(path.Length - 1, f.Length - 4);
                string filename = f.Substring(path.Length, f.Length - 4 - path.Length); //-4 to remove file extension
                string description = splitWord(filename);

                db.ImagePaths.InsertOnSubmit(
                    new ImagePath()
                    {
                        Id = id,
                        Path = f.Substring(6),
                        Description = description
                    }
                );
                id++;
            }

            
            db.SubmitChanges();
        }



        private string splitWord(string s)
        {
            string desc = "";
            desc += Char.ToUpper(s[0]);
            int i = 1;
            foreach (Char c in s.Substring(i, s.Length-i))
            {
                if (Char.IsUpper(c))
                {
                    desc += " " + splitWord(s.Substring(i));
                    break;
                }
                else if (c == '_')
                {
                    desc += " " + splitWord(s.Substring(i + 1));
                    break;
                }
                else if (Char.IsDigit(c))
                {
                    desc += " " + c;
                }
                else
                {
                    desc += c;
                }
                i++;
            }
            return desc;
        }
    }
}