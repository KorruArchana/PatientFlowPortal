using System.Collections.Generic;
using System.Runtime.Serialization;

namespace EMIS.PatientFlow.Kiosk.Model
{
   [DataContract]
   public class PatientMatchOrder
    {
       [DataMember(Name = "PatientMatch")]
       public List<PatientMatch> PatientMatchOrderList { get; set; }
    }
}
