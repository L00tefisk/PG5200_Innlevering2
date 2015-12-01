using System.Collections.Generic;
using System.Windows;
using System.Linq;

namespace LevelEditor.Model.Commands
{
    class WandCommand : Command
    {
        private readonly Map _map;
        private readonly Tile _selectionTile;

        private List<Tile> _selectedTiles; 
        private List<Tile> _newSelectedTiles;
        private readonly List<Tile> _oldSelectedTiles;
        private Map map;
        private List<Tile> selectedTiles;
        private object targetTile;

        public WandCommand(Map map, List<Tile> selectedTiles, Tile t)
        {
            _map = map;
            _selectionTile = t;
            _selectedTiles = selectedTiles;
            _oldSelectedTiles = new List<Tile>();
            _newSelectedTiles = new List<Tile>();
        }

        public WandCommand(Map map, List<Tile> selectedTiles, object targetTile)
        {
            this.map = map;
            this.selectedTiles = selectedTiles;
            this.targetTile = targetTile;
        }

        public override void Execute()
        {
            int xIndex, yIndex;
            Tile currentTile;
            List<Tile> openList = new List<Tile>();
            List<Tile> closedList = new List<Tile>();

            openList.Add(_selectionTile);
            while (openList.Count != 0)
            {
                currentTile = openList.First();
                openList.Remove(currentTile);
                closedList.Add(currentTile);
                for (int y = -1; y < 2; y++)
                {
                    for (int x = -1; x < 2; x++)
                    {
                        xIndex = (int)currentTile.Position.X + x;
                        yIndex = (int)currentTile.Position.Y + y;

                        if (xIndex >= 0 && yIndex >= 0 && xIndex < _map.Width && yIndex < _map.Height)
                        {
                            if (!closedList.Contains(currentTile))
                            {
                               openList.Add(_map.GetTile(new Point(x,y)));
                            }
                        }
                    }
                }
            }
            _selectedTiles = closedList;
        }

        public override void Undo()
        {
            _selectedTiles = _oldSelectedTiles;
        }
    }
}
