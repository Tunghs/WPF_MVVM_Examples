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
using GalaSoft.MvvmLight.Messaging;
using System.Windows.Threading;
using ProgressBarChildView.View;

namespace ProgressBarChildView.ViewModel
{
    public class ChildViewModel : ViewModelBase
    {
        private int _progressMaximum;
        public int ProgressMaximum
        {
            get { return _progressMaximum; }
            set { _progressMaximum = value; RaisePropertyChanged("ProgressMaximum"); }
        }
        private int _progressValue;
        public int ProgressValue
        {
            get { return _progressValue; }
            set { _progressValue = value; RaisePropertyChanged("ProgressValue"); }
        }

        public ChildViewModel()
        {

        }
    }
}
