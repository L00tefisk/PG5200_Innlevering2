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
