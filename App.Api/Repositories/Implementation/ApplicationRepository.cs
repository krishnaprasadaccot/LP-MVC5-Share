using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using App.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Linq.Expressions;

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

        public IEnumerable<Application> Search(Application search)
        {
            try
            {

                StringBuilder queryBuilder = new StringBuilder();

                if (search.Id > 0)
                    queryBuilder.Append($"a.Id={search.Id}");

                if(search.HouseMembers != null)
                {
                    if(search.HouseMembers.Count>0)
                    {
                        if(!string.IsNullOrEmpty(search.HouseMembers[0].FirstName))
                        {
                            queryBuilder.Append($"{(string.IsNullOrEmpty(queryBuilder.ToString())?string.Empty:" OR ")} h.FirstName LIKE '%{search.HouseMembers[0].FirstName}%'");
                        }
                        if (!string.IsNullOrEmpty(search.HouseMembers[0].LastName))
                        {
                            queryBuilder.Append($"{(string.IsNullOrEmpty(queryBuilder.ToString()) ? string.Empty : " OR ")} h.LastName LIKE '%{search.HouseMembers[0].LastName}%'");
                        }
                        if (!string.IsNullOrEmpty(search.HouseMembers[0].DateOfBirth.ToString()))
                        {
                            queryBuilder.Append($"{(string.IsNullOrEmpty(queryBuilder.ToString()) ? string.Empty : " OR ")} CONVERT(VARCHAR(10),h.DateOfBirth,101) = '{search.HouseMembers[0].DateOfBirth.Value.ToString("MM/dd/yyyy")}'");
                        }
                    }
                }

                if (search.Status > 0)
                {
                    queryBuilder.Append($"{(string.IsNullOrEmpty(queryBuilder.ToString()) ? string.Empty : " OR ")} a.Status= {search.Status}");
                }

                var query = $@"SELECT DISTINCT a.Id, a.Status, a.CreatedBy, a.CreatedOn FROM Applications a JOIN HouseMembers h ON a.Id = h.ApplicationId WHERE {queryBuilder.ToString()} ";
                return !string.IsNullOrEmpty(queryBuilder.ToString()) ?_context.Applications.FromSql(query).ToList() : new List<Application>();
                

                
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
