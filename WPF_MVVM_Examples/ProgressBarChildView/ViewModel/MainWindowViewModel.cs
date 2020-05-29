using System;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System.Windows;
using System.Collections.ObjectModel;
using System.Threading;
using System.Threading.Tasks;
using MahApps.Metro.Controls;
using MahApps.Metro.SimpleChildWindow;
using System.IO;

using ProgressBarChildView.ChildView;
using GalaSoft.MvvmLight.Messaging;

namespace ProgressBarChildView.ViewModel
{
    public class MainWindowViewModel : ViewModelBase
    {
        public RelayCommand<object> OnButtonClickCommand { get; private set; }

        public ChildViewModel _ChildViewModel { get; set; }
        public MainWindowViewModel()
        {
            _ChildViewModel = new ChildViewModel();
            OnButtonClickCommand = new RelayCommand<object>(OnButtonClick);
        }

        private void OnButtonClick(object sender)
        {
            ProgressChildView progressChildView = new ProgressChildView() { DataContext = _ChildViewModel };

            ((MetroWindow)Application.Current.MainWindow).ShowChildWindowAsync(progressChildView);

            _ChildViewModel.ProgressMaximum = 100;
            Task.Run(() =>
            {
                try
                {
                    for (int i = 0; i < _ChildViewModel.ProgressMaximum + 1; i++)
                    {
                        _ChildViewModel.ProgressValue = i;
                        System.Threading.Thread.Sleep(100);
                    }
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.Message);
                }
            });
        }
    }
}
