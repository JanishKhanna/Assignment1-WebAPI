using APIHospital.Models;
using APIHospital.Models.BindingModel;
using APIHospital.Models.Domain;
using APIHospital.Models.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace APIHospital.Controllers
{
    
    [RoutePrefix("api/patient")]
    public class PatientController : ApiController
    {
        private ApplicationDbContext DbContext;

        public PatientController()
        {
            DbContext = new ApplicationDbContext();
        }

        [Authorize]
        public IHttpActionResult Get()
        {            
            return Ok(DbContext.Patients.ToList());
        }

        [Authorize]
        [HttpGet]
        [Route("by-id/{id:int}", Name = "PatientById")]
        public IHttpActionResult PatientById(int id)
        {
            var patient = DbContext.Patients.FirstOrDefault(p => p.Id == id);

            if(patient == null)
            {
                return NotFound();
            }

            var viewModel = new PatientViewModel(patient);

            return Ok(viewModel);
        }

        [Authorize]
        [HttpPost]
        [Route("create")]
        public IHttpActionResult CreatePatient(PatientBindingModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var patient = new Patient()
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                HasInsurance = true,
                DateOfBirth = model.DateOfBirth,
                Email = model.Email,
            };

            DbContext.Patients.Add(patient);
            DbContext.SaveChanges();

            var url = Url.Link("PatientById", new { Id = patient.Id });

            var viewModel = new PatientViewModel(patient);

            return Created(url, viewModel);
        }

        [Authorize]
        [HttpPut]
        [Route("edit/{id:int}")]
        public IHttpActionResult EditPatient(int id, PatientBindingModel model)
        {
            var patientToEdit = DbContext.Patients.FirstOrDefault(p => p.Id == id);

            if(patientToEdit == null)
            {
                return NotFound();
            }

            patientToEdit.FirstName = model.FirstName;
            patientToEdit.LastName = model.LastName;
            patientToEdit.DateOfBirth = model.DateOfBirth;
            patientToEdit.Email = model.Email;
            patientToEdit.HasInsurance = model.HasInsurance;

            DbContext.SaveChanges();

            var viewModel = new PatientViewModel(patientToEdit);

            return Ok(viewModel);
        }

        [Authorize]
        [HttpGet]
        [Route("visit-by-id/{id:int}", Name = "VisitById")]
        public IHttpActionResult VisitById(int id)
        {
            var visit = DbContext.Visits.FirstOrDefault(p => p.Id == id);

            if (visit == null)
            {
                return NotFound();
            }

            var viewModel = new VisitViewModel(visit);

            return Ok(viewModel);
        }

        [Authorize]
        [HttpPost]
        [Route("api/{id:int}/recordvisit")]
        public IHttpActionResult RecordVisit(int id, VisitBindingModel model)
        {
            var patientId = DbContext.Patients.FirstOrDefault(p => p.Id == id).Id;
            var patient = DbContext.Patients.FirstOrDefault(p => p.Id == id);

            var visit = new Visit()
            {
                Date = DateTime.Now,
                Comments = model.Comments,
                PatientId = patientId
            };

            patient.Visits.Add(visit);
            DbContext.SaveChanges();

            var url = Url.Link("VisitById", new { Id = visit.Id });

            var viewModel = new PatientViewModel(patient);

            return Created(url, viewModel);
        }

        
    }
}
