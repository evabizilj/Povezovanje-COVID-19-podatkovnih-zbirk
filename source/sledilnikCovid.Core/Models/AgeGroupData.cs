using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sledilnikCovid.Core.Models
{
    public class AgeGroupData
    {

        public string AgeGroup { get; set; }

        public int Deaths {  get; set;}

        public int Vaccinated { get; set; }

        public int Infected { get; set; }
    }
}
