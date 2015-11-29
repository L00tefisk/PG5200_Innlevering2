using LevelEditor.Model.Commands;

namespace LevelEditor.Model.Tools
{
    public class StampTool : Tool
    {
        public override void PerformAction()
        {
            CommandController.Add(new StampCommand(Map, SelectedTiles, SelectedTileId, Images));
        }
    }
}
