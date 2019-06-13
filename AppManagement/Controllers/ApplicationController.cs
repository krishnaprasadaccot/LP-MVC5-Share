using AppManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AppManagement.Controllers
{
    public class ApplicationController : BaseController
    {
        // GET: Application
        public ActionResult Index()
        {
            var members = this.GetSession<List<HouseMemberModel>>(CONSTANTS.SessionKeys.HOUSE_MEMBERS) ?? new List<HouseMemberModel>() { new HouseMemberModel() { isHeadMember=true} };
            this.SetSession(CONSTANTS.SessionKeys.HOUSE_MEMBERS, members);
            return View(new ApplicationViewModel() { HouseMembers= members ,Id=1});
        }

        public ActionResult AddMember(ApplicationViewModel application) {
            if (ModelState.IsValid)
            {
                int.TryParse(Request.QueryString["id"], out int id);
                application.HouseMembers = application.HouseMembers ?? new List<HouseMemberModel>();
                application.HouseMembers.Add(new HouseMemberModel() { Id = id });
                this.SetSession(CONSTANTS.SessionKeys.HOUSE_MEMBERS, application.HouseMembers);
            }
            return View("Index", application);
        }
        public ActionResult RemoveMember(ApplicationViewModel application)
        {
            int.TryParse(Request.QueryString["id"], out int id);
            application.HouseMembers = application.HouseMembers?.Where(m => m.Id != id).ToList();
            this.SetSession(CONSTANTS.SessionKeys.HOUSE_MEMBERS, application.HouseMembers);
            ModelState.Clear();
            return View("Index",application);
        }

        public ActionResult Save(ApplicationViewModel application,bool isNext=false)
        {
            
            if(!ModelState.IsValid)
            {
                return View("Index", application);
            }
            else
            {
                bool isAgeValid = true;
                application.HouseMembers.Select((m, i) => new { item = m, index = i }).ToList().ForEach(m =>
                {
                    if (!m.item.isHeadMember)
                    {
                        if (m.item.DateOfBirth < application.HouseMembers.Where(h => h.isHeadMember).FirstOrDefault().DateOfBirth)
                        {
                            ModelState.AddModelError($"HouseMembers[{m.index}].DateOfBirth", "Age should be less than Primary member");
                            isAgeValid = false;
                        }
                    }
                });
                return isAgeValid ? View("Index", "Home"): View("Index", application);
            }
        }

        public ActionResult Relationship(int id)
        {
            var members = this.GetSession<List<HouseMemberModel>>(CONSTANTS.SessionKeys.HOUSE_MEMBERS) ?? new List<HouseMemberModel>();
            return View(new ApplicationViewModel() { HouseMembers = members, Id = id });

        }

    }
}