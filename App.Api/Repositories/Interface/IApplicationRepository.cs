using App.Entities;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace App.Api.Repositories
{
    public interface IApplicationRepository : IDisposable
    {
        IEnumerable<Application> GetApplications();
        Application GetApplicationById(int id);
        void SaveApplication(Application application);
        void Save();
    }
}
