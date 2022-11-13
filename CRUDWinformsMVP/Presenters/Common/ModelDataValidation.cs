using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRUDWinFormsMVP.Presenters.Common
{
    public class ModelDataValidation
    {
        public void Validate(object model)
        {
            string errorMsg = string.Empty;
            // Uses the validation annotations in the model to perform validation of a model instance
            var results = new List<ValidationResult>();
            var context = new ValidationContext(model);
            if (!Validator.TryValidateObject(model, context, results, true))
            {
                foreach (var item in results)
                {
                    errorMsg += "- " + item.ErrorMessage + "\n";
                }
                throw new Exception(errorMsg);
            }
        }
    }
}
