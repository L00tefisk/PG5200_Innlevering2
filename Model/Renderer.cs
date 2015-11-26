using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace LevelEditor.Model
{
    public class Renderer : Canvas
    {
        private Map _map;
        public Renderer(Map map)
        {
            Background = Brushes.Transparent;
            Height = 1000;
            Width = 1000;
            _map = map;
            
            AddHandler(UIElement.MouseDownEvent, (RoutedEventHandler)ClickEventHandler);
        }

        private void ClickEventHandler(object sender, RoutedEventArgs e)
        {
            try
            {
                Point relativePoint = TransformToAncestor(Application.Current.MainWindow).Transform(new Point(0, 0));
                
                DependencyObject parentObject = VisualTreeHelper.GetParent((Renderer)sender);
                ScrollContentPresenter parent = parentObject as ScrollContentPresenter;
                int x = (int)Math.Round(parent.HorizontalOffset + Mouse.GetPosition(Application.Current.MainWindow).X - relativePoint.X) / 10;
                int y = (int)Math.Round(parent.VerticalOffset + Mouse.GetPosition(Application.Current.MainWindow).Y - relativePoint.Y) / 10;
                MessageBox.Show("X = " + x + "\nY = " + y + "\n");
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message);
            }
        }

        protected override void OnRender(DrawingContext dc)
        {
            base.OnRender(dc);
            // Find the visible area to draw in
            // Draw in the visible area.

            Pen pen = new Pen();
            var random = new Random();
            for (int i = 0; i < 100; i++)
                for (int j = 0; j < 100; j++)
                    dc.DrawRectangle(
                        new SolidColorBrush(new Color
                        {
                            A = 255,
                            R = (byte)random.Next(255),
                            G = (byte)random.Next(255),
                            B = (byte)random.Next(255)
                        }),
                        (Pen)null,
                        new Rect(i * 10, j * 10, 10, 10));
            
        }
    }
}
