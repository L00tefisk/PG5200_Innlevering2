using System;
using System.Collections.Generic;
using LevelEditor.Model.Commands;
using LevelEditor.Model.Tools;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows;

namespace LevelEditor.Model
{
    public class Editor
    {
        public ushort SelectedTileId { get; set; }
        public Tile SelectedTile { get; set; }
        public ushort SelectedTool { get; set; }
        public ImageSource[] Images { get; set; }
    
        private Map _map;
        public List<Tile> _selectedTiles;
        private List<Tool> _tools;
        private CommandController _commandController;

        public Editor(Map map, CommandController commandController)
        {
            _map = map;
            _commandController = commandController;
            _selectedTiles = new List<Tile>();
            _tools = new List<Tool>();
            _tools.Add(new BucketTool());
            _tools.Add(new EraserTool());
            _tools.Add(new StampTool());
            _tools.Add(new WandTool());
            _tools.Add(new SelectionTool());

            Images = new ImageSource[MainModel.ImgPaths.Count];
            for (int i = 0; i < MainModel.ImgPaths.Count; i++)
            {
                Images[i] = new BitmapImage(new Uri(MainModel.ImgPaths[i], UriKind.Relative));
            }


            SelectedTool = 2;
            SelectedTileId = 0;
            SelectedTile = new Tile(GetSelectedTileImage(), new System.Windows.Point(0,0));


            Tool.Init(_selectedTiles, _commandController, _map, SelectedTileId, Images);
        }

        /// <summary>
        /// Performs the action of the currently selected tool.
        /// </summary>
        public void PerformAction()
        {
            foreach(Tile t in _selectedTiles)
            {
                SetTile(t.Position, SelectedTileId);
            }
           // if(SelectedTool < _tools.Count)
           //     _tools[SelectedTool].PerformAction();
        }
        public void SelectTile(int x, int y)
        {
            Tile tileToAdd = new Tile(GetSelectedTileImage(), new Point(x, y));
            if (_selectedTiles.IndexOf(tileToAdd) > -1)
                return;
            _selectedTiles.Add(tileToAdd);
        }
        public ImageSource GetSelectedTileImage()
        {
            return Images[SelectedTileId];
        }
        public Tile GetTile(int x, int y)
        {
            return _map.GetTile(new Point(x, y));
        }
        public void SetTile(Point p, ushort id)
        {
            _map.SetTile(p, Images[id]);
        }
        /// <summary>
        /// Redoes an action if there is one to redo.
        /// </summary>
        public void Redo()
        {
            _commandController.Execute();
        }
        /// <summary>
        /// Undoes an action if there is one to undo.
        /// </summary>
        public void Undo()
        {
            _commandController.Undo();
        }
        public void NextTool()
        {
            SelectedTool++;
            if (SelectedTool == _tools.Count)
            {
                SelectedTool = 0;
            }
        }
        public void PreviousTool()
        {
            SelectedTool--;
            if (SelectedTool == ushort.MaxValue)
            {
                SelectedTool = (ushort)(_tools.Count - 1);
            }
        }
    }
}
