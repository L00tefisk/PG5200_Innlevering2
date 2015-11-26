using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LevelEditor.Model.Commands;

namespace LevelEditor.Model.Tools
{
    public class WandTool : Tool
    {
        public WandTool(List<Tile> selectedTiles, Map map, CommandController controller)
        {
            SelectedTiles = selectedTiles;
            Map = map;
            CommandController = controller;
        }

        public override void PerformAction()
        {
            
        }
    }
}
