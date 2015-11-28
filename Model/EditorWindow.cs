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
using System.Timers;
using System.Windows.Threading;

namespace LevelEditor.Model
{
    public class EditorWindow : Canvas
    {
        private readonly Map _map;
        private Editor _editor;
        private DispatcherTimer _drawTimer;
        public static ushort MouseX { get; set; }
        public static ushort MouseY { get; set; }
        private ushort _oldId;

        private bool _updateTiles;
        private bool _updateCursor;

        // These are used in drawing. We are specifically avoiding creating these in the draw thread
        // to avoid enormous amounts of time being wasted by garbagde collection.
        private Pen _pen;
        private Point _horizontalLineStart;
        private Point _horizontalLineEnd;
        private Point _verticalLineStart;
        private Point _verticalLineEnd;
        private Rect _imageRectangle;

        public EditorWindow(Map map, Editor editor)
        {
            _map = map;
            _editor = editor;
            _oldId = 0;
            _updateTiles = true;
            _updateCursor = true;
            HorizontalAlignment = HorizontalAlignment.Left;
            VerticalAlignment = VerticalAlignment.Top;

            Background = Brushes.Transparent;
            _drawTimer = new DispatcherTimer();
            _drawTimer.Tick += new EventHandler(Draw);
            _drawTimer.Interval = new TimeSpan(0, 0, 0, 0, 16); // ~ 60 fps
            _drawTimer.Start();
            Width = _map.Width * _map.TileSize;
            Height = _map.Height * _map.TileSize;

            // Objects used for drawing. 
            _pen = new Pen(Brushes.SlateGray, 1);
            _horizontalLineStart = new Point();
            _horizontalLineEnd = new Point();
            _verticalLineStart = new Point();
            _verticalLineEnd = new Point();
            _imageRectangle = new Rect();
            //AddHandler(UIElement.KeyDownEvent, (RoutedEventHandler)ChangeTool);
            AddHandler(UIElement.MouseRightButtonDownEvent, (RoutedEventHandler)RemoveStart);
            AddHandler(UIElement.MouseRightButtonUpEvent, (RoutedEventHandler)Remove);
            AddHandler(UIElement.MouseMoveEvent, (RoutedEventHandler)Click);
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
            }
        }

        private void Draw(object sender, EventArgs e)
        {
            _updateTiles = true;
            _updateCursor = true;
            InvalidateVisual();
        }
        protected override void OnRender(DrawingContext dc)
        {
            base.OnRender(dc);
            DependencyObject parentObject = VisualTreeHelper.GetParent(this);
            ScrollContentPresenter parent = parentObject as ScrollContentPresenter;
            double offsetX = parent.HorizontalOffset;
            double offsetY = parent.VerticalOffset;
            int xIndex = (int)offsetX / _map.TileSize;
            int yIndex = (int)offsetY / _map.TileSize;

            if (_updateTiles)
            {
                for (int y = yIndex; y < (offsetY + parent.ActualHeight) / _map.TileSize; y++)
                {
                    _horizontalLineStart.X = xIndex * _map.TileSize;
                    _horizontalLineStart.Y = y * _map.TileSize;
                    _horizontalLineEnd.X = ((xIndex + 1) * _map.TileSize) + parent.ActualWidth;
                    _horizontalLineEnd.Y = y * _map.TileSize;

                    dc.DrawLine(_pen, _horizontalLineStart, _horizontalLineEnd);

                    for (int x = xIndex; x <= (offsetX + parent.ActualWidth) / _map.TileSize; x++)
                    {
                        _verticalLineStart.X = x * _map.TileSize;
                        _verticalLineStart.Y = yIndex * _map.TileSize;
                        _verticalLineEnd.X = x * _map.TileSize;
                        _verticalLineEnd.Y = ((yIndex + 1) * _map.TileSize) + parent.ActualHeight;
                        dc.DrawLine(_pen, _verticalLineStart, _verticalLineEnd);

                        if (_map.GetTile(x, y) != null && _map.GetTile(x, y).TileId != ushort.MaxValue)
                        {
                            _imageRectangle.X = x * _map.TileSize;
                            _imageRectangle.Y = y * _map.TileSize;
                            _imageRectangle.Width = _map.TileSize;
                            _imageRectangle.Height = _map.TileSize;
                            dc.DrawImage(_map.GetTile(x, y).ImgSrc, _imageRectangle);
                        }
                    }
                }
                _pen.Freeze();
                _updateTiles = false;
            }
            if (_updateCursor && _editor.SelectedTile.TileId < Model.ImgPaths.Count)
            {
                _imageRectangle.X = MouseX * _map.TileSize;
                _imageRectangle.Y = MouseY * _map.TileSize;
                _imageRectangle.Width = _map.TileSize;
                _imageRectangle.Height = _map.TileSize;
                dc.DrawImage(_editor.SelectedTile.ImgSrc, _imageRectangle);
                _updateCursor = false;
            }
        }
    }
}
