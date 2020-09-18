using CsvHelper;
using DataContext.Mappers;
using DataContext.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace DataContext.Services
{
    public class PatientService
    {
        public List<Patient> ReadCSVFile(string location)
        {
            try
            {
                using (var reader = new StreamReader(location, Encoding.Default)) 
                using(var csv = new CsvReader(reader))
                {
                    csv.Configuration.RegisterClassMap<PatientMap>();
                    var records = csv.GetRecords<Patient>().ToList();
                    return records;
                }
            }catch(Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
