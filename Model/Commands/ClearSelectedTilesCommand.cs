using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LevelEditor.Model.Commands
{
    public class ClearSelectedTilesCommand : Command
    {
        private List<Tile> _oldSelectedTiles; 
        public ClearSelectedTilesCommand()
        {
            _oldSelectedTiles.AddRange(SelectedTiles);
        }
        public override void Execute()
        {
            SelectedTiles.Clear();
        }

        public override void Undo()
        {
            SelectedTiles.Clear();
            SelectedTiles.AddRange(_oldSelectedTiles);
        }
    }
}
