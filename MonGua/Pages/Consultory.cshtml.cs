using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataContext.Models;
using DataContext.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;

namespace MonGua.Pages
{
    public class ConsultoryModel : PageModel
    {
        public List<Consultory> consultories { get; set; } = new List<Consultory>();
        public List<ConsultoryPatients> patientsAttention { get; set; } = new List<ConsultoryPatients>();
        public List<Patient> patientsRecovered { get; set; } = new List<Patient>();
        public PatientsModel pmodel = new PatientsModel();

        private string MonGuaChain = "T,T,C,G,G,A,G,T,A,A,C,A,C,G,C,C,T,A,T,A,G,G,C,G,T,G,T,T,A,C,T,C,C,G,A,A";
        public int newConsultoryId { get; private set; }
        public string newDoctorsName { get; private set; }

        [BindProperty]
        public IFormFile Test { get;  set; }
        [BindProperty]
        public bool CheckPatient { get; set; } = false;
        public Triage newTriage { get; private set; }
        public void OnGet()
        {
            GetConsultories();
        }
        public void OnPost()
        {
            GetConsultories();
            newConsultoryId = Int32.Parse(Request.Form[nameof(newConsultoryId)]);
            newDoctorsName = Request.Form[nameof(newDoctorsName)];
            var newConsult = new Consultory
            {
                Code = newConsultoryId,
                DoctorsName = newDoctorsName,
                Available = true
            };
            consultories.Add(newConsult);
        }
        public void OnPostDelete(int id)
        {
            GetConsultories();
            var toDelete = consultories.Find(con => con.Code == id);
            consultories.Remove(toDelete);
        }
        private void GetConsultories()
        {
            string wanted_path = Path.GetDirectoryName(System.AppDomain.CurrentDomain.BaseDirectory);
            var patientservice = new ConsultoryService();
            if (consultories != null && consultories.Count > 0)
            {
                consultories = consultories.Concat(patientservice.ReadCSVFile(wanted_path + "\\Consultories.csv")).ToList();
            }
            else
            {
                consultories = patientservice.ReadCSVFile(wanted_path + "\\Consultories.csv");
            }
        }
        public void OnPostNextPatient(int id)
        {
            GetConsultories();
            var index = consultories.FindIndex(c => c.Code == id);
            var consult = consultories.Find(con => con.Code == id);

            consultories[index].Available = false;
            var patient = pmodel.GetNextPatient();

            var patientInAttention = new ConsultoryPatients
            {
                Id = patient.Id,
                Name = patient.Name,
                Age = patient.Age,
                Gender = patient.Gender,
                Document = patient.Document,
                Triage = patient.Triage,
                Symptoms = patient.Symptoms,
                State = State.EnAtencion,
                ConsultoryCode = consult.Code,
                DoctorsName = consult.DoctorsName,
                Infected = false
            };
            patientsAttention.Add(patientInAttention);
            TempData["attention"] = JsonConvert.SerializeObject(patientsAttention); 
        }

        public void OnPostDnaTest()
        { 
            if(Test != null)
            {
                var result = new StringBuilder();
                using (var reader = new StreamReader(Test.OpenReadStream()))
                {
                    while (reader.Peek() >= 0)
                        result.AppendLine(reader.ReadLine());
                }
                var dna = result;

                if(dna.ToString().Contains(MonGuaChain))
                {
                    Newtonsoft.Json.Linq.JArray tempData = (Newtonsoft.Json.Linq.JArray)JsonConvert.DeserializeObject((string)TempData["attention"]);
                    patientsAttention = tempData.ToObject<List<ConsultoryPatients>>();
                    patientsAttention[0].Infected = true;
                    
                }
                TempData["attention"] = JsonConvert.SerializeObject(patientsAttention);
                GetConsultories();
            }
        }
        public void OnPostDiagnostics()
        {
            if(!CheckPatient)
            {
                newTriage = (Triage)Enum.Parse(typeof(Triage), Request.Form[nameof(newTriage)].ToString());
            }
            else
            {
                Newtonsoft.Json.Linq.JArray tempData = (Newtonsoft.Json.Linq.JArray)JsonConvert.DeserializeObject((string)TempData["attention"]);
                patientsAttention = tempData.ToObject<List<ConsultoryPatients>>();
                var patientRecovered = new Patient
                {
                    Id = patientsAttention[0].Id,
                    Name = patientsAttention[0].Name,
                    Age = patientsAttention[0].Age,
                    Document = patientsAttention[0].Document,
                    Gender = patientsAttention[0].Gender,
                    State = State.Recuperado
                };
                patientsAttention.RemoveAt(0);
                TempData["attention"] = JsonConvert.SerializeObject(patientsAttention);

                patientsRecovered.Add(patientRecovered);
                GetConsultories();

            }
        }
    }
}
