using DatabaseFirstLibrary;
using EntityframeworkDemo_DBFirst.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EntityframeworkDemo_DBFirst.Controllers
{
    public class CustMoneyController : Controller
    {
        List<TransactionsDetails> detailsofTrans = new List<TransactionsDetails>();
        SomeBankEntities1 db1=new SomeBankEntities1();
        // GET: CustMoney
        public ActionResult Index()
        {
           detailsofTrans=(List<TransactionsDetails>)TempData["mydatatrans"];

            return View(detailsofTrans);
        }

        public ActionResult ShowTransactions()
        {

            return View();  
        }

        [HttpPost]
        public ActionResult ShowTransactions(int custid) {
            IQueryable<fn_ShowTransactionHistory_Result> result=db1.fn_ShowTransactionHistory(custid);
List<fn_ShowTransactionHistory_Result> list =result.ToList();
           

            foreach (var item in list)
            {
                TransactionsDetails t = new TransactionsDetails();
                t.tranid = Convert.ToInt32(item.tranid);
                t.trandate = Convert.ToDateTime(item.trandate);
                t.amt = Convert.ToDecimal(item.amt);
                detailsofTrans.Add(t);

            }

            TempData["mydatatrans"] = detailsofTrans;

            return RedirectToAction("Index");
        }

    }
}