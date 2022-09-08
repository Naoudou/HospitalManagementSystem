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
using System.Text;
using System.IO;

namespace HospitalManagementSystem.Controllers
{
    [Authorize]
    public class CompteController : Controller
    {
        private Clinique_dbEntities db = new Clinique_dbEntities();

        // GET: Compte
        public async Task<ActionResult> Index()
        {
            var comptes = db.Comptes.Include(c => c.Configuration).Include(c => c.Personnel);
            return View(await comptes.ToListAsync());
        }

        

     public ActionResult GetAll()
        {
            var comptes = db.Comptes.Include(c => c.Configuration).Include(c => c.Personnel);
            return PartialView( comptes.ToList());
        }

        // GET: Compte/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Compte compte = await db.Comptes.FindAsync(id);
            if (compte == null)
            {
                return HttpNotFound();
            }
            return View(compte);
        }

        // GET: Compte/Create
        public ActionResult Create()
        {
            ViewBag.IdRole = new SelectList(db.Configurations.Where(x => x.Libelle.Equals("Administrateur") || x.Libelle.Equals("Simple utilisateur")), "IdConfiguration", "Libelle");
            ViewBag.CodePersonnel = new SelectList(db.Personnels, "Code", "NomComplet");
            return View();
        }


        public ActionResult Profil()
        {

            return View();
        }

        [HttpPost]
        public async Task<ActionResult> SaveProfil(HttpPostedFileBase imgfile, string code)
        {

            if (imgfile != null && imgfile.ContentLength > 0)
            {
                Personnel personnel = await db.Personnels.FindAsync(code);
                string imgname = Path.GetFileName(imgfile.FileName);
                string imgext = Path.GetExtension(imgname);
                imgname = personnel.Code + imgext;
                string imgpath = Path.Combine(Server.MapPath("~/Img"), imgname);
                imgfile.SaveAs(imgpath);
                string p = imgpath.Substring(15);
               // User u = await db.Users.FindAsync(account.User.Matricule);
                personnel.Photo = imgname;
                db.Entry(personnel).State = EntityState.Modified;
                await db.SaveChangesAsync();
            }


            return View("Profil");
        }


        // POST: Compte/Create
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,Login,MotDePasse,Statut,CodePersonnel,IdRole")] Compte compte)
        {
            if (ModelState.IsValid)
            {
                db.Comptes.Add(compte);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.IdRole = new SelectList(db.Configurations.Where(x=>x.Libelle.Equals("Administrateur") || x.Libelle.Equals("Simple utilisateur")), "IdConfiguration", "Libelle", compte.IdRole);
            ViewBag.CodePersonnel = new SelectList(db.Personnels, "Code", "NomComplet", compte.CodePersonnel);
            return View(compte);
        }

        // GET: Compte/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Compte compte = await db.Comptes.FindAsync(id);
            if (compte == null)
            {
                return HttpNotFound();
            }
            ViewBag.IdRole = new SelectList(db.Configurations.Where(x => x.Libelle.Equals("Administrateur") || x.Libelle.Equals("Simple utilisateur")), "IdConfiguration", "Libelle", compte.IdRole);
            ViewBag.CodePersonnel = new SelectList(db.Personnels, "Code", "NomComplet", compte.CodePersonnel);
            return View(compte);
        }

        // POST: Compte/Edit/5
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,Login,MotDePasse,Statut,CodePersonnel,IdRole")] Compte compte)
        {
            if (ModelState.IsValid)
            {

                compte.MotDePasse = CreateMD5(compte.MotDePasse);


                db.Entry(compte).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.IdRole = new SelectList(db.Configurations.Where(x => x.Libelle.Equals("Administrateur") || x.Libelle.Equals("Simple utilisateur")), "IdConfiguration", "Libelle", compte.IdRole);
            ViewBag.CodePersonnel = new SelectList(db.Personnels, "Code", "NomComplet", compte.CodePersonnel);
            return View(compte);
        }


        public static string CreateMD5(string input)
        {
            // Use input string to calculate MD5 hash
            using (System.Security.Cryptography.MD5 md5 = System.Security.Cryptography.MD5.Create())
            {
                byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(input);
                byte[] hashBytes = md5.ComputeHash(inputBytes);

                // Convert the byte array to hexadecimal string
                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < hashBytes.Length; i++)
                {
                    sb.Append(hashBytes[i].ToString("X2"));
                }
                return sb.ToString();
            }
        }

        // GET: Compte/Delete/5
        public async Task<ActionResult> ActiverDesactiver(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Compte compte = await db.Comptes.FindAsync(id);
            if (compte == null)
            {
                return HttpNotFound();
            }
            return View(compte);
        }

        // POST: Compte/Delete/5
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        public async Task<ActionResult> ActiverDesactiverConfirmed(int id)
        {
            Compte compte = await db.Comptes.FindAsync(id);
            if (compte.Statut == 1)
            {
                compte.Statut = 0;
            }
            else
            {
                compte.Statut = 1;
            }
            db.Entry(compte).State = EntityState.Modified;
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
