using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace HospitalManagementSystem.Models
{
    public class BilanJournalier
    {
        public BilanJournalier()
        {
        }

        public BilanJournalier(string libelle, double prixUnitaire, double prixTotal)
        {
            Libelle = libelle;
            PrixUnitaire = prixUnitaire;
            PrixTotal = prixTotal;
        }

        public string Libelle { get; set; }
       
        public double PrixUnitaire { get; set; }
        public double PrixTotal { get; set; }
    }
    public class J_Medicament
    {
        public J_Medicament(int id, string libelle)
        {
            Id = id;
            Libelle = libelle;
        }

        public int Id { get; set; }
        public string Libelle { get; set; }
    }

    public class Dossier
    {
       

        public Dossier()
        {
        }

        public Dossier(string nomPatient, DateTime dateConsultation, DateTime dateNaissance, string adresse, string telephone, string email, int? sexe, string antecedentFamiliaux, string antecedentChirurgicaux, string antecedentMedicaux, string allergie, string situationMatrimoniale, double? temperature, double? poid, double? tention, double? taille, int? frequeneCardiaque, int? frequenceRespiratoire, string signeClinique, string motif, string signePhysique, string resumeSyndromique, string histoireMaladie, string diagnostique, string examen, string resultatExamen, DateTime rendezVous, string prescription)
        {
            NomPatient = nomPatient;
            DateConsultation = dateConsultation;
            DateNaissance = dateNaissance;
            Adresse = adresse;
            Telephone = telephone;
            Email = email;
            Sexe = sexe;
            AntecedentFamiliaux = antecedentFamiliaux;
            AntecedentChirurgicaux = antecedentChirurgicaux;
            AntecedentMedicaux = antecedentMedicaux;
            Allergie = allergie;
            SituationMatrimoniale = situationMatrimoniale;
            Temperature = temperature;
            Poid = poid;
            Tention = tention;
            Taille = taille;
            FrequeneCardiaque = frequeneCardiaque;
            FrequenceRespiratoire = frequenceRespiratoire;
            SigneClinique = signeClinique;
            Motif = motif;
            SignePhysique = signePhysique;
            ResumeSyndromique = resumeSyndromique;
            HistoireMaladie = histoireMaladie;
            Diagnostique = diagnostique;
            Examen = examen;
            ResultatExamen = resultatExamen;
            RendezVous = rendezVous;
            Prescription = prescription;
        }

        public string NomPatient { get; set; }
        public DateTime DateConsultation { get; set; }
        public  DateTime DateNaissance { get; set; }
        public string Adresse { get; set; }
        public string Telephone { get; set; }
        public string Email { get; set; }
        public int? Sexe { get; set; }
        public string AntecedentFamiliaux { get; set; }
        public string AntecedentChirurgicaux { get; set; }
        public string AntecedentMedicaux { get; set; }
        public string Allergie { get; set; }
        public string SituationMatrimoniale { get; set; }
        public double? Temperature { get; set; }
        public double? Poid { get; set; }
        public double? Tention { get; set; }
        public double? Taille { get; set; }
        public int? FrequeneCardiaque { get; set; }
        public int? FrequenceRespiratoire { get; set; }

        public string SigneClinique { get; set; }
        public string Motif { get; set; }
        public string SignePhysique { get; set; }
        public string ResumeSyndromique { get; set; }
        public string HistoireMaladie { get; set; }
        public string Diagnostique { get; set; }
        public string Examen { get; set; }
        public string ResultatExamen { get; set; }
        public DateTime RendezVous { get; set; }
        public string Prescription { get; set; }
        



        
       
    }

    public class J_FamilleActe
    {
        public J_FamilleActe()
        {
        }

        public J_FamilleActe(int id, string libelle)
        {
            Id = id;
            Libelle = libelle;
        }

        public int Id { get; set; }
        public string Libelle { get; set; }
    }

    public class J_LibelleActe
    {


        public J_LibelleActe()
        {
        }

        public J_LibelleActe(string code, string libelle)
        {
            Code = code;
            Libelle = libelle;
        }

        public string Code { get; set; }
        public string Libelle { get; set; }
    }

    public class L_Examen
    {


        public int Id { get; set; }
        public Nullable<int> CodeFiche { get; set; }
        public string CodePatient { get; set; }
        public string[] CodeActeMedicale { get; set; }
        public string CodeMedecin { get; set; }
        [DataType(DataType.MultilineText)]
        public string SigneClinique { get; set; }
        [DataType(DataType.MultilineText)]
        public string ExamenPhysique { get; set; }
        [DataType(DataType.MultilineText)]
        public string CodeInfirmier { get; set; }
        public string CodeLaborantin { get; set; }
        public byte Paiement { get; set; }
        [DataType(DataType.MultilineText)]
        public string Motif { get; set; }
        [DataType(DataType.MultilineText)]
        public string ResumeSyndromique { get; set; }
        [DataType(DataType.MultilineText)]
        public string HistoireMaladie { get; set; }
        [Display(Name = "Enquête Système")]
        public string EnqueteSysteme { get; set; }
        [DataType(DataType.MultilineText)]
        public string Diagnostique { get; set; }
        public byte Status { get; set; }
        public double Prix { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime Date { get; set; }

        public L_Examen()
        {
        }

        public L_Examen(int id, int? codeFiche, string codePatient, string[] codeActeMedicale, string codeMedecin, string signeClinique, string examenPhysique, string codeInfirmier, string codeLaborantin, byte paiement, string motif, string resumeSyndromique, string histoireMaladie, string enqueteSysteme, string diagnostique, byte status, double prix, DateTime date, Fiche fiche, LibelleActe libelleActe, Personnel personnel, Personnel personnel1, Personnel personnel2, ICollection<Prescription> prescriptions)
        {
            Id = id;
            CodeFiche = codeFiche;
            CodePatient = codePatient;
            CodeActeMedicale = codeActeMedicale;
            CodeMedecin = codeMedecin;
            SigneClinique = signeClinique;
            ExamenPhysique = examenPhysique;
            CodeInfirmier = codeInfirmier;
            CodeLaborantin = codeLaborantin;
            Paiement = paiement;
            Motif = motif;
            ResumeSyndromique = resumeSyndromique;
            HistoireMaladie = histoireMaladie;
            EnqueteSysteme = enqueteSysteme;
            Diagnostique = diagnostique;
            Status = status;
            Prix = prix;
            Date = date;
            Fiche = fiche;
            LibelleActe = libelleActe;
            Personnel = personnel;
            Personnel1 = personnel1;
            Personnel2 = personnel2;
            Prescriptions = prescriptions;
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
       



        public virtual Fiche Fiche { get; set; }
        public virtual LibelleActe LibelleActe { get; set; }
        public virtual Personnel Personnel { get; set; }
        public virtual Personnel Personnel1 { get; set; }
        public virtual Personnel Personnel2 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Prescription> Prescriptions { get; set; }
    }

 
 

    
        //DataContract for Serializing Data - required to serve in JSON format
        [DataContract]
        public class DataPoint
        {
            public DataPoint(string label, int? y)
            {
                this.Label = label;
                this.Y = y;
            }

            //Explicitly setting the name to be used while serializing to JSON.
            [DataMember(Name = "label")]
            public string Label = "";

            //Explicitly setting the name to be used while serializing to JSON.
            [DataMember(Name = "y")]
            public Nullable<int> Y = null;
        }

    class CompteWiewModel
    {
       

        public CompteWiewModel(string matricule, string nom, string prenom, string nomComplet, string typePersonnel, string email, string role, int status, string photo)
        {
            Matricule = matricule;
            Nom = nom;
            Prenom = prenom;
            NomComplet = nomComplet;
            TypePersonnel = typePersonnel;
            Email = email;
            Role = role;
            Status = status;
            Photo = photo;
        }

        public string Matricule { get; set; }
        public string Nom { get; set; }
        public string Prenom { get; set; }
        public string NomComplet { get; set; }
        public string TypePersonnel { get; set; }
        public string Email { get; set; }
        public string Role { get; set; }
        public int Status { get; set; }
        public String Photo { get; set; }

    }

}