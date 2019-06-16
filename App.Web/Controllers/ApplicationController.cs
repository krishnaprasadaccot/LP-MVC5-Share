using App.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AutoMapper;
using App.Entities;
using Newtonsoft.Json;

namespace App.Web.Controllers
{
    public class ApplicationController : BaseController
    {
        public ActionResult Index()
        {
            var application  = this.GetSession<ApplicationViewModel>(CONSTANTS.SessionKeys.ACTIVE_APPLICATION) ?? new ApplicationViewModel() { HouseMembers = new List<HouseMemberModel>() { new HouseMemberModel() { IsHeadMember = true } } };
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
                    if (!m.item.IsHeadMember)
                    {
                        if (m.item.DateOfBirth < application.HouseMembers.Where(h => h.IsHeadMember).FirstOrDefault().DateOfBirth)
                        {
                            ModelState.AddModelError($"HouseMembers[{m.index}].DateOfBirth", "Age should be less than Primary member");
                            isAgeValid = false;
                        }
                    }
                });
                this.SetSession(CONSTANTS.SessionKeys.ACTIVE_APPLICATION, application);
                if(isAgeValid)
                {
                    application.Status = 1;
                    application.HouseMembers.ToList().ForEach(m =>
                    {
                        if (m.Id < 0)
                            m.Id = 0;
                    });
                    Application postData = Mapper.Map<ApplicationViewModel, Application>(application);
                    var response = this.ApiPost<Application>(CONSTANTS.ApiUrls.BASE_ADDRESS, CONSTANTS.ApiUrls.APPLICATION_SAVE, postData);
                    if (response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        int.TryParse(response.Content.ReadAsStringAsync().Result,out int newAppId);
                        postData = ApiGet<Application>(CONSTANTS.ApiUrls.BASE_ADDRESS, string.Format(CONSTANTS.ApiUrls.APPLICATION_GET, newAppId));
                        this.SetSession(CONSTANTS.SessionKeys.ACTIVE_APPLICATION, Mapper.Map<Application, ApplicationViewModel>(postData));
                        if (isNext)
                            return RedirectToAction("Relationship", new { id = postData.HouseMembers.ToList().Find(m => m.IsHeadMember).Id });
                        else
                            return RedirectToAction("Index","Home");
                    }
                    else
                    {
                        ViewBag.Error = $"Application failed to save with error: {response.Content.ToString()}";
                        return View("Index", application);
                    }
                }
                else 
                    return View("Index", application);
            }
        }

        public ActionResult Relationship(int id)
        {
            var application = this.GetSession<ApplicationViewModel>(CONSTANTS.SessionKeys.ACTIVE_APPLICATION);
            
            var selectedMember = application.HouseMembers.ToList().Find(m => m.Id == id);
            if(selectedMember.Relationships == null || selectedMember.Relationships.Count <=0)
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
            if(application.HouseMembers.Count >1)
                for (int index = 0; index < application.HouseMembers.Count; index++)
                {
                    var item = application.HouseMembers[index];
                    if (item.Relationships == null || item.Relationships.Count <=0)
                    {
                        sessionApp.HouseMembers.ToList().Find(s => s.Id == item.Id).Relationships = application.HouseMembers.ToList().FindAll(r => r.Id != item.Id).Select(rl => new RelationshipModel { MemberId = item.Id, RelativeId = rl.Id }).ToList();
                        ModelState.AddModelError($"HouseMembers[{index}].Relationships[0].Relationship", "Relationship is required");
                        sessionApp.ActiveMemberId = item.Id;
                        this.SetSession(CONSTANTS.SessionKeys.ACTIVE_APPLICATION, sessionApp);
                        return View("Relationship", sessionApp);
                    }
                }

            
            var response = this.ApiPost<IEnumerable<HouseMember>>(CONSTANTS.ApiUrls.BASE_ADDRESS, CONSTANTS.ApiUrls.RELATIONSHIP_SAVE, Mapper.Map<IEnumerable<HouseMemberModel>, IEnumerable<HouseMember>>(sessionApp.HouseMembers));
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                var savedApp = ApiGet<Application>(CONSTANTS.ApiUrls.BASE_ADDRESS, string.Format(CONSTANTS.ApiUrls.APPLICATION_GET, sessionApp.Id));
                this.SetSession(CONSTANTS.SessionKeys.ACTIVE_APPLICATION, Mapper.Map<Application, ApplicationViewModel>(savedApp));

                return View("Confirmation");
            }
            else
            {
                ViewBag.Error = $"Application failed to save with error: {response.Content.ToString()}";
                return View("Relationship", sessionApp);
            }
        }
    }
}