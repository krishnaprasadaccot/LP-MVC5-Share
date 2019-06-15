using System;
using System.Collections.Generic;
using System.Text;
using App.Entities;
using Microsoft.EntityFrameworkCore;

namespace App.Api.Repositories
{
    public class ApplicationRepository : IApplicationRepository
    {
        AppDbContext _context;
        public ApplicationRepository(AppDbContext context)
        {
            this._context = context;
        }

        public Application GetApplicationById(int id)
        {
            try
            {
                return _context.Applications.Find(id);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public IEnumerable<Application> GetApplications()
        {
            try
            {
                return _context.Applications;
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

        public void SaveApplication(Application application)
        {
            try
            {
                if (application.Id <= 0)
                    _context.Applications.Add(application);
                else
                    _context.Entry(application).State = EntityState.Modified;
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
