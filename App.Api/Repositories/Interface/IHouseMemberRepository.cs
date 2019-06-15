using App.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace App.Api.Repositories
{
    public interface IHouseMemberRepository : IDisposable
    {
        IEnumerable<HouseMember> GetHouseMembers(int applicationId);
        HouseMember GetHouseMemberById(int id);
        void SaveHouseMember(HouseMember member);
        void Save();
    }
}
