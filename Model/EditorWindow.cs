using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace LevelEditor.Model
{
    public class EditorWindow : Canvas
    {
        private readonly Map _map;
        private Editor _editor;
        public EditorWindow(Map map, Editor editor)
        {
            Background = Brushes.Transparent;
            Height = 1000;
            Width = 1000;
            _map = map;
            _editor = editor;
            AddHandler(UIElement.MouseDownEvent, (RoutedEventHandler)Click);
        }

        private void Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Point relativePoint = TransformToAncestor(Application.Current.MainWindow).Transform(new Point(0, 0));
                
                DependencyObject parentObject = VisualTreeHelper.GetParent((EditorWindow)sender);
                ScrollContentPresenter parent = parentObject as ScrollContentPresenter;
                int x = (int)Math.Round(parent.HorizontalOffset + Mouse.GetPosition(Application.Current.MainWindow).X - relativePoint.X) / _map.TileSize;
                int y = (int)Math.Round(parent.VerticalOffset + Mouse.GetPosition(Application.Current.MainWindow).Y - relativePoint.Y) / _map.TileSize;
                MessageBox.Show("X = " + x + "\nY = " + y + "\n");
                _editor.SelectTile(x, y);
                _editor.SetTiles();
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message);
            }
        }

        protected override void OnRender(DrawingContext dc)
        {
            base.OnRender(dc);
            // Find the visible area to draw in
            // Draw in the visible area
     //       for (int y = 0; y < _map.Height; y++)
     //           for (int x = 0; x < _map.Width; x++)
     //               if (_map.GetTile(x, y) != null) ;
     //                   dc.DrawImage(_map.GetTile(x,y).Source, new Rect(x * 32, y*32, 32, 32));
            
        }
    }
}
