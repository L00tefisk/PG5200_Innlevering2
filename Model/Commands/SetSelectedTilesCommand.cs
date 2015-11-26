using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LevelEditor.Model.Commands
{
    class SetSelectedTilesCommand : ICommandPattern
    {
        private readonly List<Tile> _oldList;
        private readonly List<Tile> _newList;
        private readonly Map _map;
        public SetSelectedTilesCommand(Map map, List<Tile> selectionList)
        {
            _newList = selectionList;
            _map = map;
            _oldList = new List<Tile>();
            foreach (Tile t in _newList)
            {
                _oldList.Add(map.GetTile(t.X, t.Y));
            }
        }

        public void Execute()
        {
            foreach(Tile t in _newList)
                _map.SetTile(t);   
        }

        public void Undo()
        {
            foreach (Tile t in _oldList)
                _map.SetTile(t);
        }
    }
}
