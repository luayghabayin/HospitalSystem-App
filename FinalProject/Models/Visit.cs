using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace FinalProject.Models
{
    [Table("tblVisit")]
    public class Visit
    {
        internal object doctor;
        internal object patient;
        //Fields plus their validation
        public int Id { get; set; }

        [Required(ErrorMessage = "Please enter the Doctor ID for the Visit")]
        public int DoctorId { get; set; }

        [Required(ErrorMessage = "Please enter the Doctor ID for the Patient")]
        public int PatientId { get; set; }

        [Required(ErrorMessage = "Please enter the Patients date of admission")]
        public DateTime DateOfAdmission { get; set; }

        [Required(ErrorMessage = "Please enter the Patients date of discharge")]
        public DateTime DateOfDischarge { get; set; }

        [Required(ErrorMessage = "Please enter the Patients complaint")]
        public string Complaint { get; set; }
        public Doctor Doctor { get; set; }
        public Patient Patient { get; set; }
    }
}