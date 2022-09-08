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
    public class ParametreController : Controller
    {
        private Clinique_dbEntities db = new Clinique_dbEntities();

        // GET: Parametre
        public async Task<ActionResult> Index()
        {
            var parametres = db.Parametres.Include(p => p.Patient);
            return View(await parametres.ToListAsync());
        }

        // GET: Parametre/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Parametre parametre = await db.Parametres.FindAsync(id);
            if (parametre == null)
            {
                return HttpNotFound();
            }
            return View(parametre);
        }

        // GET: Patient/Details/5
        public ActionResult DetailsP(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Parametre parametre = db.Parametres.Where(x=> x.Date.Day.Equals(DateTime.Now.Day)
            && x.Date.Year.Equals(DateTime.Now.Year) && x.Date.Month.Equals(DateTime.Now.Month) && x.CodePatient.Equals(id) ).FirstOrDefault();
            if (parametre == null)
            {
                return HttpNotFound();
            }
            return PartialView(parametre);
        }
        // GET: Parametre/Create
        public ActionResult Create()
        {
            ViewBag.CodePatient = new SelectList(db.Patients, "Code", "Nom");
            return View();
        }
        public ActionResult Part_Create()
        {
            ViewBag.CodePatient = new SelectList(db.Patients, "Code", "Nom");
            return PartialView("Part_Create");
        }

        

        // POST: Parametre/Create
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,Tention,Temperature,Poids,Date,CodePatient,Glycemie,FrequenceCardiaque,FrequenceRespiratoire,Taille")] Parametre parametre)
        {
            DateTime d= DateTime.Now;
            parametre.Date = d;
            if (parametre!=null)
            {
                db.Parametres.Add(parametre);
                await db.SaveChangesAsync();
                return RedirectToAction("Index","Patient");
            }

            ViewBag.CodePatient = new SelectList(db.Patients, "Code", "Nom", parametre.CodePatient);
            return View(parametre);
        }

        // GET: Parametre/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Parametre parametre = await db.Parametres.FindAsync(id);
            if (parametre == null)
            {
                return HttpNotFound();
            }
            ViewBag.CodePatient = new SelectList(db.Patients, "Code", "Nom", parametre.CodePatient);
            return View(parametre);
        }

        // POST: Parametre/Edit/5
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,Tention,Temperature,Poids,Date,CodePatient,Glycemie,FrequenceCardiaque,FrequenceRespiratoire,Taille")] Parametre parametre)
        {
            if (ModelState.IsValid)
            {
                db.Entry(parametre).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.CodePatient = new SelectList(db.Patients, "Code", "Nom", parametre.CodePatient);
            return View(parametre);
        }

        // GET: Parametre/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Parametre parametre = await db.Parametres.FindAsync(id);
            if (parametre == null)
            {
                return HttpNotFound();
            }
            return View(parametre);
        }

        // POST: Parametre/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Parametre parametre = await db.Parametres.FindAsync(id);
            db.Parametres.Remove(parametre);
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
