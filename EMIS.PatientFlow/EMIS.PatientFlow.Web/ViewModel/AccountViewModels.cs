using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using EMIS.PatientFlow.Entities;
using EMIS.PatientFlow.Web.Models;

namespace EMIS.PatientFlow.Web.ViewModel
{
    public class ExternalLoginConfirmationViewModel
    {
        [Required]
        [Display(Name = "User name")]
        public string UserName { get; set; }
    }

    public class ManageUserViewModel
    {
        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Current password")]
        public string OldPassword { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "New password")]
        public string NewPassword { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm new password")]
        [Compare("NewPassword", ErrorMessage = "The new password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }
    }

    public class LoginViewModel
    {
        [Required(ErrorMessage = "Username is required.")]
        [Display(Name = "User name")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Password  is required.")]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [Display(Name = "Remember me?")]
        public bool RememberMe { get; set; }
    }

    public class RegisterViewModel
    {
        [Required(ErrorMessage = "Enter a user name so they can log in")]
        [Display(Name = "User name")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Create a password using 8 characters or more.It can be any combination of letters, numbers, and symbols")]
        [StringLength(100, ErrorMessage = "Password must be 8 characters or more. It can be any combination of letters, numbers, and symbols", MinimumLength = 8)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
		[RegularExpression(@"^\S$|^\S[\s\S]*\S$", ErrorMessage = "Password can’t start or end with a blank space.")]
		public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }

        [Required(ErrorMessage = "Enter an email address for this user")]
        [DataType(DataType.EmailAddress)]
        [EmailAddress(ErrorMessage = "Please enter a valid email address")]
        public string Email { get; set; }

        public List<AuthRole> Roles { get; set; }
        public List<AuthRole> UserInRoles { get; set; }
        public List<string> SelectedRoles { get; set; }
        public List<System.Web.Mvc.SelectListItem> OrganisationList { get; set; }
		public List<System.Web.Mvc.SelectListItem> RolesList { get; set; }
		public List<int> OrganisationIds { get; set; }
		public string UserId { get; set; }
		public string SelectedRole { get; set; }
	}

    public class UpdateUserViewModel
    {
        [Required]
        [Display(Name = "User name")]
        public string UserName { get; set; }

        [Required]
        [DataType(DataType.EmailAddress)]
        [EmailAddress]
        public string Email { get; set; }
        public List<AuthRole> Roles { get; set; }
        public List<AuthRole> UserInRoles { get; set; }       
        public string SelectedRoles { get; set; }
        public List<System.Web.Mvc.SelectListItem> OrganisationList { get; set; }
        public List<int> OrganisationIds { get; set; }
		public List<System.Web.Mvc.SelectListItem> RolesList { get; set; }
		public string UserId { get; set; }
    }

    public class ManageRoleViewModel
    {
        [Required]
        [Display(Name = "Role name")]
        public string RoleName { get; set; }

        public List<AuthRole> Roles { get; set; }
    }

    public class ResetPasswordViewModel
    {
        [Required]
        [Display(Name = "User name")]
        public string UserName { get; set; }
        
        [Required]
        [StringLength(100, ErrorMessage = "Create a password using 8 characters or more.It can be any combination of letters, numbers and symbols", MinimumLength = 8)]
        [DataType(DataType.Password)]
        [Display(Name = "New password")]
        [RegularExpression(@"^\S$|^\S[\s\S]*\S$", ErrorMessage = "Password can’t start or end with a blank space.")]
        public string NewPassword { get; set; }

		[DataType(DataType.Password)]
		[Display(Name = "Confirm new password")]
		[Compare("NewPassword", ErrorMessage = "The new password and confirmation password do not match.")]
		public string ConfirmPassword { get; set; }
	}
}
