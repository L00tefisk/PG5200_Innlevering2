using System.Collections.Generic;

namespace LevelEditor.Model.Commands
{
    public abstract class Command
    {
        protected static List<Editor.Selection> SelectedTiles;
        public abstract void Execute();
        public abstract void Undo();
    }
}
