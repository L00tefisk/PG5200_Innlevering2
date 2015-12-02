using System.Collections.ObjectModel;
using System.Linq;
using GalaSoft.MvvmLight;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using GalaSoft.MvvmLight.Command;
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

        public ICommand AddLayerCommand { get; private set; }
        public ICommand RemoveLayerCommand { get; private set; }
        public ICommand MoveLayerUpCommmand { get; private set; }
        public ICommand MoveLayerDownCommmand { get; private set; }

        private void CreateCommands()
        {
            AddLayerCommand = new RelayCommand(AddLayer);
            RemoveLayerCommand = new RelayCommand(RemoveLayer, CanRemoveLayer);
            MoveLayerUpCommmand = new RelayCommand(MoveLayerUp, CanMoveLayerUp);
            MoveLayerDownCommmand = new RelayCommand(MoveLayerDown, CanMoveLayerDown);
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
            AddLayer();
            
        }

        private int layerIndexName = 1;
        private void AddLayer()
        {
            Layers.Add(new Layer("Layer " + layerIndexName));
            layerIndexName++;
            //SelectedLayer = Layers.Last();
        }

        private void RemoveLayer()
        {
            int i = Layers.IndexOf(SelectedLayer);
            Layers.Remove(SelectedLayer);

            if (i == Layers.Count)
                SelectedLayer = Layers[i - 1];
            else
                SelectedLayer = Layers[i];

        }

        private void MoveLayerUp()
        {
            int i = Layers.IndexOf(SelectedLayer);

            Layer temp = Layers[i - 1];
            Layers[i - 1] = SelectedLayer;
            Layers[i] = temp;

            SelectedLayer = Layers[i - 1];
        }
        private void MoveLayerDown()
        {
            int i = Layers.IndexOf(SelectedLayer);
            Layer temp = Layers[i + 1];
            Layers[i + 1] = SelectedLayer;
            Layers[i] = temp;

            SelectedLayer = Layers[i + 1];
        }

        private bool CanRemoveLayer()
        {
            return Layers.Count > 1;
        }

        private bool CanMoveLayerDown()
        { 
            return Layers.IndexOf(SelectedLayer) < Layers.Count - 1;
        }

        private bool CanMoveLayerUp()
        {
            return Layers.IndexOf(SelectedLayer) >= 1;
        }

    }
}