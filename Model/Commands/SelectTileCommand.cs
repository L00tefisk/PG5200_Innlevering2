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
        private readonly Tile _selectedTile;
        private readonly List<Tile> _selectedTiles;

        public SelectTileCommand(List<Tile> selectedTiles, Tile selectedTile)
        {
            _selectedTiles = selectedTiles;
            _selectedTile = selectedTile;
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
