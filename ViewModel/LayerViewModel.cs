using System.Collections.ObjectModel;
using System.Windows.Controls;
using System.Windows.Data;
using LevelEditor.Model;

namespace LevelEditor.ViewModel
{
    public class LayerViewModel : ListView
    {
        public ObservableCollection<Layer> Layers { get; set; }

        private Layer _selectedItem;
        public LayerViewModel()
        {
            Layers = new ObservableCollection<Layer>();

            for (int i = 0; i < 5; i++)
                Layers.Add(new Layer("Layer "+i+1));

            ItemsSource = Layers;

            SelectedItem = Layers[0];

            DisplayMemberPath = "Name";
        }
    }

}