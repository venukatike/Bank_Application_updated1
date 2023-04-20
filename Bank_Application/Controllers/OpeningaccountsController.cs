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
    public class OpeningaccountsController : Controller
    {
        private BankApplicationEntities14 db = new BankApplicationEntities14();

        // GET: Openingaccounts
        public ActionResult Index(string searching)
        {
            return View(db.Openingaccounts.Where(x => x.account_number.Contains(searching) || searching == null));
            // return View(db.Openingaccounts.ToList());
        }

        // GET: Openingaccounts/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Openingaccount openingaccount = db.Openingaccounts.Find(id);
            if (openingaccount == null)
            {
                return HttpNotFound();
            }
            return View(openingaccount);
        }

        // GET: Openingaccounts/Create
        public ActionResult Create(int Customerid = 100) 
        {
             Openingaccount cus = new Openingaccount();
             var lastcustomer = db.Openingaccounts.OrderByDescending(x => x.CustomerID).FirstOrDefault();
             if (Customerid != 100)
             {
                 cus = db.Openingaccounts.Where(x => x.CustomerID == Customerid).FirstOrDefault<Openingaccount>();
             }
             else if (lastcustomer == null)
             {
                 cus.account_number = "WB00";
             }
             else
             {
                 cus.account_number = "WB00" + (Convert.ToInt32(lastcustomer.account_number.Substring(3, lastcustomer.account_number.Length - 3)) + 1).ToString("D3");
             }

             return View(cus);
            
        }

        // POST: Openingaccounts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Openingaccount R,[Bind(Include = "CustomerID,account_number,Password,Fullname,Fathername,Adharno,Email,Address,DOB,Age,accounttype,Phoneno")] Openingaccount openingaccount)
        {
            if (ModelState.IsValid)
            {
                db.Openingaccounts.Add(openingaccount);
                //db.Openingaccounts.Add(R);
                db.SaveChanges();
                //return RedirectToAction("Details", openingaccount.CustomerID);
                ViewBag.Message = "Successfully Opened The Account";
                //return RedirectToAction("Edit");
            }

            return View(openingaccount);
        }

        // GET: Openingaccounts/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Openingaccount openingaccount = db.Openingaccounts.Find(id);
            if (openingaccount == null)
            {
                return HttpNotFound();
            }
            return View(openingaccount);
        }

        // POST: Openingaccounts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CustomerID,account_number,Password,Fullname,Fathername,Adharno,Email,Address,DOB,Age,accounttype,Phoneno")] Openingaccount openingaccount)
        {
            if (ModelState.IsValid)
            {
                db.Entry(openingaccount).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(openingaccount);
        }

        // GET: Openingaccounts/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Openingaccount openingaccount = db.Openingaccounts.Find(id);
            if (openingaccount == null)
            {
                return HttpNotFound();
            }
            return View(openingaccount);
        }

        // POST: Openingaccounts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Openingaccount openingaccount = db.Openingaccounts.Find(id);
            db.Openingaccounts.Remove(openingaccount);
            db.SaveChanges();
            return RedirectToAction("Index");
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
