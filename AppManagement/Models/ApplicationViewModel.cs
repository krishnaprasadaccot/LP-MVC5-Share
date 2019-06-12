


using System.Collections.Generic;

namespace AppManagement.Models
{
    public class ApplicationViewModel
    {
        public int Id { get; set; }
        public IEnumerable<HouseMemberModel> HouseMembers { get; set; }

    }
}