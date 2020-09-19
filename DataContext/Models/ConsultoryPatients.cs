using System;
using System.Collections.Generic;
using System.Text;

namespace DataContext.Models
{
    public class ConsultoryPatients
    {
        public int Id { get; set; }
        public int Document { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public string Gender { get; set; }
        public Triage Triage { get; set; }
        public string Symptoms { get; set; }
        public State State { get; set; }
        public int ConsultoryCode { get; set; }
        public string DoctorsName { get; set; }
        public bool Infected { get; set; } = false;
    }
}
