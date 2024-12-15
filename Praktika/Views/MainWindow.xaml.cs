using System.Windows;
using Praktika.ViewModels;

namespace Praktika
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private MainWindowVM _mainWindowVM;

        public MainWindow(MainWindowVM mainWindowVM)
        {
            InitializeComponent();
            _mainWindowVM = mainWindowVM;
            _mainWindowVM._mv = this;
            DataContext = _mainWindowVM;
        }
    }
}