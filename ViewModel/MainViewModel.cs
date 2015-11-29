using System.Windows.Input;
using GalaSoft.MvvmLight;
using System.Collections.Generic;
using System;
using System.CodeDom;
using System.IO.MemoryMappedFiles;
using System.IO.Packaging;
using System.Linq;
using System.Linq.Expressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using GalaSoft.MvvmLight.Command;
using LevelEditor.Model;
using LevelEditor;
using LevelEditor.View;

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
        private Model.MainModel _mainModel;
        private InputHandler _inputHandler;

        public EditorWindow LevelView
        {
            get
            {
                return _mainModel.MapView;
            }
            set
            {
                if (_mainModel.MapView != value)
                {
                    _mainModel.MapView = value;
                    RaisePropertyChanged(() => LevelView);
                }
            }
        }
        public WrapPanel DynamicGrid
        {
            get
            {
                return _mainModel.TilePanel;
            }
            set
            {
                if (_mainModel.TilePanel != value)
                {
                    _mainModel.TilePanel = value;
                    RaisePropertyChanged(() => DynamicGrid);
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
            //NewCommand = new RelayCommand(NewModel, CanPerform);
        }
        #endregion

        /// <summary>
        /// Initializes a new instance of the MainViewModel class.
        /// </summary>
        public MainViewModel()
        {
           // exportToDatabase();

            CreateCommands();
            _mainModel = new MainModel();
            _inputHandler = new InputHandler();
        }

        private void ProcessInput(object sender, RoutedEventArgs e)
        {
            
        }
        private bool CanPerform()
        {
            //TODO: Additional validation
            return true;
        }

        private void exportToDatabase()
        {
            LevelEditorDatabaseDataContext db = new LevelEditorDatabaseDataContext();

            try
            {
                IOrderedQueryable<ImagePath> toDelete =
                    (from a in db.ImagePaths orderby a.Id select a);
                db.ImagePaths.DeleteAllOnSubmit(toDelete);

                db.SubmitChanges();
            
                db.ExecuteCommand("DBCC CHECKIDENT('dbo.ImagePaths', RESEED, 0);");
            }
            catch (Exception exp)
            {
                MessageBox.Show(exp.Message);
            }
            finally
            {
                string path = "../../Sprites/Tiles/";

                foreach (string f in System.IO.Directory.GetFiles(path))
                {
                    //string filename = f.Substring(path.Length - 1, f.Length - 4);
                    string filename = f.Substring(path.Length, f.Length - 4 - path.Length);
                    //-4 to remove file extension
                    string description = splitWord(filename);

                    db.ImagePaths.InsertOnSubmit(
                        new ImagePath()
                        {
                            Path = f.Substring(6),
                            Description = description
                        }
                    );
                }
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