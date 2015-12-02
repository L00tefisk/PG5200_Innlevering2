using System;
using System.Collections.Generic;
using System.Windows.Media;

namespace LevelEditor.Model.Commands
{
    class SetTileCommand : Command
    {
        private readonly List<Editor.Selection> _list;
        private readonly int _oldId;
        private readonly int _newId;
        private readonly Editor _editor;
        public SetTileCommand(Editor editor, List<Editor.Selection> selectionList)
        {
            _list = new List<Editor.Selection>(selectionList.Count);
            _editor = editor;
            _newId = editor.SelectedTileId;
            _oldId = editor.GetTile(selectionList[0].X, selectionList[0].Y).Id;
            _list.AddRange(selectionList);
        }

        public override void Execute()
        {
            foreach (Editor.Selection t in _list)
                _editor.SetTile(t.X, t.Y, _newId);
        }

        public override void Undo()
        {
            foreach (Editor.Selection t in _list)
                _editor.SetTile(t.X, t.Y, _oldId);
        }
    }
}
