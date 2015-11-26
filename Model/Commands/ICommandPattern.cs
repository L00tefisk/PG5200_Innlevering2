using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LevelEditor.Model
{
    public interface ICommandPattern
    {
        void Execute();
        void Undo();
    }
}
