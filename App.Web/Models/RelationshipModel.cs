using System;
using System.ComponentModel.DataAnnotations;

namespace App.Web.Models
{
    public class RelationshipModel
    {
        public int Id { get; set; }
        public int MemberId { get; set; }
        public int RelativeId { get; set; }
        [Required]
        public string Relationship { get; set; }
    }
}