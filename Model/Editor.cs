﻿using System;
using System.Collections.Generic;
using System.Linq;
using LevelEditor.Model.Commands;
using System.Windows.Media;
using System.Windows.Media.Imaging;
namespace LevelEditor.Model
{
    public class Editor
    {
        public struct Selection
        {
            public int X;
            public int Y;
            public int Id;
        }

        public int SelectedTileId { get; set; }
        private readonly ImageSource[] _images;

        private readonly Map _map;
        private List<Selection> _selectedTiles;
        private readonly CommandController _commandController;

        public Editor()
        {
            _commandController = new CommandController();
            _selectedTiles = new List<Selection>();
            _images = new ImageSource[Model.Instance.ImagePaths.Count];

            for (int i = 0; i < Model.Instance.ImagePaths.Count; i++)
            {
                _images[i] = new BitmapImage(new Uri(Model.Instance.ImagePaths[i].Path, UriKind.Relative));
                _images[i].Freeze();
                //_images[i] = null;
            }

            _map = new Map(100, 100);
            SelectedTileId = 0;
        }
        public void PerformAction()
        {
            if (_selectedTiles.Count <= 0)
                return;

            _commandController.Add(new SetTileCommand(this, _selectedTiles));
            _selectedTiles.Clear();
        }
        public void SelectTile(int x, int y)
        {
            Selection sel;
            sel.X = x;
            sel.Y = y;
            sel.Id = SelectedTileId;
            if(!_selectedTiles.Contains(sel))
                _selectedTiles.Add(sel);
        }
        public ImageSource GetSelectedTileImage()
        {
            return _images[SelectedTileId];
        }
        public Tile GetTile(int x, int y)
        {
            if (x >= 0 && _map.Width > x && y >= 0 && _map.Width > y)
                return _map.GetTile(x, y);
        
            return null;
        }
        public void SetTile(int x, int y, int id)
        {
            if (x >= 0 && _map.Width > x && y >= 0 && _map.Width > y)
            {
                if (id >= _images.Length || id < 0)
                    _map.SetTile(x, y, int.MaxValue, null);
                else
                    _map.SetTile(x, y, id, _images[id]);
            }

        }
        public void Redo()
        {
            _commandController.Execute();
        }
        public void Undo()
        {
            _commandController.Undo();
        }
        public int GetMapWidth()
        {
            return _map.Width;
        }
        public int GetMapHeight()
        {
            return _map.Height;
        }
        public int GetTileSize()
        {
            return _map.GetTileSize();
        }
    }
}
