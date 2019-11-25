using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EMIS.PatientFlow.API.Utilities
{
    internal static class Validation
    {
        public static bool TryValidate(object obj, out List<ValidationResult> results)
        {
            var context = new ValidationContext(obj, serviceProvider: null, items: null);

            results = new List<ValidationResult>();

            return Validator.TryValidateObject(obj, context, results, validateAllProperties: true);
        }

        public static void ArgumentIsNull(object argument, string argumentName)
        {
            if (argument == null)
            {
                throw new ArgumentNullException(argumentName);
            }
        }
    }
}
