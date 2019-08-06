using LMS.Entities;
using LMS.Models;
using PagedList;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;
using SelectPdf;
using System.Windows.Forms;
using System.Diagnostics;

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
         
        public ActionResult Index(MemberViewModel model)
        {
            model.MemberList = _context.Members.Where(s => ((model.Name == null) || s.MemberName.StartsWith(model.Name)) && ((model.GroupId==null)|| (s.GroupId==model.GroupId))).OrderBy(s=>s.Id).ToPagedList(model.Page, model.PageSize);
            return View(model);
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
                var group = _context.Groups.Find(model.GroupId);
                //if(group.MaxGroupMembers<= group.Members.Where(s=>s.Status == (byte)EnumStatus.Active).Count())
                if(true)
                {
                    ModelState.AddModelError(string.Empty, "এ দলে সদস্য পূর্ণ হয়ে গেছে ");
                    return View(model);
                }
                else
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
                        EduQualification = model.EduQualification,
                        Status = (byte)EnumStatus.Active
                    });
                    _context.SaveChanges();
                    return RedirectToAction("Index");
                }
               
            }
            return View(model);
        }

        public JsonResult GetMemberImg(int id)
        {
            var member = _context.Members.Find(id);
            return Json(member.Image);
        }

        public ActionResult Edit(int id)
        {
            var member = _context.Members.Find(id);
            var memberModel = new MemberModel()
            {
                Id = member.Id,
                MemberName = member.MemberName,
                GroupId = member.GroupId,
                Image = member.Image,
                FatherName = member.FatherName,
                MothersName = member.MothersName,
                PresentAddress = member.PresentAddress,
                ParmanentAddress = member.ParmanentAddress,
                DateOfBirth = member.DateOfBirth,
                MobileNo = member.MobileNo,
                NID = member.NID,
                Religion = member.Religion,
                BloodGroup = member.BloodGroup,
                Nationlity = member.Nationlity,
                Profession = member.Profession,
                OfficeAddress = member.OfficeAddress,
                PartnerName = member.PartnerName,
                EduQualification = member.EduQualification,
            } ;
            return View(memberModel);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(MemberModel model)
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
                var member = _context.Members.Find(model.Id);
                if (member != null)
                {
                    member.MemberName = model.MemberName;
                    member.GroupId = model.GroupId;
                    if (ImagePath != "")
                    {
                        member.Image = ImagePath;
                    }
                    member.FatherName = model.FatherName;
                    member.MothersName = model.MothersName;
                    member.PresentAddress = model.PresentAddress;
                    member.ParmanentAddress = model.ParmanentAddress;
                    member.DateOfBirth = model.DateOfBirth;
                    member.MobileNo = model.MobileNo;
                    member.NID = model.NID;
                    member.Religion = model.Religion;
                    member.BloodGroup = model.BloodGroup;
                    member.Nationlity = model.Nationlity;
                    member.Profession = model.Profession;
                    member.OfficeAddress = model.OfficeAddress;
                    member.PartnerName = model.PartnerName;
                    member.EduQualification = model.EduQualification;
                    _context.Entry(member).State = System.Data.Entity.EntityState.Modified;
                    _context.SaveChanges();
                }
                return RedirectToAction("Index");
            }
            return View(model);
        }
        public ActionResult Deactive(int id)
        {
            var memeber = _context.Members.Find(id);
            if (memeber != null)
            {
                memeber.Status = (byte)EnumStatus.Deactive;
                _context.Entry(memeber).State = System.Data.Entity.EntityState.Modified;
                _context.SaveChanges();
            }
            return RedirectToAction("Index");
        }
        public ActionResult Active(int id)
        {
            var memeber = _context.Members.Find(id);
            if (memeber != null)
            {
                memeber.Status = (byte)EnumStatus.Active;
                _context.Entry(memeber).State = System.Data.Entity.EntityState.Modified;
                _context.SaveChanges();
            }
            return RedirectToAction("Index");
        }
        public ActionResult Details(int id)
        {
            return View(_context.Members.Find(id));
        }

        [AllowAnonymous]
        public ActionResult ConvertHtmlToPdf(int id)
        {
            HtmlToPdf converter = new HtmlToPdf();
            var leftPartUrl = String.Format("{0}://{1}:{2}", Request.RequestContext.HttpContext.Request.Url.Scheme, Request.RequestContext.HttpContext.Request.Url.Host, Request.RequestContext.HttpContext.Request.Url.Port);
            // set converter options
            converter.Options.PdfPageSize = PdfPageSize.A4;
            converter.Options.PdfPageOrientation = PdfPageOrientation.Portrait;
            converter.Options.MarginLeft = 20;
            converter.Options.MarginRight = 20;
            converter.Options.MarginTop = 10;
            converter.Options.MarginBottom = 10;



            // footer settings //
            converter.Options.DisplayFooter = true;
            converter.Footer.DisplayOnFirstPage = true;
            converter.Footer.DisplayOnOddPages = true;
            converter.Footer.DisplayOnEvenPages = true;
            converter.Footer.Height = 50;



            // convert url //
            string url = Url.Action("GeneratePdfOrPrint", new { id = id });
            url = String.Format("{1}{0}", url, leftPartUrl);
            PdfDocument doc = converter.ConvertUrl(url);
            MemoryStream stream = new MemoryStream();

            // set the document properties //
            HtmlToPdfResult result = converter.ConversionResult;
            doc.DocumentInformation.Title = result.WebPageInformation.Title;
            doc.DocumentInformation.Subject = result.WebPageInformation.Description;
            doc.DocumentInformation.Keywords = result.WebPageInformation.Keywords;

            doc.DocumentInformation.Author = "Pdf";
            doc.DocumentInformation.CreationDate = DateTime.Now;
            // create a new pdf font (component standard font)
            PdfFont font1 = doc.AddFont(PdfStandardFont.TimesRoman);
            font1.Size = 30;
            doc.Save(stream);
            doc.Close();
            return File(stream.ToArray(), "application/pdf");
        }
        public ActionResult GeneratePdfOrPrint(int id)
        {
            return View(_context.Members.Find(id));
        }
    }
}