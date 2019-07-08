using LMS.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace LMS.Models
{
    [NotMapped]
    public class LoanModel:Loan
    {
        private LMSDbContext _context;
        public List<Member> MemberList { get; set; }

        public LoanModel()
        {
            _context = new LMSDbContext();
            MemberList = _context.Members.Where(s => s.Status == (byte)EnumStatus.Active).ToList();
        }

    }
}