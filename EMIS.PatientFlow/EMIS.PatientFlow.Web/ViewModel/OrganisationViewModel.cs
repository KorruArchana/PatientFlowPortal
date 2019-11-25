using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using EMIS.PatientFlow.Common.Validations;
using EMIS.PatientFlow.Entities;
using EMIS.PatientFlow.Entities.Enums;
using System.Web.Mvc;
using Newtonsoft.Json;
using System;

namespace EMIS.PatientFlow.Web.ViewModel
{
    public class OrganisationViewModel
    {  
        public OrganisationViewModel()
        {
            IsRefresh="false";
            IsChanged = "false";

        }
        public int Id { get; set; }

        [Required(ErrorMessage = "Enter the name of the organisation")]
        public string OrganisationName { get; set; }

        public int SystemTypeId { get; set; }
        public string SystemType { get; set; }

        [Required(ErrorMessage = "Enter the EMIS CDB or site number")]
        public string SiteNumber { get; set; }

        public List<Entity> SystemTypeList { get; set; }
        public List<Department> DepartmentList { get; set; }
        public List<Kiosk> KioskList { get; set; }

        [Required( ErrorMessage = "Enter the username")]
        public string Username { get; set; }

		[Required(ErrorMessage = "Enter the password")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        public string SupplierId { get; set; }

        [Required(ErrorMessage = "Enter the IP address")]
		public string IpAddress { get; set; }

		public string InternalIpAddress { get; set; }

		public int OrganisationId { get; set; }

		[Required(ErrorMessage = "Enter the Database name")]
		public string OrganisationKey { get; set; }

        public List<Site> SiteList { get; set; }

		public List<AppointmentSlotType> SlotsList { get; set; }

		public string IsRefresh { get; set; }

        public string IsChanged { get; set; }

		[RequiredIf("SystemTypeId", new object[]{Constants.Topas}, ErrorMessage = "WebServiceUrl is required")]
		public string WebServiceUrl { get; set; }

		public List<SelectListItem> SystemTypesList { get; set; }

        public List<string> IpAddresses { get; set; }
        public DateTime ModifiedDate { get; set; }

        public string LastUpdatedDate
        {
            get
            {
                return ModifiedDate.ToString("dd-MM-yyyy HH:mm");
            }
        }
        public int LinkCount { get; set; }
	}

    public class OrganisationListViewModel
    {
        public List<Organisation> OrganisationList { get; set; }
        public int draw { get; set; }
        public int recordsTotal { get; set; }
        public int recordsFiltered { get; set; }
        public Organisation[] data { get; set; }
    }
}