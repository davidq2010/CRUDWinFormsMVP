using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CRUDWinFormsMVP.Models;
using CRUDWinFormsMVP.Views;

namespace CRUDWinFormsMVP.Presenters
{
    public class PetPresenter
    {
        private readonly IPetView view;
        private readonly IPetRepository repository;
        private readonly BindingSource petsBindingSource;
        private IEnumerable<PetModel> pets;

        public PetPresenter(IPetView view, IPetRepository repository)
        {
            this.view = view ?? throw new ArgumentNullException(nameof(view));
            this.repository = repository ?? throw new ArgumentNullException(nameof(repository));
            petsBindingSource = new BindingSource();

            this.view.SearchEvent += SearchPet;
            this.view.AddEvent += AddPet;
            this.view.EditEvent += LoadPetToEdit;
            this.view.DeleteEvent += DeletePet;
            this.view.SaveEvent += SavePet;
            this.view.CancelEvent += CancelAction;

            this.view.SetPetListBindingSource(petsBindingSource);
            LoadAllPets();
            this.view.Show();
        }

        private void LoadAllPets()
        {
            pets = repository.GetAll();
            petsBindingSource.DataSource = pets;
        }

        private void CancelAction(object? sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        private void SavePet(object? sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        private void DeletePet(object? sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        private void LoadPetToEdit(object? sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        private void AddPet(object? sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        private void SearchPet(object? sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(view.SearchValue))
            {
                pets = repository.GetByValue(view.SearchValue);
            }
            else
            {
                pets = repository.GetAll();
            }
            petsBindingSource.DataSource = pets;
        }
    }
}
