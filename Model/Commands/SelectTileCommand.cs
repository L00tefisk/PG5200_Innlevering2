using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace LevelEditor.Model.Commands
{
    public class SelectTileCommand : Command
    {
        private readonly List<Tile> _selectedTiles;
        private readonly Tile _selectedTile;
        private ushort _x;
        private ushort _y;

        public SelectTileCommand(List<Tile> selectedTiles, Tile selectedTile, ushort x, ushort y)
        {
            _selectedTiles = selectedTiles;
            _selectedTile = selectedTile;
            _x = x;
            _y = y;
        }
        public override void Execute()
        {
            _selectedTiles.Add(_selectedTile);
        }

        public override void Undo()
        {
            _selectedTiles.Remove(_selectedTile);
        }
    }
}
