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
        public static ushort MouseX { get; set; }
        public static ushort MouseY { get; set; }
        private ushort _oldId;
        public EditorWindow(Map map, Editor editor)
        {
            _map = map;
            _editor = editor;
            _oldId = 0;
            HorizontalAlignment = HorizontalAlignment.Left;
            VerticalAlignment = VerticalAlignment.Top;

            Background = Brushes.Transparent;
            Width = _map.Width * _map.TileSize;
            Height = _map.Height * _map.TileSize;


            AddHandler(UIElement.KeyDownEvent, (RoutedEventHandler)ChangeTool);
            AddHandler(UIElement.MouseRightButtonDownEvent, (RoutedEventHandler)RemoveStart);
            AddHandler(UIElement.MouseRightButtonUpEvent, (RoutedEventHandler)Remove);
            AddHandler(UIElement.MouseMoveEvent, (RoutedEventHandler)Click);
            AddHandler(UIElement.MouseDownEvent, (RoutedEventHandler)Click);
   //         AddHandler()
        }

        private void ChangeTool(object sender, RoutedEventArgs e)
        {
            if (Keyboard.IsKeyToggled(Key.W))
                _editor.SelectedTool = 2;
            else if (Keyboard.IsKeyToggled(Key.S))
                _editor.SelectedTool = 4;
        }
        private void RemoveStart(object sender, RoutedEventArgs e)
        {
            _oldId = _editor.SelectedTileId;
        }
        private void Remove(object sender, RoutedEventArgs e)
        {
            _editor.SelectedTileId = _oldId;
            InvalidateVisual();
        }
        private void Click(object sender, RoutedEventArgs e)
        {
            if (IsMouseOver)
            {
                Point relativePoint = TransformToAncestor(Application.Current.MainWindow).Transform(new Point(0, 0));

                DependencyObject parentObject = VisualTreeHelper.GetParent((EditorWindow)sender);
                ScrollContentPresenter parent = parentObject as ScrollContentPresenter;

                MouseX = (ushort)(Math.Floor(Mouse.GetPosition(this).X / _map.TileSize));
                MouseY = (ushort)(Math.Floor(Mouse.GetPosition(this).Y / _map.TileSize));

                if (Mouse.LeftButton == MouseButtonState.Pressed)
                {
                    _editor.SelectTile(MouseX, MouseY);
                    _editor.PerformAction();
                }
                else if (Mouse.RightButton == MouseButtonState.Pressed)
                {
                    _editor.SelectedTileId = ushort.MaxValue;
                    _editor.SelectTile(MouseX, MouseY);
                    _editor.PerformAction();
                }
                InvalidateVisual();
            }
        }

        protected override void OnRender(DrawingContext dc)
        {
            DependencyObject parentObject = VisualTreeHelper.GetParent(this);
            ScrollContentPresenter parent = parentObject as ScrollContentPresenter;
            base.OnRender(dc);
            // Find the visible area to draw in
            // Draw in the visible area

            double offsetX = parent.HorizontalOffset;
            double offsetY = parent.VerticalOffset;
            int xIndex = (int)offsetX / _map.TileSize;
            int yIndex = (int)offsetY / _map.TileSize;

            Pen pen = new Pen(Brushes.SlateGray, 1);
            for (int y = yIndex; y < (offsetY + parent.ActualHeight)/_map.TileSize; y++)
            {
                dc.DrawLine(pen, new Point(xIndex * _map.TileSize, y * _map.TileSize), new Point(((xIndex + 1) * _map.TileSize) + parent.ActualWidth, y * _map.TileSize));
                for (int x = xIndex; x <= (offsetX + parent.ActualWidth) /_map.TileSize; x++)
                {
                    dc.DrawLine(pen, new Point(x * _map.TileSize, yIndex * _map.TileSize), new Point(x * _map.TileSize, ((yIndex + 1) * _map.TileSize) + parent.ActualHeight));
                    if (_map.GetTile(x, y) != null && _map.GetTile(x, y).TileId != ushort.MaxValue)
                        dc.DrawImage(_map.GetTile(x, y).ImgSrc,
                            new Rect(x*_map.TileSize, y*_map.TileSize, _map.TileSize, _map.TileSize));
                }
            }
            pen.Freeze();
            if (_editor.SelectedTileId < Model.ImgPaths.Count)
            {
                ImageSource imgSrc = new BitmapImage(new Uri(Model.ImgPaths[_editor.SelectedTileId], UriKind.Relative));
                dc.DrawImage(imgSrc, new Rect(MouseX * _map.TileSize, MouseY * _map.TileSize, _map.TileSize, _map.TileSize));
            }
        }
    }
}
