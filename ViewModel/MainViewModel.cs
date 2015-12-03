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

        private Model.Model _mainModel { get; set; }

        public LayerViewModel LayerViewModel { get; set; }
        
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

        /// <summary>
        /// Initializes a new instance of the MainViewModel class.
        /// </summary>
        public MainViewModel()
        {
            //DatabaseHelper.ExportToDatabase(); //Uncomment this to generate the database

            _mainModel = Model.Model.Instance;

            
        }


    }
}