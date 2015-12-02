﻿using System;
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
        public static Point MousePosition;

        private readonly Editor _editor;
        private double _scaleFactor;
        private Point _selectionPointStart;
        private Point _selectionPointEnd;
        private readonly Rectangle _selectionRect;
        private readonly List<Tile> _tempSelectedTileList;
        private readonly List<Tile> _tempTiles; 


        public EditorWindow(Editor editor)
        {
            _editor = editor;
            _scaleFactor = 1;
            _tempSelectedTileList = new List<Tile>();
            _tempTiles = new List<Tile>();
            _selectionPointStart = new Point();
            _selectionPointEnd = new Point();
            MousePosition = new Point();
            _selectionRect = new Rectangle();
            HorizontalAlignment = HorizontalAlignment.Left;
            VerticalAlignment = VerticalAlignment.Top;

            Background = Brushes.Transparent;
            Width = _editor.GetMapWidth() * _editor.GetTileSize();
            Height = _editor.GetMapHeight() * _editor.GetTileSize();

            AddHandler(UIElement.MouseDownEvent, (RoutedEventHandler)SelectBegin);
            AddHandler(UIElement.MouseUpEvent, (RoutedEventHandler)SelectEnd);
            
            AddHandler(UIElement.MouseRightButtonDownEvent, (RoutedEventHandler)Undo);

            AddHandler(UIElement.MouseMoveEvent, (RoutedEventHandler)Click);
            AddHandler(UIElement.MouseDownEvent, (RoutedEventHandler)Click);
            AddHandler(UIElement.MouseLeftButtonUpEvent, (RoutedEventHandler)ClickEnd);

            Random rng = new Random();
            int mapWidth = editor.GetMapWidth();
            int mapHeight = editor.GetMapHeight();

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

        private void Undo(object sender, RoutedEventArgs e)
        {
            _editor.Undo();
        }

        private void SelectBegin(object sender, RoutedEventArgs e)
        {
            if (Mouse.MiddleButton == MouseButtonState.Pressed)
            {
                foreach (Tile t in _tempSelectedTileList)
                    Children.Remove(t);

                _tempSelectedTileList.Clear();
                _selectionPointStart = MousePosition;

                if (!Children.Contains(_selectionRect))
                    Children.Add(_selectionRect);

                Canvas.SetTop(_selectionRect, MousePosition.Y*32);
                Canvas.SetLeft(_selectionRect, MousePosition.X*32);
            }
        }
        private void ClickEnd(object sender, RoutedEventArgs e)
        {
            if (Mouse.LeftButton == MouseButtonState.Pressed)
            {
                for (int i = 0; i < _tempTiles.Count; i++)
                {
                    _editor.SelectTile(_tempTiles[i].X, _tempTiles[i].Y);
                    Children.Remove(_tempTiles[i]);
                }
                _tempTiles.Clear();
                _editor.PerformAction();
            }
        }
        private void SelectEnd(object sender, RoutedEventArgs e)
        {
            if (Mouse.MiddleButton == MouseButtonState.Released)
            {
                double dX = Math.Abs(_selectionPointEnd.X - _selectionPointStart.X) + 1;
                double dY = Math.Abs(_selectionPointEnd.Y - _selectionPointStart.Y) + 1;

                for (int y = 0; y < dY; y++)
                {
                    for (int x = 0; x < dX; x++)
                    {
                        Tile tempTile = new Tile(_editor.GetSelectedTileImage(), x, y, _editor.SelectedTileId);
                        _tempSelectedTileList.Add(tempTile);
                        _tempSelectedTileList[_tempSelectedTileList.Count - 1].Opacity = 0.5;
                        Children.Add(_tempSelectedTileList[_tempSelectedTileList.Count - 1]);
                        Canvas.SetTop(_tempSelectedTileList[_tempSelectedTileList.Count - 1],
                            MousePosition.Y*32 + (_tempSelectedTileList[_tempSelectedTileList.Count - 1].Y*32));
                        Canvas.SetLeft(_tempSelectedTileList[_tempSelectedTileList.Count - 1],
                            MousePosition.X*32 + (_tempSelectedTileList[_tempSelectedTileList.Count - 1].X*32));
                    }
                }
                Children.Remove(_selectionRect);
            }
        }

        private void Click(object sender, RoutedEventArgs e)
        {
            MousePosition.X = (ushort)(Math.Floor(Mouse.GetPosition(this).X / 32));
            MousePosition.Y = (ushort)(Math.Floor(Mouse.GetPosition(this).Y / 32));
            if (Mouse.LeftButton == MouseButtonState.Pressed)
            {
                Tile tempTile = _editor.GetTile((int)MousePosition.X, (int)MousePosition.Y);
                if (tempTile.Id != _editor.SelectedTileId)
                {
                    if (_tempSelectedTileList.Count > 0)
                    {
                    //    Point tileLocation = new Point();
                    //    for (int i = 0; i < _tempSelectedTileList.Count; i++)
                    //    {
                    //        tileLocation.Y = (MousePosition.Y*32 + (_tempSelectedTileList[i].Y*32));
                    //        tileLocation.X = (MousePosition.X*32 + (_tempSelectedTileList[i].X*32));
                    //        _editor.SelectTile((int) tileLocation.X/32, (int) tileLocation.Y/32);

                    //        if (!Children.Contains(_tempSelectedTileList[i]))
                    //        {
                    //            _tempTiles.Add(_tempSelectedTileList[i]);
                    //            Children.Add(_tempTiles[i]);
                    //            SetTop(_tempTiles[i], _tempTiles[i].Y * 32);
                    //            SetLeft(_tempTiles[i], _tempTiles[i].X * 32);
                    //        }
                    //    }
                    }
                    else
                    {
                        Tile temp = new Tile(_editor.GetSelectedTileImage(), (int) MousePosition.X, (int) MousePosition.Y,
                            _editor.SelectedTileId);
                        if (!Children.Contains(temp))
                        {
                            _tempTiles.Add(temp);
                            Children.Add(_tempTiles[_tempTiles.Count - 1]);
                            SetTop(_tempTiles[_tempTiles.Count - 1], _tempTiles[_tempTiles.Count - 1].Y * 32);
                            SetLeft(_tempTiles[_tempTiles.Count - 1], _tempTiles[_tempTiles.Count - 1].X * 32);
                        }
                    }
                }
            }
            if (Mouse.MiddleButton == MouseButtonState.Pressed)
            {
                _selectionPointEnd = MousePosition;
                double dX = _selectionPointEnd.X - _selectionPointStart.X;
                double dY = _selectionPointEnd.Y - _selectionPointStart.Y;

                if (dX <= 0)
                {
                    Canvas.SetLeft(_selectionRect, MousePosition.X*32);
                    _selectionRect.Width = Math.Abs(dX-1) * 32;
                }
                else
                {
                    _selectionRect.Width = Math.Abs(dX+1) * 32;
                }

                if (dY <= 0)
                {
                    Canvas.SetTop(_selectionRect, MousePosition.Y*32);
                    _selectionRect.Height = Math.Abs(dY-1) * 32;
                }
                else
                {
                   _selectionRect.Height = Math.Abs(dY+1) * 32;
                }
            }
            else
            {
                if (_tempSelectedTileList.Count > 0)
                {
                    for (int i = 0; i < _tempSelectedTileList.Count; i++)
                    {
                        Canvas.SetTop(_tempSelectedTileList[i], MousePosition.Y * 32 + (_tempSelectedTileList[i].Y * 32));
                        Canvas.SetLeft(_tempSelectedTileList[i], MousePosition.X * 32 + (_tempSelectedTileList[i].X * 32));
                        if (_tempSelectedTileList[0].Id != _editor.SelectedTileId)
                            _tempSelectedTileList[i].ChangeTile(_editor.GetSelectedTileImage());
                    }
                }
                Canvas.SetTop(_selectionRect, MousePosition.Y * 32);
                Canvas.SetLeft(_selectionRect, MousePosition.X * 32);
            }
        }
    }
}