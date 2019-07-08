using LMS.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace LMS.Models
{
    [NotMapped]
    public class MemberModel:Member
    {
        public List<Group> GroupList { get; set; }

        public HttpPostedFileBase File { get; set; }
        private LMSDbContext _context;
        public MemberModel()
        {
            _context = new LMSDbContext();
            GroupList = _context.Groups.Where(s=>s.Status==(byte)EnumStatus.Active).ToList();
        }
    }
}