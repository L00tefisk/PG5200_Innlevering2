using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using GalaSoft.MvvmLight;
using LevelEditor.Model;

namespace LevelEditor.ViewModel
{
    public class TileSelectionViewModel : ViewModelBase
    {
        private Model.Model _model;
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

        public TileSelectionViewModel()
        {
            _model = Model.Model.Instance;;   
        }


    }
}
