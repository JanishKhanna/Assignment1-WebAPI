using APIHospital.Models.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace APIHospital.Models.ViewModel
{
    public class VisitViewModel
    {
        public int VisitorId { get; set; }
        public DateTime Date { get; set; }
        public string Comments { get; set; }

        public VisitViewModel()
        {

        }

        public VisitViewModel(Visit visit)
        {
            VisitorId = visit.Id;
            Date = visit.Date;
            Comments = visit.Comments;
        }
    }
}