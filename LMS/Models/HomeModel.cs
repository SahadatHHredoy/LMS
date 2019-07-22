using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using LMS.Entities;

namespace LMS.Models
{
    public class HomeModel
    {
        private LMSDbContext _context;
        public OrganizationSetting OrganizationSetting { get; set; }
        public int TotalMember { get; set; }
        public HomeModel()
        {
            _context = new LMSDbContext();
            OrganizationSetting = _context.OrganizationSettings.FirstOrDefault();
            TotalMember = _context.Members.Count();
        }
    }
}