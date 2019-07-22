using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace LMS.Entities
{
    [Table("Groups")]
    public class Group: Entry
    {
        [Display(Name ="দলের নাম")]
        [Required(ErrorMessage ="দলের নাম প্রদান করতে হবে !")]
        public string GroupName { get; set; }
        [Display(Name = "দল প্রধানের নাম")]
        public string LeaderName { get; set; }
        [Display(Name ="সর্বোচ্চ সদস্য সংখ্যা")]
        [Required(ErrorMessage = "সর্বোচ্চ সদস্য সংখ্যা প্রদান করতে হবে !")]
        public int MaxGroupMembers { get; set; }
        public virtual ICollection<Member> Members { get; set; }
    }
}