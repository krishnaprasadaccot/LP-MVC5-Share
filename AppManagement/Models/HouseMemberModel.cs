using System;
using System.ComponentModel.DataAnnotations;

namespace AppManagement.Models
{
    public class HouseMemberModel
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "First Name")]
        [MaxLength(32,ErrorMessage ="Maximum 32 characters only allowed for this field")]
        public string FirstName { get; set; }

        [Display(Name = "M.I.")]
        [MaxLength(32, ErrorMessage = "Maximum 32 characters only allowed for this field")]
        public string MiddleName { get; set; }

        [Required]
        [Display(Name = "Last Name")]
        [MaxLength(32, ErrorMessage = "Maximum 32 characters only allowed for this field")]
        public string LastName { get; set; }

        [Display(Name = "Suffix")]
        public string Suffix { get; set; }

        [Required(ErrorMessage ="Date of Birth is required")]
        [CustomDateRange(minRange:-125,ErrorMessage ="Maximum allowed Age is 125")]
        [DataType(DataType.DateTime, ErrorMessage = "Valid Date of Birth is required")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/dd/yyyy}")]
        [Display(Name = "Date of Birth(mm/dd/yyyy)")]
        public DateTime? DateOfBirth { get; set; } = null;

        [Required]
        [Display(Name = "Gender")]
        public string Gender { get; set; }

        public string Relationship { get; set; }

        public bool isHeadMember { get; set; }
    }
}