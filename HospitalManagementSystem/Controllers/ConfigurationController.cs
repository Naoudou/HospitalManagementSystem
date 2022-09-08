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
    public class ConfigurationController : Controller
    {
        private Clinique_dbEntities db = new Clinique_dbEntities();

        // GET: Configuration
        public async Task<ActionResult> Index()
        {
            var configurations = db.Configurations.Include(c => c.Nature);
            return View(await configurations.ToListAsync());
        }

        // GET: Configuration/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Configuration configuration = await db.Configurations.FindAsync(id);
            if (configuration == null)
            {
                return HttpNotFound();
            }
            return View(configuration);
        }

        // GET: Configuration/Create
        public ActionResult Create()
        {
            ViewBag.IdNature = new SelectList(db.Natures, "IdNature", "Libelle");
            return View();
        }

        // POST: Configuration/Create
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "IdConfiguration,Libelle,IdNature")] Configuration configuration)
        {
            if (ModelState.IsValid)
            {
                db.Configurations.Add(configuration);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.IdNature = new SelectList(db.Natures, "IdNature", "Libelle", configuration.IdNature);
            return View(configuration);
        }

        // GET: Configuration/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Configuration configuration = await db.Configurations.FindAsync(id);
            if (configuration == null)
            {
                return HttpNotFound();
            }
            ViewBag.IdNature = new SelectList(db.Natures, "IdNature", "Libelle", configuration.IdNature);
            return View(configuration);
        }

        // POST: Configuration/Edit/5
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "IdConfiguration,Libelle,IdNature")] Configuration configuration)
        {
            if (ModelState.IsValid)
            {
                db.Entry(configuration).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.IdNature = new SelectList(db.Natures, "IdNature", "Libelle", configuration.IdNature);
            return View(configuration);
        }

        // GET: Configuration/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Configuration configuration = await db.Configurations.FindAsync(id);
            if (configuration == null)
            {
                return HttpNotFound();
            }
            return View(configuration);
        }

        // POST: Configuration/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Configuration configuration = await db.Configurations.FindAsync(id);
            db.Configurations.Remove(configuration);
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
