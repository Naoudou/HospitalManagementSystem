using HospitalManagementSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HospitalManagementSystem.Controllers
{
    [Authorize]
    public class DossierController : Controller
    {

        private Clinique_dbEntities db = new Clinique_dbEntities();
        // GET: Dossier
        public ActionResult Index(string code)
        {
            var consultations = db.ActePoses.Where(x=>x.Fiche.CodePatient.Equals(code)).ToList();
            //var consPatients = consultations.Where(x => x.CodePatient.Equals(code));
            //List<Dossier> dossiers = new List<Dossier>();
            //Patient p = db.Patients.Find(code);
            //foreach(var c in consPatients)
            //{

            //    dossiers.Add(new Dossier(p.NomComplet, c.Date, DateTime.Now, p.Adresse, p.Telephone, p.Email, p.Sexe, p.AntecedentFamiliaux, p.AntecedentChirucauxPersonnel, p.AntecedentMedicauxPersonnel, p.Allergie, p.SituationMatrimogniale,
            //        p.Parametres.Where(x => x.Date.Day.Equals(c.Date.Day) && x.Date.Year.Equals(c.Date.Year) && x.Date.Month.Equals(c.Date.Month)).FirstOrDefault().Temperature,
            //        p.Parametres.Where(x => x.Date.Day.Equals(c.Date.Day) && x.Date.Year.Equals(c.Date.Year) && x.Date.Month.Equals(c.Date.Month)).FirstOrDefault().Poids,
            //        p.Parametres.Where(x => x.Date.Day.Equals(c.Date.Day) && x.Date.Year.Equals(c.Date.Year) && x.Date.Month.Equals(c.Date.Month)).FirstOrDefault().Tention,
            //        p.Parametres.Where(x => x.Date.Day.Equals(c.Date.Day) && x.Date.Year.Equals(c.Date.Year) && x.Date.Month.Equals(c.Date.Month)).FirstOrDefault().Taille,
            //        p.Parametres.Where(x => x.Date.Day.Equals(c.Date.Day) && x.Date.Year.Equals(c.Date.Year) && x.Date.Month.Equals(c.Date.Month)).FirstOrDefault().FrequenceCardiaque,
            //        p.Parametres.Where(x => x.Date.Day.Equals(c.Date.Day) && x.Date.Year.Equals(c.Date.Year) && x.Date.Month.Equals(c.Date.Month)).FirstOrDefault().FrequenceRespiratoire
            //     , c.SigneClinique, c.Motif, c.ExamenPhysique, c.ResumeSyndromique, c.HistoireMaladie, c.Diagnostique, c.LibelleActe.Libelle, "", DateTime.Now, ""));
            //}

            return View();
        }

        public ActionResult GetDossier()
        {
            var consultations = db.ActePoses.Where(x => x.Fiche.CodePatient.Equals("AvSA260720")).ToList();
            

            return PartialView(consultations);
        }

        // GET: Dossier/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Dossier/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Dossier/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Dossier/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Dossier/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Dossier/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Dossier/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
