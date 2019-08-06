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
    public class TemporaryDepositController : Controller
    {
        private readonly LMSDbContext _context;

        // GET: TemporaryDeposit
        public TemporaryDepositController()
        {
            _context = new LMSDbContext();
        }
        public ActionResult Index(TemporaryDepositViewModel model)
        {
            DateTime dtFrom = Convert.ToDateTime(model.DateFrom);
            DateTime dtTo = Convert.ToDateTime(model.DateTo);
            model.TemporaryDeposits = _context.TemporaryDeposits.Where(s => ((model.DateFrom == null) || s.Date >= dtFrom) && ((model.DateTo == null) || s.Date <= dtTo)).OrderBy(s => s.Date).ToPagedList(model.Page, model.PageSize);
            return View(model);
        }
        public ActionResult Add()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Add(TemporaryDeposit model)
        {
            if (ModelState.IsValid)
            {
                _context.TemporaryDeposits.Add(model);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(model);
        }
        public ActionResult Edit(int id)
        {
            return View(_context.TemporaryDeposits.Find(id));
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(TemporaryDeposit model)
        {
            if (ModelState.IsValid)
            {
                var entity = _context.TemporaryDeposits.Find(model.Id);
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
            var undoTemporaryDeposit = new UndoTemporaryDeposit()
            {
                TemporaryDeposit = _context.TemporaryDeposits.Find(id),
                TemporaryDepositId = id,
            };
            return View(undoTemporaryDeposit);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Undo(UndoTemporaryDeposit model)
        {
            if (ModelState.IsValid)
            {
                _context.UndoTemporaryDeposits.Add(model);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(model);
        }
        public ActionResult EditUndo(int id)
        {
            var undoTemporaryDeposit = _context.UndoTemporaryDeposits.Find(id);
            return View(undoTemporaryDeposit);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditUndo(UndoTemporaryDeposit model)
        {
            if (ModelState.IsValid)
            {
                var entity = _context.UndoTemporaryDeposits.Find(model.Id);
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