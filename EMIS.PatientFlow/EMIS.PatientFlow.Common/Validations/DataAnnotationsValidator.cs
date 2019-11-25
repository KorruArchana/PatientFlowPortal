using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EMIS.PatientFlow.Common.Validations
{
   public class DataAnnotationsValidator
    {
       public bool TryValidate(object obj, out List<ValidationResult> results)
       {
           var context = new ValidationContext(obj, serviceProvider: null, items: null);

           results = new List<ValidationResult>();

           return Validator.TryValidateObject(obj, context, results, validateAllProperties: true);
       }
    }
}
