using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace LMS.Entities
{
    public class PermanentDeposit:Entry
    {
        [Display(Name ="তারিখ")]
        public DateTime? Date { get; set; }
        [Required(ErrorMessage = "এই তথ্যটি পূরণ করতে হবে !")]
        [Display(Name = "জমাদাতা")]
        public string Depositor { get; set; }
        [Display(Name = "অংক")]
        public double? Amount { get; set; }
       
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
       
        [Display(Name = "মোবাইল নং ")]
        [Required(ErrorMessage = "এই তথ্যটি পূরণ করতে হবে !")]
        public string MobileNo { get; set; }
        public virtual ICollection<UndoPermanentDeposit> UndoPermanentDeposits { get; set; }
    }
}