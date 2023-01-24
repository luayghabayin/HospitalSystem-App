using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FinalProject.Models;
using System.Data.Entity;
using System.Text;

namespace FinalProject.Controllers
{
    public class HomeController : Controller
    {
        ContextClass cc = new ContextClass();

        //Default login page
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        //Login page with form POST
        [HttpPost]
        public ActionResult Login(Login login)
        {
            if (ModelState.IsValid)
            {
                if (!string.IsNullOrEmpty(login.Username) && !string.IsNullOrEmpty(login.Password))
                {
                    List<Login> logins = cc.Logins.ToList();
                    foreach (Login l in logins)
                    {
                        if (l.Username.Equals(login.Username) && l.Password.Equals(login.Password))
                        {
                            //Redirect to doctors landing page when user successfully logs in
                            return RedirectToAction("Doctors");
                        }
                    }
                }
            }
            return View();
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Doctors(string dname, string phone)
        {
            List<Doctor> doctors = cc.Doctors.ToList();

           //If search is null, return all doctors from DB
            if (string.IsNullOrEmpty(dname) && string.IsNullOrEmpty(phone))
            {
                return View(doctors);
            }
            else
            {
                //Filter our model by the search criteria
                if (!string.IsNullOrEmpty(dname))
                {
                    doctors = doctors.Where(d => d.Name.ToLower().Contains(dname.ToLower())).ToList();
                }

                if (!string.IsNullOrEmpty(phone))
                {
                    doctors = doctors.Where(d => d.Telephone.Contains(phone)).ToList();
                }
            }

            return View(doctors);
        }


        public ActionResult Patients(string pname, string phone)
        {
            List<Patient> patients = cc.Patients.Include(p => p.doctor).ToList();

            //If search is null, return all patients from DB
            if (string.IsNullOrEmpty(pname) && string.IsNullOrEmpty(phone))
            {
                return View(patients);
            }
            else
            {
                //Filter our model by the search criteria
                if (!string.IsNullOrEmpty(pname))
                {
                    patients = patients.Where(p => p.Name.ToLower().Contains(pname.ToLower())).ToList();
                }

                if (!string.IsNullOrEmpty(phone))
                {
                    patients = patients.Where(p => p.Telephone.Contains(phone)).ToList();
                }
            }

            return View(patients);
        }

        public ActionResult Visits(string dname, string pname)
        {

            List<Visit> visits = cc.Visits.Include(p => p.doctor).ToList();
            visits = cc.Visits.Include(p => p.patient).ToList();

            //If search is null, return all visits from DB
            if (string.IsNullOrEmpty(dname) && string.IsNullOrEmpty(pname))
            {
                return View(visits);
            }
            else
            {
                //Filter our model by the search criteria
                if (!string.IsNullOrEmpty(dname))
                {
                    visits = visits.Where(v => v.Doctor.Name.ToLower().Contains(dname.ToLower())).ToList();
                }

                if (!string.IsNullOrEmpty(pname))
                {
                    visits = visits.Where(v => v.Patient.Name.ToLower().Contains(pname.ToLower())).ToList();
                }
            }
            return View(visits);
        }

        //Details ActionResults
        public ActionResult DoctorDetails(int ID)
        {
            Doctor doctor = getDoctorByID(ID);
            return View(doctor);
        }

        public ActionResult PatientDetails(int ID)
        {
            Patient patient = getPatientByID(ID);
            return View(patient);
        }

        public ActionResult VisitDetails(int ID)
        {
            Visit visit = getVisitByID(ID);
            return View(visit);
        }
        //Create Action Results
        [HttpGet]
        public ActionResult CreateDoctor()
        {
            return View();
        }
        [HttpPost]
        public ActionResult CreateDoctor(Doctor doctor)
        {
            if(ModelState.IsValid)
            {
            cc.Doctors.Add(doctor);
            cc.SaveChanges();
            return RedirectToAction("Doctors");
            }
            return View();
        }
        [HttpGet]
        public ActionResult CreatePatient()
        {
            return View();
        }
        [HttpPost]
        public ActionResult CreatePatient(Patient patient)
        {
            if (ModelState.IsValid)
            {
            cc.Patients.Add(patient);
            cc.SaveChanges();
            return RedirectToAction("Patients");
            }
            return View();
        }
        [HttpGet]
        public ActionResult CreateVisit()
        {
            return View();
        }
        [HttpPost]
        public ActionResult CreateVisit(Visit visit)
        {
            if (ModelState.IsValid)
            {
                cc.Visits.Add(visit);
                cc.SaveChanges();
                return RedirectToAction("Visits");
            }
            return View();
        }

        //Edit ActionResults
        [HttpGet]
        public ActionResult EditDoctor(int id)
        {
            Doctor doctor = cc.Doctors.Single(doc => doc.Id == id);
            return View(doctor);
        }
        [HttpPost]
        public ActionResult EditDoctor(Doctor doctor)
        {
            if (ModelState.IsValid)
            {
                cc.Entry(doctor).State = System.Data.Entity.EntityState.Modified;
                cc.SaveChanges();
                return RedirectToAction("Doctors");
            }
            return View();
        }

        [HttpGet]
        public ActionResult EditPatient(int id)
        {
            Patient patient = cc.Patients.Single(pat => pat.Id == id);
            return View(patient);
        }
        [HttpPost]
        public ActionResult EditPatient(Patient patient)
        {
            if (ModelState.IsValid)
            {
                cc.Entry(patient).State = System.Data.Entity.EntityState.Modified;
                cc.SaveChanges();
                return RedirectToAction("Patients");
            }
            return View();
        }

        [HttpGet]
        public ActionResult EditVisit(int id)
        {
            Visit visit = cc.Visits.Single(vis => vis.Id == id);
            return View(visit);
        }
        [HttpPost]
        public ActionResult EditVisit(Visit visit)
        {
            if (ModelState.IsValid)
            {
                cc.Entry(visit).State = System.Data.Entity.EntityState.Modified;
                cc.SaveChanges();
                return RedirectToAction("Visits");
            }
            return View();
        }
        //Delete ActionResults (both get and post)
        [HttpGet]
        public ActionResult DeleteDoctor(int id)
        {
            Doctor doctor = cc.Doctors.Single(doc => doc.Id == id);
            return View(doctor);
        }
        [HttpPost, ActionName("DeleteDoctor")]
        public ActionResult DeleteDoctorConfirmed(int id)
        {
            Doctor doctor = cc.Doctors.Single(doc => doc.Id == id);
            cc.Doctors.Remove(doctor);
            cc.SaveChanges();
            return RedirectToAction("Doctors");
        }
        [HttpGet]
        public ActionResult DeletePatient(int id)
        {
            Patient patient = cc.Patients.Single(pat => pat.Id == id);
            return View(patient);
        }
        [HttpPost, ActionName("DeletePatient")]
        public ActionResult DeletePatientConfirmed(int id)
        {
            Patient patient = cc.Patients.Single(pat => pat.Id == id);
            cc.Patients.Remove(patient);
            cc.SaveChanges();
            return RedirectToAction("Patients");
        }
        [HttpGet]
        public ActionResult DeleteVisit(int id)
        {
            Visit visit = cc.Visits.Single(vis => vis.Id == id);
            return View(visit);
        }
        [HttpPost, ActionName("DeleteVisit")]
        public ActionResult DeleteVisitConfirmed(int id)
        {
            Visit visit = cc.Visits.Single(vis => vis.Id == id);
            cc.Visits.Remove(visit);
            cc.SaveChanges();
            return RedirectToAction("Visits");
        }

        //Helper methods for creating our model objects
        public Doctor getDoctorByID(int id)
        {
            ContextClass doctorContext = new ContextClass();
            return doctorContext.Doctors.Single(d => d.Id == id);
        }

        public Patient getPatientByID(int id)
        {
            ContextClass paitentContext = new ContextClass();
            var pat = paitentContext.Patients.Single(p => p.Id == id);
            pat.Doctor = getDoctorByID(pat.DoctorId);
            return pat;
        }

        public Visit getVisitByID(int id)
        {
            ContextClass visitcontext = new ContextClass();
            var visit = visitcontext.Visits.Single(v => v.Id == id);
            visit.Doctor = getDoctorByID(visit.DoctorId);
            visit.Patient = getPatientByID(visit.PatientId);
            return visit;
        }

        //Endpoint for exporting Doctors as a CSV file for the user
        public FileResult ExportDoctors()
        {
            //Load doctor's data
            List<Doctor> doctors = cc.Doctors.ToList();

            //Using a string builder to create our data string
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("Doctor ID, Name, Office, E-Mail, Telephone, Address");
            foreach (Doctor d in doctors)
            {
                sb.AppendLine(d.Id + "," + d.Name + "," + d.Office + "," + d.Email + "," + d.Telephone + "," + d.Address);
            }

            //convert our data to a bytestream and then send it to the user
            byte[] data = Encoding.ASCII.GetBytes(sb.ToString());
            return File(data, System.Net.Mime.MediaTypeNames.Text.Plain, "Doctors.csv");
        }

        //Endpoint for exporting patients as a CSV file for the user
        public FileResult ExportPatients()
        {
            //Load patients data
            List<Patient> patients = cc.Patients.Include(p => p.doctor).ToList();

            //Using a string builder to create our data string
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("Patient ID, Name, E-Mail, Address, Telephone, Date of Birth, Doctor");
            foreach (Patient p in patients)
            {
                sb.AppendLine(p.Id + "," + p.Name + "," + p.Email + "," + p.Address + "," + p.Telephone + "," + p.DateOfBirth.ToString() + "," + p.Doctor.Name);
            }

            //convert our data to a bytestream and then send it to the user
            byte[] data = Encoding.ASCII.GetBytes(sb.ToString());
            return File(data, System.Net.Mime.MediaTypeNames.Text.Plain, "Patients.csv");
        }

        //Endpoint for exporting visits as a CSV file for the user
        public FileResult ExportVisits()
        {
            //Load visits data
            List<Visit> visits = cc.Visits.Include(p => p.doctor).ToList();
            visits = cc.Visits.Include(p => p.patient).ToList();

            //Using a string builder to create our data string
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("Visit ID, Date of Admission, Date of Discharge, Patient's Name, Complaint, Doctor's Name");
            foreach (Visit v in visits)
            {
                sb.AppendLine(v.Id + "," + v.DateOfAdmission.ToString() + "," + v.DateOfDischarge.ToString() + "," + v.Patient.Name + "," + v.Complaint + "," + v.Doctor.Name);
            }

            //convert our data to a bytestream and then send it to the user
            byte[] data = Encoding.ASCII.GetBytes(sb.ToString());
            return File(data, System.Net.Mime.MediaTypeNames.Text.Plain, "Visits.csv");
        }
    }
}