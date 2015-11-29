using System;
using System.Collections.Generic;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows;

namespace LevelEditor.Model
{
    public class EditorWindow : Canvas
    {
        private Editor _editor;
        public static Point MousePosition;
        private ushort _oldId;
        private double _scaleFactor;
        private Point _selectedTilesOffset;
        private List<Tile> _tempSelectedTileList;
        public EditorWindow(Map map, Editor editor)
        {
            _editor = editor;
            _oldId = 0;
            _scaleFactor = 1;
            _selectedTilesOffset.X = Double.MaxValue;
            _selectedTilesOffset.Y = Double.MaxValue;
            _tempSelectedTileList = new List<Tile>();
            MousePosition = new Point();
            HorizontalAlignment = HorizontalAlignment.Left;
            VerticalAlignment = VerticalAlignment.Top;

            Background = Brushes.Transparent;
            Width = map.Width * map.TileSize;
            Height = map.Height * map.TileSize;

            AddHandler(UIElement.MouseRightButtonDownEvent, (RoutedEventHandler)SelectTile);
            AddHandler(UIElement.MouseMoveEvent, (RoutedEventHandler)Click);
            //AddHandler(UIElement.MouseDownEvent, (RoutedEventHandler)Zoom);

            Random rng = new Random();
            for(int y = 0; y < map.Height; y++)
            {
                for (int x = 0; x < map.Width; x++)
                {
                    Children.Add(map._level[y * map.Width + x]);
                    Canvas.SetTop(map._level[y * map.Width + x], y * 32);
                    Canvas.SetLeft(map._level[y * map.Width + x], x * 32);
                    
                    map._level[y * map.Width + x].Source = _editor.Images[rng.Next(0, MainModel.ImgPaths.Count)];
                }
            }
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
        private void SelectTile(object sender, RoutedEventArgs e)
        {
            _selectedTilesOffset.X = Double.MaxValue;
            _selectedTilesOffset.Y = Double.MaxValue;
            for (int i = 0; i < _tempSelectedTileList.Count; i++)
                Children.Remove(_tempSelectedTileList[i]);

            MousePosition = Mouse.GetPosition(this);
            MousePosition.X = Math.Floor(MousePosition.X / 32);
            MousePosition.Y = Math.Floor(MousePosition.Y / 32);
            _tempSelectedTileList.Add(new Tile(_editor.GetSelectedTileImage(), new Point(MousePosition.X, MousePosition.Y)));


            _tempSelectedTileList[_tempSelectedTileList.Count - 1].Opacity = 0.5;
            Children.Add(_tempSelectedTileList[_tempSelectedTileList.Count - 1]);
            if (_selectedTilesOffset.X > _tempSelectedTileList[_tempSelectedTileList.Count - 1].Position.X)
                _selectedTilesOffset.X = _tempSelectedTileList[_tempSelectedTileList.Count - 1].Position.X;
            if (_selectedTilesOffset.Y > _tempSelectedTileList[_tempSelectedTileList.Count - 1].Position.Y)
                _selectedTilesOffset.Y = _tempSelectedTileList[_tempSelectedTileList.Count - 1].Position.Y;
            Canvas.SetTop(_tempSelectedTileList[_tempSelectedTileList.Count - 1], MousePosition.Y * 32 + ((_tempSelectedTileList[_tempSelectedTileList.Count - 1].Position.Y - _selectedTilesOffset.Y) * 32));
            Canvas.SetLeft(_tempSelectedTileList[_tempSelectedTileList.Count - 1], MousePosition.X * 32 + ((_tempSelectedTileList[_tempSelectedTileList.Count - 1].Position.X - _selectedTilesOffset.X) * 32));

        }
        private void Click(object sender, RoutedEventArgs e)
        {
            if (_tempSelectedTileList.Count != 0)
            {
                for(int i = 0; i < _tempSelectedTileList.Count; i++)
                { 
                    MousePosition.X = (ushort)(Math.Floor(Mouse.GetPosition(this).X / 32));
                    MousePosition.Y = (ushort)(Math.Floor(Mouse.GetPosition(this).Y / 32));
                    Canvas.SetTop(_tempSelectedTileList[i], MousePosition.Y * 32 + ((_tempSelectedTileList[i].Position.Y - _selectedTilesOffset.Y) * 32));
                    Canvas.SetLeft(_tempSelectedTileList[i], MousePosition.X * 32 + ((_tempSelectedTileList[i].Position.X - _selectedTilesOffset.X) * 32));
                }
            }
            if (Mouse.LeftButton == MouseButtonState.Pressed)
            {
                if (_tempSelectedTileList.Count > 0)
                {
                    Point tileLocation = new Point();
                    for (int i = 0; i < _tempSelectedTileList.Count; i++)
                    {
                        tileLocation.Y = (MousePosition.Y * 32 + ((_tempSelectedTileList[i].Position.Y - _selectedTilesOffset.Y) * 32));
                        tileLocation.X = (MousePosition.X * 32 + ((_tempSelectedTileList[i].Position.X - _selectedTilesOffset.X) * 32));
                        _editor.SelectTile((int)tileLocation.X/ 32, (int)tileLocation.Y/32);
                    }
                    _editor.PerformAction();
                }
            }
            if (Mouse.RightButton == MouseButtonState.Pressed)
            {
                Tile tempTile = new Tile(_editor.GetSelectedTileImage(), new Point(MousePosition.X, MousePosition.Y));
                if (!Children.Contains(tempTile))
                {
                    
                    if (!_tempSelectedTileList.Contains(tempTile))
                    {
                        _tempSelectedTileList.Add(tempTile);
                        _tempSelectedTileList[_tempSelectedTileList.Count - 1].Opacity = 0.5;

                        Children.Add(_tempSelectedTileList[_tempSelectedTileList.Count - 1]);
                        if (_selectedTilesOffset.X > _tempSelectedTileList[_tempSelectedTileList.Count - 1].Position.X)
                            _selectedTilesOffset.X = _tempSelectedTileList[_tempSelectedTileList.Count - 1].Position.X;
                        if (_selectedTilesOffset.Y > _tempSelectedTileList[_tempSelectedTileList.Count - 1].Position.Y)
                            _selectedTilesOffset.Y = _tempSelectedTileList[_tempSelectedTileList.Count - 1].Position.Y;


                    }
                }
                // add more tiles to the selected tiles list.
            }
        }
    }
}
