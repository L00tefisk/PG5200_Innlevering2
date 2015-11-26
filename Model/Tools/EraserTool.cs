using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LevelEditor.Model.Commands;

namespace LevelEditor.Model.Tools
{
    public class EraserTool : Tool 
    {
        public override void PerformAction()
        {
            CommandController.Add(new EraserToolCommand());
        }
    }
}
