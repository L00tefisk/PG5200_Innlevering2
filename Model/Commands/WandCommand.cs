using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LevelEditor.Model.Commands
{
    class WandCommand : ICommandPattern
    {
        private Map _map;
        private List<Tile> _selectedTiles; 
        private List<Tile> _newSelectedTiles;
        private List<Tile> _oldSelectedTiles;
        private Tile _selectionTile;
        public WandCommand(Map map, List<Tile> selectedTiles, Tile t)
        {
            _map = map;
            _selectionTile = t;
            _selectedTiles = selectedTiles;
            _oldSelectedTiles = new List<Tile>();
            _newSelectedTiles = new List<Tile>();
        }

        public void Execute()
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

                        if (xIndex >= 0 && yIndex >= 0 && xIndex < _map.Level[0].Count && yIndex < _map.Level.Count)
                        {
                            if (!closedList.Contains(currentTile))
                            {
                               openList.Add(_map.Level[y][x]);  
                            }
                        }
                    }
                }
            }

        }

        public void Undo()
        {
            
        }
    }
}
