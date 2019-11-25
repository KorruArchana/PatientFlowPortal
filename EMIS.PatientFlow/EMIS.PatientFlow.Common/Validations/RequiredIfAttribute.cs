using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Reflection;
using System.Linq;

namespace EMIS.PatientFlow.Common.Validations
{
    [AttributeUsageAttribute(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter, AllowMultiple = true)]
    public class RequiredIfAttribute : RequiredAttribute
    {
        private string OtherProperty { get; set; }
		private List<object> Conditions { get; set; }

		public RequiredIfAttribute(string otherProperty, object[] conditions)
		{
			OtherProperty = otherProperty;
			Conditions = conditions.ToList();
		}

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            PropertyInfo property = validationContext.ObjectType.GetProperty(OtherProperty);
            if (property == null)
                return new ValidationResult(String.Format("Property {0} not found.", OtherProperty));

            object propertyValue = property.GetValue(validationContext.ObjectInstance, null);

			return Enumerable.Contains(Conditions, propertyValue) ? base.IsValid(value, validationContext) : null;
        }
    }
}
