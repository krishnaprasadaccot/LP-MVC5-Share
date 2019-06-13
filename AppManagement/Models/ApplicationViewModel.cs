


using System.Collections;
using System.Collections.Generic;

namespace AppManagement.Models
{
    public class ApplicationViewModel
    {
        public int Id { get; set; }
        public IList<HouseMemberModel> HouseMembers { get; set; }

    }
}