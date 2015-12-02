using System;
using System.Collections.Generic;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows;
using System.Windows.Shapes;

namespace LevelEditor.Model
{
    public class EditorWindow : Canvas
    {
        private Editor _editor;
        public static Point MousePosition;
        private double _scaleFactor;
        private Point _selectedTilesOffset;
        private Point _selectionPointStart;
        private Point _selectionPointEnd;

        private Rectangle _selectionRect;
        private List<Tile> _tempSelectedTileList;
        public EditorWindow(Editor editor)
        {
            _editor = editor;
            _scaleFactor = 1;
            _selectedTilesOffset.X = Double.MaxValue;
            _selectedTilesOffset.Y = Double.MaxValue;
            _tempSelectedTileList = new List<Tile>();
            _selectionPointStart = new Point();
            _selectionPointEnd = new Point();
            MousePosition = new Point();
            _selectionRect = new Rectangle();
            HorizontalAlignment = HorizontalAlignment.Left;
            VerticalAlignment = VerticalAlignment.Top;

            Background = Brushes.Transparent;
            Width = _editor.GetMapWidth() * _editor.GetTileSize();
            Height = _editor.GetMapHeight() * _editor.GetTileSize();

            AddHandler(UIElement.MouseRightButtonDownEvent, (RoutedEventHandler)SelectBegin);
            AddHandler(UIElement.MouseRightButtonUpEvent, (RoutedEventHandler)SelectEnd);

            AddHandler(UIElement.MouseMoveEvent, (RoutedEventHandler)Click);
            //AddHandler(UIElement.MouseDownEvent, (RoutedEventHandler)Zoom);

            int mapHeight = editor.GetMapHeight();
            int mapWidth = editor.GetMapWidth();
            Random rng = new Random();
            for (int y = 0; y < mapHeight; y++)
            {
                for (int x = 0; x < mapWidth; x++)
                {
                    Tile t = editor.GetTile(x, y);
                    Children.Add(t);
                    Canvas.SetTop(t, y * 32);
                    Canvas.SetLeft(t, x * 32);

                }
            }
            _selectionRect.Fill = new SolidColorBrush(Colors.Black);
            _selectionRect.Stroke = new SolidColorBrush(Colors.Black);
            _selectionRect.Width = 32;
            _selectionRect.Height = 32;
            _selectionRect.Opacity = 0.5;
        }
        private void Zoom(object sender, RoutedEventArgs e)
        {
            if(Mouse.RightButton == MouseButtonState.Pressed)
            {
                _scaleFactor -= 0.1;
                if (_scaleFactor < 0.5)
                    _scaleFactor = 0.5;
            }
            else if(Mouse.LeftButton == MouseButtonState.Pressed)
            {
                _scaleFactor += 0.1;
                if (_scaleFactor > 2)
                    _scaleFactor = 2;
            }
            RenderTransform = new ScaleTransform(_scaleFactor, _scaleFactor, Mouse.GetPosition(this).X, Mouse.GetPosition(this).Y);
        }

        /*
            holy fuck what is this even
            we will have to purge it with fire
            this monster should not walk the same soil as us.
        */
        private void SelectBegin(object sender, RoutedEventArgs e)
        {
            foreach (Tile t in _tempSelectedTileList)
            {
                Children.Remove(t);
            }
            _tempSelectedTileList.Clear();
            _selectionPointStart = MousePosition;
            Children.Add(_selectionRect);
            Canvas.SetTop(_selectionRect, MousePosition.Y * 32);
            Canvas.SetLeft(_selectionRect, MousePosition.X * 32);
        }
        private void SelectEnd(object sender, RoutedEventArgs e)
        {
            double dX = _selectionPointEnd.X - _selectionPointStart.X;
            double dY = _selectionPointEnd.Y - _selectionPointStart.Y;
            for (int y = 0; y < dY; y++)
            {
                for(int x = 0; x < dX; x++)
                {
                    Tile tempTile = new Tile(_editor.GetSelectedTileImage(), x, y, _editor.SelectedTileId);
                    _tempSelectedTileList.Add(tempTile);
                    _tempSelectedTileList[_tempSelectedTileList.Count - 1].Opacity = 0.5;
                    Children.Add(_tempSelectedTileList[_tempSelectedTileList.Count - 1]);
                }
            }
            Children.Remove(_selectionRect);
        }
        
        private void Click(object sender, RoutedEventArgs e)
        {
            MousePosition.X = (ushort)(Math.Floor(Mouse.GetPosition(this).X / 32));
            MousePosition.Y = (ushort)(Math.Floor(Mouse.GetPosition(this).Y / 32));
            if (_tempSelectedTileList.Count != 0)
            {
                for(int i = 0; i < _tempSelectedTileList.Count; i++)
                { 
                    
                    Canvas.SetTop(_tempSelectedTileList[i], MousePosition.Y * 32 + (_tempSelectedTileList[i].Y * 32));
                    Canvas.SetLeft(_tempSelectedTileList[i], MousePosition.X * 32 + (_tempSelectedTileList[i].X * 32));
                }
            }
            if (Mouse.LeftButton == MouseButtonState.Pressed)
            {
                if (_tempSelectedTileList.Count > 0)
                {
                    Point tileLocation = new Point();
                    for (int i = 0; i < _tempSelectedTileList.Count; i++)
                    {
                        tileLocation.Y = (MousePosition.Y * 32 + (_tempSelectedTileList[i].Y * 32));
                        tileLocation.X = (MousePosition.X * 32 + (_tempSelectedTileList[i].X * 32));
                        _editor.SelectTile((int)tileLocation.X/ 32, (int)tileLocation.Y/32);
                    }
                    _editor.PerformAction();
                }
                else
                {
                    _editor.SelectTile((int)MousePosition.X, (int)MousePosition.Y);
                    _editor.PerformAction();
                }
            }
            if (Mouse.RightButton == MouseButtonState.Pressed)
            {
                _selectionPointEnd = MousePosition;
                double dX = _selectionPointEnd.X - _selectionPointStart.X;
                double dY = _selectionPointEnd.Y - _selectionPointStart.Y;

                if (dX <= 0)
                {
                    Canvas.SetLeft(_selectionRect, MousePosition.X * 32);
                }
                if (dY <= 0)
                {
                    Canvas.SetTop(_selectionRect, MousePosition.Y * 32);
                }

                _selectionRect.Height = Math.Abs(dY) * 32;
                _selectionRect.Width = Math.Abs(dX) * 32;


                // add more tiles to the selected tiles list.
            }
            else
            {
                if(_tempSelectedTileList.Count > 0 && _tempSelectedTileList[0].Id != _editor.SelectedTileId)
                {
                    for (int i = 0; i < _tempSelectedTileList.Count; i++)
                        _tempSelectedTileList[i].ChangeTile(_editor.GetSelectedTileImage());
                }
                Canvas.SetTop(_selectionRect, MousePosition.Y * 32);
                Canvas.SetLeft(_selectionRect, MousePosition.X * 32);
            }

        }
    }
}


                //if (_tempSelectedTileList.Count > 0)
                //{
                //    Point tileLocation = new Point();
                //    for (int i = 0; i<_tempSelectedTileList.Count; i++)
                //    {
                //        tileLocation.Y = (MousePosition.Y* 32 + ((_tempSelectedTileList[i].Position.Y - _selectedTilesOffset.Y) * 32));
                //        tileLocation.X = (MousePosition.X* 32 + ((_tempSelectedTileList[i].Position.X - _selectedTilesOffset.X) * 32));
                //        _editor.SelectTile((int)tileLocation.X / 32, (int)tileLocation.Y / 32);
                //    }
                //    _editor.PerformAction();
                //}

                //
                //if (_tempSelectedTileList.Find((tile) => { return tile.Position == tempTile.Position; }) == null)
                //{
                //    

                //    //
                //    if (_selectedTilesOffset.X > _tempSelectedTileList[_tempSelectedTileList.Count - 1].Position.X)
                //        _selectedTilesOffset.X = _tempSelectedTileList[_tempSelectedTileList.Count - 1].Position.X;
                //    if (_selectedTilesOffset.Y > _tempSelectedTileList[_tempSelectedTileList.Count - 1].Position.Y)
                //        _selectedTilesOffset.Y = _tempSelectedTileList[_tempSelectedTileList.Count - 1].Position.Y;
                //}