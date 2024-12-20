using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Domain.Interfaces;
using Domain.Models;
using Praktika.Views;

namespace Praktika.ViewModels
{
    class PartnersAddEditVM : INotifyPropertyChanged
    {
        private readonly IRepository<City> _cityRepo;
        private readonly IRepository<PartnerType> _partnerTypesRepo;
        private readonly IRepository<Street> _streetRepo;

        private PartnerAddEditView _view;

        private ObservableCollection<City> _citys;

        public ObservableCollection<City> Citys {
            get => _citys;
            set
            {
                _citys = value;
                OnPropertyChanged();
            }
        }
        public ObservableCollection<PartnerType> PartnerTypes;
        public ObservableCollection<Street> Streets;

        public Window Parent;

        public bool IsEdit { get; set; }
        public Partner partner { get; set; }

        public PartnersAddEditVM(PartnerAddEditView view,IRepository<City> cRepo, IRepository<PartnerType> tRepo, IRepository<Street> sRepo) 
        {
            _view = view;
            _view.DataContext = this;
            _cityRepo = cRepo;
            _partnerTypesRepo = tRepo;
            _streetRepo = sRepo;

            Init();
        }

        private async void Init()
        {
            _view.Show();
            
            Citys = new ObservableCollection<City>(await _cityRepo.GetAllAsync());
            PartnerTypes = new ObservableCollection<PartnerType>(await _partnerTypesRepo.GetAllAsync());
            Streets = new ObservableCollection<Street>(await _streetRepo.GetAllAsync());
            OnPropertyChanged();
        }

        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }

        public event PropertyChangedEventHandler? PropertyChanged;
    }
}
