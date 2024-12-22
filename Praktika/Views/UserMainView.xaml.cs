using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Praktika.ViewModels;

namespace Praktika.Views
{
    /// <summary>
    /// Логика взаимодействия для UserMainView.xaml
    /// </summary>
    public partial class UserMainView : Window
    {
        public UserMainView()
        {
            
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            ((UserMainVM)this.DataContext).BackToLoginPageCommand.Execute(null);
        }
    }
}
