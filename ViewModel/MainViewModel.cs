using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Input;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using PG5200_Innlevering1.SettingsEditor.Model;
using System.Collections.ObjectModel;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Windows;

namespace PG5200_Innlevering1.SettingsEditor.ViewModel
{
    /// <summary>
    /// This class contains properties that the main View can data bind to.
    /// <para>
    /// See http://www.mvvmlight.net
    /// </para>
    /// </summary>
    public class MainViewModel : ViewModelBase
    {
        #region Commands

        public ICommand NewCommand { get; private set; }
        public ICommand RemoveCommand { get; private set; }
        public ICommand DefaultCommand { get; private set; }
        public ICommand SaveCommand { get; private set; }
        public ICommand LoadCommand { get; private set; }

        #endregion

        /// <summary>
        /// Initializes a new instance of the MainViewModel class.
        /// </summary>
        public MainViewModel()
        {
            CreateCommands();

            Load();
        }

        private void CreateCommands()
        {
            SaveCommand = new RelayCommand(Save);
            LoadCommand = new RelayCommand(Load);
        }

        private bool CanRemove()
        {
            //return SelectedItem != null;
            return true;
        }

        public void Default()
        {
            PopulateView();
        }

        public void Save()
        {
        
        }

        public void Load()
        {
            
        }

        public void PopulateView()
        {
           
        }
    }
}