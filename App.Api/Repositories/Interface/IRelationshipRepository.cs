using App.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace App.Api.Repositories
{
    public interface IRelationshipRepository:IDisposable
    {
        IEnumerable<HouseMemebrRelationship> GetRelationships(int memberId);
        HouseMemebrRelationship GetRelationshipById(int id);
        void SaveRelationship(HouseMemebrRelationship relationship);
        void Save();
    }
}
