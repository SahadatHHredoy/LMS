using LMS.Entities;
using LMS.Models;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LMS.Controllers
{
    public class PermanentDepositController : Controller
    {
        private readonly LMSDbContext _context;

        // GET: PermanentDeposit
        public PermanentDepositController()
        {
            _context = new LMSDbContext();
        }
        public ActionResult Index(PermanentDepositViewModel model)
        {
            DateTime dtFrom = Convert.ToDateTime(model.DateFrom);
            DateTime dtTo = Convert.ToDateTime(model.DateTo);
            model.PermanentDeposits = _context.PermanentDeposits.Where(s => ((model.DateFrom == null) || s.Date >= dtFrom) && ((model.DateTo == null) || s.Date <= dtTo)).OrderBy(s => s.Date).ToPagedList(model.Page, model.PageSize);
            return View(model);
        }

        public ActionResult Add()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Add(PermanentDeposit model)
        {
            if (ModelState.IsValid)
            {
                _context.PermanentDeposits.Add(model);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(model);
        }
        public ActionResult Edit(int id)
        {
            return View(_context.PermanentDeposits.Find(id));
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(PermanentDeposit model)
        {
            if (ModelState.IsValid)
            {
                var entity = _context.PermanentDeposits.Find(model.Id);
                if (entity != null)
                {
                    entity.Date = model.Date;
                    entity.Depositor = model.Depositor;
                    entity.FatherName = model.FatherName;
                    entity.MothersName = model.MothersName;
                    entity.ParmanentAddress = model.ParmanentAddress;
                    entity.PresentAddress = model.PresentAddress;
                    entity.MobileNo = model.MobileNo;
                    entity.Amount = model.Amount;
                    _context.Entry(entity).State = System.Data.Entity.EntityState.Modified;
                    _context.SaveChanges();
                }
                
                return RedirectToAction("Index");
            }
            return View(model);
        }
        public ActionResult Undo(int id)
        {
            var undoPermanentDeposit = new UndoPermanentDeposit()
            {
                PermanentDeposit = _context.PermanentDeposits.Find(id),
                PermanentDepositId = id,
            };
            return View(undoPermanentDeposit);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Undo(UndoPermanentDeposit model)
        {
            if (ModelState.IsValid)
            {
                _context.UndoPermanentDeposits.Add(model);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(model);
        }
        public ActionResult EditUndo(int id)
        {
            var undoPermanentDeposit = _context.UndoPermanentDeposits.Find(id);
            return View(undoPermanentDeposit);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditUndo(UndoPermanentDeposit model)
        {
            if (ModelState.IsValid)
            {
                var entity = _context.UndoPermanentDeposits.Find(model.Id);
                if (entity != null)
                {
                    entity.Date = model.Date;
                    entity.Amount = model.Amount;
                    _context.Entry(entity).State = System.Data.Entity.EntityState.Modified;
                    _context.SaveChanges();
                }
                return RedirectToAction("Index");
            }
            return View(model);
        }
    }
}