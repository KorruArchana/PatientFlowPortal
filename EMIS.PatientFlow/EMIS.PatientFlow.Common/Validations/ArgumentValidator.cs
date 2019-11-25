using System;

namespace EMIS.PatientFlow.Common.Validations
{
    public static class ArgumentValidator
    {
        public static void IsNull(object argument, string argumentName)
        {
            if (argument == null)
            {
                throw new ArgumentNullException(argumentName);
            }
        }

        public static void IsNullOrEmpty(string argument, string argumentName)
        {
            if (string.IsNullOrEmpty(argument))
            {
                throw new ArgumentNullException(argumentName);
            }
        }

        public static void IsNegativeOrZero(int argument, string argumentName)
        {
            if (argument <= 0)
            {
                throw new ArgumentNullException(argumentName);
            }
        }

        public static void IsNegativeOrZero(long argument, string argumentName)
        {
            if (argument <= 0L)
            {
                throw new ArgumentNullException(argumentName);
            }
        }

        public static void IsNegativeOrZero(decimal argument, string argumentName)
        {
            if (argument <= 0m)
            {
                throw new ArgumentNullException(argumentName);
            }
        }

        public static void IsNegativeOrZero(double argument, string argumentName)
        {
            if (argument <= 0d)
            {
                throw new ArgumentNullException(argumentName);
            }
        }
    }
}
