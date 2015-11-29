using System.Collections.ObjectModel;
using System.Windows.Controls;

namespace LevelEditor.ViewModel
{
    public class Layer
    {
        public string Name { get; set; }

        public Layer(string name)
        {
            Name = name;
        }
    }
    public class LayerViewModel : ListView
    {
        public ObservableCollection<Layer> Layers { get; set; }

        private Layer _selectedItem;
        private LayerViewModel()
        {
            Layers = new ObservableCollection<Layer>();

            for (int i = 0; i < 5; i++)
                Layers.Add(new Layer("Layer "+i+1));

            ItemsSource = Layers;

            this.SelectedItem = Layers[0];
            
        }
    }
}