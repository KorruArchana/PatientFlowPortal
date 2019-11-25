using System;
using System.Collections.Generic;
using EMIS.PatientFlow.Common.Interfaces;
using EMIS.PatientFlow.Common.Logging;

namespace EMIS.PatientFlow.Web.Resolver
{
    public class DiResolver
    {
        readonly Dictionary<Type, Type> _mapping = new Dictionary<Type, Type>();

        static DiResolver _resolver;

        private DiResolver()
        {
             _mapping.Add(typeof(ILogger), typeof(FileLogger));
        }

        public static DiResolver CurrentInstance
        {
            get
            {
                return _resolver ?? (_resolver = new DiResolver());
            }
        }

        public T Reslove<T>()
        {
            try
            {
                var resloveType = _mapping[typeof(T)];

                return (T)Activator.CreateInstance(resloveType);
            }
            catch (Exception)
            {
                throw new Exception(String.Format("Could not Find type {0}", typeof(T)));
            }
        }

        public T Reslove<T>(object[] parameter)
        {
            try
            {
                var resloveType = _mapping[typeof(T)];

                return (T)Activator.CreateInstance(resloveType, parameter);
            }
            catch (Exception)
            {
                throw new Exception(String.Format("Could not Find type {0}", typeof(T)));
            }
        }
    }
}
