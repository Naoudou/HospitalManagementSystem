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
    public class PrescriptionController : Controller
    {
        private Clinique_dbEntities db = new Clinique_dbEntities();

        // GET: Prescription
        public async Task<ActionResult> Index()
        {
            var prescriptions = db.Prescriptions.Include(p => p.ActePose).Include(p => p.Medicament);
            return View(await prescriptions.ToListAsync());
        }

        // GET: Prescription/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Prescription prescription = await db.Prescriptions.FindAsync(id);
            if (prescription == null)
            {
                return HttpNotFound();
            }
            return View(prescription);
        }

        // GET: Prescription/Create
        public ActionResult Create()
        {
            ViewBag.IdConsultation = new SelectList(db.ActePoses, "Id", "CodeActeMedicale");
            ViewBag.IdMedicament = new SelectList(db.Medicaments, "Id", "NomCommercial");
            return View();
        }

        public ActionResult Part_Create_prescription()
        {
            ViewBag.IdConsultation = new SelectList(db.ActePoses, "Id", "CodeActeMedicale");
            ViewBag.IdMedicament = new SelectList(db.Medicaments, "Id", "NomCommercial");
            return PartialView("Part_Create_prescription");
        }

        //envoie asynchrone de la liste des médicament
        

         [HttpGet]
        public ActionResult GetMedocAjax()
        {
            var medicaments = db.Medicaments.ToList();
            List<J_Medicament> j_Medicaments = new List<J_Medicament>();
            foreach (var item in medicaments)
            {

                j_Medicaments.Add(new J_Medicament(item.Id, item.NomCommercial));


            }
            return Json(j_Medicaments, JsonRequestBehavior.AllowGet);
        }


        // POST: Prescription/Create
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,IdMedicament,IdConsultation,Date,Possologie")] Prescription prescription)
        {
            if (ModelState.IsValid)
            {
                db.Prescriptions.Add(prescription);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.IdConsultation = new SelectList(db.ActePoses, "Id", "CodeActeMedicale", prescription.IdConsultation);
            ViewBag.IdMedicament = new SelectList(db.Medicaments, "Id", "NomCommercial", prescription.IdMedicament);
            return View(prescription);
        }

        // GET: Prescription/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Prescription prescription = await db.Prescriptions.FindAsync(id);
            if (prescription == null)
            {
                return HttpNotFound();
            }
            ViewBag.IdConsultation = new SelectList(db.ActePoses, "Id", "CodeActeMedicale", prescription.IdConsultation);
            ViewBag.IdMedicament = new SelectList(db.Medicaments, "Id", "NomCommercial", prescription.IdMedicament);
            return View(prescription);
        }

        // POST: Prescription/Edit/5
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,IdMedicament,IdConsultation,Date,Possologie")] Prescription prescription)
        {
            if (ModelState.IsValid)
            {
                db.Entry(prescription).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.IdConsultation = new SelectList(db.ActePoses, "Id", "CodeActeMedicale", prescription.IdConsultation);
            ViewBag.IdMedicament = new SelectList(db.Medicaments, "Id", "NomCommercial", prescription.IdMedicament);
            return View(prescription);
        }

        // GET: Prescription/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Prescription prescription = await db.Prescriptions.FindAsync(id);
            if (prescription == null)
            {
                return HttpNotFound();
            }
            return View(prescription);
        }

        // POST: Prescription/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Prescription prescription = await db.Prescriptions.FindAsync(id);
            db.Prescriptions.Remove(prescription);
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
