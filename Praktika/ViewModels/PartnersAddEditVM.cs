using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using CommunityToolkit.Mvvm.Input;
using Domain.Interfaces;
using Domain.Models;
using Praktika.Views;

namespace Praktika.ViewModels
{
    class PartnersAddEditVM : INotifyPropertyChanged
    {
        private Partner _partner;

        private City _selectedCity;
        private PartnerType _selectedType;
        private Street _selectedStreet;
        private bool _canSave;

        private readonly IRepository<City> _cityRepo;
        private readonly IRepository<PartnerType> _partnerTypesRepo;
        private readonly IRepository<Street> _streetRepo;
        private readonly IRepository<Partner> _partnerRepo;
        private PartnerAddEditView _view;

        private ObservableCollection<City> _citys;
        private ObservableCollection<PartnerType> _partnerTypes;
        private ObservableCollection<Street> _streets;

        public ObservableCollection<City> Citys {
            get => _citys;
            set
            {
                _citys = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<PartnerType> PartnerTypes
        {
            get => _partnerTypes;
            set
            {
                _partnerTypes = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<Street> Streets
        {
            get => _streets;
            set
            {
                _streets = value;
                OnPropertyChanged();
            }
        }

        public UserMainView Parent;
        public bool IsEdit { get; set; }
        public Partner Partner 
        {
            get => _partner;
            set
            {
                _partner = value;
                OnPropertyChanged();
            }
        }

        public PartnerType SelectedType
        {
            get => _selectedType;
            set
            {
                _selectedType = value;
                OnPropertyChanged();
            }
        }

        public City SelectedCity
        {
            get => _selectedCity;
            set
            {
                _selectedCity = value;
                OnPropertyChanged();
            }
        }

        public Street SelectedStreet
        {
            get => _selectedStreet;
            set
            {
                _selectedStreet = value;
                OnPropertyChanged();
            }
        }

        public PartnersAddEditVM(PartnerAddEditView view, IRepository<Partner> pRepo, IRepository<City> cRepo, IRepository<PartnerType> tRepo, IRepository<Street> sRepo) 
        {
            if (!IsEdit)
            {
                Partner = new Partner();
            }
            

            _view = view;
            _view.DataContext = this;

            _cityRepo = cRepo;
            _partnerTypesRepo = tRepo;
            _streetRepo = sRepo;
            _partnerRepo = pRepo;


            CancelCommand = new AsyncRelayCommand(CancelCommandHandler);
            SaveCommand = new AsyncRelayCommand(SaveCommandHandler);
        }

        private async Task CancelCommandHandler()
        {
            _partnerRepo.ResetChanges();
            await ((UserMainVM)Parent.DataContext).Init();
            _view.Hide();
        }

        private async Task SaveCommandHandler()
        {
            if (!IsEdit)
            {
                try
                {
                    await _partnerRepo.AddAsync(Partner);
                    await ((UserMainVM)Parent.DataContext).Init();
                }
                catch
                {
                    MessageBox.Show("Чтото пошло не так, возможно вы не заполнили все поля или допустили ошибки");
                    return;
                }
            }
            else
            {
                await _partnerRepo.EditAsync(Partner);
                await ((UserMainVM)Parent.DataContext).Init();
            }

            _view.Hide();
        }

        public async void Init()
        {
            Citys = new ObservableCollection<City>(await _cityRepo.GetAllAsync());
            PartnerTypes = new ObservableCollection<PartnerType>(await _partnerTypesRepo.GetAllAsync());
            Streets = new ObservableCollection<Street>(await _streetRepo.GetAllAsync());
            _view.ShowDialog();
        }

        public ICommand CancelCommand { get; set; }
        public ICommand SaveCommand { get; set; }

        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }

        public event PropertyChangedEventHandler? PropertyChanged;
    }
}
