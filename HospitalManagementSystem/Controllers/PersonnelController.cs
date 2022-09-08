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
    public class PersonnelController : Controller
    {
        private Clinique_dbEntities db = new Clinique_dbEntities();

        // GET: Personnel
        public async Task<ActionResult> Index()
        {
            var personnels = db.Personnels.Include(p => p.Configuration).Include(p => p.Configuration1).Include(p => p.Configuration2);
            return View(await personnels.ToListAsync());
        }

        // GET: Personnel/Details/5
        public async Task<ActionResult> Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Personnel personnel = await db.Personnels.FindAsync(id);
            if (personnel == null)
            {
                return HttpNotFound();
            }
            return View(personnel);
        }

        // GET: Personnel/Create
        public ActionResult Create()
        {
            ViewBag.IdSpecialite = new SelectList(db.Configurations.Where(x=>x.Nature.Libelle.Equals("Spécialité")), "IdConfiguration", "Libelle");
            ViewBag.IdService = new SelectList(db.Configurations.Where(x => x.Nature.Libelle.Equals("Service")), "IdConfiguration", "Libelle");
            ViewBag.IdTypePersonnel = new SelectList(db.Configurations.Where(x => x.Nature.Libelle.Equals("Qualité Employé")), "IdConfiguration", "Libelle");
            return View();
        }

        // POST: Personnel/Create
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Code,Nom,Prenom,NomComplet,DateNaissance,Adresse,Telephone,Email,Sexe,Photo,IdSpecialite,IdService,IdTypePersonnel")] Personnel personnel)
        {
            //Rendre auto increment le code d'un personel
            //int cod = 0;
            //var personnels = db.Personnels;

            //List<string> listCode = new List<string>();
            //List<int> listCodeInt = new List<int>(); ;


            //foreach (var i in personnels)
            //{
            //    listCode.Add(i.Code);
            //}

            //foreach (var c in listCode)
            //{
            //    int co = 0;
            //    bool a = int.TryParse(c, out co) ? true : false;
            //    if (a == true)
            //    {

            //        listCodeInt.Add(co);
            //    }
            //}

            //if (listCodeInt == null)
            //{
            //    cod = 0;
            //}
            //if (listCodeInt != null)
            //{
            //    cod = listCodeInt.Max() + 1;
            //}
            if (personnel.IdTypePersonnel !=10)
            {
                personnel.IdSpecialite = null;
            }


            personnel.NomComplet = personnel.Nom + " " + personnel.Prenom;
           // personnel.Code = cod.ToString();

            //create d'un compte de l'utilisateur qu'on crée
            Compte compte = new Compte();
            compte.Login = personnel.Email;
            compte.MotDePasse = personnel.Nom;
            compte.Statut = 1;
            compte.IdRole = 15;
            compte.CodePersonnel = personnel.Code;
            if (ModelState.IsValid)
            {
                db.Personnels.Add(personnel);
                db.Comptes.Add(compte);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.IdSpecialite = new SelectList(db.Configurations.Where(x => x.Nature.Libelle.Equals("Spécialité")), "IdConfiguration", "Libelle");
            ViewBag.IdService = new SelectList(db.Configurations.Where(x => x.Nature.Libelle.Equals("Service")), "IdConfiguration", "Libelle");
            ViewBag.IdTypePersonnel = new SelectList(db.Configurations.Where(x => x.Nature.Libelle.Equals("Qualité Employé")), "IdConfiguration", "Libelle");
            return View(personnel);
        }

        // GET: Personnel/Edit/5
        public async Task<ActionResult> Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Personnel personnel = await db.Personnels.FindAsync(id);
            if (personnel == null)
            {
                return HttpNotFound();
            }
            ViewBag.IdSpecialite = new SelectList(db.Configurations.Where(x => x.Nature.Libelle.Equals("Spécialité")), "IdConfiguration", "Libelle");
            ViewBag.IdService = new SelectList(db.Configurations.Where(x => x.Nature.Libelle.Equals("Service")), "IdConfiguration", "Libelle");
            ViewBag.IdTypePersonnel = new SelectList(db.Configurations.Where(x => x.Nature.Libelle.Equals("Qualité Employé")), "IdConfiguration", "Libelle");
            return View(personnel);
        }

        // POST: Personnel/Edit/5
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Code,Nom,Prenom,NomComplet,DateNaissance,Adresse,Telephone,Email,Sexe,Photo,IdSpecialite,IdService,IdTypePersonnel")] Personnel personnel)
        {
            if (ModelState.IsValid)
            {
                db.Entry(personnel).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.IdSpecialite = new SelectList(db.Configurations.Where(x => x.Nature.Libelle.Equals("Spécialité")), "IdConfiguration", "Libelle");
            ViewBag.IdService = new SelectList(db.Configurations.Where(x => x.Nature.Libelle.Equals("Service")), "IdConfiguration", "Libelle");
            ViewBag.IdTypePersonnel = new SelectList(db.Configurations.Where(x => x.Nature.Libelle.Equals("Qualité Employé")), "IdConfiguration", "Libelle");
            return View(personnel);
        }

        // GET: Personnel/Delete/5
        public async Task<ActionResult> Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Personnel personnel = await db.Personnels.FindAsync(id);
            if (personnel == null)
            {
                return HttpNotFound();
            }
            return View(personnel);
        }

        // POST: Personnel/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(string id)
        {
            Personnel personnel = await db.Personnels.FindAsync(id);
            db.Personnels.Remove(personnel);
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
