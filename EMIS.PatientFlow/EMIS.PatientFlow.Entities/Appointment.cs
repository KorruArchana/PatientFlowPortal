using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMIS.PatientFlow.SyncService.Data
{
    public class Appointment
    {
        [Required]
        public Patient BookedPatient { get; set; }
        [Required]
        public Member SessionHolder { get; set; }
        [Required]
        public DateTime AppointmentTime { get; set; }
        public long ID { get; set; }
    }
}
