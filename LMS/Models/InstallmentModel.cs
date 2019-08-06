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
               
               
            }
        }
    }
}