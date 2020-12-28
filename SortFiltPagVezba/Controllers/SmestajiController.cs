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
    public class SmestajiController : Controller
    {
        private SmestajDbContext db = new SmestajDbContext();


        public enum SortTypes
        {
            Naziv_rastuce,
            Naziv_opadajuce,
            Ocena_rastuce,
            Ocena_opadajuce
        }

        public static Dictionary<string, SortTypes> SortTypesDict = new Dictionary<string, SortTypes>
        {
            {"Naziv", SortTypes.Naziv_rastuce },
            {"Naziv-opadajuce", SortTypes.Naziv_opadajuce},
            {"Ocena", SortTypes.Ocena_rastuce },
            {"Ocena-opadajuce", SortTypes.Ocena_opadajuce }
        };



        private int SmestajPerPage = 2;
       
        // GET: Smestaji
        public ActionResult Index(SmestajFilter smestajFilter, string sortType ="Naziv", int page = 1)
        {
            IQueryable<Smestaj> smestaji = db.Smestaji;
            if(sortType == "")
            {
                sortType = "Naziv";
            }
            SortTypes sortBy = SortTypesDict[sortType];
            
            //FILTER
            if(!smestajFilter.Naziv.IsNullOrWhiteSpace())
            {
                smestaji = smestaji.Where(f => f.Naziv.Contains(smestajFilter.Naziv));

            }
            if(!smestajFilter.Adresa.IsNullOrWhiteSpace())
            {
                smestaji = smestaji.Where(f => f.Adresa.Contains(smestajFilter.Adresa));
            }

            //SORTIRANJE 
            switch (sortBy)
            {
                case SortTypes.Naziv_rastuce:
                    smestaji = smestaji.OrderBy(x => x.Naziv);
                    break;
                case SortTypes.Naziv_opadajuce:
                    smestaji = smestaji.OrderByDescending(x => x.Naziv);
                    break;
                case SortTypes.Ocena_rastuce:
                    smestaji = smestaji.OrderBy(x => x.Ocena);
                    break;
                case SortTypes.Ocena_opadajuce:
                    smestaji = smestaji.OrderByDescending(x => x.Ocena);
                    break;
            }

            ViewBag.selectionList = new SelectList(SortTypesDict, "Key", "Key", sortType);
            ViewBag.CurrentSortType = sortType;
            ViewBag.filter = smestajFilter;
            return View(smestaji.ToPagedList(page, SmestajPerPage));
        }

        // GET: Smestaji/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Smestaj smestaj = db.Smestaji.Find(id);
            if (smestaj == null)
            {
                return HttpNotFound();
            }
            return View(smestaj);
        }

        // GET: Smestaji/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Smestaji/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Naziv,Opis,Adresa,Ocena")] Smestaj smestaj)
        {
            if (ModelState.IsValid)
            {
                db.Smestaji.Add(smestaj);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(smestaj);
        }

        // GET: Smestaji/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Smestaj smestaj = db.Smestaji.Find(id);
            if (smestaj == null)
            {
                return HttpNotFound();
            }
            return View(smestaj);
        }

        // POST: Smestaji/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Naziv,Opis,Adresa,Ocena")] Smestaj smestaj)
        {
            if (ModelState.IsValid)
            {
                db.Entry(smestaj).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(smestaj);
        }

        // GET: Smestaji/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Smestaj smestaj = db.Smestaji.Find(id);
            if (smestaj == null)
            {
                return HttpNotFound();
            }
            return View(smestaj);
        }

        // POST: Smestaji/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Smestaj smestaj = db.Smestaji.Find(id);
            db.Smestaji.Remove(smestaj);
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
