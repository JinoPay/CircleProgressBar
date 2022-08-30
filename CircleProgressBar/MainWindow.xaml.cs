using System.Windows;

namespace CircleProgressBar;

/// <summary>
///     Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
        DataContext = MainWindowViewModel;
    }

    public MainWindowViewModel MainWindowViewModel { get; set; } = new();
}