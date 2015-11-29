using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace LevelEditor.Model.Commands
{
    class StampCommand : Command
    {
        private readonly List<Tile> _oldList;
        private readonly List<Tile> _newList;
        private readonly Map _map;
        private readonly ImageSource[] _images;
        private readonly ushort _tileId;
        public StampCommand(Map map, List<Tile> selectionList, ushort selectedTileId, ImageSource[] images)
        {
            _newList = new List<Tile>(selectionList.Count);
            _newList.AddRange(selectionList);
            _map = map;
            _oldList = new List<Tile>();
            _images = images;
            _tileId = selectedTileId;
            foreach (Tile t in _newList)
            {
                _oldList.Add(map.GetTile(t.Position));
            }
            if(_newList.Count == 0)
            {
                _newList.Add(new Tile(null, EditorWindow.MousePosition));
            }
        }

        public override void Execute()
        {
            //foreach (Tile t in _newList)
            //    t.ChangeTile(_images[_tileId]);

        }

        public override void Undo()
        {
            //foreach (Tile t in _oldList)
              //  _map.SetTile(t);
        }
    }
}
