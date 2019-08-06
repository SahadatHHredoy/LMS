using LMS.Entities;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LMS.Models
{
   public class CostViewModel
    {
        public Int32 Page { get; set; }
        public Int32 PageSize { get; set; }
        public string DateFrom { get; set; }
        public string DateTo { get; set; }

        public IPagedList<Cost> CostPageList;
        public CostViewModel()
        {
            Page = 1;
            PageSize = 50;
        }
    }
    public class LoanViewModel
    {
        public Int32 Page { get; set; }
        public Int32 PageSize { get; set; }
        public string DateFrom { get; set; }
        public string DateTo { get; set; }

        public int? MemberId { get; set; }
        public List<Member> MemberList { get; set; }

        public IPagedList<Loan> LoanPageList;
        public LoanViewModel()
        {
            Page = 1;
            PageSize = 50;
            MemberList = new MemberModel().GetAllMember();
        }
    }
    public class InstallmentViewModel
    {
        public Int32 Page { get; set; }
        public Int32 PageSize { get; set; }
        public string DateFrom { get; set; }
        public string DateTo { get; set; }

        public int? MemberId { get; set; }
        public List<Member> MemberList { get; set; }

        public IPagedList<Installment> InstallmentPageList;
        public InstallmentViewModel()
        {
            Page = 1;
            PageSize = 50;
            MemberList = new MemberModel().GetAllMember();
        }
    }
    public class PermanentDepositViewModel
    {
        public Int32 Page { get; set; }
        public Int32 PageSize { get; set; }
        public string DateFrom { get; set; }
        public string DateTo { get; set; }


        public IPagedList<PermanentDeposit> PermanentDeposits;
        public PermanentDepositViewModel()
        {
            Page = 1;
            PageSize = 50;
        }
    }
    public class TemporaryDepositViewModel
    {
        public Int32 Page { get; set; }
        public Int32 PageSize { get; set; }
        public string DateFrom { get; set; }
        public string DateTo { get; set; }


        public IPagedList<TemporaryDeposit> TemporaryDeposits;
        public TemporaryDepositViewModel()
        {
            Page = 1;
            PageSize = 50;
        }
    }
    public class MemberViewModel
    {
        public Int32 Page { get; set; }
        public Int32 PageSize { get; set; }
        public string Name { get; set; }
        public int? GroupId { get; set; }
        public List<Group> GroupList { get; set; }


        public IPagedList<Member> MemberList;
        public MemberViewModel()
        {
            Page = 1;
            PageSize = 50;
            GroupList = new GroupModel().GetAllGroup();
        }
    }
}