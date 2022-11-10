using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace WinFormsApp1.Models
{
    public class PetModel
    {
        private int id;
        private string? name;
        private string? type;
        private string? color;

        [DisplayName("Pet ID")]
        public int Id { get => id; set => id = value; }

        [DisplayName("Pet Name")]
        [Required(ErrorMessage = "The pet name is required")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "The pet name must be between 2 and 50 characters")]
        public string? Name 
        { 
            get => name;
            set => name = value ?? throw new ArgumentNullException(nameof(value));
        }

        [DisplayName("Pet Type")]
        [Required(ErrorMessage = "The pet type is required")]
        public string? Type 
        { 
            get => type; 
            set => type = value ?? throw new ArgumentNullException(nameof(value)); 
        }

        [DisplayName("Pet Color")]
        [Required(ErrorMessage = "The pet color is required")]
        public string? Color 
        {
            get => color; 
            set => color = value ?? throw new ArgumentNullException(nameof(value)); 
        }
    }
}
