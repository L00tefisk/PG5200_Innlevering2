using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace LevelEditor.Model
{
    public class TileButton : Button, IEquatable<TileButton>, IEquatable<Int32>
    {
        public ushort TileId { get; set; }
        public TileButton(ushort id, string description)
        {
            Image img = new Image();
            TileId = id;
            img.Width = 50;
            img.Height = 50;
            img.Margin = new Thickness(10);
            ToolTip = description;
            img.Source = new BitmapImage(new Uri("../" + Model.Instance.ImagePaths[id].Path, UriKind.Relative));
            Content = img;
        }
        public bool Equals(TileButton other)
        {
            return other.TileId == TileId;
        }
        public bool Equals(int id)
        {
            return id == TileId;
        }
    }
}
