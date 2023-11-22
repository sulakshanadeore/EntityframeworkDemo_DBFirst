
using EntityframeworkDemo_DBFirst.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using Microsoft.Win32;

namespace EntityframeworkDemo_DBFirst.Controllers
{
    public class CustomerTransferMoneyController : Controller
    {
        DatabaseFirstLibrary.SomeBankEntities1 db = new DatabaseFirstLibrary.SomeBankEntities1();

        // GET: CustomerTransferMoney
        List<TransactionsDetails> listTransactions = new List<TransactionsDetails>();
        public ActionResult Index()
        {
            listTransactions = (List<TransactionsDetails>)TempData["list_tran"];

            return View(listTransactions);
        }

        public ActionResult ShowTransactionHistoryForAccountNo()
        {
            return View();
        }

        [HttpPost]
        public ActionResult ShowTransactionHistoryForAccountNo(int custid)
        {


            IQueryable<DatabaseFirstLibrary.fn_ShowTransactionHistory_Result> result=db.fn_ShowTransactionHistory(custid);
           List<DatabaseFirstLibrary.fn_ShowTransactionHistory_Result> list= result.AsEnumerable<DatabaseFirstLibrary.fn_ShowTransactionHistory_Result>().ToList();

            foreach (var item in list)
            {
                TransactionsDetails t = new TransactionsDetails();
                t.tranid=Convert.ToInt32(item.tranid);
                t.trandate = Convert.ToDateTime(item.trandate);
                t.amt = Convert.ToDecimal(item.amt);
                listTransactions.Add(t);
            }
            TempData["list_tran"]=listTransactions;
                        return RedirectToAction("Index");
        }


        public ActionResult TransferMoney()
        {
            return View();
        }

        [HttpPost]
        public ActionResult TransferMoney(CustMoneyTrans trans)
        {

            db.sp_TransferMoney(trans.CustFrom, trans.CustTo, trans.Amt);
            db.SaveChanges();
            return Content("Transferred Money successfully");

        }

    }




}