using System;
using System.Windows;

namespace CircleProgressBar;

public class DispatherService
{
    public static void Invoke(Action action)
    {
        var dispatchObject = Application.Current?.Dispatcher;
        if (dispatchObject == null || dispatchObject.CheckAccess())
            action();
        else
            dispatchObject.Invoke(action);
    }
}