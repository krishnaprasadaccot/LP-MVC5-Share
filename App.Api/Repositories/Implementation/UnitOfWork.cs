
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace App.Api.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        AppDbContext _context;
        IApplicationRepository _applicationRepository;
        IHouseMemberRepository _houseMemberRepository;
        IRelationshipRepository _relationshipRepository;
        public UnitOfWork(AppDbContext context)
        {
            this._context = context;
        }

        public IApplicationRepository ApplicationRepository
        {
            get
            {
                if (this._applicationRepository == null)
                {
                    this._applicationRepository = new ApplicationRepository(_context);
                }
                return _applicationRepository;
            }
        }
        public IHouseMemberRepository HouseMemberRepository
        {
            get
            {

                if (this._houseMemberRepository == null)
                {
                    this._houseMemberRepository = new HouseMemberRepository(_context);
                }
                return _houseMemberRepository;
            }
        }

        public IRelationshipRepository RelationshipRepository
        {
            get
            {

                if (this._relationshipRepository == null)
                {
                    this._relationshipRepository = new RelationshipRepository(_context);
                }
                return _relationshipRepository;
            }
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }

}
