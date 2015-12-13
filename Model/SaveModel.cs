using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace LevelEditor.Model
{
    public class SaveModel
    {
        public List<ushort[][]> Layers;

        public SaveModel()
        {
            Layers = new List<ushort[][]>();

        }
        public void GenerateSaveModel(Model model)
        {
            foreach (Layer layer in model.Layers)
            {
                ushort[][] tempInts = new ushort[100][];

                for (int i = 0; i < 100; i++)
                    tempInts[i] = new ushort[100];

                for (int i = 0; i < 100; i++)
                    for (int j = 0; j < 100; j++)
                        tempInts[i][j] = 0;

                foreach (Tile t in layer.Map.Tiles)
                {
                    if(t.Id != int.MaxValue)
                        tempInts[t.X][t.Y] = (ushort)(t.Id+1);
                }
                Layers.Add(tempInts);
            }
        }

        

        public void Create()
        {
            
        }
    }
}
