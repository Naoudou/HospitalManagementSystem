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
    public class CaisseController : Controller
    {
        private Clinique_dbEntities db = new Clinique_dbEntities();

        // GET: Caisse
        public async Task<ActionResult> Index()
        {
            var fiches = db.Fiches.Include(f => f.Configuration).Include(f => f.LibelleActe).Include(f => f.Patient).Include(f => f.Personnel);
            return View(await fiches.ToListAsync());
        }

        public ActionResult GetAll()
        {
            var fiches = db.Fiches.Where(x => x.Date.Day.Equals(DateTime.Now.Day)

            && x.Date.Year.Equals(DateTime.Now.Year) && x.Date.Month.Equals(DateTime.Now.Month)).Include(f => f.Configuration).Include(f => f.LibelleActe).Include(f => f.Patient).Include(f => f.Personnel);
            return PartialView("GetAll", fiches.ToList());
        }



        // GET: Caisse/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Fiche fiche = await db.Fiches.FindAsync(id);
            if (fiche == null)
            {
                return HttpNotFound();
            }
            return View(fiche);
        }

        // GET: Caisse/Create
        public ActionResult Create()
        {
            ViewBag.Raison = new SelectList(db.Configurations, "IdConfiguration", "Libelle");
            ViewBag.CodeExamen = new SelectList(db.LibelleActes, "Code", "Libelle");
            ViewBag.CodePatient = new SelectList(db.Patients, "Code", "Nom");
            ViewBag.CodeSecretaire = new SelectList(db.Personnels, "Code", "Nom");
            return View();
        }

        //paiement de la consultation
        public async Task<ActionResult> Payer(int? id)
        {
            Fiche fiche = await db.Fiches.FindAsync(id);
            if (fiche.Paiement == 0)
            {
                fiche.Paiement = 1;
            }
            //else
            //{
            //    fiche.Paiement = 0;
            //}
           
            db.Entry(fiche).State = EntityState.Modified;
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        // POST: Caisse/Create
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,CodePatient,CodeSecretaire,Date,Paiement,Raison,CodeExamen")] Fiche fiche)
        {
            if (ModelState.IsValid)
            {
                db.Fiches.Add(fiche);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.Raison = new SelectList(db.Configurations, "IdConfiguration", "Libelle", fiche.Raison);
            ViewBag.CodeExamen = new SelectList(db.LibelleActes, "Code", "Libelle", fiche.CodeExamen);
            ViewBag.CodePatient = new SelectList(db.Patients, "Code", "Nom", fiche.CodePatient);
            ViewBag.CodeSecretaire = new SelectList(db.Personnels, "Code", "Nom", fiche.CodeSecretaire);
            return View(fiche);
        }

        // GET: Caisse/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Fiche fiche = await db.Fiches.FindAsync(id);
            if (fiche == null)
            {
                return HttpNotFound();
            }
            ViewBag.Raison = new SelectList(db.Configurations, "IdConfiguration", "Libelle", fiche.Raison);
            ViewBag.CodeExamen = new SelectList(db.LibelleActes, "Code", "Libelle", fiche.CodeExamen);
            ViewBag.CodePatient = new SelectList(db.Patients, "Code", "Nom", fiche.CodePatient);
            ViewBag.CodeSecretaire = new SelectList(db.Personnels, "Code", "Nom", fiche.CodeSecretaire);
            return View(fiche);
        }

        // POST: Caisse/Edit/5
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,CodePatient,CodeSecretaire,Date,Paiement,Raison,CodeExamen")] Fiche fiche)
        {
            if (ModelState.IsValid)
            {
                db.Entry(fiche).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.Raison = new SelectList(db.Configurations, "IdConfiguration", "Libelle", fiche.Raison);
            ViewBag.CodeExamen = new SelectList(db.LibelleActes, "Code", "Libelle", fiche.CodeExamen);
            ViewBag.CodePatient = new SelectList(db.Patients, "Code", "Nom", fiche.CodePatient);
            ViewBag.CodeSecretaire = new SelectList(db.Personnels, "Code", "Nom", fiche.CodeSecretaire);
            return View(fiche);
        }

        // GET: Caisse/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Fiche fiche = await db.Fiches.FindAsync(id);
            if (fiche == null)
            {
                return HttpNotFound();
            }
            return View(fiche);
        }

        // POST: Caisse/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Fiche fiche = await db.Fiches.FindAsync(id);
            db.Fiches.Remove(fiche);
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
