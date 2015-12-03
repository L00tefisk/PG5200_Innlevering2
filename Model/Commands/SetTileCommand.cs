using System;
using System.Collections.Generic;
using System.Windows.Media;

namespace LevelEditor.Model.Commands
{
    class SetTileCommand : Command
    {
        private readonly List<Editor.Selection> _newList;
        private readonly List<Editor.Selection> _oldList;
        private readonly Editor _editor;
        public SetTileCommand(Editor editor, List<Editor.Selection> selectionList)
        {
            _editor = editor;
            _newList = new List<Editor.Selection>();
            _oldList = new List<Editor.Selection>();

            _newList.AddRange(selectionList);
            _oldList.AddRange(selectionList);

            Editor.Selection selection;
            for (int i = 0; i < selectionList.Count; i++)
            {
                selection = selectionList[i];
                selection.Id = _editor.GetTile(selectionList[i].X, selectionList[i].Y).Id;
                _oldList[i] = selection;
            }
        }

        public override void Execute()
        {
            foreach (Editor.Selection t in _newList)
                _editor.SetTile(t.X, t.Y, t.Id);
        }

        public override void Undo()
        {
            foreach (Editor.Selection t in _oldList)
                _editor.SetTile(t.X, t.Y, t.Id);
        }
    }
}
