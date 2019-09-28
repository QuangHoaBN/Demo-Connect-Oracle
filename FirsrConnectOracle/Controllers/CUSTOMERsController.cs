using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using FirsrConnectOracle.Models;

namespace FirsrConnectOracle.Controllers
{
    public class CUSTOMERsController : Controller
    {
        private BillCustomerEntities db = new BillCustomerEntities();

        // GET: CUSTOMERs
        public ActionResult Index()
        {
            return View(db.CUSTOMERS.ToList());
        }

        // GET: CUSTOMERs/Details/5
        public ActionResult Details(decimal id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CUSTOMER cUSTOMER = db.CUSTOMERS.Find(id);
            if (cUSTOMER == null)
            {
                return HttpNotFound();
            }
            return View(cUSTOMER);
        }

        // GET: CUSTOMERs/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: CUSTOMERs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.


        //[Bind(Include = "ID,NAME,CITY")] CUSTOMER cUSTOMER = CUSTOMER cUSTOMER
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,NAME,CITY")] CUSTOMER cUSTOMER)
        {
            if (ModelState.IsValid)
            {
                db.CUSTOMERS.Add(cUSTOMER);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(cUSTOMER);
        }

        // GET: CUSTOMERs/Edit/5
        public ActionResult Edit(decimal id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CUSTOMER cUSTOMER = db.CUSTOMERS.Find(id);
            if (cUSTOMER == null)
            {
                return HttpNotFound();
            }
            return View(cUSTOMER);
        }

        // POST: CUSTOMERs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.

        ////Cach 1:
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Edit([Bind(Include = "ID,NAME,CITY")] CUSTOMER cUSTOMER)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.Entry(cUSTOMER).State = EntityState.Modified;
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }
        //    return View(cUSTOMER);
        //}

        //Cach 2: 
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(CUSTOMER cUSTOMER)
        {
            CUSTOMER cusInDB = db.CUSTOMERS.Find(cUSTOMER.ID);
            cusInDB.NAME = cUSTOMER.NAME;
            cusInDB.CITY = cUSTOMER.CITY;
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        // GET: CUSTOMERs/Delete/5
        public ActionResult Delete(decimal id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            //Lay ra object co ma la id
            CUSTOMER cUSTOMER = db.CUSTOMERS.Find(id);
            if (cUSTOMER == null)
            {
                return HttpNotFound();
            }
            return View(cUSTOMER);
        }

        // POST: CUSTOMERs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(decimal id)
        {
            CUSTOMER cUSTOMER = db.CUSTOMERS.Find(id);
            db.CUSTOMERS.Remove(cUSTOMER);
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
