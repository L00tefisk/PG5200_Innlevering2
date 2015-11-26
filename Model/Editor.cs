using System;
using System.Collections.Generic;
using System.Data.Linq.Mapping;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LevelEditor.Model.Commands;
using LevelEditor.Model.Tools;

namespace LevelEditor.Model
{
    public class Editor
    {
        public ushort SelectedTileId { get; set; }
    
        private Map _map;
        private List<Tile> _selectedTiles;
        private List<Tool> _tools;
        private CommandController _commandController;

        public Editor(Map map, CommandController commandController)
        {
            _map = map;
            _commandController = commandController;
            _selectedTiles = new List<Tile>();
            _tools = new List<Tool>();
            _tools.Add(new BucketTool(_selectedTiles));
            _tools.Add(new EraserTool(_selectedTiles));
            _tools.Add(new StampTool(_selectedTiles));
            _tools.Add(new WandTool(_selectedTiles));
        }

        /// <summary>
        /// Sets all the tiles that are currently selected to your selected tile.
        /// </summary>
        public void SetTiles()
        {
            _commandController.Add(new SetSelectedTilesCommand(_map, _selectedTiles));
        }

        public void SelectTile(int x, int y)
        {
            _selectedTiles.Add(new Tile(SelectedTileId, x, y));
        }

        public Tile GetTile(int x, int y)
        {
            return _map.GetTile(x, y);
        }
    }
}
