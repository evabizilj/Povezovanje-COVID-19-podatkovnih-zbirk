using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sledilnikCovid.Core.Models
{
    public class VariantDto
    {
        public string Variant { get; set; }

        public int NumberOfDeaths { get; set; }

        public int NumberOfInfected { get; set; }

        public int NumberOfIntensiveCarePatients { get; set; }

        public int NumberOfNormalCarePatients { get; set; }
    }
}
