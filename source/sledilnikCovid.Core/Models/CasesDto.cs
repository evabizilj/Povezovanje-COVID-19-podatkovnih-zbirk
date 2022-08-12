using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sledilnikCovid.Core.Models
{
    public class CasesDto
    {
        public DateTime Date { get; set; }

        public List<RegionData>? Region { get; set; }


    }
}
