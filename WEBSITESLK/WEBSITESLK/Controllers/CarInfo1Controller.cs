using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WEBSITESLK.Models;

namespace WEBSITESLK.Controllers
{ [Authorize]
    public class CarInfo1Controller : Controller
    {
        private Car db = new Car();
        // GET: CarInfo1
        public ActionResult Index()
        {
            return View(db.CarInfo1.ToList());
        }
        // GET: CarInfo1/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CarInfo1 carInfo1 = db.CarInfo1.Find(id);
            if (carInfo1 == null)
            {
                return HttpNotFound();
            }
            return View(carInfo1);
        }

        // GET: CarInfo1/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: CarInfo1/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "VIN,Brand,Model,Price,StoreLoc,Owner")] CarInfo1 carInfo1)
        {
            if (ModelState.IsValid)
            {
                db.CarInfo1.Add(carInfo1);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(carInfo1);
        }

        // GET: CarInfo1/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CarInfo1 carInfo1 = db.CarInfo1.Find(id);
            if (carInfo1 == null)
            {
                return HttpNotFound();
            }
            return View(carInfo1);
        }

        // POST: CarInfo1/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "VIN,Brand,Model,Price,StoreLoc,Owner")] CarInfo1 carInfo1)
        {
            if (ModelState.IsValid)
            {
                db.Entry(carInfo1).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(carInfo1);
        }

        // GET: CarInfo1/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CarInfo1 carInfo1 = db.CarInfo1.Find(id);
            if (carInfo1 == null)
            {
                return HttpNotFound();
            }
            return View(carInfo1);
        }

        // POST: CarInfo1/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            CarInfo1 carInfo1 = db.CarInfo1.Find(id);
            db.CarInfo1.Remove(carInfo1);
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
