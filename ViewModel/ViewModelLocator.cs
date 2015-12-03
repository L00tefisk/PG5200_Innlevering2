using GalaSoft.MvvmLight.Ioc;
using Microsoft.Practices.ServiceLocation;

namespace LevelEditor.ViewModel
{
    /// <summary>
    /// This class contains static references to all the view models in the
    /// application and provides an entry point for the bindings.
    /// <para>
    /// See http://www.galasoft.ch/mvvm
    /// </para>
    /// </summary>
    public class ViewModelLocator
    {
        static ViewModelLocator()
        {
            ServiceLocator.SetLocatorProvider(() => SimpleIoc.Default);

            SimpleIoc.Default.Register<MainViewModel>();
            SimpleIoc.Default.Register<MapViewModel>();
            SimpleIoc.Default.Register<LayerViewModel>();
        }

        /// <summary>
        /// Gets the Main property.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance",
            "CA1822:MarkMembersAsStatic",
            Justification = "This non-static member is needed for data binding purposes.")]
        public MainViewModel Main => ServiceLocator.Current.GetInstance<MainViewModel>();
        public MapViewModel Map => ServiceLocator.Current.GetInstance<MapViewModel>();
        public LayerViewModel Layer => ServiceLocator.Current.GetInstance<LayerViewModel>();

        /// <summary>
        /// Cleans up all the resources.
        /// </summary>
        public static void Cleanup()
        {
        }
    }
}