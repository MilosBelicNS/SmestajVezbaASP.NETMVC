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
    public class SobeController : Controller
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
        int SobaPerPage = 2;
        // GET: Sobas
        public ActionResult Index(SobaFilter sobaFilter, string sort = "BrojKreveta", int page= 1)
        {
            IQueryable<Soba> sobe = db.Sobe.Include(s => s.Smestaj);
            if (sort == "")
            {
                sort = "BrojKreveta";
            }
            SortTypes sortType = dictionary[sort];

            //FILTER
            if (sobaFilter.BrojKreveta != null)
            {
                sobe = sobe.Where(p => p.BrojKreveta == sobaFilter.BrojKreveta);
            }
            if (sobaFilter.CenaNocOd != null)
            {
                sobe = sobe.Where(p => p.CenaNoc >= sobaFilter.CenaNocOd);
            }
            if (sobaFilter.CenaNocDo != null)
            {
                sobe = sobe.Where(p => p.CenaNoc <= sobaFilter.CenaNocDo);
            }
            if (!sobaFilter.SmestajNaziv.IsNullOrWhiteSpace())
            {
                sobe = sobe.Where(p => p.Smestaj.Naziv.Contains(sobaFilter.SmestajNaziv));
            }

            //SORT
            switch (sortType)
            {
                case SortTypes.BrojKreveta:
                    sobe = sobe.OrderBy(s => s.BrojKreveta);
                    break;
                case SortTypes.BrojKrevetaOp:
                    sobe = sobe.OrderByDescending(s => s.BrojKreveta);
                    break;
                case SortTypes.CenaNoc:
                    sobe = sobe.OrderBy(s => s.CenaNoc);
                    break;
                case SortTypes.CenaNocOp:
                    sobe = sobe.OrderByDescending(s => s.CenaNoc);
                    break;
            }

            ViewBag.selectionList = new SelectList(dictionary, "Key", "Key", sort);
            ViewBag.chosenSort = sort;
            ViewBag.filter = sobaFilter;

            return View(sobe.ToPagedList(page, SobaPerPage));
        }

        // GET: Sobe/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Soba soba = db.Sobe.Find(id);
            if (soba == null)
            {
                return HttpNotFound();
            }
            return View(soba);
        }

        // GET: Sobe/Create
        public ActionResult Create()
        {
            ViewBag.SmestajId = new SelectList(db.Smestaji, "Id", "Naziv");
            return View();
        }

        // POST: Sobe/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,BrojSobe,BrojKreveta,CenaNoc,SmestajId")] Soba soba)
        {
            if (ModelState.IsValid)
            {
                db.Sobe.Add(soba);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.SmestajId = new SelectList(db.Smestaji, "Id", "Naziv", soba.SmestajId);
            return View(soba);
        }

        // GET: Sobe/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Soba soba = db.Sobe.Find(id);
            if (soba == null)
            {
                return HttpNotFound();
            }
            ViewBag.SmestajId = new SelectList(db.Smestaji, "Id", "Naziv", soba.SmestajId);
            return View(soba);
        }

        // POST: Sobe/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,BrojSobe,BrojKreveta,CenaNoc,SmestajId")] Soba soba)
        {
            if (ModelState.IsValid)
            {
                db.Entry(soba).State = EntityState.Modified;
              
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.SmestajId = new SelectList(db.Smestaji, "Id", "Naziv", soba.SmestajId);
            return View(soba);
        }

        

        // GET: Sobe/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Soba soba = db.Sobe.Find(id);
            if (soba == null)
            {
                return HttpNotFound();
            }
            return View(soba);
        }

        // POST: Sobe/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Soba soba = db.Sobe.Find(id);
            db.Sobe.Remove(soba);
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
