using LMS.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace LMS.Models
{
    [NotMapped]
    public class GroupModel : Group
    {
        private LMSDbContext _context;
        public GroupModel()
        {
            _context = new LMSDbContext();
        }
        public List<Group> GetAllGroup()
        {
            return _context.Groups.Where(s => s.Status == (byte)EnumStatus.Active).ToList();
        }
    }
}