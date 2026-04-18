using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Collections.Generic;
using System.Windows.Input;

namespace Laboratory_ProductManager.AppUI.ViewModels
{
    public abstract class BaseViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private bool _isBusy;
        private string _errorMessage;

        public bool IsBusy
        {
            get => _isBusy;
            set
            {
                if (SetProperty(ref _isBusy, value))
                {
                    OnPropertyChanged(nameof(IsNotBusy));
                    CommandManager.InvalidateRequerySuggested();
                }
            }
        }

        public bool IsNotBusy => !IsBusy;

        public string ErrorMessage
        {
            get => _errorMessage;
            set => SetProperty(ref _errorMessage, value);
        }

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        protected bool SetProperty<T>(ref T field, T value, [CallerMemberName] string propertyName = null)
        {
            if (EqualityComparer<T>.Default.Equals(field, value))
                return false;

            field = value;
            OnPropertyChanged(propertyName);
            CommandManager.InvalidateRequerySuggested();
            return true;
        }
    }
}
