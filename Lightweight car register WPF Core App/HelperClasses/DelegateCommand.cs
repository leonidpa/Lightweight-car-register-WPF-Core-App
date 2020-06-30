using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace Lightweight_car_register_WPF_Core_App.HelperClasses
{
    public class DelegateCommand : ICommand
    {
        #region Fields

        private readonly Action _executeMethod;
        private readonly Func<bool> _canExecuteMethod;
        private List<WeakReference> _canExecuteChangedHandlers;

        #endregion

        #region Constructors

        public DelegateCommand(Action executeMethod) : this(executeMethod, null)
        {
        }

        public DelegateCommand(Action executeMethod, Func<bool> canExecuteMethod)
        {
            if (executeMethod == null)
            {
                throw new ArgumentNullException("executeMethod");
            }

            _executeMethod = executeMethod;
            _canExecuteMethod = canExecuteMethod;
        }

        #endregion

        #region Public Events

        public event EventHandler CanExecuteChanged
        {
            add
            {
            }
            remove
            {
            }
        }

        #endregion

        #region ICommand Members

        bool ICommand.CanExecute(object parameter)
        {
            return CanExecute();
        }

        void ICommand.Execute(object parameter)
        {
            Execute();
        }

        #endregion

        #region Public Methods

        public bool CanExecute()
        {
            if (_canExecuteMethod != null)
            {
                return _canExecuteMethod();
            }

            return true;
        }

        public void Execute()
        {
            if (_executeMethod != null)
            {
                _executeMethod();
            }
        }

        public void RaiseCanExecuteChanged()
        {
            OnCanExecuteChanged();
        }

        protected virtual void OnCanExecuteChanged()
        {
        }

        #endregion
    }
}
