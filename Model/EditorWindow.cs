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
        private Image[,] _imageArray;
        private Editor _editor;
        public static ushort MouseX { get; set; }
        public static ushort MouseY { get; set; }
        private ushort _oldId;
        private double _scaleFactor;

        public EditorWindow(Map map, Editor editor)
        {
            _editor = editor;
            _oldId = 0;
            _scaleFactor = 1;
            HorizontalAlignment = HorizontalAlignment.Left;
            VerticalAlignment = VerticalAlignment.Top;

            Background = Brushes.Transparent;
            Width = map.Width * map.TileSize;
            Height = map.Height * map.TileSize;

           // AddHandler(UIElement.MouseRightButtonDownEvent, (RoutedEventHandler)RemoveStart);
           // AddHandler(UIElement.MouseRightButtonUpEvent, (RoutedEventHandler)Remove);
           // AddHandler(UIElement.MouseMoveEvent, (RoutedEventHandler)Click);
            AddHandler(UIElement.MouseDownEvent, (RoutedEventHandler)Zoom);

            Random rng = new Random();
            for(int y = 0; y < map.Height; y++)
            {
                for (int x = 0; x < map.Width; x++)
                {
                    Canvas.SetTop(map._level[y][x], y * 32);
                    Canvas.SetLeft(map._level[y][x], x * 32);
                    Children.Add(map._level[y][x]);
                    map._level[y][x].Source = _editor.Images[rng.Next(0, Model.ImgPaths.Count)];
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

                MouseX = (ushort)(Math.Floor(Mouse.GetPosition(this).X / 32));
                MouseY = (ushort)(Math.Floor(Mouse.GetPosition(this).Y / 32));

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
    }
}
