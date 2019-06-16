using App.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace App.Api.Repositories
{
    public class HouseMemberRepository:IHouseMemberRepository
    {
        AppDbContext _context;
        public HouseMemberRepository(AppDbContext context)
        {
            this._context = context;
        }

        public HouseMember GetHouseMemberById(int id)
        {
            try
            {
                return _context.HouseMembers.Find(id);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public IEnumerable<HouseMember> GetHouseMembers(int applicationId)
        {
            try
            {
                return _context.HouseMembers.Where(m => m.ApplicationId == applicationId).AsEnumerable();
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

        public void SaveHouseMember(HouseMember member)
        {
            try
            {
                if (member.Id <= 0)
                    _context.HouseMembers.Add(member);
                else
                    _context.Entry(member).State = EntityState.Modified;
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
