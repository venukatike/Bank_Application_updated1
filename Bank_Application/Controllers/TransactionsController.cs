using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Bank_Application.Models;

namespace Bank_Application.Controllers
{
    public class TransactionsController : Controller
    {
        private BankApplicationEntities14 db = new BankApplicationEntities14();

        // GET: Transactions
        public ActionResult Index(string searching)
        {
           

            var accounts = db.accounts.Include(a => a.Openingaccount);

            return View(db.accounts.Where(x => x.account_number.Contains(searching) || searching == null));
            //return View(accounts.ToList());
        }

        // GET: Transactions/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            account account = db.accounts.Find(id);
            if (account == null)
            {
                return HttpNotFound();
            }
            return View(account);
        }

        // GET: Transactions/Create
        public ActionResult Create()
        {
            ViewBag.CustomerID = new SelectList(db.Openingaccounts, "CustomerID", "CustomerID");
            return View();
        }

        // POST: Transactions/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(string btn,[Bind(Include = "accountID,account_number,amount,CustomerID")] account account)
        {
            int num = 1;
            if (btn == "Withdraw")
            {
                var data = db.accounts.Where(account1 => account1.account_number == account.account_number).FirstOrDefault();
                if (account.amount <= data.amount)
                {
                    data.amount -= account.amount;
                    int msg = db.SaveChanges();
                    if (msg == 1)
                    {
                        //ViewBag.data = "Withdraw Done";
                        TempData["SuccessMessage"] = "Withdraw successful.";
                    }
                    else
                    {
                        TempData["SuccessMessage"] = "Withdraw Not successful.";
                    }


                }
                else
                {
                    //ViewBag.data = "Insufficent balance";
                    TempData["SuccessMessage"] = "Insufficent balance.";
                }

            }
            else if (btn == "Deposit")
            {
                var data = db.accounts.Where(account1 => account1.account_number == account.account_number).FirstOrDefault();
                data.amount += account.amount;
                int msg = db.SaveChanges();
                if (msg == 1)
                {
                    //ViewBag.data = "Deposit Done";
                    TempData["SuccessMessage"] = "Deposit successful.";
                }
                else
                {
                    //ViewBag.data = "Deposit Not Done";
                    TempData["SuccessMessage"] = "Deposit Not successful.";
                }

            }
            else if (btn == "Show")
            {
                var data = db.accounts.Where(account1 => account1.account_number == account.account_number).FirstOrDefault();

                ViewBag.Show = data.amount;
                num = 0;
             
            }
            if (ModelState.IsValid)
            {
                if(num == 1)
                {
                    db.accounts.Add(account);
                    db.SaveChanges();
                   // return RedirectToAction("Index");
                }
               
               
            }
           

            ViewBag.CustomerID = new SelectList(db.Openingaccounts, "CustomerID", "CustomerID", account.CustomerID);
            return View(account);
        }

        // GET: Transactions/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            account account = db.accounts.Find(id);
            if (account == null)
            {
                return HttpNotFound();
            }
            ViewBag.CustomerID = new SelectList(db.Openingaccounts, "CustomerID", "account_number", account.CustomerID);
            return View(account);
        }

        // POST: Transactions/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "accountID,account_number,amount,CustomerID")] account account)
        {
            if (ModelState.IsValid)
            {
                db.Entry(account).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CustomerID = new SelectList(db.Openingaccounts, "CustomerID", "account_number", account.CustomerID);
            return View(account);
        }

        // GET: Transactions/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            account account = db.accounts.Find(id);
            if (account == null)
            {
                return HttpNotFound();
            }
            return View(account);
        }

        // POST: Transactions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            account account = db.accounts.Find(id);
            db.accounts.Remove(account);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult FundTransfer([Bind(Include = "accountID,account_number,Toaccount_number,amount,CustomerID")] account account)
        {
            var data = db.accounts.Where(account1 => account1.account_number == account.account_number).FirstOrDefault();
            var data1 = db.accounts.Where(account1 => account1.account_number == account.Toaccount_number).FirstOrDefault();
            if (account.amount <= data.amount)
            {
                data.amount -= account.amount;
                db.SaveChanges();
                if (data != data1)
                {
                    data1.amount += account.amount;
                    int m = db.SaveChanges();
                    if (m == 1)
                    {
                        TempData["SuccessMessage"] = "Transfer successful.";
                    }
                    else
                    {
                        TempData["SuccessMessage"] = "Transfer successful.";
                    }
                }


            }

            return View(account);
        }



        public ActionResult FundTransfer()
        {
            return View();
        }


        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
