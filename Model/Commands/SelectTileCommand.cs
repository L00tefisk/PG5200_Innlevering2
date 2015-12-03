using System.Collections.Generic;

namespace LevelEditor.Model.Commands
{
    public class SelectTileCommand : Command
    {
        private readonly List<Tile> _selectedTiles;
        private readonly List<Tile> _oldList;
        private readonly List<Tile> _newList;  
        public SelectTileCommand(List<Tile> selectedTiles, Tile selectedTile)
        {
            _selectedTiles = selectedTiles; 
        }
        public override void Execute()
        {
            _selectedTiles.Clear();
            _selectedTiles.AddRange(_newList);
        }
        public override void Undo()
        {
            _selectedTiles.Clear();
            _selectedTiles.AddRange(_oldList);
        }
    }
}
