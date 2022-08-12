using sledilnikCovid.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sledilnikCovid.Application.Contracts
{
    public interface IRegionService
    {
        public Task<List<CasesDto>> FetchDataCases(string region, DateTime? from, DateTime? to);

        public Task<List<LastweekDto>> FetchDataLastWeek();

        public Task<List<LastweekDto>> FetchVaccinationComparison(DateTime fromDateWave1, DateTime fromDateWave2, DateTime toDateWave1, DateTime toDateWave2);

        public Task<List<LastweekDto>> FetchAverageAgeOfDeath(DateTime fromDateWave1, DateTime fromDateWave2, DateTime toDateWave1, DateTime toDateWave2);

        public Task<List<LastweekDto>>FetchVaccinationHospitalisation();

        public Task<List<LastweekDto>> FetchVariantInfectionLevel();

    }
}
