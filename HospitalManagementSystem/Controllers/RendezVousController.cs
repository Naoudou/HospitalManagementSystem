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
    public class RendezVousController : Controller
    {
        private Clinique_dbEntities db = new Clinique_dbEntities();

        // GET: RendezVous
        public async Task<ActionResult> Index()
        {
            var rendezVous = db.RendezVous.Where(x => x.Date.Day.Equals(DateTime.Now.Day)
           && x.Date.Year.Equals(DateTime.Now.Year) && x.Date.Month.Equals(DateTime.Now.Month)).Include(r => r.Configuration).Include(r => r.Patient).Include(r => r.Personnel);
            return View(await rendezVous.ToListAsync());


            // var rendezVous = db.RendezVous.Include(r => r.Configuration).Include(r => r.Patient).Include(r => r.Personnel);
            // return View(await rendezVous.ToListAsync());


        }
        public ActionResult GetRdvDuJour()
        {
            var rendezVous = db.RendezVous.Where(x => x.Date.Day.Equals(DateTime.Now.Day)
            && x.Date.Year.Equals(DateTime.Now.Year) && x.Date.Month.Equals(DateTime.Now.Month)).Include(r => r.Configuration).Include(r => r.Patient).Include(r => r.Personnel);
            return PartialView(rendezVous.ToList());
        }
        
        // GET: RendezVous/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RendezVou rendezVou = await db.RendezVous.FindAsync(id);
            if (rendezVou == null)
            {
                return HttpNotFound();
            }
            return View(rendezVou);
        }

        // GET: RendezVous/Create
        public ActionResult Create()
        {
            ViewBag.IdType = new SelectList(db.Configurations.Where(x => x.Nature.Libelle.Equals("Type Rendez-vous")), "IdConfiguration", "Libelle");
            ViewBag.CodePatient = new SelectList(db.Patients, "Code", "Nom");
            ViewBag.CodeMedecin = new SelectList(db.Personnels.Where(x => x.Configuration2.Libelle.Equals("Médecin")), "Code", "Nom");
            return View();
        }

        public ActionResult CreateRdvPartiel()
        {
            ViewBag.IdType = new SelectList(db.Configurations.Where(x=>x.Nature.Libelle.Equals("Type Rendez-vous")), "IdConfiguration", "Libelle");
            ViewBag.CodePatient = new SelectList(db.Patients, "Code", "Nom");
            ViewBag.CodeMedecin = new SelectList(db.Personnels.Where(x=>x.Configuration2.Libelle.Equals("Médecin")), "Code", "Nom");
            return PartialView("CreateRdvPartiel");
        }

        // POST: RendezVous/Create
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,IdType,CodePatient,CodeMedecin,Date,Statut,Raison")] RendezVou rendezVou)
        {
            rendezVou.IdType = 9;
            rendezVou.Statut = 0;
            if (ModelState.IsValid)
            {
                db.RendezVous.Add(rendezVou);
                await db.SaveChangesAsync();
                return RedirectToAction("Index","Consultation");
            }

            ViewBag.IdType = new SelectList(db.Configurations.Where(x => x.Nature.Libelle.Equals("Type Rendez-vous")), "IdConfiguration", "Libelle", rendezVou.IdType);
            ViewBag.CodePatient = new SelectList(db.Patients, "Code", "Nom", rendezVou.CodePatient);
            ViewBag.CodeMedecin = new SelectList(db.Personnels.Where(x => x.Configuration2.Libelle.Equals("Médecin")), "Code", "Nom", rendezVou.CodeMedecin);
            return View(rendezVou);
        }

        // GET: RendezVous/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RendezVou rendezVou = await db.RendezVous.FindAsync(id);
            if (rendezVou == null)
            {
                return HttpNotFound();
            }
            ViewBag.IdType = new SelectList(db.Configurations, "IdConfiguration", "Libelle", rendezVou.IdType);
            ViewBag.CodePatient = new SelectList(db.Patients, "Code", "Nom", rendezVou.CodePatient);
            ViewBag.CodeMedecin = new SelectList(db.Personnels, "Code", "Nom", rendezVou.CodeMedecin);
            return View(rendezVou);
        }

        // POST: RendezVous/Edit/5
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,IdType,CodePatient,CodeMedecin,Date,Statut,Raison")] RendezVou rendezVou)
        {
            if (ModelState.IsValid)
            {
                db.Entry(rendezVou).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.IdType = new SelectList(db.Configurations, "IdConfiguration", "Libelle", rendezVou.IdType);
            ViewBag.CodePatient = new SelectList(db.Patients, "Code", "Nom", rendezVou.CodePatient);
            ViewBag.CodeMedecin = new SelectList(db.Personnels, "Code", "Nom", rendezVou.CodeMedecin);
            return View(rendezVou);
        }

        // GET: RendezVous/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RendezVou rendezVou = await db.RendezVous.FindAsync(id);
            if (rendezVou == null)
            {
                return HttpNotFound();
            }
            return View(rendezVou);
        }

        // POST: RendezVous/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            RendezVou rendezVou = await db.RendezVous.FindAsync(id);
            db.RendezVous.Remove(rendezVou);
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
