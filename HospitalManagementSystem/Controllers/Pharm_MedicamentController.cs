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
    public class Pharm_MedicamentController : Controller
    {
        private Clinique_dbEntities db = new Clinique_dbEntities();

        // GET: Pharm_Medicament
        public async Task<ActionResult> Index()
        {
            var medicaments = db.Medicaments.Include(m => m.Configuration);
            return View(await medicaments.ToListAsync());
        }

        // GET: Pharm_Medicament/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Medicament medicament = await db.Medicaments.FindAsync(id);
            if (medicament == null)
            {
                return HttpNotFound();
            }
            return View(medicament);
        }

        // GET: Pharm_Medicament/Create
        public ActionResult Create()
        {
            ViewBag.CategorieMedicament = new SelectList(db.Configurations.Where(x => x.Nature.Libelle.Equals("Catégorie Médicament")), "IdConfiguration", "Libelle");
            return View();
        }

        // POST: Pharm_Medicament/Create
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,NomCommercial,NomScientifique,Quantite,Prix,Disponibilite,Commentaire,SeuilCritique,CategorieMedicament")] Medicament medicament)
        {
            medicament.Disponibilite = 1;
            if (ModelState.IsValid)
            {
                db.Medicaments.Add(medicament);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.CategorieMedicament = new SelectList(db.Configurations.Where(x=>x.Nature.Libelle.Equals("Catégorie Médicament")), "IdConfiguration", "Libelle", medicament.CategorieMedicament);
            return View(medicament);
        }

        // GET: Pharm_Medicament/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Medicament medicament = await db.Medicaments.FindAsync(id);
            if (medicament == null)
            {
                return HttpNotFound();
            }
            ViewBag.CategorieMedicament = new SelectList(db.Configurations.Where(x => x.Nature.Libelle.Equals("Catégorie Médicament")), "IdConfiguration", "Libelle", medicament.CategorieMedicament);
            return View(medicament);
        }

        // POST: Pharm_Medicament/Edit/5
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,NomCommercial,NomScientifique,Quantite,Prix,Disponibilite,Commentaire,SeuilCritique,CategorieMedicament")] Medicament medicament)
        {
            if (ModelState.IsValid)
            {
                db.Entry(medicament).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.CategorieMedicament = new SelectList(db.Configurations.Where(x => x.Nature.Libelle.Equals("Catégorie Médicament")), "IdConfiguration", "Libelle", medicament.CategorieMedicament);
            return View(medicament);
        }

        // GET: Pharm_Medicament/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Medicament medicament = await db.Medicaments.FindAsync(id);
            if (medicament == null)
            {
                return HttpNotFound();
            }
            return View(medicament);
        }

        // POST: Pharm_Medicament/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Medicament medicament = await db.Medicaments.FindAsync(id);
            db.Medicaments.Remove(medicament);
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
