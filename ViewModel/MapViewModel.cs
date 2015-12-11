using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using LevelEditor.Model;
using Microsoft.Practices.ServiceLocation;

namespace LevelEditor.ViewModel
{
    public class MapViewModel : ViewModelBase 
    {
        public Canvas LevelView
        {
            get
            {
                return _mapCanvas;
            }
            set
            {
                if (_mapCanvas != value)
                {
                    _mapCanvas = value;
                    RaisePropertyChanged(() => LevelView);
                }
            }
        }

        public ICommand UndoCommand { get; private set; }
        public ICommand RedoCommand { get; private set; }
        public ICommand ScrollChangedCommand { get; private set; }

        private Canvas _mapCanvas;
        private Model.Model _modelInstance = Model.Model.Instance;
        private Editor _editor;

        public static Point MousePosition;
        private Point _selectionPointStart;
        private Point _selectionPointEnd;
        private readonly Rectangle _selectionRect;
        private readonly List<Tile> _tempSelectedTileList;
        private readonly List<Tile> _tempTiles;

        private void CreateCommands()
        {
            UndoCommand = new RelayCommand(Undo);
            RedoCommand = new RelayCommand(Redo);
        }
        public MapViewModel()
        {
            CreateCommands();

            _mapCanvas = new Canvas();
            MousePosition = new Point();
            _tempTiles = new List<Tile>();
            _selectionRect = new Rectangle();
            _selectionPointEnd = new Point();
            _selectionPointStart = new Point();
            _tempSelectedTileList = new List<Tile>();


            _editor = _modelInstance._editor;
            _mapCanvas.HorizontalAlignment = HorizontalAlignment.Left;
            _mapCanvas.VerticalAlignment = VerticalAlignment.Top;

            _mapCanvas.Background = Brushes.Transparent;
            _mapCanvas.Width = _editor.GetMapWidth() * _editor.GetTileSize();
            _mapCanvas.Height = _editor.GetMapHeight() * _editor.GetTileSize();

            _mapCanvas.AddHandler(UIElement.MouseRightButtonDownEvent, (RoutedEventHandler)SelectBegin);
            _mapCanvas.AddHandler(UIElement.MouseRightButtonUpEvent, (RoutedEventHandler)SelectEnd);

            _mapCanvas.AddHandler(UIElement.MouseMoveEvent, (RoutedEventHandler)Click);
            _mapCanvas.AddHandler(UIElement.MouseDownEvent, (RoutedEventHandler)Click);
            _mapCanvas.AddHandler(UIElement.MouseLeftButtonUpEvent, (RoutedEventHandler)ClickEnd);

            int mapWidth = _editor.GetMapWidth();
            int mapHeight = _editor.GetMapHeight();

            for (int y = 0; y < mapHeight; y++)
            {
                for (int x = 0; x < mapWidth; x++)
                {
                    Tile t = _editor.GetTile(x, y);
                    _mapCanvas.Children.Add(t);
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
        private void Undo()
        {
            _editor.Undo();
        }
        private void Redo()
        {
            _editor.Redo();
        }
        private void SelectBegin(object sender, RoutedEventArgs e)
        {
            if (Mouse.RightButton == MouseButtonState.Pressed)
            {
                foreach (Tile t in _tempSelectedTileList)
                    _mapCanvas.Children.Remove(t);

                _tempSelectedTileList.Clear();
                _selectionPointStart = MousePosition;

                if (!_mapCanvas.Children.Contains(_selectionRect))
                    _mapCanvas.Children.Add(_selectionRect);

                Canvas.SetTop(_selectionRect, MousePosition.Y * 32);
                Canvas.SetLeft(_selectionRect, MousePosition.X * 32);
            }
        }
        private void SelectEnd(object sender, RoutedEventArgs e)
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

                    _mapCanvas.Children.Add(_tempSelectedTileList[_tempSelectedTileList.Count - 1]);

                    Canvas.SetTop(_tempSelectedTileList[_tempSelectedTileList.Count - 1],
                        MousePosition.Y * 32 + (_tempSelectedTileList[_tempSelectedTileList.Count - 1].Y * 32));

                    Canvas.SetLeft(_tempSelectedTileList[_tempSelectedTileList.Count - 1],
                        MousePosition.X * 32 + (_tempSelectedTileList[_tempSelectedTileList.Count - 1].X * 32));
                }
            }
            _mapCanvas.Children.Remove(_selectionRect);
        }
        private void Click(object sender, RoutedEventArgs e)
        {
            MousePosition.X = (ushort)(Math.Floor(Mouse.GetPosition(_mapCanvas).X / 32));
            MousePosition.Y = (ushort)(Math.Floor(Mouse.GetPosition(_mapCanvas).Y / 32));

            if (Mouse.LeftButton == MouseButtonState.Pressed)
            {
                // _tempSelectedTileList should always have atleast one element.
                foreach (Tile t in _tempSelectedTileList)
                {
                    int tilePositionY = (int)(MousePosition.Y + t.Y);
                    int tilePositionX = (int)(MousePosition.X + t.X);
                    Tile temp = new Tile(_editor.GetSelectedTileImage(), tilePositionX, tilePositionY, t.Id);
                    if (!_tempTiles.Exists(element => temp.X == element.X && temp.Y == element.Y && temp.Id == element.Id))
                    {
                        _tempTiles.Add(temp);
                        _mapCanvas.Children.Add(temp);
                        Canvas.SetTop(_tempTiles[_tempTiles.Count - 1], temp.Y * 32);
                        Canvas.SetLeft(_tempTiles[_tempTiles.Count - 1], temp.X * 32);
                    }
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
                    _selectionRect.Width = Math.Abs(dX - 1) * 32;
                }
                else
                    _selectionRect.Width = Math.Abs(dX + 1) * 32;

                if (dY <= 0)
                {
                    Canvas.SetTop(_selectionRect, MousePosition.Y * 32);
                    _selectionRect.Height = Math.Abs(dY - 1) * 32;
                }
                else
                    _selectionRect.Height = Math.Abs(dY + 1) * 32;
            }
            else
            {
                if (_tempSelectedTileList.Count > 0)
                {
                    foreach (Tile t in _tempSelectedTileList)
                    {
                        Canvas.SetTop(t, MousePosition.Y * 32 + (t.Y * 32));
                        Canvas.SetLeft(t, MousePosition.X * 32 + (t.X * 32));
                        if (_tempSelectedTileList[0].Id != _editor.SelectedTileId)
                            _editor.SetTile(t.X, t.Y, _editor.SelectedTileId);
                    }
                }
                Canvas.SetTop(_selectionRect, MousePosition.Y * 32);
                Canvas.SetLeft(_selectionRect, MousePosition.X * 32);
            }
        }
        private void ClickEnd(object sender, RoutedEventArgs e)
        {
            foreach (Tile t in _tempTiles)
            {
                _editor.SelectTile(t.X, t.Y);
                _mapCanvas.Children.Remove(t);
            }
            _editor.PerformAction();
            PlaceTiles(_tempTiles);
            _tempTiles.Clear();
        }
        private void PlaceTiles(List<Tile> tiles)
        {
            Queue<Tile> toDoList = new Queue<Tile>();
            foreach (Tile t in tiles)
            {
                if (getTileType(t) == "Object")
                    continue;

                if (!toDoList.Contains(t))
                    toDoList.Enqueue(t);

                for (int i = 1; i <= 4; i++)
                {
                    Tile tempTile = null;

                    switch (i)
                    {
                        case 1:
                            tempTile = _editor.GetTile(t.X, t.Y - 1);
                            break;
                        case 2:
                            tempTile = _editor.GetTile(t.X, t.Y + 1);
                            break;
                        case 3:
                            tempTile = _editor.GetTile(t.X - 1, t.Y);
                            break;
                        case 4:
                            tempTile = _editor.GetTile(t.X + 1, t.Y);
                            break;
                    }
                    if (tempTile?.Source != null)
                    {
                        if (!toDoList.Contains(tempTile))
                            toDoList.Enqueue(tempTile);
                    }
                }

            }

            while (toDoList.Count > 0)
            {
                Tile t = toDoList.Dequeue();

                string type = getTileType(t);

                if (!type.Equals("Object"))
                {
                    if (_modelInstance.ImagePaths[t.Id].Description.Contains("Half"))
                        placeHalf(t, type);
                    else
                        placeSolidTerrain(t, type);
                }

                _editor.SetTile(t.X, t.Y, t.Id);
            }
        }
        private void placeHalf(Tile t, string tiletype)
        {
            string type = tiletype + " Half";
            bool tileOver = false,
                tileLeft = false,
                tileRight = false;


            for (int i = 1; i <= 3; i++)
            {
                switch (i)
                {
                    case 1:
                        tileOver = _editor.GetTile(t.X, t.Y - 1)?.Source != null;
                        break;
                    case 2:
                        tileLeft = _editor.GetTile(t.X - 1, t.Y)?.Source != null;
                        break;
                    case 3:
                        tileRight = _editor.GetTile(t.X + 1, t.Y)?.Source != null;
                        break;
                }
            }

            if (tileOver)
            {
                t.Id = getImageId(type + " Center");
            }
            else
            {
                if (tileLeft)
                {
                    if (tileRight)
                        t.Id = getImageId(type + " Mid");
                    else
                        t.Id = getImageId(type + " Right");
                }
                else if (tileRight)
                {
                    t.Id = getImageId(type + " Left");
                }
                else
                {
                    t.Id = getImageId(type);
                }
            }
        }
        private void placeSolidTerrain(Tile t, string type)
        {
            bool tileOver = false;
            bool tileUnder = false;
            bool tileLeft = false;
            bool tileRight = false;

            for (int i = 1; i <= 4; i++)
            {
                switch (i)
                {
                    case 1:
                        tileOver = _editor.GetTile(t.X, t.Y - 1)?.Source != null;
                        break;
                    case 2:
                        tileUnder = _editor.GetTile(t.X, t.Y + 1)?.Source != null;
                        break;
                    case 3:
                        tileLeft = _editor.GetTile(t.X - 1, t.Y)?.Source != null;
                        break;
                    case 4:
                        tileRight = _editor.GetTile(t.X + 1, t.Y)?.Source != null;
                        break;
                }
            }
            bool isHalf = _modelInstance.ImagePaths[t.Id].Description.Contains("Half");


            if (tileOver)
            {
                t.Id = getImageId(type + " Center");
            }
            else
            {
                if (tileLeft)
                {
                    if (tileRight)
                        t.Id = getImageId(type + " Mid");
                    else if (tileUnder)
                        t.Id = getImageId(type + " Right");
                    else
                        t.Id = getImageId(type + " Cliff Right");
                }
                else if (!tileUnder)
                {
                    if (tileRight)
                        t.Id = getImageId(type + " Cliff Left");
                    else
                        t.Id = getImageId(type + " Platform");
                }
                else if (!tileRight)
                    t.Id = getImageId(type + " Top");
                else
                    t.Id = getImageId(type + " Left");
            }
        }
        private void placeTiles_old(List<Tile> tiles)
        {
            bool tileOver = false,
                 tileUnder = false,
                 tileLeft = false,
                 tileRight = false;

            LinkedList<Tile> visitedList = new LinkedList<Tile>();
            Queue<Tile> toDoList = new Queue<Tile>();

            foreach (Tile t in tiles)
            {
                toDoList.Enqueue(t);
                visitedList.AddLast(t);
            }

            Tile tempTile = null;

            while (toDoList.Count > 0)
            {
                Tile t = toDoList.Dequeue();
                visitedList.AddLast(t);

                for (int i = 1; i <= 4; i++)
                {
                    switch (i)
                    {
                        case 1:
                            tempTile = _editor.GetTile(t.X, t.Y - 1);
                            tileOver = tempTile?.Source != null;
                            break;
                        case 2:
                            tempTile = _editor.GetTile(t.X, t.Y + 1);
                            tileUnder = tempTile?.Source != null;
                            break;
                        case 3:
                            tempTile = _editor.GetTile(t.X - 1, t.Y);
                            tileLeft = tempTile?.Source != null;
                            break;
                        case 4:
                            tempTile = _editor.GetTile(t.X + 1, t.Y);
                            tileRight = tempTile?.Source != null;
                            break;
                    }
                    if (tempTile?.Source != null)
                    {
                        if (!visitedList.Contains(tempTile))
                            toDoList.Enqueue(tempTile);
                    }
                }

                if (tileOver)
                {
                    t.Id = getImageId("grassCenter");
                }
                else
                {
                    if (tileLeft)
                    {
                        if (tileRight)
                            t.Id = getImageId("grassMid");
                        else if (tileUnder)
                            t.Id = getImageId("grassRight");
                        else
                            t.Id = getImageId("grassCliffRight");
                    }
                    else if (!tileUnder)
                    {
                        if (tileRight)
                            t.Id = getImageId("grassCliffLeft");
                        else
                            t.Id = getImageId("grass");
                    }
                    else if (!tileRight)
                        t.Id = getImageId("grassTop");
                    else
                        t.Id = getImageId("grassLeft");

                }
                _editor.SetTile(t.X, t.Y, t.Id);

            }
        }
        private int getImageId(string name)
        {
            for (int i = 0; i < _modelInstance.ImagePaths.Count; i++)
            {
                if (_modelInstance.ImagePaths[i].Description.Contains(name))
                    return i;
            }
            return -1;
        }
        private string getTileType(Tile t)
        {
            string d = _modelInstance.ImagePaths[t.Id].Description;
            string s = "";

            for (int i = 0; d[i] != ' '; i++)
                s += d[i];
            return s;
        }
    }
}
