using System.Windows;

namespace Praktika
{
    public class App : System.Windows.Application
    {
        readonly MainWindow mainWindow;
        public App(MainWindow mainWindow)
        {
            this.mainWindow = mainWindow;
        }
        protected override void OnStartup(StartupEventArgs e)
        {
            mainWindow.Show();
            base.OnStartup(e);
        }
    }
}
