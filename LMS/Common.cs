using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LMS
{
    public static  class Common
    {
        public static string DateFormat(this DateTime date)
        {
            return string.Format("{0:dd-MM-yyyy}", date);
        }
        public static string DateFormat(this DateTime? date)
        {
            return date.HasValue? string.Format("{0:dd-MM-yyyy}", date):"";
        }
    }

    public enum EnumStatus : byte
    {
        Active =1,
        Deactive =0,
    }
   
}