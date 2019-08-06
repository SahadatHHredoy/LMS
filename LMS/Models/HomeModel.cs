using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;
using SelectPdf;
using System.Windows.Forms;
using System.Diagnostics;
using LMS.Entities;
using System.ComponentModel.DataAnnotations.Schema;

namespace LMS.Models
{
    [NotMapped]
    public class HomeModel : Home
    {
        private LMSDbContext _context;
        public string Date { get; set; }

        public DateTime dt { get; set; }
        public HomeModel()
        {
            _context = new LMSDbContext();
        }

        public void LoadData()
        {

            dt = Convert.ToDateTime(Date);
            GetAdhai(dt);
            GetPerot(dt);
            GetJer(dt);
            GetMotMat(dt);
            GetAmanat(dt);
        }

        public void GetAmanat(DateTime dt)
        {
            var perDeposits = _context.PermanentDeposits.ToList();
            var tempDeposits = _context.TemporaryDeposits.ToList();
            var loans = _context.Loans.ToList();

            var esthayiAmanat = (perDeposits.Sum(s => s.Amount == null ? 0 : s.Amount)) - (ThayiAmanotPirot ?? 0);
            var osthayiAmanat = (tempDeposits.Sum(s => s.Amount == null ? 0 : s.Amount)) - (OsthayiAmanotPirot ?? 0);
            var lab = loans.Sum(s => s.ServiceAmount == null ? 0 : s.ServiceAmount) + loans.Sum(s => s.ProfitAmount == null ? 0 : s.ProfitAmount);

            EsthayeAmanat = esthayiAmanat ?? 0;
            OsthayiAmanot = osthayiAmanat ?? 0;
            Lab = lab ?? 0;
            Total = (EsthayeAmanat ?? 0) + (OsthayiAmanot ?? 0) + (Lab ?? 0) + (BortomanSonchoi ?? 0);
        }

        public void GetMotMat(DateTime dt)
        {
            var installments = _context.Installments.ToList();
            var loans = _context.Loans.ToList();

            var totalLoan = loans.Sum(s => s.PayableAmount == null ? 0 : s.PayableAmount);
            var totalPayment = installments.Sum(s => s.Payment == null ? 0 : s.Payment);

            var bortomanSonchoiEstiti = installments.Sum(s => s.Submit == null ? 0 : s.Submit);
            var rinEstiti = (totalLoan ?? 0) - (totalPayment ?? 0);

            BortomanSonchoi = bortomanSonchoiEstiti ?? 0;
            RinEsthite = rinEstiti;
            Sakollo = (BortomanSonchoi ?? 0) + (RinEsthite ?? 0) + (BortomanNogotAstiti ?? 0);

        }

        public void GetJer(DateTime dt)
        {
            JerTaka= OpeningBalance(dt);
            Mot = (SakolloAdhai??0) + (JerTaka ?? 0);
            Khoroc = SakolloKhoroc ?? 0;
            BortomanNogotAstiti = (Mot ?? 0) - (Khoroc ?? 0);
        }

        public void GetPerot(DateTime dt)
        {
            var todayInstallments = _context.Installments.Where(s => s.Date.Year == dt.Year && s.Date.Month == dt.Month && s.Date.Day == dt.Day);
            var todayLoan = _context.Loans.Where(s => s.Date.Year == dt.Year && s.Date.Month == dt.Month && s.Date.Day == dt.Day);
            var todayUndoTempDeposit = _context.UndoTemporaryDeposits.ToList();//Where(s => s.Date.Value.Year == dt.Year && s.Date.Value.Month == dt.Month && s.Date.Value.Day == dt.Day);
            var todayUndoPerDeposit = _context.UndoPermanentDeposits.ToList();//.Where(s => s.Date.Value.Year == dt.Year && s.Date.Value.Month == dt.Month && s.Date.Value.Day == dt.Day);
            var todayCost = _context.Costs.Where(s => s.Date.Year == dt.Year && s.Date.Month == dt.Month && s.Date.Day == dt.Day);

            var sonchoiPerot = todayInstallments.Sum(s => s.Undo == null ? 0 : s.Undo);
            var rinProdan = todayLoan.Sum(s => s.PayableAmount == null ? 0 : s.PayableAmount);
            var estiyeAmanatPerot = todayUndoPerDeposit.Sum(s => s.Amount == null ? 0 : s.Amount);
            var ostiyeAmanatPerot = todayUndoTempDeposit.Sum(s => s.Amount == null ? 0 : s.Amount);
            var onnanoKhoroc = todayCost.Sum(s => s.Amount == null ? 0 : s.Amount);

            SonchoiPirot = sonchoiPerot ?? 0;
            RinProdan = rinProdan ?? 0;
            ThayiAmanotPirot = estiyeAmanatPerot ?? 0;
            OsthayiAmanotPirot = OsthayiAmanotPirot ?? 0;
            OnonannoKhoroc = onnanoKhoroc ?? 0;
            SakolloKhoroc = (SonchoiPirot ?? 0) + (RinProdan ?? 0) + (ThayiAmanotPirot ?? 0) + (OsthayiAmanotPirot ?? 0) + (OnonannoKhoroc ?? 0);



        }

        public void GetAdhai(DateTime dt)
        {
            var todayInstallments = _context.Installments.Where(s => s.Date.Year == dt.Year && s.Date.Month == dt.Month && s.Date.Day == dt.Day);
            var todayPerDeposit = _context.PermanentDeposits.Where(s => s.Date.Value.Year == dt.Year && s.Date.Value.Month == dt.Month && s.Date.Value.Day == dt.Day);
            var todayTempDeposit = _context.PermanentDeposits.Where(s => s.Date.Value.Year == dt.Year && s.Date.Value.Month == dt.Month && s.Date.Value.Day == dt.Day);
            var todayLoan = _context.Loans.Where(s => s.Date.Year == dt.Year && s.Date.Month == dt.Month && s.Date.Day == dt.Day);

            var sonchoiAdhai = todayInstallments.Sum(s => s.Submit == null ? 0 : s.Submit);
            var rinAdhai = todayInstallments.Sum(s => s.Payment == null ? 0 : s.Payment);
            var tempDeposit = todayTempDeposit.Sum(s => s.Amount == null ? 0 : s.Amount);
            var perDeposit = todayPerDeposit.Sum(s => s.Amount == null ? 0 : s.Amount);
            var serviceCharge = todayLoan.Sum(s => s.ServiceAmount == null ? 0 : s.ServiceAmount);

            SonchoiAdai = sonchoiAdhai ?? 0;
            RinAdhai = rinAdhai ?? 0;
            OnnannoAdhai = (tempDeposit ?? 0) + (perDeposit ?? 0) + (serviceCharge ?? 0);
            SakolloAdhai = (SonchoiAdai ?? 0) + (RinAdhai ?? 0) + (OnnannoAdhai ?? 0);

        }

        public double OpeningBalance(DateTime dt)
        {
            double openingBalnce = 0;
            var loan = _context.Loans.Where(s => s.Date < dt);
            var installment = _context.Installments.Where(s => s.Date < dt);
            var tempDeposits = _context.TemporaryDeposits.Where(s => s.Date < dt);
            var perDeposits = _context.PermanentDeposits.Where(s => s.Date < dt);
            var undoPerDeposits = _context.UndoPermanentDeposits.Where(s => s.Date < dt);
            var undoTempDeposits = _context.UndoTemporaryDeposits.Where(s => s.Date < dt);
            var foundAmount = tempDeposits.Sum(s => s.Amount == null ? 0 : s.Amount) +
                               perDeposits.Sum(s => s.Amount == null ? 0 : s.Amount) +
                              installment.Sum(s => s.Submit == null ? 0 : s.Submit) +
                                installment.Sum(s => s.Payment == null ? 0 : s.Payment);
            var eraseAmount = loan.Sum(s => s.LoanAmount == null ? 0 : s.PayableAmount) +
                                installment.Sum(s => s.Undo == null ? 0 : s.Undo) +
                                undoPerDeposits.Sum(s => s.Amount == null ? 0 : s.Amount) +
                               undoTempDeposits.Sum(s => s.Amount == null ? 0 : s.Amount);

            openingBalnce = foundAmount ?? 0 - eraseAmount ?? 0;
            return openingBalnce;
        }


    }
}