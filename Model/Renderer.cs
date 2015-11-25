using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace LevelEditor.Model
{
    public class Renderer : Canvas
    {
        public Renderer()
        {
            Background = Brushes.Transparent;
            Height = 1000;
            Width = 1000;
        }

        protected override void OnRender(DrawingContext dc)
        {
            base.OnRender(dc);
            Pen pen = new Pen();
            
            dc.DrawRectangle(Brushes.Black, pen, new Rect(0, 0, 600, 400));
            dc.DrawEllipse(Brushes.Green, pen, new Point(300, 300), 50, 50);
           
            var random = new Random();
            for (int i = 0; i < 100; i++)
                for (int j = 0; j < 100; j++)
                    dc.DrawRectangle(
                        random.Next(2) == 0 ? Brushes.Black : Brushes.Red,
                        (Pen)null,
                        new Rect(i * 10, j * 10, 10, 10));
        }
    }
}
