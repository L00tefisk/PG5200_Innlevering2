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
            int numElements = 500;
            TilePanel = new WrapPanel();

            int row = 0;

            for (int i = 0; i < numElements; i++)
            {
                string text = "";

                if (i < 10)
                    text += 0;
                if (i+1 < 100)
                    text += 0;
                text += (i+1).ToString();

                TextBlock textField = new TextBlock
                {
                    Margin = new System.Windows.Thickness(10),
                    Text = text,
                    FontSize = 12,
                    FontWeight = FontWeights.Bold
                };

                TilePanel.Children.Add(textField);
            }
        }


    }
}
