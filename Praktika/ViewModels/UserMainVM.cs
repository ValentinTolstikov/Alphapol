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
using Microsoft.Extensions.DependencyInjection;
using Praktika.Views;

namespace Praktika.ViewModels
{
    public class UserMainVM : INotifyPropertyChanged
    {
        private ObservableCollection<Partner> _partners;
        private Partner _selectedPartner;

        public MainWindow parent {  get; set; }
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
        public Partner SelectedPartner {
            get => _selectedPartner;
            set
            {
                _selectedPartner = value;
                OnPropertyChanged();
            }
        }

        private readonly IRepository<Partner> _partnerRepo;
        private readonly IRepository<PartnerType> _partnerTypeRepo;
        private readonly IRepository<City> _cityRepo;
        private readonly IRepository<Street> _StreetRepo;
        private readonly UserMainView _userMainView;
        private IServiceScopeFactory serviceScopeFac;

        private User? _currentUser = MainWindowVM.CurrentUser;
        private string _userFIO;

        public void show()
        {
                _userMainView.Show();
                _userMainView.InitializeComponent();
        }

        public UserMainVM(UserMainView mv, IServiceScopeFactory serviceScope, IRepository<Partner> partnersRepository, IRepository<PartnerType> partnersTypeRepository, IRepository<City> cityRepo, IRepository<Street> streetRepo)
        {
            serviceScopeFac = serviceScope;

            _partnerTypeRepo = partnersTypeRepository;
            _cityRepo = cityRepo;
            _StreetRepo = streetRepo;

            _partnerRepo = partnersRepository;
            _userMainView = mv;
            _userMainView.DataContext = this;
            GetUserFIO();

            BackToLoginPageCommand = new RelayCommand(BackToLoginHandler);
            CreateNewPartnerCommand = new RelayCommand(CreateNewPartnerHandler);
            UpdatePartnerCommand = new RelayCommand(UpdatePartnerCommandHandler);

            Init();
        }

        private void UpdatePartnerCommandHandler(object obj)
        {
            if(SelectedPartner is null)
            {
                return;
            }

            var scope = serviceScopeFac.CreateScope();
            var vm = scope.ServiceProvider.GetRequiredService<PartnersAddEditVM>();
            vm.IsEdit = true;
            vm.Partner = SelectedPartner;
            vm.Parent = _userMainView;
            vm.Init();
        }

        private void CreateNewPartnerHandler(object obj)
        {
            var scope = serviceScopeFac.CreateScope();
            var vm = scope.ServiceProvider.GetRequiredService<PartnersAddEditVM>();
            vm.IsEdit = false;
            vm.Parent = _userMainView;
            vm.Init();
        }

        public async Task Init()
        {
            await Task.Run(async () =>
            {
                var partners = await _partnerRepo.GetAllAsync();
                foreach (var partner in partners)
                {
                    partner.IdTypeNavigation = await _partnerTypeRepo.GetByIdAsync(partner.IdType);
                    partner.IdCityNavigation = await _cityRepo.GetByIdAsync(partner.IdCity);
                    partner.IdStreetNavigation = await _StreetRepo.GetByIdAsync(partner.IdStreet);
                }
                Partners = new ObservableCollection<Partner>(partners);
            });
            
        }

        private void BackToLoginHandler(object obj)
        {
            parent.Show();
            parent.Focus();
            _userMainView.Hide();
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
        public ICommand CreateNewPartnerCommand { get; set; }
        public ICommand UpdatePartnerCommand { get; set; }

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
