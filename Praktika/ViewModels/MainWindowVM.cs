using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using Domain.Interfaces;
using Domain.Models;

namespace Praktika.ViewModels
{
    public class MainWindowVM : INotifyPropertyChanged
    {
        public static User? CurrentUser { get; private set; }
        private readonly IUserService _userService;

        public event PropertyChangedEventHandler? PropertyChanged;

        public ICommand LoginCommand { get; set; }

        private string _login;
        private string _password;

        public bool IsCapthaVisible { get; set; } = false;

        public MainWindowVM(IUserService userService) 
        {
            _userService = userService;

            LoginCommand = new RelayCommand(LoginHandler);
        }

        private async void LoginHandler(object obj)
        {
            User? user = await _userService.LoginAsync(Login, Password);
            if (user is null)
            {
                MessageBox.Show("Пользователь с таким логином и паролем не найден.");
            }
            else 
            {

            }
        }

        public string Login 
        {
            get => _login;
            set
            {
                _login = value;
                OnPropertyChanged();
            }
        }

        public string Password
        {
            get => _password;
            set
            {
                _password = value;
                OnPropertyChanged();
            }
        }

        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
