using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using App.Api.Repositories;
using App.Entities;
using System.Collections;

namespace App.Api.Controllers
{
    public class ApplicationController : BaseController
    {
        
        public ApplicationController(IUnitOfWork unitOfWork):base(unitOfWork)
        {
        }

        
        [HttpGet]
        public IEnumerable<Application> Get()
        {
            try
            {
                return _unitOfWork.ApplicationRepository.GetApplications();
            }
            catch (Exception)
            {

                throw;
            }
            
        }

        
        [HttpGet("{id}")]
        public Application Get(int id)
        {
            try
            {
                var app = _unitOfWork.ApplicationRepository.GetApplicationById(id);
                app.HouseMembers = _unitOfWork.HouseMemberRepository.GetHouseMembers(id).ToList();
                if(app.HouseMembers != null)
                    {
                    app.HouseMembers.ForEach(m =>
                    {
                        m.Relationships = _unitOfWork.RelationshipRepository.GetRelationships(m.Id).ToList();
                    });
                }
                return app;
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
                return _unitOfWork.ApplicationRepository.Search(search);
            }
            catch (Exception)
            {

                throw;
            }
        }
        
        public int Save([FromBody]Application application)
        {
            try
            {
                _unitOfWork.ApplicationRepository.SaveApplication(application);
                _unitOfWork.Save();
                if(application.HouseMembers !=null)
                    application.HouseMembers.ToList().ForEach(m =>
                    {
                        m.ApplicationId = application.Id;
                        _unitOfWork.HouseMemberRepository.SaveHouseMember(m);
                        _unitOfWork.Save();
                    });
                _unitOfWork.Save();
                return application.Id;
            }
            catch (Exception)
            {

                throw;
            }
            
        }

        
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/<controller>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
