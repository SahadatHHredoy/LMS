using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace LMS.Entities
{
    [Table("Loans")]
    public class Loan : Entry
    { 
        [Display(Name = "সদস্য")]
        [Required(ErrorMessage = "এই তথ্যটি পূরণ করতে হবে !")]
        public int MemberId { get; set; }
        [ForeignKey("MemberId")]
        public virtual Member Member { get; set; }
        [Display(Name = "ঋণের পরিমাণ")]
        [Required(ErrorMessage = "এই তথ্যটি পূরণ করতে হবে !")]
        public double? LoanAmount { get; set; }
        //[Required(ErrorMessage = "এই তথ্যটি পূরণ করতে হবে !")]
        [Display(Name = "লাভ্যংশ ")]
        public double? ProfitAmount { get; set; }
        [Display(Name = "সার্ভিস খরচ")]
    
        public double? ServiceAmount { get; set; }
        [Display(Name = "পরিশোধ যোগ্য")]
        public double? PayableAmount { get; set; }
        [Display(Name = "কিস্তি সংখ্যা")]
        //[Required(ErrorMessage = "এই তথ্যটি পূরণ করতে হবে !")]
        public int TotalInstallment { get; set; }
        [Display(Name = "প্রকল্পের নাম")]
        public string ProjectName{get;set;}
        [Display(Name = "কিস্তির ধরণ")]
        [Required(ErrorMessage = "এই তথ্যটি পূরণ করতে হবে !")]
        public string InstallmentType { get; set; }
        [Display(Name = "তারিখ")]
        [Required(ErrorMessage = "এই তথ্যটি পূরণ করতে হবে !")]
        public DateTime Date { get; set; }
        [Display(Name = "প্রথম কিস্তি")]
        [Required(ErrorMessage = "এই তথ্যটি পূরণ করতে হবে !")]
        public DateTime FirstInstallment { get; set; }
    }
}