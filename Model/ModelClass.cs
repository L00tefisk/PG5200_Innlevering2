using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LevelEditor.Model
{
    public class ModelClass
    {
        public string Name { get; set; }
        public enum Shapes
        {
            Square,
            Circle,
            Triangle
        }
        public ModelClass()
        {

        }
    }
}
