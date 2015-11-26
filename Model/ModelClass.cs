using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace LevelEditor.Model
{
    public class ModelClass
    {
        public Renderer MapView { get; set; }
        public WrapPanel TilePanel { get; set; }
        public string Name { get; set; }
        private Map _map;
        public enum Shapes
        {
            Square,
            Circle,
            Triangle
        }
        public ModelClass()
        {
            LevelEditorDatabaseDataContext db = new LevelEditorDatabaseDataContext();
            IOrderedQueryable<ImagePath> imagePaths =
                (from a in db.ImagePaths orderby a.Id select a);

            TilePanel = new WrapPanel();
            _map = new Map();
            

            foreach(ImagePath ip in imagePaths)
            {
                string text = "";
                
                Tile tile = new Tile((ushort)ip.Id, "../../" + ip.Path, false);
                tile.Width = 50;
                tile.Height = 50;
                tile.Margin = new Thickness(10);
                tile.Stretch = Stretch.Fill;

                tile.Focusable = true;
                tile.AddHandler(UIElement.MouseDownEvent, (RoutedEventHandler)HandleTheClick);
                TilePanel.Children.Add(tile);
            }

            MapView = new Renderer(_map);

        }

        private void HandleTheClick(object sender, RoutedEventArgs e)
        {
            Tile i = (Tile) sender;
            Console.WriteLine(i.BitmapID);
        }
    }
}
