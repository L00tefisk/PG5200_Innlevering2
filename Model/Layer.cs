using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LevelEditor.Model
{
    public class Layer
    {
        private bool _isVisisble;
        public string Name { get; set; }

        public bool IsVisisble
        {
            get { return true; }
            set { _isVisisble = value; }
        }

        public Layer(string name)
        {
            Name = name;
            
            IsVisisble = true;
        }
    }
}
