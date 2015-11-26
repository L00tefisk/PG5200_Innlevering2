using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LevelEditor.Model.Commands;

namespace LevelEditor.Model.Tools
{
    public class BucketTool : Tool
    {
        public override void PerformAction()
        {
            CommandController.Add(new BucketToolCommand());
        }
    }
}
