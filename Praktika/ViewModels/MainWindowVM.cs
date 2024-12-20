using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows;
using System.Windows.Input;
using Domain.Interfaces;
using Domain.Models;
using Microsoft.Extensions.DependencyInjection;
using Praktika.Views;

namespace Praktika.ViewModels
{
    public class MainWindowVM : INotifyPropertyChanged
    {
        System.Timers.Timer timer;
        
        public static User? CurrentUser;
        
        private readonly IUserService _userService;
        private readonly IServiceProvider _serviceProvider;
        public MainWindow _mv;

        private bool _isCapthaFailed = false;
        private Random rnd = new Random();
        private string _login;
        private string _password;
        private string _capthaInput;
        private string _captha = "De0L";
        private bool _isCapthaVisible;

        public ICommand LoginCommand { get; set; }

        public MainWindowVM(IUserService userService, IServiceProvider service) 
        {
            _userService = userService;
            _serviceProvider = service;
            LoginCommand = new RelayCommand(LoginHandler, null);
        }

        private async void LoginHandler(object obj)
        {
            if (_isCapthaVisible && CapthaInput != Captha)
            {
                MessageBox.Show("Капча введена неверно");
                GenerateCaptha();
                return;
            }

            User? user = await _userService.LoginAsync(Login, Password);
            if (user is null)
            {
                MessageBox.Show("Пользователь с таким логином и паролем не найден.");
                IsCapthaVisible = true;
                _isCapthaFailed = false;
                GenerateCaptha();
                return;
            }
            else 
            {
                CurrentUser = user;
                _mv.Hide();
                var mv = _serviceProvider.GetRequiredService<UserMainVM>();
                mv.parent = _mv;
                mv.show();
            }
        }

        public string Captha
        {
            get 
            {
                return _captha;
            } 
            set
            {
                _captha = value;
                OnPropertyChanged();
            }
        }

        private void GenerateCaptha()
        {
            string letters = "qwrtyuu";
            string nums = "1235678";

            StringBuilder sb = new StringBuilder();

            for (int i = 0; i < 3; i++)
            {
                sb.Append(letters[rnd.Next(0,nums.Length)]);
            }

            sb.Append(nums[rnd.Next(0, nums.Length)]);
            Captha = sb.ToString();
        }

        public string CapthaInput
        {
            get => _capthaInput;
            set
            {
                _capthaInput = value;
                OnPropertyChanged();
            }
        }

        public bool IsCapthaVisible
        {
            get => _isCapthaVisible;
            set
            {
                _isCapthaVisible = value;
                OnPropertyChanged();
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

        public event PropertyChangedEventHandler? PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
