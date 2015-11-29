using LevelEditor.Model.Commands;

namespace LevelEditor.Model.Tools
{
    class SelectionTool : Tool
    {
        public override void PerformAction()
        {
            CommandController.Add(new SelectTileCommand(SelectedTiles, new Tile(null, EditorWindow.MousePosition)));
        }
    }
}
