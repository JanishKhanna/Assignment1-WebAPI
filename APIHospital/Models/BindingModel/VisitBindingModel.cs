using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace APIHospital.Models.BindingModel
{
    public class VisitBindingModel
    {
        [Required]
        public DateTime Date { get; set; }

        [Required]
        public string Comments { get; set; }               
    }
}