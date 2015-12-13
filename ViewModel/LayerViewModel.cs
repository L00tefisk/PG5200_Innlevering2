using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using LevelEditor.Model;
using Microsoft.Practices.ServiceLocation;

namespace LevelEditor.ViewModel
{
    public class LayerViewModel : ViewModelBase
    {

        public ObservableCollection<Layer> Layers
        {
            get { return Model.Model.Instance.Layers; }
            set
            {
                if (Model.Model.Instance.Layers != value)
                {
                    Model.Model.Instance.Layers = value;
                    RaisePropertyChanged(() => Layers);
                }
            }
        }
        public Layer SelectedLayer
        {
            get
            {
                return Model.Model.Instance._editor.CurrentLayer;
            }
            set
            {
                if (Model.Model.Instance._editor.CurrentLayer != value)
                {
                    Model.Model.Instance._editor.CurrentLayer = value;
                    RaisePropertyChanged(() => SelectedLayer);
                    
                }
            }
        }

        public ICommand AddLayerCommand { get; private set; }
        public ICommand RemoveLayerCommand { get; private set; }
        public ICommand MoveLayerUpCommmand { get; private set; }
        public ICommand MoveLayerDownCommmand { get; private set; }
        public ICommand LoadCommand { get; private set; }

        private void CreateCommands()
        {
            AddLayerCommand = new RelayCommand(AddLayer);
            RemoveLayerCommand = new RelayCommand(RemoveLayer, CanRemoveLayer);
            MoveLayerUpCommmand = new RelayCommand(MoveLayerUp, CanMoveLayerUp);
            MoveLayerDownCommmand = new RelayCommand(MoveLayerDown, CanMoveLayerDown);

            LoadCommand = new RelayCommand(Load);
        }

        public LayerViewModel()
        {
            CreateCommands();
            //MainViewModel = ServiceLocator.Current.GetInstance<MainViewModel>();

            Layers.Add(new Layer("Layer " + _layerIndexName));
            SelectedLayer = Layers[0];
        }

        private int _layerIndexName = 1;
        private void AddLayer()
        {
            Layers.Add(new Layer("Layer " + _layerIndexName));
            _layerIndexName++;
            SelectedLayer = Layers.Last();

            ServiceLocator.Current.GetInstance<MainViewModel>().MapViewModel.AddLayer();
        }
        private void RemoveLayer()
        {
            int i = Layers.IndexOf(SelectedLayer);
            ServiceLocator.Current.GetInstance<MainViewModel>().MapViewModel.RemoveLayer();
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

            return Layers.Count > 1 && SelectedLayer != null;
        }
        private bool CanMoveLayerDown()
        {
            return Layers.IndexOf(SelectedLayer) < Layers.Count - 1;
        }
        private bool CanMoveLayerUp()
        {
            return Layers.IndexOf(SelectedLayer) >= 1;
        }

        private void Load()
        {
            StreamReader reader = new StreamReader("level.json");

            SaveModel saveModel = Model.Model.Instance.Load(reader.ReadToEnd());

            reader.Close();

            PopulateView(saveModel);
        }
        private void PopulateView(SaveModel saveModel)
        {
            int i = 1;
            foreach (ushort[][] map in saveModel.Layers)
            {
                AddLayer();
                SelectedLayer = Layers[i];

                for (int x = 0; x < 100; x++)
                    for (int y = 0; y < 100; y++)
                        if (map[x][y] != 0)
                            Model.Model.Instance._editor.SetTile(x, y, map[x][y] - 1);

            }

            //Hack since you can't clear the layer list
            SelectedLayer = Layers[0];
            RemoveLayer();
            SelectedLayer = Layers.Last();
        }

    }

}