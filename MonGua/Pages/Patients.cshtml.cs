using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using DataContext.Models;
using DataContext.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;

namespace MonGua.Pages
{
    public class PatientsModel : PageModel
    {
        [BindProperty]
        public List<Patient> patients { get; set; } = new List<Patient>();

        public List<Patient> sickPatients { get; set; } = new List<Patient>();
        public List<Patient> patientsInAttention { get; set; } = new List<Patient>();
        public List<Patient> recoveredPatients { get; set; } = new List<Patient>();

        public int newId { get; private set; }
        public int newDocument { get; private set; }
        public string newName { get; private set; }
        public int newAge { get; private set; }
        public string newGender { get; private set; }
        public Triage newTriage { get; private set; }
        public string newSymptoms { get; private set; } 
        public void OnGet()
        {
            GetPatients();
            OrderPatients();
            
        }
        public void OnPost()
        {

            GetPatients();
            newId = patients.Max(p => p.Id) + 1;
            newDocument = Int32.Parse(Request.Form[nameof(newDocument)]);
            newAge = Int32.Parse(Request.Form[nameof(newAge)]);
            newTriage = (Triage)Enum.Parse(typeof(Triage),Request.Form[nameof(newTriage)].ToString());
            newName = Request.Form[nameof(newName)];
            newGender = Request.Form[nameof(newGender)];
            newSymptoms = Request.Form[nameof(newSymptoms)];

            var newPatient = new Patient
            {
                Id = newId,
                Document = newDocument,
                Name = newName,
                Age = newAge,
                Gender = newGender,
                Triage = newTriage,
                Symptoms = newSymptoms,
                State = State.Enfermo
            };
            patients.Add(newPatient);
            OrderPatients();
        }

        private void OrderPatients()
        {
            patients = patients.OrderBy(patient => patient.Triage).ThenBy(p => p.Id).ToList();
        }
        private void GetPatients()
        {
            string wanted_path = Path.GetDirectoryName(System.AppDomain.CurrentDomain.BaseDirectory);
            var patientservice = new PatientService();
            if (patients != null && patients.Count > 0)
            {
                patients = patients.Concat(patientservice.ReadCSVFile(wanted_path + "\\Patients.csv")).ToList();
            }
            else
            {
                patients = patientservice.ReadCSVFile(wanted_path + "\\Patients.csv");
            }

        }

        public Patient GetNextPatient()
        {
            GetPatients();
            return patients[0];
        }
    }
}
