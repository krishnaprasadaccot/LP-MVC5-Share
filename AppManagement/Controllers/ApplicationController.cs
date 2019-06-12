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
            var memebrs = new List<HouseMemberModel>() { new HouseMemberModel { Id = 0, FirstName = "First Name", LastName = "Last Name", DateOfBirth = DateTime.Now,Gender="Male", Suffix="Mr" } };
            return View(new ApplicationViewModel() { HouseMembers=memebrs});
        }
    }
}