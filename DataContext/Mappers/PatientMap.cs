using CsvHelper.Configuration;
using DataContext.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataContext.Mappers
{
    public sealed class PatientMap : ClassMap<Patient>
    {
        public PatientMap()
        {
            Map(x => x.Id).Name("Id");
            Map(x => x.Document).Name("Document");
            Map(x => x.Name).Name("Name");
            Map(x => x.Age).Name("Age");
            Map(x => x.Gender).Name("Gender");
            Map(x => x.Triage).Name("Triage");
            Map(x => x.Symptoms).Name("Symptoms");
        }
    }
}
