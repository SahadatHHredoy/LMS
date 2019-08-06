using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace LMS.Entities
{
    public class LMSDbContext:DbContext
    {
        public LMSDbContext() : base("LMSConnString")
        {

        }
        public DbSet<Group> Groups { get; set; }
        public DbSet<Member> Members { get; set; }
        public DbSet<Loan> Loans { get; set; }
        public DbSet<Installment> Installments { get; set; }
        public DbSet<Cost> Costs { get; set; }
        public DbSet<TemporaryDeposit> TemporaryDeposits { get; set; }
        public DbSet<PermanentDeposit> PermanentDeposits { get; set; }
        public DbSet<UndoPermanentDeposit> UndoPermanentDeposits { get; set; }
        public DbSet<UndoTemporaryDeposit> UndoTemporaryDeposits { get; set; }
    }
}