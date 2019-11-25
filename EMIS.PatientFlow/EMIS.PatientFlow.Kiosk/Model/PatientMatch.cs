using System.Runtime.Serialization;

namespace EMIS.PatientFlow.Kiosk.Model
{
   [DataContract]
   public class PatientMatch
   {
       [DataMember(Name = "ScreenCode")]
        public string ScreenCode { get; set; }
        [DataMember(Name = "Order")]
        public int Order { get; set; }
   }
}
