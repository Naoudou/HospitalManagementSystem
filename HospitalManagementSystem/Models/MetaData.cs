using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HospitalManagementSystem.Models
{
    public partial class ActePose
    {


        public ActePose(ActePose actePose)
        {
            Id = actePose.Id;
            CodeFiche = actePose.CodeFiche;
            CodePatient = actePose. CodePatient;
            CodeActeMedicale = actePose. CodeActeMedicale;
            CodeMedecin = actePose. CodeMedecin;
            SigneClinique = actePose. SigneClinique;
            ExamenPhysique = actePose. ExamenPhysique;
            CodeInfirmier = actePose. CodeInfirmier;
            CodeLaborantin = actePose. CodeLaborantin;
            Paiement = actePose.Paiement;
            Motif = actePose.Motif;
            ResumeSyndromique = actePose.ResumeSyndromique;
            HistoireMaladie = actePose. HistoireMaladie;
            EnqueteSysteme = actePose. EnqueteSysteme;
            Diagnostique = actePose.Diagnostique;
            Status = actePose. Status;
            Prix = actePose. Prix;
            Date = actePose. Date;
            Fiche = actePose. Fiche;
            LibelleActe = actePose. LibelleActe;
            Personnel = actePose.Personnel;
            Personnel1 = actePose. Personnel1;
            Personnel2 = actePose.Personnel2;
            Prescriptions = actePose.Prescriptions;
        }
        public int Id { get; set; }
        public Nullable<int> CodeFiche { get; set; }
        public string CodePatient { get; set; }
        public string CodeActeMedicale { get; set; }
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

        public ActePose()
        {
        }

        public ActePose(int id, int? codeFiche, string codePatient, string codeActeMedicale, string codeMedecin, string signeClinique, string examenPhysique, string codeInfirmier, string codeLaborantin, byte paiement, string motif, string resumeSyndromique, string histoireMaladie, string enqueteSysteme, string diagnostique, byte status, double prix, DateTime date, Fiche fiche, LibelleActe libelleActe, Personnel personnel, Personnel personnel1, Personnel personnel2, ICollection<Prescription> prescriptions)
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
    }

    public partial class Compte
    {
        public Compte(int id, string login, string motDePasse, byte statut, string codePersonnel, int idRole)
        {
            Id = id;
            Login = login;
            MotDePasse = motDePasse;
            Statut = statut;
            CodePersonnel = codePersonnel;
            IdRole = idRole;
        }

        public int Id { get; set; }
        public string Login { get; set; }
        public string MotDePasse { get; set; }
        public byte Statut { get; set; }
        public string CodePersonnel { get; set; }
        public int IdRole { get; set; }
    }

    public partial class Configuration
    {
        public Configuration(int idConfiguration, string libelle, int idNature)
        {
            IdConfiguration = idConfiguration;
            Libelle = libelle;
            IdNature = idNature;
        }

        public int IdConfiguration { get; set; }
        public string Libelle { get; set; }
        public int IdNature { get; set; }
    }
    public partial class Configuration
    {
        public Configuration(int idMedicament, int idEquivalent, string commentaire)
        {
            IdMedicament = idMedicament;
            IdEquivalent = idEquivalent;
            Commentaire = commentaire;
        }

        public int IdMedicament { get; set; }
        public int IdEquivalent { get; set; }
        public string Commentaire { get; set; }

    }
    public partial class FamilleActe
    {
        public FamilleActe(int id, string libelle, string description)
        {
            Id = id;
            Libelle = libelle;
            Description = description;
        }

        public int Id { get; set; }
        public string Libelle { get; set; }
        public string Description { get; set; }
    }
    public partial class Equivalence
    {
        public Equivalence()
        {
        }

        public Equivalence(int idMedicament, int idEquivalent, string commentaire)
        {
            IdMedicament = idMedicament;
            IdEquivalent = idEquivalent;
            Commentaire = commentaire;
        }

        public int Id { get; set; }
        public int IdMedicament { get; set; }
        public int IdEquivalent { get; set; }

        [DataType(DataType.MultilineText)]
        public string Commentaire { get; set; }
    }
    public partial class Fiche
    {
        public Fiche(int id, string codePatient, string codeSecretaire, DateTime date, byte paiement, int? raison, string codeExamen)
        {
            Id = id;
            CodePatient = codePatient;
            CodeSecretaire = codeSecretaire;
            Date = date;
            Paiement = paiement;
            Raison = raison;
            CodeExamen = codeExamen;
        }

        public int Id { get; set; }
        public string CodePatient { get; set; }
        public string CodeSecretaire { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public System.DateTime Date { get; set; }
        public byte Paiement { get; set; }
        public Nullable<int> Raison { get; set; }
        public string CodeExamen { get; set; }

    }
    public partial class LibelleActe
    {


        public LibelleActe(string code, string libelle, int familleActe, string description, double? prix)
        {
            Code = code;
            Libelle = libelle;
            FamilleActe = familleActe;
            Description = description;
            Prix = prix;
        }

        public string Code { get; set; }
        public string Libelle { get; set; }
        public int FamilleActe { get; set; }
        public string Description { get; set; }
        public Nullable<double> Prix { get; set; }
    }
    public partial class Medicament
    {

          
        public Medicament(int id, string nomCommercial, string nomScientifique, int quantite, double prix, byte disponibilite, string commentaire, int seuilCritique, int categorieMedicament)
        {
            Id = id;
            NomCommercial = nomCommercial;
            NomScientifique = nomScientifique;
            Quantite = quantite;
            Prix = prix;
            Disponibilite = disponibilite;
            Commentaire = commentaire;
            SeuilCritique = seuilCritique;
            CategorieMedicament = categorieMedicament;
        }

        public int Id { get; set; }
        public string NomCommercial { get; set; }
        public string NomScientifique { get; set; }
        public int Quantite { get; set; }
        public double Prix { get; set; }
        public byte Disponibilite { get; set; }
        public string Commentaire { get; set; }
        public int SeuilCritique { get; set; }
        public int CategorieMedicament { get; set; }
    }
    public partial class Nature
    {
        public Nature(int idNature, string libelle)
        {
            IdNature = idNature;
            Libelle = libelle;
        }

        public int IdNature { get; set; }
        public string Libelle { get; set; }
    }
 
    public partial class Parametre
    {
        public Parametre(int id, double? tention, double? temperature, double? poids, DateTime date, string codePatient, double? glycemie, int? frequenceCardiaque, int? frequenceRespiratoire, double? taille)
        {
            Id = id;
            Tention = tention;
            Temperature = temperature;
            Poids = poids;
            Date = date;
            CodePatient = codePatient;
            Glycemie = glycemie;
            FrequenceCardiaque = frequenceCardiaque;
            FrequenceRespiratoire = frequenceRespiratoire;
            Taille = taille;
        }

        public int Id { get; set; }
        public Nullable<double> Tention { get; set; }
        public Nullable<double> Temperature { get; set; }
        public Nullable<double> Poids { get; set; }
        public System.DateTime Date { get; set; }
        public string CodePatient { get; set; }
        public Nullable<double> Glycemie { get; set; }
        public Nullable<int> FrequenceCardiaque { get; set; }
        public Nullable<int> FrequenceRespiratoire { get; set; }
        public Nullable<double> Taille { get; set; }

    }
    public partial class Patient
    {
        public Patient(string code, string nom, string prenom, string nomComplet, string dateNaissance, string adresse, string telephone, string email, byte? sexe, string antecedentFamiliaux, string antecedentChirucauxPersonnel, string antecedentMedicauxPersonnel, string allergie, string situationMatrimogniale)
        {
            Code = code;
            Nom = nom;
            Prenom = prenom;
            NomComplet = nomComplet;
            DateNaissance = dateNaissance;
            Adresse = adresse;
            Telephone = telephone;
            Email = email;
            Sexe = sexe;
            AntecedentFamiliaux = antecedentFamiliaux;
            AntecedentChirucauxPersonnel = antecedentChirucauxPersonnel;
            AntecedentMedicauxPersonnel = antecedentMedicauxPersonnel;
            Allergie = allergie;
            SituationMatrimogniale = situationMatrimogniale;
        }

        public string Code { get; set; }
        public string Nom { get; set; }
        public string Prenom { get; set; }
        public string NomComplet { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public string DateNaissance { get; set; }
        public string Adresse { get; set; }
        public string Telephone { get; set; }
        public string Email { get; set; }
        public Nullable<byte> Sexe { get; set; }
        [DataType(DataType.MultilineText)]
        public string AntecedentFamiliaux { get; set; }
        [DataType(DataType.MultilineText)]
        public string AntecedentChirucauxPersonnel { get; set; }
        [DataType(DataType.MultilineText)]
        public string AntecedentMedicauxPersonnel { get; set; }
        [DataType(DataType.MultilineText)]
        public string Allergie { get; set; }
        [DataType(DataType.MultilineText)]
        public string SituationMatrimogniale { get; set; }
    }
    public partial class Personnel
    {
        public Personnel(string code, string nom, string prenom, string nomComplet, DateTime dateNaissance, string adresse, string telephone, string email, byte sexe, string photo, int? idSpecialite, int? idService, int? idTypePersonnel)
        {
            Code = code;
            Nom = nom;
            Prenom = prenom;
            NomComplet = nomComplet;
            DateNaissance = dateNaissance;
            Adresse = adresse;
            Telephone = telephone;
            Email = email;
            Sexe = sexe;
            Photo = photo;
            IdSpecialite = idSpecialite;
            IdService = idService;
            IdTypePersonnel = idTypePersonnel;
        }

        public string Code { get; set; }
        public string Nom { get; set; }
        public string Prenom { get; set; }
        public string NomComplet { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public System.DateTime DateNaissance { get; set; }
        public string Adresse { get; set; }
        public string Telephone { get; set; }
        public string Email { get; set; }
        public byte Sexe { get; set; }
        public string Photo { get; set; }
        public Nullable<int> IdSpecialite { get; set; }
        public Nullable<int> IdService { get; set; }
        public Nullable<int> IdTypePersonnel { get; set; }
    }
    public partial class Planning
    {
        public Planning(int id, DateTime dateDebut, DateTime dateFin, DateTime heureDebut, DateTime heureFin, string codePersonnel)
        {
            Id = id;
            DateDebut = dateDebut;
            DateFin = dateFin;
            HeureDebut = heureDebut;
            HeureFin = heureFin;
            CodePersonnel = codePersonnel;
        }

        public int Id { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public System.DateTime DateDebut { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public System.DateTime DateFin { get; set; }
        [DataType(DataType.Time)]
        [DisplayFormat(DataFormatString = "{0:hh\\:mm}")]
        public System.DateTime HeureDebut { get; set; }
        [DataType(DataType.Time)]
        [DisplayFormat(DataFormatString = "{0:hh\\:mm}")]
        public System.DateTime HeureFin { get; set; }
        public string CodePersonnel { get; set; }
    }
    public partial class Prescription
    {
        public Prescription()
        {
        }

        public Prescription(int id, int idMedicament, int idConsultation, DateTime date, string possologie)
        {
            Id = id;
            IdMedicament = idMedicament;
            IdConsultation = idConsultation;
            Date = date;
            Possologie = possologie;
        }

        public int Id { get; set; }
        public int IdMedicament { get; set; }
        public int IdConsultation { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public System.DateTime Date { get; set; }
        public string Possologie { get; set; }
    }
    public partial class RendezVou
    {
        public RendezVou()
        {
        }

        public RendezVou(int id, int idType, string codePatient, string codeMedecin, DateTime date, byte statut, string raison)
        {
            Id = id;
            IdType = idType;
            CodePatient = codePatient;
            CodeMedecin = codeMedecin;
            Date = date;
            Statut = statut;
            Raison = raison;
        }

        public int Id { get; set; }
        public int IdType { get; set; }
        public string CodePatient { get; set; }
        public string CodeMedecin { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public System.DateTime Date { get; set; }
        public byte Statut { get; set; }
        [DataType(DataType.MultilineText)]
        public string Raison { get; set; }
    }
    public partial class VenteMedicament
    {
        public VenteMedicament(int id, DateTime date, int quantite, string codePharmacien, int idMedicament)
        {
            Id = id;
            Date = date;
            Quantite = quantite;
            CodePharmacien = codePharmacien;
            IdMedicament = idMedicament;
        }

        public VenteMedicament()
        {
        }

        public int Id { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public System.DateTime Date { get; set; }
        public int Quantite { get; set; }
        public string CodePharmacien { get; set; }
        public int IdMedicament { get; set; }
    }
  
}