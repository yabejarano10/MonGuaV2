using System;
using System.Collections.Generic;
using System.Text;

namespace DataContext.Models
{
    public enum Triage
    {
        AtenciónInmediata,
        RiesgoVital,
        UrgenciaMenor,
        NoUrgencia
    }

    public enum State
    {
        Enfermo,
        EnAtencion,
        Recuperado
    }
    public class Patient
    {
        public int Id { get; set; }
        public int Document {get; set;}
        public string Name {get; set;}
        public int Age {get; set;}
        public string Gender {get; set;}
        public Triage Triage { get; set; }
        public string Symptoms { get; set; }
        public State State { get; set; }


    }
}
