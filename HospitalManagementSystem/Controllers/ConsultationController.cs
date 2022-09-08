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
    public class ConsultationController : Controller
    {
        private Clinique_dbEntities db = new Clinique_dbEntities();

        // GET: Consultation
        public async Task<ActionResult> Index()
        {
            var actePoses = db.ActePoses.Where(x=>x.Fiche.Paiement==3 && x.Date.Day.Equals(DateTime.Now.Day) 
            && x.Date.Year.Equals(DateTime.Now.Year) && x.Date.Month.Equals(DateTime.Now.Month) && x.LibelleActe.Libelle =="Consultation") .Include(a => a.Fiche).Include(a => a.LibelleActe).Include(a => a.Personnel).Include(a => a.Personnel1).Include(a => a.Personnel2);
            return View(await actePoses.ToListAsync());
        }


        //validation du paiement d'un examen
        public async Task<ActionResult> Payer(int? id)
        {
            ActePose actePose = await db.ActePoses.FindAsync(id);
            if (actePose.Paiement == 0)
            {
                actePose.Paiement = 1;
            }
            else
            {
                actePose.Paiement = 0;
            }

            db.Entry(actePose).State = EntityState.Modified;
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        //envoie la liste des attent de confirmation de vrai d'examen
        public ActionResult GetListFraisExamen()
        {
            var actePoses = db.ActePoses.Where(x => x.Fiche.Paiement == 3 && x.Date.Day.Equals(DateTime.Now.Day)
            && x.Date.Year.Equals(DateTime.Now.Year) && x.Date.Month.Equals(DateTime.Now.Month)).Include(a => a.Fiche)
            .Include(a => a.LibelleActe).Include(a => a.Personnel).Include(a => a.Personnel1).Include(a => a.Personnel2);
            return PartialView( actePoses.ToList());
        }

        //Renvoir la vue des patient ayant payé leur consultation
        public ActionResult AttenteConsultation()
        {

            var fiches = db.Fiches.Where(x => x.Paiement ==1 && x.Date.Day. Equals(DateTime.Now.Day) 
            
            && x.Date.Year.Equals(DateTime.Now.Year) && x.Date.Month.Equals(DateTime.Now.Month) ).Include(f => f.Configuration).Include(f => f.LibelleActe).Include(f => f.Patient).Include(f => f.Personnel);

            return PartialView("AttenteConsultation", fiches.AsQueryable()); 
        }



        // GET: Consultation/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ActePose actePose = await db.ActePoses.FindAsync(id);
            if (actePose == null)
            {
                return HttpNotFound();
            }
            return View(actePose);
        }

        // GET: Consultation/Create
        public ActionResult Create()
        {
            ViewBag.CodeFiche = new SelectList(db.Fiches, "Id", "Patient.NomComplet");
            ViewBag.CodeActeMedicale = new SelectList(db.LibelleActes, "Code", "Libelle");
            ViewBag.CodeMedecin = new SelectList(db.Personnels, "Code", "Nom");
            ViewBag.CodeInfirmier = new SelectList(db.Personnels, "Code", "Nom");
            ViewBag.CodeLaborantin = new SelectList(db.Personnels, "Code", "Nom");
            return View();
        }

        //action pour lancer une vue partielle de creation de la consultation
        public ActionResult Part_Create()
        {
            ViewBag.CodeFiche = new SelectList(db.Fiches, "Id", "Patient.NomComplet");
            ViewBag.CodeActeMedicale = new SelectList(db.LibelleActes, "Code", "Libelle");
            ViewBag.CodeMedecin = new SelectList(db.Personnels, "Code", "Nom");
            ViewBag.CodeInfirmier = new SelectList(db.Personnels, "Code", "Nom");
            ViewBag.CodeLaborantin = new SelectList(db.Personnels, "Code", "Nom");
            return PartialView();

        }
             //action pour lancer une vue partielle de creation de la consultation
        public ActionResult Part_Create_Examen()
        {
            ViewBag.CodeFiche = new SelectList(db.Fiches, "Id", "Patient.NomComplet");
            ViewBag.CodeActeMedicale = new SelectList(db.LibelleActes, "Code", "Libelle");
            ViewBag.CodeFamilleActe = new SelectList(db.FamilleActes, "Id", "Libelle");
            ViewBag.CodeMedecin = new SelectList(db.Personnels, "Code", "Nom");
            ViewBag.CodeInfirmier = new SelectList(db.Personnels, "Code", "Nom");
            ViewBag.CodeLaborantin = new SelectList(db.Personnels, "Code", "Nom");
            return PartialView();
        }

        [HttpGet]
        public ActionResult GetActeAjax(int familleActe)
        {
            var libelleActes = db.LibelleActes.Where(x => x.FamilleActe1.Id.Equals(familleActe)).ToList();
            var l = libelleActes.ToList();
            List<J_LibelleActe> j_LibelleActes = new List<J_LibelleActe>();
            foreach (var item in l)
            {
                j_LibelleActes.Add(new J_LibelleActe(item.Code, item.Libelle));
            }
            return Json(j_LibelleActes, JsonRequestBehavior.AllowGet);
        }


        [HttpGet]
        public ActionResult GetFamilleActeAjax()
        {
            var familleActes = db.FamilleActes.ToList();
            List<J_FamilleActe> j_FamilleActes = new List<J_FamilleActe>();
            foreach(var item in familleActes)
            {
               
                    j_FamilleActes.Add(new J_FamilleActe(item.Id, item.Libelle));
               
                
            }
            return Json(j_FamilleActes, JsonRequestBehavior.AllowGet);
        }

        // POST: Consultation/Create
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,CodeFiche,CodeActeMedicale,CodeMedecin,SigneClinique,ExamenPhysique,CodeInfirmier,CodeLaborantin,Paiement,Motif,ResumeSyndromique,HistoireMaladie,EnqueteSysteme,Diagnostique,Status,Prix,Date")] ActePose actePose)
        {
            //creation d'un objet fiche afin de changer sont paiement à 3 pour signifier que la consultation à eu lieu.
            var fiche = db.Fiches.Find(actePose.CodeFiche);
            //initialisation des champ inutils dans la consultation
            actePose.Date = DateTime.Now;
            actePose.CodeInfirmier = null;
            actePose.CodeLaborantin = null;

           // actePose.CodeMedecin = "NAFU120720";
            actePose.CodeActeMedicale = "CONS_SPE";
           // actePose.LibelleActe.Libelle = "Consultation";
            double? prix = db.LibelleActes.Where(x => x.Libelle == "Consultation").FirstOrDefault().Prix;
            if (prix != null)
            {
                actePose.Prix =(double) prix;
            }
            actePose.Status = 1;
            actePose.Paiement = 1;
            



            if (ModelState.IsValid)
            {
                db.ActePoses.Add(actePose);
               // await db.SaveChangesAsync();

                //Après la consultation, on change le statut afin que celui qui a déjà etait consulté n'aparaisse pas dans la liste d'attente
                fiche.Paiement = 3;
                db.Entry(fiche).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.CodeFiche = new SelectList(db.Fiches, "Id", "CodePatient", actePose.CodeFiche);
            ViewBag.CodeActeMedicale = new SelectList(db.LibelleActes, "Code", "Libelle", actePose.CodeActeMedicale);
            ViewBag.CodeMedecin = new SelectList(db.Personnels, "Code", "Nom", actePose.CodeMedecin);
            ViewBag.CodeInfirmier = new SelectList(db.Personnels, "Code", "Nom", actePose.CodeInfirmier);
            ViewBag.CodeLaborantin = new SelectList(db.Personnels, "Code", "Nom", actePose.CodeLaborantin);
            return View(actePose);
        }


        //creation d'un examen
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Part_Create_Examen([Bind(Include = "Id,CodeFiche,CodeActeMedicale,CodeMedecin,SigneClinique,ExamenPhysique,CodeInfirmier,CodeLaborantin,Paiement,Motif,ResumeSyndromique,HistoireMaladie,EnqueteSysteme,Diagnostique,Status,Prix,Date")] ActePose actePose)
        {
           
            //initialisation des champ inutils dans la consultation
            actePose.Date = DateTime.Now;
            actePose.CodeInfirmier = null;
            actePose.ExamenPhysique = null;
            actePose.EnqueteSysteme = null;
            actePose.HistoireMaladie= null;
            actePose.Personnel2= null;

           
            double? prix = db.LibelleActes.Where(x => x.Code.Equals(actePose.CodeActeMedicale)).FirstOrDefault().Prix;
            if (prix != null)
            {
                actePose.Prix = (double)prix;
            }
            actePose.Status = 0;
            actePose.Paiement = 0;




            if (ModelState.IsValid)
            {
                db.ActePoses.Add(actePose);
                
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.CodeFiche = new SelectList(db.Fiches, "Id", "CodePatient", actePose.CodeFiche);
            ViewBag.CodeActeMedicale = new SelectList(db.LibelleActes, "Code", "Libelle", actePose.CodeActeMedicale);
            ViewBag.CodeFamilleActe = new SelectList(db.FamilleActes, "Id", "Libelle");
            ViewBag.CodeMedecin = new SelectList(db.Personnels, "Code", "Nom", actePose.CodeMedecin);
            ViewBag.CodeInfirmier = new SelectList(db.Personnels, "Code", "Nom", actePose.CodeInfirmier);
            ViewBag.CodeLaborantin = new SelectList(db.Personnels, "Code", "Nom", actePose.CodeLaborantin);
            return View(actePose);
        }



        [HttpPost]
        public ActionResult ExamenPostAjax(List<string> listOfExams, int CodeFiche, string CodeMedecin)
        {
            ActePose actePose = new ActePose();
            List<ActePose> listExams = new List<ActePose>();

            //initialisation des champ inutils dans la consultation
            actePose.Date = DateTime.Now;
            actePose.CodeInfirmier = null;
            actePose.ExamenPhysique = null;
            actePose.EnqueteSysteme = null;
            actePose.HistoireMaladie = null;
            actePose.Personnel2 = null;
            actePose.CodeFiche = CodeFiche;
            actePose.CodeMedecin = CodeMedecin;
            actePose.Status = 0;
            actePose.Paiement = 0;

            foreach(var e in listOfExams)
            {
                
                actePose.CodeActeMedicale = e;
                double? prix = db.LibelleActes.Where(x => x.Code.Equals(actePose.CodeActeMedicale)).FirstOrDefault().Prix;
                if (prix != null)
                {
                    actePose.Prix = (double)prix;
                }
                ActePose a = new ActePose(actePose);
                listExams.Add(new ActePose(a));
            }

            //double? prix = db.LibelleActes.Where(x => x.Code.Equals(actePose.CodeActeMedicale)).FirstOrDefault().Prix;
            //if (prix != null)
            //{
            //    actePose.Prix = (double)prix;
            //}
            




            if (ModelState.IsValid)
            {
                foreach(var exa in listExams)
                {
                    db.ActePoses.Add(exa);
                    db.SaveChanges();
                }
               

                
                return Json(data: "ok", JsonRequestBehavior.AllowGet);
            }

            return Json(data:"error",JsonRequestBehavior.AllowGet);
        }


       



        [HttpGet]
        public ActionResult Test(int familleActe)
        {
            
            return Json(familleActe, JsonRequestBehavior.AllowGet);
        }
        // GET: Consultation/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ActePose actePose = await db.ActePoses.FindAsync(id);
            if (actePose == null)
            {
                return HttpNotFound();
            }
            ViewBag.CodeFiche = new SelectList(db.Fiches, "Id", "CodePatient", actePose.CodeFiche);
            ViewBag.CodeActeMedicale = new SelectList(db.LibelleActes, "Code", "Libelle", actePose.CodeActeMedicale);
            ViewBag.CodeMedecin = new SelectList(db.Personnels, "Code", "Nom", actePose.CodeMedecin);
            ViewBag.CodeInfirmier = new SelectList(db.Personnels, "Code", "Nom", actePose.CodeInfirmier);
            ViewBag.CodeLaborantin = new SelectList(db.Personnels, "Code", "Nom", actePose.CodeLaborantin);
            return View(actePose);
        }

        // POST: Consultation/Edit/5
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,CodeFiche,CodeActeMedicale,CodeMedecin,SigneClinique,ExamenPhysique,CodeInfirmier,CodeLaborantin,Paiement,Motif,ResumeSyndromique,HistoireMaladie,EnqueteSysteme,Diagnostique,Status,Prix,Date")] ActePose actePose)
        {
            if (ModelState.IsValid)
            {
                db.Entry(actePose).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.CodeFiche = new SelectList(db.Fiches, "Id", "CodePatient", actePose.CodeFiche);
            ViewBag.CodeActeMedicale = new SelectList(db.LibelleActes, "Code", "Libelle", actePose.CodeActeMedicale);
            ViewBag.CodeMedecin = new SelectList(db.Personnels, "Code", "Nom", actePose.CodeMedecin);
            ViewBag.CodeInfirmier = new SelectList(db.Personnels, "Code", "Nom", actePose.CodeInfirmier);
            ViewBag.CodeLaborantin = new SelectList(db.Personnels, "Code", "Nom", actePose.CodeLaborantin);
            return View(actePose);
        }

        // GET: Consultation/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ActePose actePose = await db.ActePoses.FindAsync(id);
            if (actePose == null)
            {
                return HttpNotFound();
            }
            return View(actePose);
        }

        // POST: Consultation/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            ActePose actePose = await db.ActePoses.FindAsync(id);
            db.ActePoses.Remove(actePose);
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
