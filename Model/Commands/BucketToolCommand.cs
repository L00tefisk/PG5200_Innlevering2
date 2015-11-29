using System;
using System.Collections.Generic;

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
