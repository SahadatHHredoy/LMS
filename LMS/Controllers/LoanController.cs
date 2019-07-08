using LMS.Entities;
using LMS.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LMS.Controllers
{
    public class LoanController : Controller
    {
        private readonly LMSDbContext _context;
        // GET: Loan
        public LoanController()
        {
            _context = new LMSDbContext();
        }
        public ActionResult Index()
        {
            return View(_context.Loans.ToList());
        }
        public ActionResult Add()
        {
            return View(new LoanModel());
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Add(LoanModel model)
        {
            if (ModelState.IsValid)
            {
                _context.Loans.Add(new Loan
                {
                    Date = model.Date,
                    MemberId = model.MemberId,
                    ProjectName = model.ProjectName,
                    TotalInstallment = model.TotalInstallment,
                    InstallmentType = model.InstallmentType,
                    FirstInstallment = model.FirstInstallment,
                    LoanAmount= model.LoanAmount,
                    ProfitAmount = model.ProfitAmount,
                    ServiceAmount = model.ServiceAmount,
                    PayableAmount = model.PayableAmount,
                    Status = (byte)EnumStatus.Active
                });
                _context.SaveChanges();

                var member = _context.Members.Find(model.MemberId);
                if (member != null)
                {
                    member.DueLoan = model.PayableAmount;
                    _context.Entry(member).State = EntityState.Modified;
                    _context.SaveChanges();
                }
                var orgSetting = _context.OrganizationSettings.FirstOrDefault();
                if (orgSetting != null)
                {
                    orgSetting.TotalTransaction += model.PayableAmount;
                    orgSetting.TotalLoan += model.PayableAmount;
                    orgSetting.TotalProfit += (model.ProfitAmount + model.ServiceAmount);
                    _context.Entry(orgSetting).State = EntityState.Modified;
                    _context.SaveChanges();
                }
                return RedirectToAction("Index");
            }
            return View(model);
        }
    }
}