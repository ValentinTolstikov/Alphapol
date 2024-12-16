using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Domain.Models;

namespace Praktika.ViewModels
{
    public class UserMainVM : INotifyPropertyChanged
    {
        private User? _currentUser = MainWindowVM.CurrentUser;
        private string _userFIO;

    
        public UserMainVM()
        {
            GetUserFIO();
        }

        private void GetUserFIO()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(_currentUser.Surname);
            sb.Append(" ");
            sb.Append(_currentUser.Name);
            sb.Append(" ");
            sb.Append(_currentUser.Otc);
            UserFIO = sb.ToString();
        }

        public string UserFIO
        {
            get => _userFIO;
            set 
            {
                _userFIO = value;
                OnPropertyChanged();
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
