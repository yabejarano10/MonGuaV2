using System;
using System.Collections.Generic;
using System.Text;

namespace DataContext.Models
{
    public class Consultory
    {
        public int Code { get; set; }
        public string DoctorsName { get; set; }
        public bool Available { get; set; } = true;
    }
}
