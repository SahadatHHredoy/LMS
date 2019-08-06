using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace LMS.Entities
{
    public class UndoPermanentDeposit:Entry
    {
        public int PermanentDepositId { get; set; }
        [ForeignKey("PermanentDepositId")]
        public virtual PermanentDeposit PermanentDeposit { get; set; }

        [Display(Name = "টাকার পরিমাণ")]
  
        public double? Amount { get; set; }
        [Display(Name = "তারিখ")]
        [Required(ErrorMessage = "তথ্য টি পূরণ করতে হবে")]
        public DateTime? Date { get; set; }
    }
}