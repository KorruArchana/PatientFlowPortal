using System.Runtime.Serialization;

namespace EMIS.PatientFlow.Kiosk.Model
{
    [DataContract]
    public class KioskStatus
    {
        [DataMember(Name = "Status")]
        public int Status { get; set; }
    }
}