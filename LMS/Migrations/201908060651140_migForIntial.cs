namespace LMS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class migForIntial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Costs",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CostDetail = c.String(nullable: false),
                        Amount = c.Double(),
                        Date = c.DateTime(nullable: false),
                        Status = c.Int(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Groups",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        GroupName = c.String(nullable: false),
                        LeaderName = c.String(),
                        MaxGroupMembers = c.Int(nullable: false),
                        Status = c.Int(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Members",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        GroupId = c.Int(nullable: false),
                        Image = c.String(),
                        MemberName = c.String(nullable: false),
                        FatherName = c.String(nullable: false),
                        MothersName = c.String(nullable: false),
                        ParmanentAddress = c.String(nullable: false),
                        PresentAddress = c.String(nullable: false),
                        DateOfBirth = c.DateTime(nullable: false),
                        MobileNo = c.String(nullable: false),
                        NID = c.String(nullable: false),
                        Religion = c.String(nullable: false),
                        BloodGroup = c.String(),
                        Nationlity = c.String(nullable: false),
                        Profession = c.String(nullable: false),
                        OfficeAddress = c.String(nullable: false),
                        PartnerName = c.String(),
                        EduQualification = c.String(),
                        Status = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Groups", t => t.GroupId, cascadeDelete: true)
                .Index(t => t.GroupId);
            
            CreateTable(
                "dbo.Installments",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Date = c.DateTime(nullable: false),
                        MemberId = c.Int(nullable: false),
                        Submit = c.Double(),
                        Undo = c.Double(),
                        Profit = c.Double(),
                        InstallmentNo = c.Int(),
                        Payment = c.Double(),
                        LoanDue = c.Double(),
                        SavingAmount = c.Double(),
                        Status = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Members", t => t.MemberId, cascadeDelete: true)
                .Index(t => t.MemberId);
            
            CreateTable(
                "dbo.Loans",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        MemberId = c.Int(nullable: false),
                        LoanAmount = c.Double(nullable: false),
                        ProfitAmount = c.Double(),
                        ServiceAmount = c.Double(),
                        PayableAmount = c.Double(),
                        TotalInstallment = c.Int(nullable: false),
                        ProjectName = c.String(),
                        InstallmentType = c.String(nullable: false),
                        Date = c.DateTime(nullable: false),
                        FirstInstallment = c.DateTime(nullable: false),
                        Status = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Members", t => t.MemberId, cascadeDelete: true)
                .Index(t => t.MemberId);
            
            CreateTable(
                "dbo.PermanentDeposits",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Date = c.DateTime(),
                        Depositor = c.String(nullable: false),
                        Amount = c.Double(),
                        FatherName = c.String(nullable: false),
                        MothersName = c.String(nullable: false),
                        ParmanentAddress = c.String(nullable: false),
                        PresentAddress = c.String(nullable: false),
                        MobileNo = c.String(nullable: false),
                        Status = c.Int(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.TemporaryDeposits",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Date = c.DateTime(),
                        Depositor = c.String(),
                        Amount = c.Double(),
                        FatherName = c.String(nullable: false),
                        MothersName = c.String(nullable: false),
                        ParmanentAddress = c.String(nullable: false),
                        PresentAddress = c.String(nullable: false),
                        MobileNo = c.String(nullable: false),
                        Status = c.Int(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.UndoTemporaryDeposits",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        TemporaryDepositId = c.Int(nullable: false),
                        Amount = c.Double(),
                        Date = c.DateTime(nullable: false),
                        Status = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.TemporaryDeposits", t => t.TemporaryDepositId, cascadeDelete: true)
                .Index(t => t.TemporaryDepositId);
            
            CreateTable(
                "dbo.UndoPermanentDeposits",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        PermanentDepositId = c.Int(nullable: false),
                        Amount = c.Double(),
                        Date = c.DateTime(nullable: false),
                        Status = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.PermanentDeposits", t => t.PermanentDepositId, cascadeDelete: true)
                .Index(t => t.PermanentDepositId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.UndoPermanentDeposits", "PermanentDepositId", "dbo.PermanentDeposits");
            DropForeignKey("dbo.UndoTemporaryDeposits", "TemporaryDepositId", "dbo.TemporaryDeposits");
            DropForeignKey("dbo.Loans", "MemberId", "dbo.Members");
            DropForeignKey("dbo.Installments", "MemberId", "dbo.Members");
            DropForeignKey("dbo.Members", "GroupId", "dbo.Groups");
            DropIndex("dbo.UndoPermanentDeposits", new[] { "PermanentDepositId" });
            DropIndex("dbo.UndoTemporaryDeposits", new[] { "TemporaryDepositId" });
            DropIndex("dbo.Loans", new[] { "MemberId" });
            DropIndex("dbo.Installments", new[] { "MemberId" });
            DropIndex("dbo.Members", new[] { "GroupId" });
            DropTable("dbo.UndoPermanentDeposits");
            DropTable("dbo.UndoTemporaryDeposits");
            DropTable("dbo.TemporaryDeposits");
            DropTable("dbo.PermanentDeposits");
            DropTable("dbo.Loans");
            DropTable("dbo.Installments");
            DropTable("dbo.Members");
            DropTable("dbo.Groups");
            DropTable("dbo.Costs");
        }
    }
}
