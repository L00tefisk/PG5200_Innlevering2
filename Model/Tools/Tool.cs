using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace LevelEditor.Model.Tools
{
    public abstract class Tool : Button
    {
        protected static List<Tile> _selectedTiles;
    }
}