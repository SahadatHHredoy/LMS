using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace LMS.Entities
{
    public class Entry
    {
        public int Id { get; set; }
        [Display(Name ="বর্তমান অবস্থা")]
        public int? Status { get; set; }
    }
}