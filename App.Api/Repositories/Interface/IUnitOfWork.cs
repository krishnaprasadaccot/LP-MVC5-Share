using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace App.Api.Repositories
{
    public interface IUnitOfWork : IDisposable
    {
        IApplicationRepository ApplicationRepository { get; }
        IHouseMemberRepository HouseMemberRepository { get; }
        IRelationshipRepository RelationshipRepository { get; }
        void Save();
    }
}
