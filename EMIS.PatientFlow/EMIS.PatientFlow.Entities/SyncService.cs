using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EMIS.PatientFlow.Entities
{
    public class SyncService : Entity
    {
        [Required]
        public Guid ProductKey { get; set; }
        public int OrganisationId { get; set; }
        public string OrganisationName { get; set; }
        public List<int> OrganisationIds { get; set; }
		public int KioskId { get; set; }
		public string KioskName { get; set; }
      
        [Display(Name = "Is Active")]
        public bool IsActive { get; set; }
        public bool IsConnected { get; set; }
    }
}
