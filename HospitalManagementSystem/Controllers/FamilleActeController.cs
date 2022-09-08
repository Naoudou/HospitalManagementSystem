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
    public class FamilleActeController : Controller
    {
        private Clinique_dbEntities db = new Clinique_dbEntities();

        // GET: FamilleActe
        public async Task<ActionResult> Index()
        {
            return View(await db.FamilleActes.ToListAsync());
        }

        // GET: FamilleActe/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FamilleActe familleActe = await db.FamilleActes.FindAsync(id);
            if (familleActe == null)
            {
                return HttpNotFound();
            }
            return View(familleActe);
        }

        // GET: FamilleActe/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: FamilleActe/Create
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,Libelle,Description")] FamilleActe familleActe)
        {
            if (ModelState.IsValid)
            {
                db.FamilleActes.Add(familleActe);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(familleActe);
        }

        // GET: FamilleActe/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FamilleActe familleActe = await db.FamilleActes.FindAsync(id);
            if (familleActe == null)
            {
                return HttpNotFound();
            }
            return View(familleActe);
        }

        // POST: FamilleActe/Edit/5
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,Libelle,Description")] FamilleActe familleActe)
        {
            if (ModelState.IsValid)
            {
                db.Entry(familleActe).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(familleActe);
        }

        // GET: FamilleActe/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FamilleActe familleActe = await db.FamilleActes.FindAsync(id);
            if (familleActe == null)
            {
                return HttpNotFound();
            }
            return View(familleActe);
        }

        // POST: FamilleActe/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            FamilleActe familleActe = await db.FamilleActes.FindAsync(id);
            db.FamilleActes.Remove(familleActe);
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
