using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LevelEditor.Model.Commands
{
    class WandCommand : Command
    {
        private readonly Map _map;
        private readonly Tile _selectionTile;

        private List<Tile> _selectedTiles;
        private readonly List<Tile> _oldList;
        private readonly List<Tile> _newList;
        public WandCommand(Map map, List<Tile> selectedTiles, Tile t)
        {
            _map = map;
            _selectionTile = t;
            _selectedTiles = selectedTiles;
            _oldSelectedTiles = new List<Tile>();
            _newSelectedTiles = new List<Tile>();
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
                        xIndex = currentTile.X + x;
                        yIndex = currentTile.Y + y;

                        if (xIndex >= 0 && yIndex >= 0 && xIndex < _map.Width && yIndex < _map.Height)
                        {
                            if (!closedList.Contains(currentTile))
                            {
                               openList.Add(_map.GetTile(x,y));
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
