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
            // Need to update the binding source's data source to trigger the
            // DataGridView to update.
            // I'm guessing the DataSource doesn't store the reference to the 'pets' variable
            // and somehow points directly to the value at &pets (i.e., pets stores a memory address
            // to some data. DataSource doesn't store &pets, but rather points to the data at &pets.)
            petsBindingSource.DataSource = pets;
        }

        private void AddPet(object sender, EventArgs e)
        {
            view.IsEdited = false;
        }
        private void LoadPetToEdit(object sender, EventArgs e)
        {
            var pet = (PetModel)petsBindingSource.Current;
            view.PetID = pet.Id.ToString();
            view.PetName = pet.Name;
            view.PetType = pet.Type;
            view.PetColor = pet.Color;
            view.IsEdited = true;
        }
        private void SavePet(object sender, EventArgs e)
        {
            var model = new PetModel
            {
                Id = Convert.ToInt32(view.PetID),
                Name = view.PetName,
                Type = view.PetType,
                Color = view.PetColor
            };
            try
            {
                new Common.ModelDataValidation().Validate(model);
                if (view.IsEdited)
                {
                    repository.Edit(model);
                    view.Message = "Pet edited successfuly";
                }
                else //Add new model
                {
                    repository.Add(model);
                    view.Message = "Pet added sucessfully";
                }
                view.IsSuccessful = true;
                LoadAllPets();
                CleanviewFields();
            }
            catch (Exception ex)
            {
                view.IsSuccessful = false;
                view.Message = ex.Message;
            }
        }

        private void CleanviewFields()
        {
            view.PetID = "0";
            view.PetName = "";
            view.PetType = "";
            view.PetColor = "";
        }

        private void CancelAction(object sender, EventArgs e)
        {
            CleanviewFields();
        }
        private void DeletePet(object sender, EventArgs e)
        {
            try
            {
                var pet = (PetModel)petsBindingSource.Current;
                repository.Delete(pet.Id);
                view.IsSuccessful = true;
                view.Message = "Pet deleted successfully";
                LoadAllPets();
            }
            catch
            {
                view.IsSuccessful = false;
                view.Message = "An error ocurred, could not delete pet";
            }
        }

    }
}
