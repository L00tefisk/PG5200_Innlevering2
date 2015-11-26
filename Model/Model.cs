using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using LevelEditor.Model.Commands;

namespace LevelEditor.Model
{
    public class Model
    {
        static public List<String> ImgPaths { get; set; }
        public EditorWindow MapView { get; set; }
        public WrapPanel TilePanel { get; set; }
        public string Name { get; set; }
        private Map _map;
        private Editor _editor;
        private CommandController _commandController;

        public Model()
        {
            LevelEditorDatabaseDataContext db = new LevelEditorDatabaseDataContext();
            IOrderedQueryable<ImagePath> imagePaths =
                (from a in db.ImagePaths orderby a.Id select a);

            TilePanel = new WrapPanel();
            ImgPaths = new List<string>();

            foreach (ImagePath ip in imagePaths)
            {
                ImgPaths.Add("../../" + ip.Path);
                TileButton tileButton = new TileButton((ushort)ip.Id);

                tileButton.AddHandler(UIElement.MouseDownEvent, (RoutedEventHandler)Click);
                tileButton.Background = Brushes.Transparent;
                tileButton.BorderThickness = new Thickness(0);
                TilePanel.Children.Add(tileButton);
            }

            _map = new Map(100, 100);
            _commandController = new CommandController();
            _editor = new Editor(_map, _commandController);
            MapView = new EditorWindow(_map, _editor);
        }

        private void Click(object sender, RoutedEventArgs e)
        {
            _editor.SelectedTileId = ((Tile)sender).TileId;
        }
    }
}
