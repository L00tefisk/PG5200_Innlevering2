using System.Windows;
using GalaSoft.MvvmLight.Threading;

namespace PG5200_Innlevering2
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        static App()
        {
            DispatcherHelper.Initialize();
        }
    }
}
