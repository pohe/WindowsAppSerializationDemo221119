using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using WindowsAppSerializationDemo221119.Annotations;
using WindowsAppSerializationDemo221119.Common;
using WindowsAppSerializationDemo221119.Handler;
using WindowsAppSerializationDemo221119.Model;

namespace WindowsAppSerializationDemo221119.ViewModel
{
    class PersonsViewModel : INotifyPropertyChanged
    {
        public string CprNo
        {
            get { return _cprNo; }
            set
            {
                _cprNo = value; OnPropertyChanged();
            }
        }

        public string FirstName
        {
            get { return _firstName; }
            set { _firstName = value; OnPropertyChanged(); }
        }

        public string LastName
        {
            get { return _lastName; }
            set { _lastName = value; OnPropertyChanged(); }
        }

        public string Address
        {
            get { return _address; }
            set { _address = value; OnPropertyChanged(); }
        }

        private ICommand _addCommand;
        private ICommand _saveCommand;
        private ICommand _loadCommand;
        private string _cprNo;
        private string _firstName;
        private string _lastName;
        private string _address;
        public ObservableCollection<Person> Persons { get; set; }

        public ICommand AddCommand
        {
            get { return _addCommand; }
            set { _addCommand = value; }
        }

        public ICommand SaveCommand
        {
            get { return _saveCommand; }
            set { _saveCommand = value; }
        }

        public ICommand LoadCommand
        {
            get { return _loadCommand; }
            set { _loadCommand = value; }
        }

        public PersonHandler PersonHandler { get; set; }
        public PersonsViewModel()
        {
            Persons = new ObservableCollection<Person>();
            PersonHandler = new PersonHandler(this);
            _addCommand = new RelayCommand(PersonHandler.AddPersons);
            _saveCommand = new RelayCommand(PersonHandler.SavePersonsAsync);
            _loadCommand = new RelayCommand(PersonHandler.LoadPersonsAsync);
        }

        public void AddPersons()
        {
            Persons.Add(new Person(_cprNo, _firstName, _lastName, _address));
            CprNo = "";
            FirstName = "";
            LastName = "";
            Address = "";
        }


        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

}
