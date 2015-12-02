using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LevelEditor.Model
{
    public class Layer
    {
        public string Name { get; set; }
        public bool IsVisible { get; set; }
        public bool IsLocked { get; set; }
        // the tiles in this layer.
        private List<Tile> tiles;

        public Layer(string name)
        {
            Name = name;
            IsVisible = true;
            IsLocked = false;
        }

        void AddTile(Tile t)
        {
            tiles.Add(t);
        }

        void RemoveTile(Tile t)
        {
            tiles.Remove(t);
        }
    }
}
