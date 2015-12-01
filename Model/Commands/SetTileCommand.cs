using System;
using System.Collections.Generic;
using System.Windows.Media;

namespace LevelEditor.Model.Commands
{
    class SetTileCommand : Command
    {
        private readonly List<Editor.Selection> _oldList;
        private readonly List<Editor.Selection> _newList;
        private readonly int _oldId;
        private readonly int _newId;
        private readonly Editor _editor;
        public SetTileCommand(Editor editor, List<Editor.Selection> selectionList)
        {
            _newList = new List<Editor.Selection>(selectionList.Count);
            _oldList = new List<Editor.Selection>();

            _editor = editor;
            _newId = editor.SelectedTileId;

            _newList.AddRange(selectionList);
            foreach (Editor.Selection t in _newList)
            {
                Tile tile = _editor.GetTile(t.X, t.Y);
                _oldId = tile.Id;
                Editor.Selection selection = new Editor.Selection
                {
                    X = t.X,
                    Y = t.Y
                };
                _oldList.Add(selection);
            }
            if(_newList.Count == 0)
            {
                Editor.Selection selection = new Editor.Selection
                {
                    X = (int)EditorWindow.MousePosition.X,
                    Y = (int)EditorWindow.MousePosition.Y
                };
                _newList.Add(selection);
            }
        }

        public override void Execute()
        {
            foreach (Editor.Selection t in _newList)
                _editor.SetTile(t.X, t.Y, _newId);
        }

        public override void Undo()
        {
            //foreach (Tile t in _oldList)
              //  _map.SetTile(t);
        }
    }
}
