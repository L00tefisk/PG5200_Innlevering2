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
