using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LevelEditor.Model.Commands;

namespace LevelEditor.Model.Tools
{
    class SelectionTool : Tool
    {
        private ushort _x;
        private ushort _y;
        private Tile _targetTile;
        public SelectionTool(List<Tile> selectedTiles, ushort x, ushort y, Tile targetTile)
        {
            SelectedTiles = selectedTiles;
            _targetTile = targetTile;
            _x = x;
            _y = y;
        }
        public override void PerformAction()
        {
            CommandController.Add(new SelectTileCommand(SelectedTiles, _targetTile, _x, _y));
        }
    }
}
