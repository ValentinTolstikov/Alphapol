using System;
using System.Collections.Frozen;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using Domain.Interfaces;
using Domain.Models;
using Praktika.Views;

namespace Praktika.ViewModels
{
    public class UserMainVM : INotifyPropertyChanged
    {
        private ObservableCollection<Partner> _partners;

        public Window parent {  get; set; }
        public ObservableCollection<Partner> Partners {
            get
            {
                if (_partners is not null)
                    return _partners;
                else return null;
            }
            set
            {
                _partners = value;
                OnPropertyChanged();
            }
        }

        private readonly IRepository<Partner> _partnerRepo;
        private readonly IRepository<PartnerType> _partnerTypeRepo;
        private readonly UserMainView _userMainView;

        private User? _currentUser = MainWindowVM.CurrentUser;
        private string _userFIO;

        public void show()
        {
            if (_userMainView != null)
            {
                _userMainView.Show();
                _userMainView.InitializeComponent();
            }
        }

        public UserMainVM(UserMainView mv, IRepository<Partner> partnersRepository, IRepository<PartnerType> partnersTypeRepository)
        {
            _partnerTypeRepo = partnersTypeRepository;
            _partnerRepo = partnersRepository;
            _userMainView = mv;
            _userMainView.DataContext = this;
            GetUserFIO();

            BackToLoginPageCommand = new RelayCommand(BackToLoginHandler);
            Init();
        }

        private async Task Init()
        {
            await Task.Run(async () =>
            {
                var partners = await _partnerRepo.GetAllAsync();
                foreach (var partner in partners)
                {
                    partner.IdTypeNavigation = await _partnerTypeRepo.GetByIdAsync(partner.IdType);
                }
                Partners = new ObservableCollection<Partner>(partners);
            });
            
        }

        private void BackToLoginHandler(object obj)
        {
            _userMainView.Close();
            parent.Show();
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

        public ICommand BackToLoginPageCommand { get; set; }

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
