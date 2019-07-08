using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace LMS.Entities
{
    [Table("Members")]
    public class Member:Entry
    {
        [Display(Name ="দল")]
        [Required(ErrorMessage ="এই তথ্যটি পূরণ করতে হবে !")]
        public int GroupId { get; set; }
        [ForeignKey("GroupId")]
        public virtual Group Group { get; set; }
        public string Image { get; set; }
        [Display(Name = "সদস্যের নাম")]
        [Required(ErrorMessage = "এই তথ্যটি পূরণ করতে হবে !")]
        public string MemberName { get; set; }
        [Display(Name = "পিতার নাম ")]
        [Required(ErrorMessage = "এই তথ্যটি পূরণ করতে হবে !")]
        public string FatherName { get; set; }
        [Display(Name = "মাতার নাম ")]
        [Required(ErrorMessage = "এই তথ্যটি পূরণ করতে হবে !")]
        public string MothersName { get; set; }
        [Display(Name = "স্থায়ী ঠিকানা ")]
        [Required(ErrorMessage = "এই তথ্যটি পূরণ করতে হবে !")]
        public string ParmanentAddress { get; set; }
        [Display(Name = "বর্তমান ঠিকানা")]
        [Required(ErrorMessage = "এই তথ্যটি পূরণ করতে হবে !")]
        public string PresentAddress { get; set; }
        [Display(Name = "জম্ম তারিখ")]
        [Required(ErrorMessage = "এই তথ্যটি পূরণ করতে হবে !")]
        public DateTime DateOfBirth { get; set; }
        [Display(Name = "মোবাইল নং ")]
        [Required(ErrorMessage = "এই তথ্যটি পূরণ করতে হবে !")]
        public string MobileNo { get; set; }
        [Display(Name = "জাতীয় পরিচয় নং")]
        [Required(ErrorMessage = "এই তথ্যটি পূরণ করতে হবে !")]
        public string NID { get; set; }
        [Display(Name = "ধর্ম")]
        [Required(ErrorMessage = "এই তথ্যটি পূরণ করতে হবে !")]
        public string Religion { get; set; }
        [Display(Name = "রক্তের গ্রুপ")]
        public string BloodGroup { get; set; }
        [Required(ErrorMessage = "এই তথ্যটি পূরণ করতে হবে !")]
        [Display(Name = "জাতীয়তা")]
        public string Nationlity { get; set; }
        [Required(ErrorMessage = "এই তথ্যটি পূরণ করতে হবে !")]
        [Display(Name = "পেশা")]
        public string Profession { get; set; }
        [Display(Name = "কর্মস্থলের ঠিকানা ")]
        [Required(ErrorMessage = "এই তথ্যটি পূরণ করতে হবে !")]
        public string OfficeAddress { get; set; }
        [Display(Name = "স্বামী/স্ত্রীর নাম")]
        public string PartnerName { get; set; }
        [Display(Name = "শিক্ষাগত যোগ্যতা")]
        public string EduQualification { get; set; }

        public double DueLoan { get; set; }
        public double SavingAmount { get; set; }
    }
}