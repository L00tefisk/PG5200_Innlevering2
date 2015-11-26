using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LevelEditor.Model.Tools
{
    public class StampTool : Tool
    {
        public StampTool(List<Tile> selectedTiles)
        {
            _selectedTiles = selectedTiles;
        }
    }
}
