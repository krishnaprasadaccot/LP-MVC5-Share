using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using App.Entities;
using App.Web.Models;
using AutoMapper;

namespace App.Web
{
    public class MapperProfile:Profile
    {
        public MapperProfile()
        {
            CreateMap(typeof(ApplicationViewModel), typeof(Application));
            CreateMap(typeof(HouseMemberModel), typeof(HouseMember));
            CreateMap(typeof(RelationshipModel), typeof(HouseMemebrRelationship));
            CreateMap(typeof(Application), typeof(ApplicationViewModel));
            CreateMap(typeof(HouseMember), typeof(HouseMemberModel));
            CreateMap(typeof(HouseMemebrRelationship), typeof(RelationshipModel));
        }
    }
}