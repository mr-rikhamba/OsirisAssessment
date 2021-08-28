using System;
using System.ComponentModel.DataAnnotations;

namespace Movies.Logic.Models.NytModels
{
    public class LoginModel
    {
        [Required]
        public string Username { get; set; }
    }
}
