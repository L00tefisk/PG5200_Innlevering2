using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace LevelEditor.Model
{
    public class EditorWindow : Canvas
    {
        private readonly Map _map;
        private Editor _editor;
        private ushort _mouseX;
        private ushort _mouseY;
        public EditorWindow(Map map, Editor editor)
        {
            Background = Brushes.Transparent;
            Height = 1000;
            Width = 1000;
            _map = map;
            _editor = editor;
            AddHandler(UIElement.MouseMoveEvent, (RoutedEventHandler)Click);
        }

        private void Click(object sender, RoutedEventArgs e)
        {

            if (IsMouseOver)
            {
                Point relativePoint = TransformToAncestor(Application.Current.MainWindow).Transform(new Point(0, 0));

                DependencyObject parentObject = VisualTreeHelper.GetParent((EditorWindow)sender);
                ScrollContentPresenter parent = parentObject as ScrollContentPresenter;
                _mouseX = (ushort)(Math.Floor(parent.HorizontalOffset + Mouse.GetPosition(this).X - relativePoint.X) /
                            _map.TileSize);
                _mouseY = (ushort)(Math.Floor(parent.VerticalOffset + Mouse.GetPosition(this).Y - relativePoint.Y) /
                        _map.TileSize);
                if (Mouse.LeftButton == MouseButtonState.Pressed)
                {
                    try
                    {
                        _editor.SelectTile(_mouseX, _mouseY);
                        _editor.SetTiles();
                    }
                    catch (Exception exc)
                    {
                        MessageBox.Show(exc.Message);
                    }
                }
            }
            InvalidateVisual();
        }

        protected override void OnRender(DrawingContext dc)
        {
            base.OnRender(dc);
            ImageSource imgSrc = new BitmapImage(new Uri(Model.ImgPaths[_editor.SelectedTileId], UriKind.Relative));
            // Find the visible area to draw in
            // Draw in the visible area

            for (int y = 0; y < _map.Height; y++)
                for (int x = 0; x < _map.Width; x++)
                      if (_map.GetTile(x, y) != null)
                        dc.DrawImage(_map.GetTile(x,y).ImgSrc, new Rect(x * 32, y*32, 32, 32));
            
            dc.DrawImage(imgSrc, new Rect(_mouseX * _map.TileSize, _mouseY * _map.TileSize, _map.TileSize, _map.TileSize));
        }
    }
}
