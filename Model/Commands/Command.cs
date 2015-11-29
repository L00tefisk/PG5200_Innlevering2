using System.Collections.Generic;

namespace LevelEditor.Model
{
    public abstract class Command
    {
        protected static List<Tile> SelectedTiles;
        public abstract void Execute();
        public abstract void Undo();
    }
}
