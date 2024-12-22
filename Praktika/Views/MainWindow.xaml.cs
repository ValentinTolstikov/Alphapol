using System.Windows;
using Microsoft.Extensions.DependencyInjection;
using Praktika.ViewModels;

namespace Praktika
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private MainWindowVM _mainWindowVM;
        private IServiceProvider _serviceProvider;

        public MainWindow(MainWindowVM mainWindowVM, IServiceProvider services)
        {
            InitializeComponent();
            _mainWindowVM = mainWindowVM;
            _mainWindowVM._mv = this;
            DataContext = _mainWindowVM;
            _serviceProvider = services;
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            var app = _serviceProvider.GetRequiredService<App>();
            app.Shutdown();
        }
    }
}