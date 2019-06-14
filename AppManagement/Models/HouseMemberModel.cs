using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AppManagement.Models
{
    public class HouseMemberModel
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "First Name")]
        [MaxLength(32,ErrorMessage ="Maximum 32 characters only allowed for this field")]
        public virtual string FirstName { get; set; }

        [Display(Name = "M.I.")]
        [MaxLength(32, ErrorMessage = "Maximum 32 characters only allowed for this field")]
        public string MiddleName { get; set; }

        [Required]
        [Display(Name = "Last Name")]
        [MaxLength(32, ErrorMessage = "Maximum 32 characters only allowed for this field")]
        public virtual string LastName { get; set; }

        [Display(Name = "Suffix")]
        public string Suffix { get; set; }

        [Required(ErrorMessage ="Date of Birth is required")]
        [CustomDateRange(minRange:-125,ErrorMessage ="Maximum allowed Age is 125")]
        [DataType(DataType.DateTime, ErrorMessage = "Valid Date of Birth is required")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/dd/yyyy}")]
        [Display(Name = "Date of Birth(mm/dd/yyyy)")]
        public virtual DateTime? DateOfBirth { get; set; } = null;

        [Required]
        [Display(Name = "Gender")]
        public virtual string Gender { get; set; }

        public List<RelationshipModel> Relationships { get; set; }

        public bool isHeadMember { get; set; }
    }
}