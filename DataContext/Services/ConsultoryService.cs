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
    public class ConsultoryService
    {
        public List<Consultory> ReadCSVFile(string location)
        {
            try
            {
                using (var reader = new StreamReader(location, Encoding.Default))
                using (var csv = new CsvReader(reader))
                {
                    csv.Configuration.RegisterClassMap<ConsultoryMap>();
                    var records = csv.GetRecords<Consultory>().ToList();
                    return records;
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
