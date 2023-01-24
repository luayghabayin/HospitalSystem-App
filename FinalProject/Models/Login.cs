using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace FinalProject.Models
{
    [Table("tblLogin")]
    public class Login
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Please enter a username")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Please enter a password")]
        public string Password { get; set; }

    }
}