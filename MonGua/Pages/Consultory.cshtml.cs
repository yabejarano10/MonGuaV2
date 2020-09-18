using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using DataContext.Models;
using DataContext.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace MonGua.Pages
{
    public class ConsultoryModel : PageModel
    {
        public List<Consultory> consultories { get; set; } = new List<Consultory>();
        public PatientsModel pmodel = new PatientsModel();

        public int newConsultoryId { get; private set; }
        public string newDoctorsName { get; private set; }
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
            pmodel.NotifyPatientInAttention(patient);
        }
    }
}
