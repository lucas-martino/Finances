using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace Finances.WebApp.Models
{
    public class CategoriaViewModel
    {
        [Required]
        public long ID { get; set; }
        [Required]
        public string Nome { get; set; }
    }
}