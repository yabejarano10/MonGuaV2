using CsvHelper.Configuration;
using DataContext.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataContext.Mappers
{
    class ConsultoryMap : ClassMap<Consultory>
    {
        public ConsultoryMap()
        {
            Map(x => x.Code).Name("Code");
            Map(x => x.DoctorsName).Name("DoctorsName");
        }
    }
}
