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
    public class PlanningController : Controller
    {
        private Clinique_dbEntities db = new Clinique_dbEntities();

        // GET: Planning
        public async Task<ActionResult> Index()
        {
            var plannings = db.Plannings.Include(p => p.Personnel);
            return View(await plannings.ToListAsync());
        }

        // GET: Planning/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Planning planning = await db.Plannings.FindAsync(id);
            if (planning == null)
            {
                return HttpNotFound();
            }
            return View(planning);
        }

        // GET: Planning/Create
        public ActionResult Create()
        {
            ViewBag.CodePersonnel = new SelectList(db.Personnels, "Code", "Nom");
            return View();
        }

        // POST: Planning/Create
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,DateDebut,DateFin,HeureDebut,HeureFin,CodePersonnel")] Planning planning)
        {
            if (ModelState.IsValid)
            {
                db.Plannings.Add(planning);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.CodePersonnel = new SelectList(db.Personnels, "Code", "NomComplet", planning.CodePersonnel);
            return View(planning);
        }

        // GET: Planning/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Planning planning = await db.Plannings.FindAsync(id);
            if (planning == null)
            {
                return HttpNotFound();
            }
            ViewBag.CodePersonnel = new SelectList(db.Personnels, "Code", "NomComplet", planning.CodePersonnel);
            return View(planning);
        }

        // POST: Planning/Edit/5
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,DateDebut,DateFin,HeureDebut,HeureFin,CodePersonnel")] Planning planning)
        {
            if (ModelState.IsValid)
            {
                db.Entry(planning).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.CodePersonnel = new SelectList(db.Personnels, "Code", "NomComplet", planning.CodePersonnel);
            return View(planning);
        }

        // GET: Planning/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Planning planning = await db.Plannings.FindAsync(id);
            if (planning == null)
            {
                return HttpNotFound();
            }
            return View(planning);
        }

        // POST: Planning/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Planning planning = await db.Plannings.FindAsync(id);
            db.Plannings.Remove(planning);
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
