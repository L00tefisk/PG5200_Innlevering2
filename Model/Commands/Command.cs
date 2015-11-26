using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LevelEditor.Model
{
    public abstract class Command
    {
        protected static List<Tile> SelectedTiles;
        public abstract void Execute();
        public abstract void Undo();
    }
}
