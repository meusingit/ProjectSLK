using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace WEBSITESLK.Models
{
    [MetadataType(typeof(UserMetaData))]
    public partial class Table
    {
        public string ConfirmPassword { get; set; }
    }
    public class UserMetaData
    {
        [Display(Name = "First Name")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "First Name Required")]
        public string FirstName { get; set; }
        [Display(Name = "Last Name")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Last Name Required")]
        public string LastName { get; set; }
        [Display(Name = "Email ID")]
        [Required(AllowEmptyStrings = true, ErrorMessage = "Email ID Required")]
        [DataType(DataType.EmailAddress)]
        public string EmailID { get; set; }
        [Display(Name = "Date Of Birth")]
        [DataType(DataType.Date)]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Required DOB")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/DD/YYYY}")]
        public DateTime DateOfBirth { get; set; }

        [Display(Name = "Password")]
        [DataType(DataType.Password)]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Password Required")]
        [MinLength(6, ErrorMessage = "Password Must Be Atleast 6 Characters")]
        public string Password { get; set; }

        [Display(Name = "Confirm Password")]
        [DataType(DataType.Password)]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Enter The Same Password")]
        [Compare("Password", ErrorMessage = "Password Did Not Match")]
        public string ConfirmPassword { get; set; }

    }
}