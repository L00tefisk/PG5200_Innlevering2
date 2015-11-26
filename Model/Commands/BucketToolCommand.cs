using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LevelEditor.Model.Commands
{
    class BucketToolCommand : Command
    {
        private readonly List<Tile> _oldList;
        private readonly List<Tile> _newList;
        private readonly Map _map;

        public BucketToolCommand()
        {
            
        }
        public override void Execute()
        {
            throw new NotImplementedException();
        }

        public override void Undo()
        {
            throw new NotImplementedException();
        }
    }
}
