using LMS.Entities;
using LMS.Models;
using PagedList;
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
        public ActionResult Index(LoanViewModel model)
        {
            DateTime dtFrom = Convert.ToDateTime(model.DateFrom);
            DateTime dtTo = Convert.ToDateTime(model.DateTo);
            model.LoanPageList = _context.Loans.Where(s => ((model.DateFrom == null) || s.Date >= dtFrom) && ((model.DateTo == null) || s.Date <= dtTo)&& ((model.MemberId == null) || s.MemberId == model.MemberId)).OrderBy(s => s.Date).ToPagedList(model.Page, model.PageSize);
            return View(model);
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
        public ActionResult Edit(int id)
        {
            var loan = _context.Loans.Find(id);
            var loanModel = new LoanModel()
            {
                Date = loan.Date,
                MemberId = loan.MemberId,
                ProjectName = loan.ProjectName,
                TotalInstallment = loan.TotalInstallment,
                InstallmentType = loan.InstallmentType,
                FirstInstallment = loan.FirstInstallment,
                LoanAmount = loan.LoanAmount,
                ProfitAmount = loan.ProfitAmount,
                ServiceAmount = loan.ServiceAmount,
                PayableAmount = loan.PayableAmount,
                Member = loan.Member,
                MemberList = _context.Members.Where(s => s.Status == (byte)EnumStatus.Active).ToList()
            };
            return View(loanModel);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(LoanModel model)
        {
            if (ModelState.IsValid)
            {
                var loan = _context.Loans.Find(model.Id);
                if (loan != null)
                {
                    var member = _context.Members.Find(model.MemberId);
                    if (member != null)
                    {
                        member.DueLoan -= loan.PayableAmount;
                        member.DueLoan += model.PayableAmount;
                        _context.Entry(member).State = EntityState.Modified;
                        _context.SaveChanges();
                    }
                    var orgSetting = _context.OrganizationSettings.FirstOrDefault();
                    if (orgSetting != null)
                    {
                        orgSetting.TotalTransaction -= loan.PayableAmount;
                        orgSetting.TotalLoan -= loan.PayableAmount;
                        orgSetting.TotalProfit -= (loan.ProfitAmount + loan.ServiceAmount);
                        orgSetting.TotalTransaction += model.PayableAmount;
                        orgSetting.TotalLoan += model.PayableAmount;
                        orgSetting.TotalProfit += (model.ProfitAmount + model.ServiceAmount);
                        _context.Entry(orgSetting).State = EntityState.Modified;
                        _context.SaveChanges();
                    }

                    loan.Date = model.Date;
                    loan.MemberId = model.MemberId;
                    loan.ProjectName = model.ProjectName;
                    loan.TotalInstallment = model.TotalInstallment;
                    loan.InstallmentType = model.InstallmentType;
                    loan.FirstInstallment = model.FirstInstallment;
                    loan.LoanAmount = model.LoanAmount;
                    loan.ProfitAmount = model.ProfitAmount;
                    loan.ServiceAmount = model.ServiceAmount;
                    loan.PayableAmount = model.PayableAmount;
                    _context.Entry(loan).State = EntityState.Modified;
                    _context.SaveChanges();
                }
             
                return RedirectToAction("Index");
            }
            return View(model);
        }
    }
}