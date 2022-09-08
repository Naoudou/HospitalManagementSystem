using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HospitalManagementSystem.Models;
using Newtonsoft.Json;


namespace HospitalManagementSystem.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private Clinique_dbEntities db = new Clinique_dbEntities();
        public ActionResult Index()
        {
            
            List<DataPoint> dataPointsFemme = new List<DataPoint>();
            List<DataPoint> dataPointsHomme = new List<DataPoint>();

            var consultationFemme = db.ActePoses.Where(x => x.Fiche.Patient.Sexe == 0 && x.LibelleActe.Libelle=="Consultation");
            var consultationHomme = db.ActePoses.Where(x => x.Fiche.Patient.Sexe == 1 && x.LibelleActe.Libelle == "Consultation");
            var totalConsultation = db.ActePoses.Where(x => x.CodeActeMedicale == "CONS_SPE").Count();
            var totalVisite = db.Fiches.Count();
            var totalExamen = db.ActePoses.Where(x => x.CodeActeMedicale != "CONS_SPE").Count();

            var consultationDuJour = db.ActePoses.Where(x => x.Date.Day.Equals(DateTime.Now.Day)
              && x.Date.Year.Equals(DateTime.Now.Year) && x.Date.Month.Equals(DateTime.Now.Month) && x.CodeActeMedicale == "CONS_SPE").Count();

            var ExamenDuJour = db.ActePoses.Where(x => x.Date.Day.Equals(DateTime.Now.Day)
             && x.Date.Year.Equals(DateTime.Now.Year) && x.Date.Month.Equals(DateTime.Now.Month) && x.CodeActeMedicale != "CONS_SPE").Count();

            //creation d'une liste des patients des jour d'avant puis leur id 
            //var ficheDeLaVeille = db.Fiches.Where(x => x.Date.Date < DateTime.Now.Date);
            //List<string> lisCodePatient = new List<string>();
            //foreach(var f in ficheDeLaVeille)
            //{
            //    lisCodePatient.Add(f.CodePatient);
            //}

            //bool isExite(string patient)
            //{
            //    return lisCodePatient.Contains(patient);
            //}

            //var NouveauPatient = db.ActePoses.Where(x => x.Date.Day.Equals(DateTime.Now.Day)
            //&& x.Date.Year.Equals(DateTime.Now.Year) && x.Date.Month.Equals(DateTime.Now.Month) && x. ).Count();
            foreach (var c in consultationFemme)
            {
                
               // dataPointsFemme.Add(new DataPoint(c. Date.Date.ToString(), c.Fiche.ActePoses.Where(x => x.LibelleActe.Libelle == "Consultation").Count()));
                dataPointsFemme.Add(new DataPoint(c. Date.Date.ToString(), c.Fiche.ActePoses.Count()));
            }

            foreach (var c in consultationHomme)
            {
               // dataPointsHomme.Add(new DataPoint(c.Date.Date.ToString(), c.Fiche.ActePoses.Where(x => x.LibelleActe.Libelle == "Consultation").Count()));
                dataPointsHomme.Add(new DataPoint(c.Date.Date.ToString(), c.Fiche.ActePoses.Count()));
            }

           
            ViewBag.DataPointsHomme = JsonConvert.SerializeObject(dataPointsHomme);
            ViewBag.DataPointsFemme = JsonConvert.SerializeObject(dataPointsFemme);
            ViewBag.NombreTotalConsultation = totalConsultation;
            ViewBag.NombreTotalVisite =totalVisite;
            ViewBag.NombreTotalExamen =totalExamen;
            ViewBag.NombreConsultationDuJour = consultationDuJour;
            ViewBag.NombreExamenDuJour = ExamenDuJour;
            ViewBag.NombreTotalDuJour = ExamenDuJour + consultationDuJour;


            return View();
        }

        public ActionResult Index2()
        {
            var ListeActes = db.ActePoses.Where(x => x.Date.Day.Equals(DateTime.Now.Day)
            && x.Date.Year.Equals(DateTime.Now.Year) && x.Date.Month.Equals(DateTime.Now.Month));
            List<BilanJournalier> bilanJournaliers = new List<BilanJournalier>();

            foreach(var b in ListeActes)
            {
                bilanJournaliers.Add(new BilanJournalier(b.LibelleActe.Libelle,b.Prix,b.Prix));
            }


            return View(bilanJournaliers);
        }

        public ActionResult Partial_BilanJournalier()
        {
            var ListeActes = db.ActePoses.Where(x => x.Date.Day.Equals(DateTime.Now.Day)
            && x.Date.Year.Equals(DateTime.Now.Year) && x.Date.Month.Equals(DateTime.Now.Month));
            List<BilanJournalier> bilanJournaliers = new List<BilanJournalier>();

            foreach (var b in ListeActes)
            {
                bilanJournaliers.Add(new BilanJournalier(b.LibelleActe.Libelle, b.Prix, b.Prix));
            }


            return PartialView(bilanJournaliers);
        }

        public ActionResult Partial_bilan_du_Jour_x(DateTime dateTime)
        {
            var ListeActes = db.ActePoses.Where(x => x.Date.Day.Equals(dateTime.Day)
            && x.Date.Year.Equals(dateTime.Year) && x.Date.Month.Equals(dateTime.Month));
            List<BilanJournalier> bilanJournaliers = new List<BilanJournalier>();

            foreach (var b in ListeActes)
            {
                bilanJournaliers.Add(new BilanJournalier(b.LibelleActe.Libelle, b.Prix, b.Prix));
            }


            return PartialView(bilanJournaliers);
        }

        //public ActionResult Contact()
        //{
        //    ViewBag.Message = "Your contact page.";

        //    return View();
        //}
    }
}