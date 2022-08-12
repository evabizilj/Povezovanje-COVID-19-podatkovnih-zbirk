using sledilnikCovid.Application.Contracts;
using sledilnikCovid.Core.Models;
using sledilnikCovid.Infrastructure.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sledilnikCovid.Application
{
    public class RegionService : IRegionService
    {
        private readonly IFormatFetcher _formatFetcher;

        public RegionService(
            IFormatFetcher formatManager
            )
        {
            _formatFetcher = formatManager;

        }

        public async Task<List<CasesDto>> FetchDataCases(string? region, DateTime? from, DateTime? to)
        {

            var data = await _formatFetcher.FetchCases();

            //check Semantics

            if (from != null)
                data = data.Where(x => x.Date >= from).ToList();

            if (to != null)
                data = data.Where(x => x.Date <= to).ToList();

            if (region != null)
            {
                data = data.Select(x => new CasesDto
                {
                    Date = x.Date,
                    Region = x.Region?
                        .Where(r => r.RegionName == region)
                        .ToList()
                })
                .Where(y => y.Region.Any()).ToList();
            }

            return data;
        }

        public async Task<List<LastweekDto>> FetchDataLastWeek()
        {
            var data = await _formatFetcher.FetchCases();
            var dataLastWeek = data.TakeLast(7).ToList();

            var regionIndex = data[0].Region.Count;
            List<LastweekDto> groupedSums = new List<LastweekDto>();

            Enumerable.Range(0, regionIndex).ToList().
                ForEach(x =>
                {
                    int sumTotal = 0;
                    string name = data[0].Region[x].RegionName;

                    sumTotal = dataLastWeek.Aggregate(0, (sum, b) => sum + b.Region[x].DailyActiveCases);

                    LastweekDto temp = new LastweekDto
                    {
                        RegionName = name,
                        LastWeekSum = sumTotal
                    };

                    groupedSums.Add(temp);
                });

            groupedSums = groupedSums.OrderByDescending(x => x.LastWeekSum).ToList();

            return groupedSums;

        }

        public async Task<List<LastweekDto>> FetchVaccinationComparison(DateTime fromDateWave1, DateTime fromDateWave2, DateTime toDateWave1, DateTime toDateWave2)
        {
            return null;
        }

        public async Task<List<LastweekDto>> FetchAverageAgeOfDeath(DateTime fromDateWave1, DateTime fromDateWave2, DateTime toDateWave1, DateTime toDateWave2)
        {
            return null;
        }

        public async Task<List<LastweekDto>> FetchVaccinationHospitalisation()
        {
            return null;
        }

        public async Task<List<LastweekDto>> FetchVariantInfectionLevel()
        {
            return null;
        }
    }
}
