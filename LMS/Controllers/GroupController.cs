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

        public JsonResult GetMembers(int id)
        {
            var group = _context.Groups.Find(id);
            return Json(group.Members.Where(s => s.Status == (byte)EnumStatus.Active).Select(s => new
            {
                Id = s.Id,
                Name = s.MemberName,
                Image = s.Image,
                Loan = s.DueLoan,
                Saving = s.SavingAmount,
                InstallNo = s.Installments.Count(p=>p.Payment.HasValue)+1

            }));
        }

        public ActionResult Edit(int id)
        {
            var group = _context.Groups.Find(id);
            var groupModel = new GroupModel()
            {
                Id = group.Id,
                MaxGroupMembers = group.MaxGroupMembers,
                GroupName = group.GroupName,
                LeaderName = group.LeaderName

            };
            return View(groupModel);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(GroupModel model)
        {
            if (ModelState.IsValid)
            {
                var group = _context.Groups.Find(model.Id);
                if (group != null)
                {
                    group.LeaderName = model.LeaderName;
                    group.MaxGroupMembers = model.MaxGroupMembers;
                    group.GroupName = model.GroupName;
                    _context.Entry(group).State = System.Data.Entity.EntityState.Modified;
                    _context.SaveChanges();
                }
                return RedirectToAction("Index");
            }
            return View(model);
        }
        public ActionResult Deactive(int id)
        {
            var group = _context.Groups.Find(id);
            if (group != null)
            {
                group.Status = (byte)EnumStatus.Deactive;
                _context.Entry(group).State = System.Data.Entity.EntityState.Modified;
                _context.SaveChanges();
            }
            return RedirectToAction("Index");
        }
        public ActionResult Active(int id)
        {
            var group = _context.Groups.Find(id);
            if (group != null)
            {
                group.Status = (byte)EnumStatus.Active;
                _context.Entry(group).State = System.Data.Entity.EntityState.Modified;
                _context.SaveChanges();
            }
            return RedirectToAction("Index");
        }
    }
}