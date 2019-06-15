using System;
using System.Collections.Generic;
using System.Text;
using System.Transactions;

namespace App.Entities
{
    public class Application
    {
        public int Id { get; set; }
        public List<HouseMember> HouseMembers { get; set; }
        public int Status { get; set; }
    }
}
