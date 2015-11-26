﻿using System;
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
            Background = Brushes.Transparent;
            Height = 1000;
            Width = 1000;
            _map = map;
            _editor = editor;
            _oldId = 0;
            AddHandler(UIElement.KeyDownEvent, (RoutedEventHandler)ChangeTool);
            AddHandler(UIElement.MouseRightButtonDownEvent, (RoutedEventHandler)RemoveStart);
            AddHandler(UIElement.MouseRightButtonUpEvent, (RoutedEventHandler)Remove);
            AddHandler(UIElement.MouseMoveEvent, (RoutedEventHandler)Click);
            AddHandler(UIElement.MouseDownEvent, (RoutedEventHandler)Click);
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
<<<<<<< HEAD
                MouseX = (ushort)(Math.Floor(Mouse.GetPosition(this).X) /
                            _map.TileSize);
                MouseY = (ushort)(Math.Floor(Mouse.GetPosition(this).Y) /
                        _map.TileSize);
=======

                _mouseX = (ushort)(Math.Floor(Mouse.GetPosition(this).X) / _map.TileSize);
                _mouseY = (ushort)(Math.Floor(Mouse.GetPosition(this).Y) / _map.TileSize);
>>>>>>> origin/master
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
            base.OnRender(dc);
            // Find the visible area to draw in
                // Draw in the visible area

            for (int y = 0; y < _map.Height; y++)
                for (int x = 0; x < _map.Width; x++)
                    if (_map.GetTile(x, y) != null && _map.GetTile(x, y).TileId != ushort.MaxValue)
                        dc.DrawImage(_map.GetTile(x, y).ImgSrc, new Rect(x * _map.TileSize, y * _map.TileSize, _map.TileSize, _map.TileSize));

            if (_editor.SelectedTileId != ushort.MaxValue)
            {
                ImageSource imgSrc = new BitmapImage(new Uri(Model.ImgPaths[_editor.SelectedTileId], UriKind.Relative));
                dc.DrawImage(imgSrc, new Rect(MouseX * _map.TileSize, MouseY * _map.TileSize, _map.TileSize, _map.TileSize));
            }
        }
    }
}
