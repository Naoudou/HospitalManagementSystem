using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using HospitalManagementSystem.Models;

namespace HospitalManagementSystem.Controllers
{
    [Authorize]
    public class Pharm_EquivalentController : Controller
    {
        private Clinique_dbEntities db = new Clinique_dbEntities();

        // GET: Pharm_Equivalent
        public async Task<ActionResult> Index()
        {
            var equivalences = db.Equivalences.Include(e => e.Medicament).Include(e => e.Medicament1);
            return View(await equivalences.ToListAsync());
        }

        //Equivalence
        public ActionResult GetAll()
        {
            var equivalences = db.Equivalences.Include(e => e.Medicament).Include(e => e.Medicament1);
            return PartialView("GetAll", equivalences.ToList());
        }

        // GET: Pharm_Equivalent/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Equivalence equivalence = await db.Equivalences.FindAsync(id);
            if (equivalence == null)
            {
                return HttpNotFound();
            }
            return View(equivalence);
        }

        // GET: Pharm_Equivalent/Create
        public ActionResult Create()
        {
            ViewBag.IdMedicament = new SelectList(db.Medicaments, "Id", "NomCommercial");
            ViewBag.IdEquivalent = new SelectList(db.Medicaments, "Id", "NomCommercial");
            return View();
        }

        // POST: Pharm_Equivalent/Create
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,IdMedicament,IdEquivalent,Commentaire")] Equivalence equivalence)
        {
            if (ModelState.IsValid)
            {
                db.Equivalences.Add(equivalence);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.IdMedicament = new SelectList(db.Medicaments, "Id", "NomCommercial", equivalence.IdMedicament);
            ViewBag.IdEquivalent = new SelectList(db.Medicaments, "Id", "NomCommercial", equivalence.IdEquivalent);
            return View(equivalence);
        }

        // GET: Pharm_Equivalent/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Equivalence equivalence = await db.Equivalences.FindAsync(id);
            if (equivalence == null)
            {
                return HttpNotFound();
            }
            ViewBag.IdMedicament = new SelectList(db.Medicaments, "Id", "NomCommercial", equivalence.IdMedicament);
            ViewBag.IdEquivalent = new SelectList(db.Medicaments, "Id", "NomCommercial", equivalence.IdEquivalent);
            return View(equivalence);
        }

        // POST: Pharm_Equivalent/Edit/5
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,IdMedicament,IdEquivalent,Commentaire")] Equivalence equivalence)
        {
            if (ModelState.IsValid)
            {
                db.Entry(equivalence).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.IdMedicament = new SelectList(db.Medicaments, "Id", "NomCommercial", equivalence.IdMedicament);
            ViewBag.IdEquivalent = new SelectList(db.Medicaments, "Id", "NomCommercial", equivalence.IdEquivalent);
            return View(equivalence);
        }

        // GET: Pharm_Equivalent/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Equivalence equivalence = await db.Equivalences.FindAsync(id);
            if (equivalence == null)
            {
                return HttpNotFound();
            }
            return View(equivalence);
        }

        // POST: Pharm_Equivalent/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Equivalence equivalence = await db.Equivalences.FindAsync(id);
            db.Equivalences.Remove(equivalence);
            await db.SaveChangesAsync();
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
