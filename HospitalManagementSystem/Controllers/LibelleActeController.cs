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
    public class LibelleActeController : Controller
    {
        private Clinique_dbEntities db = new Clinique_dbEntities();

        // GET: LibelleActe
        public async Task<ActionResult> Index()
        {
            var libelleActes = db.LibelleActes.Include(l => l.FamilleActe1);
            return View(await libelleActes.ToListAsync());
        }

        // GET: LibelleActe/Details/5
        public async Task<ActionResult> Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LibelleActe libelleActe = await db.LibelleActes.FindAsync(id);
            if (libelleActe == null)
            {
                return HttpNotFound();
            }
            return View(libelleActe);
        }

        // GET: LibelleActe/Create
        public ActionResult Create()
        {
            ViewBag.FamilleActe = new SelectList(db.FamilleActes, "Id", "Libelle");
            return View();
        }

        // POST: LibelleActe/Create
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Code,Libelle,FamilleActe,Description,Prix")] LibelleActe libelleActe)
        {
            if (ModelState.IsValid)
            {
                db.LibelleActes.Add(libelleActe);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.FamilleActe = new SelectList(db.FamilleActes, "Id", "Libelle", libelleActe.FamilleActe);
            return View(libelleActe);
        }

        // GET: LibelleActe/Edit/5
        public async Task<ActionResult> Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LibelleActe libelleActe = await db.LibelleActes.FindAsync(id);
            if (libelleActe == null)
            {
                return HttpNotFound();
            }
            ViewBag.FamilleActe = new SelectList(db.FamilleActes, "Id", "Libelle", libelleActe.FamilleActe);
            return View(libelleActe);
        }

        // POST: LibelleActe/Edit/5
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Code,Libelle,FamilleActe,Description,Prix")] LibelleActe libelleActe)
        {
            if (ModelState.IsValid)
            {
                db.Entry(libelleActe).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.FamilleActe = new SelectList(db.FamilleActes, "Id", "Libelle", libelleActe.FamilleActe);
            return View(libelleActe);
        }

        // GET: LibelleActe/Delete/5
        public async Task<ActionResult> Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LibelleActe libelleActe = await db.LibelleActes.FindAsync(id);
            if (libelleActe == null)
            {
                return HttpNotFound();
            }
            return View(libelleActe);
        }

        // POST: LibelleActe/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(string id)
        {
            LibelleActe libelleActe = await db.LibelleActes.FindAsync(id);
            db.LibelleActes.Remove(libelleActe);
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
