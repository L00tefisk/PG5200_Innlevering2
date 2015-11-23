using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace LevelEditor.Model
{
    public class ModelClass
    {
        public WrapPanel TilePanel { get; set; }
        public string Name { get; set; }
        public enum Shapes
        {
            Square,
            Circle,
            Triangle
        }
        public ModelClass()
        {
            int numElements = 50;
            TilePanel = new WrapPanel();
            TilePanel.Width = 200;

            int row = 0;

            for (int i = 0; i < numElements; i++)
            {

                TextBlock ageText = new TextBlock
                {
                    Text = i.ToString(),
                    FontSize = 12,
                    FontWeight = FontWeights.Bold
                };
                TilePanel.Children.Add(ageText);
            }
        }


    }
}
