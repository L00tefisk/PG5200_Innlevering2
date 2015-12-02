using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using LevelEditor.Model.Commands;

namespace LevelEditor.Model
{
    public class MainModel
    {
        static public List<String> ImgPaths { get; set; }
        public EditorWindow MapView { get; set; }
        public WrapPanel TilePanel { get; set; }

        public ObservableCollection<Layer> Layers;
        
        private Editor _editor;
        

        public MainModel()
        {
            LevelEditorDatabaseDataContext db = new LevelEditorDatabaseDataContext();
            IOrderedQueryable<ImagePath> imagePaths =
                (from a in db.ImagePaths orderby a.Id select a);
            db.Connection.Close();

            TilePanel = new WrapPanel();
            ImgPaths = new List<string>();

            foreach (ImagePath ip in imagePaths)
            {
                ImgPaths.Add("../../" + ip.Path);
                TileButton tileButton = new TileButton((ushort)(ip.Id-1), ip.Description); // -1 because Eivind ruined the database

                tileButton.AddHandler(UIElement.MouseDownEvent, (RoutedEventHandler)Click);
                tileButton.Click += new RoutedEventHandler(Click);
                tileButton.Background = Brushes.Transparent;
                tileButton.BorderThickness = new Thickness(0);
                TilePanel.Children.Add(tileButton);
            }
            _editor = new Editor();
            MapView = new EditorWindow(_editor);
        }

        private void Click(object sender, RoutedEventArgs e)
        {
            ((TileButton)TilePanel.Children[
                _editor.SelectedTileId < ImgPaths.Count ? _editor.SelectedTileId : 0
            ]).Background = Brushes.Transparent;

            TileButton tileButton = (TileButton)sender;
            tileButton.Background = new SolidColorBrush(new Color { A = 50 });
            _editor.SelectedTileId = ((TileButton)sender).TileId;
            _editor.SelectedTile = new Tile(_editor.GetSelectedTileImage(), 0, 0, _editor.SelectedTileId);
        }
    }
}
