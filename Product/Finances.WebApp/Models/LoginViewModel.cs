using System;
using System.ComponentModel.DataAnnotations;

namespace Finances.WebApp.Models
{
    public class LoginViewModel
    {
        [Required]
        public string Login { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Senha { get; set; }
    }
}