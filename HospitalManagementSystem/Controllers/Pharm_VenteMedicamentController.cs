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
    public class Pharm_VenteMedicamentController : Controller
    {
        private Clinique_dbEntities db = new Clinique_dbEntities();

        // GET: Pharm_VenteMedicament
        public async Task<ActionResult> Index()
        {
            var venteMedicaments = db.VenteMedicaments.Include(v => v.Medicament).Include(v => v.Personnel);
            return View(await venteMedicaments.ToListAsync());
        }

        public ActionResult GetAll()
        {
            var venteMedicaments = db.VenteMedicaments.Include(v => v.Medicament).Include(v => v.Personnel);
            return PartialView("GetAll", venteMedicaments.ToList());
        }

        // GET: Pharm_VenteMedicament/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            VenteMedicament venteMedicament = await db.VenteMedicaments.FindAsync(id);
            if (venteMedicament == null)
            {
                return HttpNotFound();
            }
            return View(venteMedicament);
        }

        // GET: Pharm_VenteMedicament/Create
        public ActionResult Create()
        {
            ViewBag.IdMedicament = new SelectList(db.Medicaments, "Id", "NomCommercial");
            ViewBag.CodePharmacien = new SelectList(db.Personnels.Where(x=>x.Configuration2.Libelle=="Pharmacien"), "Code", "Nom");
            return View();
        }

        // POST: Pharm_VenteMedicament/Create
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,Date,Quantite,CodePharmacien,IdMedicament")] VenteMedicament venteMedicament)
        {
            venteMedicament.Date = DateTime.Now;
            if (ModelState.IsValid)
            {
                db.VenteMedicaments.Add(venteMedicament);
               // await db.SaveChangesAsync();
                //soustraction du stock

                Medicament m =await db.Medicaments.FindAsync(venteMedicament.IdMedicament);
                m.Quantite = m.Quantite - venteMedicament.Quantite;
                db.Entry(m).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.IdMedicament = new SelectList(db.Medicaments, "Id", "NomCommercial", venteMedicament.IdMedicament);
            ViewBag.CodePharmacien = new SelectList(db.Personnels.Where(x => x.Configuration2.Libelle == "Pharmacien"), "Code", "Nom", venteMedicament.CodePharmacien);
            return View(venteMedicament);
        }

        // GET: Pharm_VenteMedicament/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            VenteMedicament venteMedicament = await db.VenteMedicaments.FindAsync(id);
            if (venteMedicament == null)
            {
                return HttpNotFound();
            }
            ViewBag.IdMedicament = new SelectList(db.Medicaments, "Id", "NomCommercial", venteMedicament.IdMedicament);
            ViewBag.CodePharmacien = new SelectList(db.Personnels, "Code", "Nom", venteMedicament.CodePharmacien);
            return View(venteMedicament);
        }

        // POST: Pharm_VenteMedicament/Edit/5
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,Date,Quantite,CodePharmacien,IdMedicament")] VenteMedicament venteMedicament)
        {
            if (ModelState.IsValid)
            {
                db.Entry(venteMedicament).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.IdMedicament = new SelectList(db.Medicaments, "Id", "NomCommercial", venteMedicament.IdMedicament);
            ViewBag.CodePharmacien = new SelectList(db.Personnels, "Code", "Nom", venteMedicament.CodePharmacien);
            return View(venteMedicament);
        }

        // GET: Pharm_VenteMedicament/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            VenteMedicament venteMedicament = await db.VenteMedicaments.FindAsync(id);
            if (venteMedicament == null)
            {
                return HttpNotFound();
            }
            return View(venteMedicament);
        }

        // POST: Pharm_VenteMedicament/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            VenteMedicament venteMedicament = await db.VenteMedicaments.FindAsync(id);
            db.VenteMedicaments.Remove(venteMedicament);
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
