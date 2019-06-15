using System;
using System.Collections.Generic;

namespace App.Entities
{
    public class HouseMember
    {
        public int Id { get; set; }

        public int ApplicationId { get; set; }

        public virtual string FirstName { get; set; }

        
        public string MiddleName { get; set; }

        
        public virtual string LastName { get; set; }

        
        public string Suffix { get; set; }

        
        public virtual DateTime? DateOfBirth { get; set; } = null;

        
        public virtual string Gender { get; set; }

        public List<HouseMemebrRelationship> Relationships { get; set; }

        public bool IsHeadMember { get; set; }
    }
}