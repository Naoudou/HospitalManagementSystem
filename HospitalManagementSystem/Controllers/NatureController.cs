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
    public class NatureController : Controller
    {
        private Clinique_dbEntities db = new Clinique_dbEntities();

        // GET: Nature
        public async Task<ActionResult> Index()
        {
            return View(await db.Natures.ToListAsync());
        }

        // GET: Nature/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Nature nature = await db.Natures.FindAsync(id);
            if (nature == null)
            {
                return HttpNotFound();
            }
            return View(nature);
        }

        // GET: Nature/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Nature/Create
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "IdNature,Libelle")] Nature nature)
        {
            if (ModelState.IsValid)
            {
                db.Natures.Add(nature);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(nature);
        }

        // GET: Nature/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Nature nature = await db.Natures.FindAsync(id);
            if (nature == null)
            {
                return HttpNotFound();
            }
            return View(nature);
        }

        // POST: Nature/Edit/5
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "IdNature,Libelle")] Nature nature)
        {
            if (ModelState.IsValid)
            {
                db.Entry(nature).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(nature);
        }

        // GET: Nature/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Nature nature = await db.Natures.FindAsync(id);
            if (nature == null)
            {
                return HttpNotFound();
            }
            return View(nature);
        }

        // POST: Nature/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Nature nature = await db.Natures.FindAsync(id);
            db.Natures.Remove(nature);
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
