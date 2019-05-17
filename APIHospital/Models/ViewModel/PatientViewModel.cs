using APIHospital.Models.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace APIHospital.Models.ViewModel
{
    public class PatientViewModel
    {
        public int PatientId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }        
        public string Email { get; set; }
        public string DateOfBirth { get; set; }
        public bool HasInsurance { get; set; }

        public List<VisitViewModel> Visits { get; set; }

        public PatientViewModel()
        {

        }

        public PatientViewModel(Patient patient)
        {
            PatientId = patient.Id;
            FirstName = patient.FirstName;
            LastName = patient.LastName;
            Email = patient.Email;
            DateOfBirth = patient.DateOfBirth;
            Visits = patient.Visits.Select(p => new VisitViewModel(p)).ToList();
        }
    }
}