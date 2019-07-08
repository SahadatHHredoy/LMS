using LMS.Entities;
using LMS.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LMS.Controllers
{
    public class MemberController : Controller
    {
        // GET: Member
        private readonly LMSDbContext _context;
        public MemberController()
        {
            _context = new LMSDbContext();
        }
         
        public ActionResult Index()
        {
            return View(_context.Members.ToList());
        }
        public ActionResult Add()
        {
            return View(new MemberModel());
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Add(MemberModel model)
        {
            if (ModelState.IsValid)
            {
                string ImagePath = "";
                if (model.File != null)
                {
                    var fileNameWithoutExt = Path.GetFileNameWithoutExtension(model.File.FileName);
                    var fileExtension = Path.GetExtension(model.File.FileName);
                    var finalFileName = string.Format("{0:yyMMddhhmmss}", DateTime.Now) + fileExtension;
                    string savePath = Path.Combine(Server.MapPath("~/Uploads/"), finalFileName);
                    model.File.SaveAs(savePath);
                    ImagePath = "~/Uploads/" + finalFileName;

                }
                _context.Members.Add(new Member
                {
                    MemberName = model.MemberName,
                    GroupId = model.GroupId,
                    Image = ImagePath,
                    FatherName = model.FatherName,
                    MothersName = model.MothersName,
                    PresentAddress = model.PresentAddress,
                    ParmanentAddress = model.ParmanentAddress,
                    DateOfBirth = model.DateOfBirth,
                    MobileNo = model.MobileNo,
                    NID = model.NID,
                    Religion = model.Religion,
                    BloodGroup = model.BloodGroup,
                    Nationlity = model.Nationlity,
                    Profession = model.Profession,
                    OfficeAddress = model.OfficeAddress,
                    PartnerName = model.PartnerName,
                    DueLoan=0,
                    SavingAmount =0,
                    EduQualification = model.EduQualification,
                    Status =(byte)EnumStatus.Active
                });
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(model);
        }

        public JsonResult GetMemberImg(int id)
        {
            var member = _context.Members.Find(id);
            return Json(member.Image);
        }
    }
}