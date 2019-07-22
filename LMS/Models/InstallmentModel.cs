using LMS.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace LMS.Models
{
    [NotMapped]
    public class InstallmentModel : Installment
    {
        private LMSDbContext _context;
        public List<Group> GroupList { get; set; }
        [Display(Name ="গ্রুপ")]
        [Required(ErrorMessage ="তথ্যটি পূরণ করতে হবে !")]
        public int GroupId { get; set; }
        public List<Installment> InstallmentList { get; set; }

        public InstallmentModel()
        {
            _context = new LMSDbContext();
            GroupList = _context.Groups.Where(s => s.Status == (byte)EnumStatus.Active).ToList();

        }

        public void Add()
        {
            foreach(var installment in InstallmentList.Where(s=>s.Payment!=0 && s.Submit != 0).ToList())
            {
                installment.Date = base.Date;
                _context.Installments.Add(installment);
                _context.SaveChanges();
                var member = _context.Members.Find(installment.MemberId);
                if (member != null)
                {
                    member.SavingAmount += installment.Submit??0;
                    member.DueLoan -= installment.Payment??0;
                    _context.Entry(member).State = System.Data.Entity.EntityState.Modified;
                    _context.SaveChanges();
                }
                var orgSetting = _context.OrganizationSettings.FirstOrDefault();
                if (orgSetting != null)
                {
                    orgSetting.TotalTransaction += (installment.Submit??0+installment.Profit??0+ installment.Payment??0);
                    orgSetting.TotalLoan -= installment.Payment??0;
                    orgSetting.TotalSaving += installment.Submit??0;
                    _context.Entry(orgSetting).State = EntityState.Modified;
                    _context.SaveChanges();
                }
            }
        }
    }
}