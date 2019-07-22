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
    public class InstallmentController : Controller
    {
        // GET: Installment
        private LMSDbContext _context;

        public InstallmentController()
        {
            _context = new LMSDbContext();
        }

        public ActionResult Index(InstallmentViewModel model)
        {
            DateTime dtFrom = Convert.ToDateTime(model.DateFrom);
            DateTime dtTo = Convert.ToDateTime(model.DateTo);
            model.InstallmentPageList = _context.Installments.Where(s => ((model.DateFrom == null) || s.Date >= dtFrom) && ((model.DateTo == null) || s.Date <= dtTo) && ((model.MemberId == null) || s.MemberId == model.MemberId)).OrderBy(s => s.Date).ToPagedList(model.Page, model.PageSize);
            return View(model);
        }
        public ActionResult Add()
        {
            return View(new InstallmentModel());
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Add(InstallmentModel model)
        {
            model.Add();
            return RedirectToAction("Index");
        }
        public ActionResult Edit(int id)
        {
            return View(_context.Installments.Find(id));
        }
    }
}