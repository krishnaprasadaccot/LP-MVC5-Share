using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using App.Entities;

namespace App.Web.Models
{
    public class SearchViewModel
    {
        public int? ApplicationId { get; set; } = null;
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime? DateOfBirth { get; set; } = null;
        public int ApplicationStatus { get; set; }

        public List<Application> SearchResults { get; set; }
    }
}