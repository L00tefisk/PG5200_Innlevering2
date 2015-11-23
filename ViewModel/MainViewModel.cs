using System.Windows.Input;
using GalaSoft.MvvmLight;
using System.Collections.Generic;
using System;
using System.Windows.Controls;
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
    }
}