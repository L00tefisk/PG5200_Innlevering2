using System.Collections.ObjectModel;
using System.Linq;
using GalaSoft.MvvmLight;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using GalaSoft.MvvmLight.Command;
using LevelEditor.Model;
using Microsoft.Practices.ServiceLocation;

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

        private Model.Model _mainModel { get; set; }

        public readonly LayerViewModel LayerViewModel;
        public readonly MapViewModel MapViewModel;
        public readonly TileSelectionViewModel TileSelectionViewModel;


        public ICommand ExitCommand { get; set; }
        public ICommand AddLayerCommand { get; private set; }

        private void CreateCommands()
        {
            //AddLayerCommand = new RelayCommand(AddLayer);
            ExitCommand = new RelayCommand(Exit);
        }
        /// <summary>
        /// Initializes a new instance of the MainViewModel class.
        /// </summary>
        public MainViewModel()
        {
            //DatabaseHelper.ExportToDatabase(); //Uncomment this to generate the database

            LayerViewModel = ServiceLocator.Current.GetInstance<LayerViewModel>();
            MapViewModel = ServiceLocator.Current.GetInstance<MapViewModel>();
            TileSelectionViewModel = ServiceLocator.Current.GetInstance<TileSelectionViewModel>();

            CreateCommands();
            _mainModel = Model.Model.Instance;            
        }

        private void Exit()
        {
            Application.Current.Shutdown();
        }

    }
}