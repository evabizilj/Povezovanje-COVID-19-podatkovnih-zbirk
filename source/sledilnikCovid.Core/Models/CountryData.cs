using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sledilnikCovid.Core.Models
{
    public class CountryData
    {
        public string CountryName { get; set; }

        public int NumberOfIntesiveCarePatients { get; set; }

        public int NumberOfVaccinated { get; set; }

        public int PopulationNumber { get; set; }


    }
}
