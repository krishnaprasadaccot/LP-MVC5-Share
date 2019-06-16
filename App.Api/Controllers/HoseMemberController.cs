using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using App.Api.Repositories;
using App.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace App.Api.Controllers
{
    [ApiController]
    public class HoseMemberController :BaseController
    {
        public HoseMemberController(IUnitOfWork unitOfWork) :base(unitOfWork)
        {

        }
        public void SaveRelationships([FromBody]IEnumerable<HouseMember> houseMembers)
        {
            try
            {
                if (houseMembers != null)
                    houseMembers.ToList().ForEach(m =>
                    {
                        if(m.Relationships !=null)
                        {
                            m.Relationships.ForEach(r =>
                            {
                                _unitOfWork.RelationshipRepository.SaveRelationship(r);
                            });
                        }
                    });
                _unitOfWork.Save();
            }
            catch (Exception)
            {

                throw;
            }

        }
    }
}