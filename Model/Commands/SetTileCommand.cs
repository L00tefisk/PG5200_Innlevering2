using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LevelEditor.Model.Commands
{
    class SetTileCommand : ICommandPattern
    {
        private readonly Map _map;
        private readonly int _x, _y;
        private readonly Tile _newTile;
        private readonly Tile _oldTile;
        public SetTileCommand(Map currentMap, Tile tile, int x, int y)
        {
            _map = currentMap;
            _newTile = tile;
            _oldTile = _map.GetTile(_x, _y);
            _x = y;
            _y = x;
        }
        public void Execute()
        {
            _map.SetTile(_newTile);
        }

        public void Undo()
        {
            _map.SetTile(_oldTile);
        }
    }
}
