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
            var application  = this.GetSession<ApplicationViewModel>(CONSTANTS.SessionKeys.ACTIVE_APPLICATION) ?? new ApplicationViewModel() { HouseMembers = new List<HouseMemberModel>() { new HouseMemberModel() { isHeadMember = true } } };
            this.SetSession(CONSTANTS.SessionKeys.ACTIVE_APPLICATION, application);
            return View(application);
        }

        public ActionResult AddMember(ApplicationViewModel application)
        {
            if (ModelState.IsValid)
            {
                int.TryParse(Request.QueryString["id"], out int id);
                application.HouseMembers = application.HouseMembers ?? new List<HouseMemberModel>();
                application.HouseMembers.Add(new HouseMemberModel() { Id = id });
                this.SetSession(CONSTANTS.SessionKeys.ACTIVE_APPLICATION, application);
            }
            return View("Index", application);
        }
        public ActionResult RemoveMember(ApplicationViewModel application)
        {
            int.TryParse(Request.QueryString["id"], out int id);
            application.HouseMembers = application.HouseMembers?.Where(m => m.Id != id).ToList();
            this.SetSession(CONSTANTS.SessionKeys.ACTIVE_APPLICATION, application);
            ModelState.Clear();
            return View("Index", application);
        }

        public ActionResult Save(ApplicationViewModel application)
        {
            int.TryParse(Request.QueryString["id"], out int id);
            bool.TryParse(Request.QueryString["isNext"], out bool isNext);
            if (!ModelState.IsValid)
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
                this.SetSession(CONSTANTS.SessionKeys.ACTIVE_APPLICATION, application);
                if(isAgeValid)
                {
                    if (isNext)
                        return RedirectToAction("Relationship", new { id = application.HouseMembers.ToList().Find(m=>m.isHeadMember).Id });
                    else
                        return View("Index", "Home");
                }
                else 
                    return View("Index", application);
            }
        }

        public ActionResult Relationship(int id)
        {
            var application = this.GetSession<ApplicationViewModel>(CONSTANTS.SessionKeys.ACTIVE_APPLICATION);
            
            var selectedMember = application.HouseMembers.ToList().Find(m => m.Id == id);
            if(selectedMember.Relationships == null )
                selectedMember.Relationships = application.HouseMembers.ToList().FindAll(r => r.Id != id).Select(rl => new RelationshipModel { MemberId = selectedMember.Id, RelativeId = rl.Id }).ToList();
            application.ActiveMemberId = id;
            this.SetSession(CONSTANTS.SessionKeys.ACTIVE_APPLICATION, application);
            return View(application);

        }
        [HttpPost]
        public ActionResult Relationship(ApplicationViewModel application)
        {
            
            var sessionApp = this.GetSession<ApplicationViewModel>(CONSTANTS.SessionKeys.ACTIVE_APPLICATION);
            application.HouseMembers.ToList().ForEach(m =>
            {
                if (m.Relationships != null)
                {
                    sessionApp.HouseMembers.ToList().Find(x => x.Id == m.Id).Relationships = m.Relationships;
                }
            });
            application.ActiveMemberId = application.ActiveMemberId;
            this.SetSession(CONSTANTS.SessionKeys.ACTIVE_APPLICATION, application);
            int.TryParse(Request.QueryString["id"], out int id);
            if (!ModelState.IsValid)
            {
                return View("Relationship", sessionApp);
            }
            bool.TryParse(Request.QueryString["isSubmit"], out bool isSubmit);
            return RedirectToAction("Relationship", new { id });
        }

        public ActionResult Submit(ApplicationViewModel application) {
            var sessionApp = this.GetSession<ApplicationViewModel>(CONSTANTS.SessionKeys.ACTIVE_APPLICATION);
            application.HouseMembers.ToList().ForEach(m =>
            {
                if (m.Relationships != null)
                {
                    sessionApp.HouseMembers.ToList().Find(x => x.Id == m.Id).Relationships = m.Relationships;
                }
            });
            this.SetSession(CONSTANTS.SessionKeys.ACTIVE_APPLICATION, application);
            if (!ModelState.IsValid)
            {
                return View("Relationship", sessionApp);
            }
            for (int index = 0; index < application.HouseMembers.Count; index++)
            {
                var item = application.HouseMembers[index];
                if (item.Relationships == null)
                {
                    sessionApp.HouseMembers.ToList().Find(s => s.Id == item.Id).Relationships = application.HouseMembers.ToList().FindAll(r => r.Id != item.Id).Select(rl => new RelationshipModel { MemberId = item.Id, RelativeId = rl.Id }).ToList();
                    ModelState.AddModelError($"HouseMembers[{index}].Relationships[0].Relationship", "Relationship is required");
                    sessionApp.ActiveMemberId = item.Id;
                    this.SetSession(CONSTANTS.SessionKeys.ACTIVE_APPLICATION, sessionApp);
                    return View("Relationship", sessionApp);
                }
            }
            return View("Confirmation");
        }
    }
}