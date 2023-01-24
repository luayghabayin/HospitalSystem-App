using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace FinalProject.Models
{
    [Table("tblDoctor")]
    public class Doctor
    {
        //Fields plus their validation
        public int Id { get; set; }

        [Required(ErrorMessage = "Please enter the Doctors name")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Please enter the Doctors Office")]
        public string Office { get; set; }

        [EmailAddress(ErrorMessage = "The email address is not valid")]
        [Required(ErrorMessage = "Please enter the Doctors Email")]
        [StringLength(50, ErrorMessage = "The Email must be less than {1} charaters")]
        public string Email { get; set; }

        [Phone(ErrorMessage = "The phone number is not valid")]
        [Required(ErrorMessage = "Please enter the Doctors Phone number")]
        [StringLength(50, ErrorMessage = "The Phone number must be less than {1} charaters")]
        public string Telephone { get; set; }

        [Required(ErrorMessage = "Please enter the Doctors Address")]
        [StringLength(50, ErrorMessage = "The Address must be less than {1} charaters")]
        public string Address { get; set; }
    }
}