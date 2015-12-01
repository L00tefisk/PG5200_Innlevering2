using System.Windows.Input;
using GalaSoft.MvvmLight;
using System.Windows;
using System.Windows.Controls;
using LevelEditor.Model;
using System.Windows.Data;

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