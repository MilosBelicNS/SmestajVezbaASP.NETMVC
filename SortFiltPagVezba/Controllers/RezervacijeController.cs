using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Microsoft.Ajax.Utilities;
using PagedList;
using SortFiltPagVezba.Models;

namespace SortFiltPagVezba.Controllers
{
    public class RezervacijeController : Controller
    {
        private SmestajDbContext db = new SmestajDbContext();
        
        public enum SortTypes
        {
            BrojKreveta,
            BrojKrevetaOp,
            CenaNoc,
            CenaNocOp
        }

        public Dictionary<string, SortTypes> dictionary = new Dictionary<string, SortTypes>()
        {
            {"BrojKreveta", SortTypes.BrojKreveta },
            {"BrojKrevetaOp", SortTypes.BrojKrevetaOp },
            {"CenaNoc", SortTypes.CenaNoc },
            {"CenaNocOp", SortTypes.CenaNocOp }
        };
        // GET: Rezervacije
        private int RezPerPage = 2;
        public ActionResult Index(RezervacijaFilter rezervacijaFilter, string sort="BrojKreveta", int page= 1)
        {
            IQueryable<Rezervacija> rezervacije = db.Rezervacije.Include(r => r.Soba);
            if (sort == "")
            {
                sort = "BrojKreveta";
            }
            SortTypes sortType = dictionary[sort];

            //FILTER
            if (rezervacijaFilter.DatumPocetka != null)
            {
                rezervacije = rezervacije.Where(p => p.DatumPocetka >= rezervacijaFilter.DatumPocetka);
            }
            if (rezervacijaFilter.DatumKraja != null)
            {
                rezervacije = rezervacije.Where(p => p.DatumKraja <= rezervacijaFilter.DatumKraja);
            }
            if (rezervacijaFilter.BrojSobe != null)
            {
                rezervacije = rezervacije.Where(p => p.Soba.BrojSobe == rezervacijaFilter.BrojSobe);
            }
            //SORT
            switch (sortType)
            {
                case SortTypes.BrojKreveta:
                    rezervacije = rezervacije.OrderBy(s => s.Soba.BrojKreveta);
                    break;
                case SortTypes.BrojKrevetaOp:
                    rezervacije = rezervacije.OrderByDescending(s => s.Soba.BrojKreveta);
                    break;
                case SortTypes.CenaNoc:
                    rezervacije = rezervacije.OrderBy(s => s.Soba.CenaNoc);
                    break;
                case SortTypes.CenaNocOp:
                    rezervacije = rezervacije.OrderByDescending(s => s.Soba.CenaNoc);
                    break;
            }


            ViewBag.selectionList = new SelectList(dictionary, "Key", "Key", sort);
            ViewBag.chosenSort = sort;
            ViewBag.filter = rezervacijaFilter;

            return View(rezervacije.ToPagedList(page, RezPerPage));
        }

        // GET: Rezervacije/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Rezervacija rezervacija = db.Rezervacije.Find(id);
            if (rezervacija == null)
            {
                return HttpNotFound();
            }
            return View(rezervacija);
        }

        // GET: Rezervacije/Create
        public ActionResult Create()
        {
            ViewBag.SobaId = new SelectList(db.Sobe, "Id", "Id");
            return View();
        }

        // POST: Rezervacije/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,ImePrezime,DatumPocetka,DatumKraja,Otkazana,SobaId")] Rezervacija rezervacija)
        {
            if (ModelState.IsValid)
            {
               
                db.Rezervacije.Add(rezervacija);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.SobaId = new SelectList(db.Sobe, "Id", "Id", rezervacija.SobaId);
            return View(rezervacija);
        }

        // GET: Rezervacije/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Rezervacija rezervacija = db.Rezervacije.Find(id);
            if (rezervacija == null)
            {
                return HttpNotFound();
            }
            ViewBag.SobaId = new SelectList(db.Sobe, "Id", "Id", rezervacija.SobaId);
            return View(rezervacija);
        }

        // POST: Rezervacije/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,ImePrezime,DatumPocetka,DatumKraja,Otkazana,SobaId")] Rezervacija rezervacija)
        {
            if (ModelState.IsValid)
            {
                db.Entry(rezervacija).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.SobaId = new SelectList(db.Sobe, "Id", "Id", rezervacija.SobaId);
            return View(rezervacija);
        }

        // GET: Rezervacije/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Rezervacija rezervacija = db.Rezervacije.Find(id);
            if (rezervacija == null)
            {
                return HttpNotFound();
            }
            return View(rezervacija);
        }

        // POST: Rezervacije/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Rezervacija rezervacija = db.Rezervacije.Find(id);
            db.Rezervacije.Remove(rezervacija);
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
