using System;
using System.Collections.Generic;
using System.Data.Linq.Mapping;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LevelEditor.Model.Commands;
using LevelEditor.Model.Tools;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace LevelEditor.Model
{
    public class Editor
    {
        public ushort SelectedTileId { get; set; }
        public Tile SelectedTile { get; set; }
        public ushort SelectedTool { get; set; }
        public ImageSource[] Images { get; set; }
    
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
            _tools.Add(new BucketTool());
            _tools.Add(new EraserTool());
            _tools.Add(new StampTool());
            _tools.Add(new WandTool());
            _tools.Add(new SelectionTool());
            
            SelectedTool = 2;
            SelectedTileId = 0;
            SelectedTile = new Tile(SelectedTileId, 0, 0);

            Images = new ImageSource[Model.ImgPaths.Count];
            for(int i = 0; i < Model.ImgPaths.Count; i++)
            {
                Images[i] = new BitmapImage(new Uri(Model.ImgPaths[i], UriKind.Relative));
            }

            Tool.Init(_selectedTiles, _commandController, _map, SelectedTileId, Images);
        }

        /// <summary>
        /// Performs the action of the currently selected tool.
        /// </summary>
        public void PerformAction()
        {
           // if(SelectedTool < _tools.Count)
           //     _tools[SelectedTool].PerformAction();
        }
        public void SelectTile(int x, int y)
        {
            _map._level[y][x].ChangeTile(Images[SelectedTileId]);
            //_selectedTiles.Add(new Tile(SelectedTileId, x, y));
            //_selectedTiles
        }
        public Tile GetTile(int x, int y)
        {
            return _map.GetTile(x, y);
        }
        /// <summary>
        /// Redoes an action if there is one to redo.
        /// </summary>
        public void Redo()
        {
            _commandController.Execute();
        }
        /// <summary>
        /// Undoes an action if there is one to undo.
        /// </summary>
        public void Undo()
        {
            _commandController.Undo();
        }
        public void NextTool()
        {
            SelectedTool++;
            if (SelectedTool == _tools.Count)
            {
                SelectedTool = 0;
            }
        }
        public void PreviousTool()
        {
            SelectedTool--;
            if (SelectedTool == ushort.MaxValue)
            {
                SelectedTool = (ushort)(_tools.Count - 1);
            }
        }
    }
}
