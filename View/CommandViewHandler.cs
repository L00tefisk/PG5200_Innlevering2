using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using LevelEditor.Model.Commands;

namespace LevelEditor.View
{
    public partial class MainWindow
    {
        CommandController controller = new CommandController();
        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            ListViewItem item = ((ListViewItem)((CheckBox)sender).Parent);

            item.DataContext.ToString();
            //Layer l = (Layer)DataContext;
            //MessageBox.Show(((CheckBox)sender).IsChecked.ToString());

            /*ListViewItem i = (ListViewItem)obj;

            i.IsSelected = false;

            CheckBox box = (CheckBox) sender;*/

        }

        public void NewCommand(object sender, ExecutedRoutedEventArgs e)
        {
            MessageBox.Show("new");
        }

        private void OpenCommand(object sender, ExecutedRoutedEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void SaveCommand(object sender, ExecutedRoutedEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void SaveAsCommand(object sender, ExecutedRoutedEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void UndoCommand(object sender, ExecutedRoutedEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void RedoCommand(object sender, ExecutedRoutedEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void CutCommand(object sender, ExecutedRoutedEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void CopyCommand(object sender, ExecutedRoutedEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void PasteCommand(object sender, ExecutedRoutedEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void ExitCommand(object sender, ExecutedRoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }


        
    }

    public static class CustomCommands
    {
        public static RoutedUICommand Exit = new RoutedUICommand
                (
                        "Exit",
                        "Exit",
                        typeof(CustomCommands),
                        new InputGestureCollection()
                        {
                                        new KeyGesture(Key.F4, ModifierKeys.Alt)
                        }
                );

        //Define more commands here, just like the one above
    }
}
