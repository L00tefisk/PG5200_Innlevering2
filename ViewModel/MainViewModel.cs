using System.Collections.ObjectModel;
using System.Windows.Input;
using GalaSoft.MvvmLight;
using System.Windows;
using System.Windows.Controls;
using LevelEditor.Model;

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
        public ObservableCollection<Layer> Layers
        {
            get { return _mainModel.Layers; }
            set
            {
                if (_mainModel.Layers != value)
                {
                    _mainModel.Layers = value;
                    RaisePropertyChanged(() => Layers);
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
            //DatabaseHelper.ExportToDatabase();

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
    }
}