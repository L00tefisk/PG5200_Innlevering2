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
        public Map Map { get; set; }

        public Layer(string name)
        {
            Name = name;
            IsVisible = true;
            IsLocked = false;

            Map = new Map(100, 100);
        }

    }
}
