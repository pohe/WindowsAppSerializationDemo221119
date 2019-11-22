using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindowsAppSerializationDemo221119.Persistency;
using WindowsAppSerializationDemo221119.ViewModel;

namespace WindowsAppSerializationDemo221119.Handler
{
    class PersonHandler
    {
        private PersonsViewModel personsViewModel;

        public PersonHandler(PersonsViewModel personsViewModel)
        {
            this.personsViewModel = personsViewModel;
        }

        public void AddPersons()
        {
            personsViewModel.AddPersons();
        }

        public async void SavePersonsAsync()
        {
            PersistenceFacade.SavePersonsAsJsonAsync(personsViewModel.Persons);
        }

        public async void LoadPersonsAsync()
        {
            var persons = await PersistenceFacade.LoadPersonsFromJsonAsync();
            personsViewModel.Persons.Clear();
            if (persons != null)
                foreach (var person in persons)
                {
                    personsViewModel.Persons.Add(person);
                }
        }

    }

}
