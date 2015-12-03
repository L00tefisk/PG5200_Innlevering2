using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using GalaSoft.MvvmLight;
using LevelEditor.Model;
using LevelEditor.View;

namespace LevelEditor.ViewModel
{
    public class TileSelectionViewModel : ViewModelBase
    {
        private readonly Model.Model _model;


        private WrapPanel _tilePanel;
        public WrapPanel TilePanel
        {
            get
            {
                return _tilePanel;
            }
            set
            {
                if (_tilePanel != value)
                {
                    _tilePanel = value;
                    RaisePropertyChanged(() => TilePanel);
                }
            }
        }


        public TileSelectionViewModel()
        {
            _model = Model.Model.Instance;

            TilePanel = new WrapPanel();

            LevelEditorDatabaseDataContext db = new LevelEditorDatabaseDataContext();
            IOrderedQueryable<ImagePath> imagePaths =
                (from a in db.ImagePaths orderby a.Id select a);
            db.Connection.Close();

            foreach (ImagePath ip in imagePaths)
            {
                TileButton tileButton = new TileButton((ushort)(ip.Id - 1), ip.Description); // -1 because Eivind ruined the database

                tileButton.Background = Brushes.Transparent;
                tileButton.BorderThickness = new Thickness(0);

                tileButton.AddHandler(UIElement.MouseDownEvent, (RoutedEventHandler)SelectTile);
                tileButton.Click += SelectTile;
                TilePanel.Children.Add(tileButton);
            }

        }

        private void SelectTile(object sender, RoutedEventArgs e)
        {
            ((TileButton)TilePanel.Children[
                _model.MapView.Editor.SelectedTileId < Model.Model.ImgPaths.Count ? _model.MapView.Editor.SelectedTileId : 0
            ]).Background = Brushes.Transparent;

            TileButton tileButton = (TileButton)sender;
            tileButton.Background = new SolidColorBrush(new System.Windows.Media.Color { A = 50 });
            _model.MapView.Editor.SelectedTileId = ((TileButton)sender).TileId;
        }
    }
}
