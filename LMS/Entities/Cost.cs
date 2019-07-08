using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace LMS.Entities
{
    [Table("Costs")]
    public class Cost:Entry
    {
        [Display(Name = "বিবরণ")]
        [Required(ErrorMessage = "তথ্য টি পূরণ করতে হবে")]
        public string CostDetail { get; set; }
        [Display(Name = "টাকার পরিমাণ")]
        [Required(ErrorMessage = "তথ্য টি পূরণ করতে হবে")]
        public double Amount { get; set; }
    }
}