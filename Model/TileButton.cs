using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace LevelEditor.Model
{
    public class TileButton : Button
    {
        public ushort TileId { get; set; }

        public TileButton(ushort id)
        {
            TileId = id;
            Image img = new Image();
            img.Width = 50;
            img.Height = 50;
            img.Margin = new Thickness(10);
            //       tileButton.Stretch = Stretch.Fill;

            img.Focusable = true;
            img.Source = new BitmapImage(new Uri(Model.ImgPaths[id], UriKind.Relative));
            Content = img;
        }
    }
}
