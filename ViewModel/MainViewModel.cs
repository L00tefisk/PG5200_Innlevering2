using System.Collections.ObjectModel;
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
        private readonly Model.MainModel _mainModel;

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

        private Layer _selectedLayer;

        public Layer SelectedLayer
        {
            get
            {
                return _selectedLayer;
            }
            set
            {
                if (_selectedLayer != value)
                {
                    _selectedLayer = value;
                    RaisePropertyChanged(() => SelectedLayer);

                    //MessageBox.Show(_selectedLayer.Name +" is "+_selectedLayer.IsVisisble);
                }
            }
        }

        #endregion
        
        #region Commands
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
            //DatabaseHelper.ExportToDatabase(); //Uncomment this to generate the database

            CreateCommands();
            _mainModel = new MainModel();
            
        }
        private bool CanPerform()
        {
            //TODO: Additional validation
            return true;
        }
    }
}