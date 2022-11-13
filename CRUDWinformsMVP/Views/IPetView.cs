using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRUDWinFormsMVP.Views
{
    public interface IPetView
    {
        string PetID { get; set; }
        string PetName { get; set; }
        string PetType { get; set; }
        string PetColor { get; set; }

        string SearchValue { get; set; }
        bool IsEdited { get; set; }
        bool IsSuccessful { get; set; }
        string Message { get; set; }

        // I think these are the key for the View:
        // Guarantees that the View does not implement any business logic as
        // the Presenter will hook up to these events.
        event EventHandler SearchEvent;
        event EventHandler AddEvent;
        event EventHandler DeleteEvent;
        event EventHandler EditEvent;
        event EventHandler SaveEvent;
        event EventHandler CancelEvent;

        void SetPetListBindingSource(BindingSource bindingSource);
        void Show();
    }
}
