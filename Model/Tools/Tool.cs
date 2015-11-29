using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using LevelEditor.Model.Commands;
using System.Windows.Media;

namespace LevelEditor.Model.Tools
{
    public abstract class Tool : Button
    {
        protected static ushort SelectedTileId;
        protected static List<Tile> SelectedTiles;
        protected static CommandController CommandController;
        protected static Map Map;
        protected static ImageSource[] Images;

        public static void Init(List<Tile> selectedTiles, CommandController commandController, Map map, ushort selectedTileId, ImageSource[] images)
        {
            SelectedTiles = selectedTiles;
            CommandController = commandController;
            Map = map;
            SelectedTileId = selectedTileId;
            Images = images;
        }
        public abstract void PerformAction();
    }
}