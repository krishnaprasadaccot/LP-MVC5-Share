


using System.Collections;
using System.Collections.Generic;

namespace App.Web.Models
{
    public class ApplicationViewModel
    {
        public int Id { get; set; }
        public IList<HouseMemberModel> HouseMembers { get; set; }
        public int ActiveMemberId { get; set; }
        public int Status { get; set; }
    }
}