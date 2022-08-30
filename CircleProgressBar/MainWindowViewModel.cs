using System;
using System.Timers;
using System.Windows;
using System.Windows.Media;

namespace CircleProgressBar;

public class MainWindowViewModel : BindableBase
{
    private readonly Timer _timer = new();
    private Geometry? _arkData;

    private int _angle;

    public MainWindowViewModel()
    {
        ArkData = CreateArc(100, 0);
        StartTimerCommand = new RelayCommand(ExecuteStartTimerCommand);
        _timer.Interval = 10;
        _timer.Elapsed += TimerElapsed;
        StartTimerCommand = new RelayCommand(ExecuteStartTimerCommand);
    }

    public Geometry? ArkData
    {
        get => _arkData;
        set => SetProperty(ref _arkData, value);
    }

    public RelayCommand StartTimerCommand { get; set; }

    private Geometry? CreateArc(double radius, double angle)
    {
        var endPoint = new Point(
            radius * Math.Sin(angle * Math.PI / 180) + radius,
            radius * -Math.Cos(angle * Math.PI / 180) + radius);

        var segment = new ArcSegment(
            endPoint, new Size(radius, radius), 0,
            angle >= 180, SweepDirection.Clockwise, true);

        var figure = new PathFigure { StartPoint = new Point(radius, 0) };
        figure.Segments.Add(segment);

        var geometry = new PathGeometry();
        geometry.Figures.Add(figure);


        return geometry;
    }

    private void ExecuteStartTimerCommand(object obj)
    {
        _angle = 0;
        ArkData = CreateArc(100, _angle);

        _timer.Start();
    }

    private void TimerElapsed(object? sender, ElapsedEventArgs e)
    {
        _angle++;
        DispatherService.Invoke(() => { ArkData = CreateArc(100, _angle); });

        if (_angle >= 360) _timer.Stop();
    }
}