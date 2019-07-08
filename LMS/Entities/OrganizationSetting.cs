using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace LMS.Entities
{
    [Table("OrganizationSettings")]
    public class OrganizationSetting:Entry
    {
        [Display(Name ="প্রতিষ্ঠানের নাম")]
        [Required(ErrorMessage = "তথ্য টি পূরণ করতে হবে")]
        public string OrganizationName { get; set; }
        public double TotalTransaction { get; set; }
        public double TotalCost { get; set; }
        public double TotalProfit { get; set; }
        public double TotalSaving { get; set; }
        public double TotalLoan { get; set; }
    }
}