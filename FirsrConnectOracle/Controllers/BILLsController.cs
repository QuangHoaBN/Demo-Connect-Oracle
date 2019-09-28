using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using FirsrConnectOracle.Models;
using FirsrConnectOracle.ViewModels;

namespace FirsrConnectOracle.Controllers
{
    public class BILLsController : Controller
    {
        private BillCustomerEntities db = new BillCustomerEntities();

        // GET: BILLs
        public ActionResult Index()
        {
            var bILLS = db.BILLS.Include(b => b.CUSTOMER);
            return View(bILLS.ToList());
        }

        // GET: BILLs/Details/5
        public ActionResult Details(decimal id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BILL bILL = db.BILLS.Find(id);
            if (bILL == null)
            {
                return HttpNotFound();
            }
            return View(bILL);
        }

        // GET: BILLs/Create
        public ActionResult Create()
        {
            //Call View default List Customer
            ViewBag.CUSTOMER_ID = new SelectList(db.CUSTOMERS, "ID", "NAME");
            return View();
        }

        //// POST: BILLs/Create
        //// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        //// more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Create([Bind(Include = "ID,CUSTOMER_ID,TOTAL")] BILL bILL)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.BILLS.Add(bILL);
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }

        //    ViewBag.CUSTOMER_ID = new SelectList(db.CUSTOMERS, "ID", "NAME", bILL.CUSTOMER_ID);
        //    return View(bILL);
        //}

        //POST: BILLs/Create
        [HttpPost]
        [ValidateAntiForgeryToken] // ngan chan gia mao
        public ActionResult Create(BILL bill) {
            //Check value of a attribute
            if (!ModelState.IsValid)
            {
                var viewModel = new BillFormViewModel
                {
                    BILL = bill,
                    CUSTOMERs=db.CUSTOMERS.ToList()
                };
                return View(viewModel);
            }
            if (bill.ID == 0 | bill.ID == null)
            {
                return View("This bill is not created!");
            }
            else
            {
                db.BILLS.Add(bill);
            }
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        // GET: BILLs/Edit/5
        public ActionResult Edit(decimal id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            //Lay ra object thong qua id
            BILL bILL = db.BILLS.Find(id);
            if (bILL == null)
            {
                return HttpNotFound();
            }

            //// Cach 1: 
            ////goi ra dropdown list voi gia tri duoc chon biLL.CUSTOMER_ID
            //ViewBag.CUSTOMER_ID = new SelectList(db.CUSTOMERS, "ID", "NAME", bILL.CUSTOMER_ID);
            //return View(bILL);

            // Cach 2: Thong Qua ViewModel
            var viewModel = new BillFormViewModel
            {
                BILL = bILL,
                CUSTOMERs = db.CUSTOMERS.ToList()
            };
            return View(viewModel);



        }

        // POST: BILLs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]

        ////[Bind(Include = "ID,CUSTOMER_ID,TOTAL")] BILL bILL = BILL bILL = db.BILLS.Find(id);
        /// <param name="bill"></param>
        /// <returns></returns>
        //public ActionResult Edit([Bind(Include = "ID,CUSTOMER_ID,TOTAL")] BILL bILL)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.Entry(bILL).State = EntityState.Modified;
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }
        //    ViewBag.CUSTOMER_ID = new SelectList(db.CUSTOMERS, "ID", "NAME", bILL.CUSTOMER_ID);
        //    return View(bILL);
        //}

        //POST: BILLs/Edit/id

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(BILL bill) {
            //Tim bill trong database
            BILL billInDB = db.BILLS.Find(bill.ID);
            //Update
            billInDB.CUSTOMER_ID = bill.CUSTOMER_ID;
            billInDB.TOTAL = bill.TOTAL;
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        // GET: BILLs/Delete/5
        public ActionResult Delete(decimal id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BILL bILL = db.BILLS.Find(id);
            if (bILL == null)
            {
                return HttpNotFound();
            }
            return View(bILL);
        }

        // POST: BILLs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(decimal id)
        {
            BILL bILL = db.BILLS.Find(id);
            db.BILLS.Remove(bILL);
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
