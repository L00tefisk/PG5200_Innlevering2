using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using LevelEditor.Model.Commands;

namespace LevelEditor.Model.Tools
{
    public abstract class Tool : Button
    {
        protected static List<Tile> SelectedTiles;
        protected static CommandController CommandController;
        protected static Map Map;
        public abstract void PerformAction();
    }
}