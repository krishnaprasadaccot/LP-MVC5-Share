using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using App.Entities;
using Microsoft.EntityFrameworkCore;

namespace App.Api.Repositories
{
    public class RelationshipRepository : IRelationshipRepository
    {
        AppDbContext _context;
        public RelationshipRepository(AppDbContext context)
        {
            this._context = context;
        }

        public HouseMemebrRelationship GetRelationshipById(int id)
        {
            try
            {
                return _context.Relationships.Find(id);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public IEnumerable<HouseMemebrRelationship> GetRelationships(int memberId)
        {
            try
            {
                return _context.Relationships.Where(r => r.MemberId == memberId).ToList();
            }
            catch (Exception)
            {

                throw;
            }
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        public void SaveRelationship(HouseMemebrRelationship relationship)
        {
            try
            {
                if (relationship.Id <= 0)
                    _context.Relationships.Add(relationship);
                else
                    _context.Entry(relationship).State = EntityState.Modified;
            }
            catch (Exception)
            {

                throw;
            }
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
