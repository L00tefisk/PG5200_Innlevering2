using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LevelEditor.Model.Tools
{
    public class BucketTool : Tool
    {
        public BucketTool(List<Tile> selectedTiles)
        {
            _selectedTiles = selectedTiles;
        }
    }
}
