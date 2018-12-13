using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace Finances.WebApp.Models
{
    public class CategoriaViewModel
    {
        public int ID { get; set; }
        public string Nome { get; set; }
        public string Cor { get; set; }
        public string Icone { get; set; }
    }
}