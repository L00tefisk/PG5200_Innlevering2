using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LevelEditor.Model.Commands
{
    class StampCommand : Command
    {
        private readonly List<Tile> _oldList;
        private readonly List<Tile> _newList;
        private readonly Map _map;
        public StampCommand(Map map, List<Tile> selectionList, ushort selectedTileId)
        {
            _newList = new List<Tile>(selectionList.Count);
            _newList.AddRange(selectionList);
            _map = map;
            _oldList = new List<Tile>();
            foreach (Tile t in _newList)
            {
                _oldList.Add(map.GetTile(t.X, t.Y));
            }
            if(_newList.Count == 0)
            {
                _newList.Add(new Tile(selectedTileId, EditorWindow.MouseX, EditorWindow.MouseY));
            }
        }

        public override void Execute()
        {
            foreach(Tile t in _newList)
                _map.SetTile(t);

        }

        public override void Undo()
        {
            foreach (Tile t in _oldList)
                _map.SetTile(t);
        }
    }
}
