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

        public Layer(string name)
        {
            Name = name;
        }
    }
}
