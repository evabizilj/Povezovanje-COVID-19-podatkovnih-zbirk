using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sledilnikCovid.Core.Models
{
    public class VaccinationComparisonDto
    {

        public int NumberOfDeaths { get; set; }

        public int MaxDailyDeaths { get; set; }

        public int NumberOfInfected { get; set; }

        public int NumberOfVaccinated { get; set; }

    }
}
