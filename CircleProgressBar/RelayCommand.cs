using System;
using System.Windows.Input;

namespace CircleProgressBar;

public class RelayCommand : ICommand
{
    private Action<object> execute;

    private Predicate<object> canExecute;

    public RelayCommand(Action<object> execute)
        : this(execute, DefaultCanExecute)
    {
    }

    public RelayCommand(Action<object> execute, Predicate<object> canExecute)
    {
        if (execute == null) throw new ArgumentNullException("execute");

        if (canExecute == null) throw new ArgumentNullException("canExecute");

        this.execute = execute;
        this.canExecute = canExecute;
    }

    public bool CanExecute(object parameter)
    {
        return canExecute != null && canExecute(parameter);
    }

    public event EventHandler CanExecuteChanged
    {
        add
        {
            CommandManager.RequerySuggested += value;
            CanExecuteChangedInternal += value;
        }

        remove
        {
            CommandManager.RequerySuggested -= value;
            CanExecuteChangedInternal -= value;
        }
    }

    public void Execute(object parameter)
    {
        execute(parameter);
    }

    private static bool DefaultCanExecute(object parameter)
    {
        return true;
    }

    private event EventHandler CanExecuteChangedInternal;

    public void Destroy()
    {
        canExecute = _ => false;
        execute = _ => { return; };
    }


    public void OnCanExecuteChanged()
    {
        var handler = CanExecuteChangedInternal;
        if (handler != null)
            //DispatcherHelper.BeginInvokeOnUIThread(() => handler.Invoke(this, EventArgs.Empty));
            handler.Invoke(this, EventArgs.Empty);
    }
}