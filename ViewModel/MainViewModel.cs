using System.Collections.ObjectModel;
using System.Linq;
using System.Threading;
using GalaSoft.MvvmLight;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using GalaSoft.MvvmLight.Command;
using LevelEditor.Model;
using Microsoft.Practices.ServiceLocation;
using System;
using System.IO;

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

        
        public readonly MapViewModel MapViewModel;
        public readonly LayerViewModel LayerViewModel;
        public readonly TileSelectionViewModel TileSelectionViewModel;


        public ICommand ExitCommand { get; set; }
        public ICommand SaveCommand { get; set; }

        private void CreateCommands()
        {
            SaveCommand = new RelayCommand(Save);
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
            //TileSelectionViewModel = ServiceLocator.Current.GetInstance<TileSelectionViewModel>();

            CreateCommands();
            _mainModel = Model.Model.Instance;            
        }

        private void Save()
        {
            StreamWriter writer = new StreamWriter("level.json");
            writer.Write(_mainModel.Save());
            writer.Close();
        }

        private void Exit()
        {
            Application.Current.Shutdown();
        }

    }
}