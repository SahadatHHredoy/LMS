using LMS.Entities;
using LMS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LMS.Controllers
{
    public class GroupController : Controller
    {
        private readonly LMSDbContext _context;
        public GroupController()
        {
            _context = new LMSDbContext();
        }
        // GET: Group
        public ActionResult Index()
        {
            return View(_context.Groups.ToList());
        }
        public ActionResult Add()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Add(GroupModel model)
        {
            if (ModelState.IsValid)
            {
                _context.Groups.Add(new Group { GroupName= model.GroupName, LeaderName= model.LeaderName,Status=(byte)EnumStatus.Active,MaxGroupMembers=model.MaxGroupMembers});
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(model);
        }
    }
}