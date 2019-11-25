using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EMIS.PatientFlow.Entities
{
    public class Organisation : Entity
    {
        [Required(ErrorMessage = "Enter OrganisationName")]
        public string OrganisationName { get; set; }
        public string OrganisationKey { get; set; }
		[Required(ErrorMessage = "Enter SystemType")]
        public int SystemTypeId { get; set; }
        public string SystemType { get; set; }
        public string SiteNumber { get; set; }
        public DateTime ModifiedDate { get; set; }
		public List<Entity> SystemTypeList { get; set; }
        public List<Department> DepartmentList { get; set; }
        public List<Kiosk> KioskList { get; set; }
        public string DatabaseName { get; set; }
        public WebUser User { get; set; }
        public int LinkCount { get; set; }
        public long? SiteId { get; set; }
        public List<Site> SiteList { get; set; }
		public bool MainLocation { get; set; }
		public string SiteName { get; set; }
    }
}
