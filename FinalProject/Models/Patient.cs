using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace FinalProject.Models
{
    [Table("tblPatient")]
    public class Patient
    {
        internal object doctor;
        internal object patient;
        //Fields plus their validation
        public int Id { get; set; }

        [Required(ErrorMessage = "Please enter the Patients name")]
        [StringLength(50, ErrorMessage = "The name must be less than {1} charaters")]
        public string Name { get; set; }

        [EmailAddress(ErrorMessage = "The email address is not valid")]
        [Required(ErrorMessage = "Please enter the Patients Email")]
        [StringLength(50, ErrorMessage = "The Email must be less than {1} charaters")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Please enter the Patients Address")]
        [StringLength(50, ErrorMessage = "The Address must be less than {1} charaters")]
        public string Address { get; set; }

        [Phone(ErrorMessage = "The phone number is not valid")]
        [Required(ErrorMessage = "Please enter the Patients Phone number")]
        [StringLength(50, ErrorMessage = "The Phone number must be less than {1} charaters")]
        public string Telephone { get; set; }

        [Required(ErrorMessage = "Please enter the Patients date of birth")]
        public DateTime DateOfBirth { get; set; }

        [Required(ErrorMessage = "Please enter the Doctor ID for the Patient")]
        public int DoctorId { get; set; }
        public Doctor Doctor { get; set; }
    }
}