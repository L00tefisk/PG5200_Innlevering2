using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using LevelEditor.Model.Commands;

namespace LevelEditor.Model.Tools
{
    public abstract class Tool : Button
    {
        protected static ushort SelectedTileId;
        protected static List<Tile> SelectedTiles;
        protected static CommandController CommandController;
        protected static Map Map;

        public static void Init(List<Tile> selectedTiles, CommandController commandController, Map map, ushort selectedTileId)
        {
            SelectedTiles = selectedTiles;
            CommandController = commandController;
            Map = map;
            SelectedTileId = selectedTileId;
        }
        public abstract void PerformAction();
    }
}