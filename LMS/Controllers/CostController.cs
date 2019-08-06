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
    public class CostController : Controller
    {
        private LMSDbContext _context;
        // GET: Cost

        public CostController()
        {
            _context = new LMSDbContext();
        }
        public ActionResult Index(CostViewModel model)
        {
            DateTime dtFrom = Convert.ToDateTime(model.DateFrom);
            DateTime dtTo = Convert.ToDateTime(model.DateTo);
            model.CostPageList = _context.Costs.Where(s => ((model.DateFrom == null) || s.Date >= dtFrom) && ((model.DateTo == null) || s.Date <= dtTo)).OrderBy(s=>s.Date).ToPagedList(model.Page, model.PageSize);
            return View(model);
        }

        public ActionResult Add()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Add(Cost cost)
        {
            if (ModelState.IsValid)
            {
                
                _context.Costs.Add(cost);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(cost);
        }
        public ActionResult Edit(int id)
        {
            return View(_context.Costs.Find(id));
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Cost cost)
        {
            if (ModelState.IsValid)
            {

                _context.Costs.Add(cost);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(cost);
        }
    }
}