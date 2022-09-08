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
    public class Sec_PatientController : Controller
    {
        private Clinique_dbEntities db = new Clinique_dbEntities();

        // GET: Patient
        public async Task<ActionResult> Index()
        {
            return View(await db.Patients.ToListAsync());
        }

        //Get all

        public ActionResult GetAll()
        {
            return PartialView(db.Patients.ToList());
        }

        // GET: Patient/Details/5
        public async Task<ActionResult> Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Patient patient = await db.Patients.FindAsync(id);
            if (patient == null)
            {
                return HttpNotFound();
            }
            return View(patient);
        }



        // GET: Patient/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Patient/Create
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Code,Nom,Prenom,NomComplet,DateNaissance,Adresse,Telephone,Email,Sexe,AntecedentFamiliaux,AntecedentChirucauxPersonnel,AntecedentMedicauxPersonnel,Allergie,SituationMatrimogniale")] Patient patient)
        {
            int cod = 0;
            var patients = db.Patients;

            List<string> listCode = new List<string>();
            List<int> listCodeInt = new List<int>(); ;


            foreach (var i in patients)
            {
                listCode.Add(i.Code);
            }

            foreach (var c in listCode)
            {
                int co = 0;
                bool a = int.TryParse(c, out co) ? true : false;
                if (a == true)
                {

                    listCodeInt.Add(co);
                }
            }

            if (listCodeInt == null)
            {
                cod = 0;
            }
            if (listCodeInt != null)
            {
                cod = listCodeInt.Max() + 1;
            }



            patient.NomComplet = patient.Nom + " " + patient.Prenom;
            patient.Code = cod.ToString();



            if (ModelState.IsValid)
            {
                db.Patients.Add(patient);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(patient);
        }

        // GET: Patient/Edit/5
        public async Task<ActionResult> Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Patient patient = await db.Patients.FindAsync(id);
            if (patient == null)
            {
                return HttpNotFound();
            }
            return View(patient);
        }

        // POST: Patient/Edit/5
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Code,Nom,Prenom,NomComplet,DateNaissance,Adresse,Telephone,Email,Sexe,AntecedentFamiliaux,AntecedentChirucauxPersonnel,AntecedentMedicauxPersonnel,Allergie,SituationMatrimogniale")] Patient patient)
        {
            if (ModelState.IsValid)
            {
                db.Entry(patient).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(patient);
        }

        // GET: Patient/Delete/5
        public async Task<ActionResult> Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Patient patient = await db.Patients.FindAsync(id);
            if (patient == null)
            {
                return HttpNotFound();
            }
            return View(patient);
        }

        // POST: Patient/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(string id)
        {
            Patient patient = await db.Patients.FindAsync(id);
            db.Patients.Remove(patient);
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
        //private Clinique_dbEntities db = new Clinique_dbEntities();

        //// GET: Sec_Patient
        //public async Task<ActionResult> Index()
        //{
        //    return View(await db.Patients.ToListAsync());
        //}

        //// GET: Sec_Patient/Details/5
        //public async Task<ActionResult> Details(string id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Patient patient = await db.Patients.FindAsync(id);
        //    if (patient == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(patient);
        //}

        //// GET: Sec_Patient/Create
        //public ActionResult Create()
        //{
        //    return View();
        //}

        //// POST: Sec_Patient/Create
        //// Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        //// plus de détails, voir  https://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<ActionResult> Create([Bind(Include = "Code,Nom,Prenom,NomComplet,DateNaissance,Adresse,Telephone,Email,Sexe,AntecedentFamiliaux,AntecedentChirucauxPersonnel,AntecedentMedicauxPersonnel,Allergie,SituationMatrimogniale")] Patient patient)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.Patients.Add(patient);
        //        await db.SaveChangesAsync();
        //        return RedirectToAction("Index");
        //    }

        //    return View(patient);
        //}

        //// GET: Sec_Patient/Edit/5
        //public async Task<ActionResult> Edit(string id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Patient patient = await db.Patients.FindAsync(id);
        //    if (patient == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(patient);
        //}

        //// POST: Sec_Patient/Edit/5
        //// Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        //// plus de détails, voir  https://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<ActionResult> Edit([Bind(Include = "Code,Nom,Prenom,NomComplet,DateNaissance,Adresse,Telephone,Email,Sexe,AntecedentFamiliaux,AntecedentChirucauxPersonnel,AntecedentMedicauxPersonnel,Allergie,SituationMatrimogniale")] Patient patient)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.Entry(patient).State = EntityState.Modified;
        //        await db.SaveChangesAsync();
        //        return RedirectToAction("Index");
        //    }
        //    return View(patient);
        //}

        //// GET: Sec_Patient/Delete/5
        //public async Task<ActionResult> Delete(string id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Patient patient = await db.Patients.FindAsync(id);
        //    if (patient == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(patient);
        //}

        //// POST: Sec_Patient/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public async Task<ActionResult> DeleteConfirmed(string id)
        //{
        //    Patient patient = await db.Patients.FindAsync(id);
        //    db.Patients.Remove(patient);
        //    await db.SaveChangesAsync();
        //    return RedirectToAction("Index");
        //}

        //protected override void Dispose(bool disposing)
        //{
        //    if (disposing)
        //    {
        //        db.Dispose();
        //    }
        //    base.Dispose(disposing);
        //}
    }
}
