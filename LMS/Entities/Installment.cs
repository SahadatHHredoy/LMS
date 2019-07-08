using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace LMS.Entities
{
    [Table("Installments")]
    public class Installment : Entry
    {
        [Display(Name ="তারিখ")]
        [Required(ErrorMessage ="তথ্য টি পূরণ করতে হবে")]
        public DateTime Date { get; set; }
        [Display(Name = "সদস্য")]
        [Required(ErrorMessage = "তথ্য টি পূরণ করতে হবে")]
        public int MemberId { get; set; }
        [ForeignKey("MemberId")]
        public virtual Member Member { get; set; }
        [Display(Name = "জমা")]
        public double? Submit { get; set; }
        [Display(Name = "উত্তোলন")]
        public double? Undo { get; set; }
        [Display(Name = "লাভ")]
        public double? Profit { get; set; }
        [Display(Name = "কিস্তি নং")]
        public int? InstallmentNo { get; set; }
        [Display(Name = "আদায়")]
        public double? Payment { get; set; }
        [Display(Name = "ঋণ স্থিতি")]
        public double? LoanDue { get; set; }
        [Display(Name = "জমা স্থিতি")]
        public double? SavingAmount{get;set;}
    }
}