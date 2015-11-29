using LevelEditor.Model.Commands;

namespace LevelEditor.Model.Tools
{
    public class WandTool : Tool
    {
        public override void PerformAction()
        {
            CommandController.Add(new WandCommand(Map, SelectedTiles, new Tile(null, EditorWindow.MousePosition)));
        }
    }
}
