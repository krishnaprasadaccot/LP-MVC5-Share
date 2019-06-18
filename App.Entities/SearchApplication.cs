using System;
using System.Collections.Generic;
using System.Text;

namespace App.Entities
{
    public class SearchApplication
    {
        public int ApplicationId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public int ApplicationStatus { get; set; }
    }
}
