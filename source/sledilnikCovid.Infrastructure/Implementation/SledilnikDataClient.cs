using sledilnikCovid.Core.Models;
using sledilnikCovid.Infrastructure.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sledilnikCovid.Infrastructure.Implementation
{
    public class SledilnikDataClient : IFormatFetcher
    {

        private static readonly HttpClient client = new HttpClient();

        public async Task<List<string>> getCSV()
        {

            var response = await client.GetAsync("https://raw.githubusercontent.com/sledilnik/data/master/csv/region-cases.csv");
            if (response.IsSuccessStatusCode)
            {
                string responseBody = await response.Content.ReadAsStringAsync();
                List<string> responseList = responseBody.Split('\n').ToList();
                responseList = responseList.Take(responseList.Count() - 1).ToList();

                return responseList;
            }
            else
            { 
                return null;
            }
        }

        public List<Tuple<string, int>> GetListOfRegions(List<string> header)
        {
            List<Tuple<string, int>> regions = new List<Tuple<string, int>>();
            string regionPrev = "", regionNext = "";

            for (int i = 1; i < header.Count; i++)
            {
                string[] splitAttribute = header[i].Split('.');
                regionPrev = splitAttribute[1];

                if (regionNext != regionPrev)
                {
                    regions.Add(new Tuple<string, int>(splitAttribute[1], i));
                    regionNext = regionPrev;
                }
            }

            return regions;
        }

        public async Task<List<CasesDto>> FetchCases() {

            List<CasesDto> listCasesDto = new List<CasesDto>();

            List<string> listOfRows = await getCSV();

            List<string> header = listOfRows.ElementAt(0).Split(',').ToList();
            listOfRows = listOfRows.Skip(1).ToList();

            List<Tuple<string, int>> allRegions = GetListOfRegions(header);

            allRegions.RemoveAll(t => t.Item1 == "foreign" || t.Item1 == "unknown");

            //index = 0 je header
            for (int i = 0; i < listOfRows.Count; i++)
            {
                List<string> splitRow = listOfRows[i].Split(',').ToList();

                DateTime currentDate = DateTime.Parse(splitRow[0]);

                List<RegionData> listRegion = new List<RegionData>();

                int index = 0;

                allRegions.ForEach(region => {

                    var dac = (splitRow[allRegions[index].Item2] == "") ? 0
                            : int.Parse(splitRow[allRegions[index].Item2]);
                    var dtd = (splitRow[allRegions[index].Item2 + 2] == "") ? 0
                            : int.Parse(splitRow[allRegions[index].Item2 + 2]);
                    var fvtd = (splitRow[allRegions[index].Item2 + 3] == "") ? 0
                            : int.Parse(splitRow[allRegions[index].Item2 + 3]);
                    var svtd = (splitRow[allRegions[index].Item2 + 4] == "") ? 0
                            : int.Parse(splitRow[allRegions[index].Item2 + 4]);

                    RegionData tempRegion = new RegionData
                    {
                        RegionName = allRegions[index].Item1,
                        DailyActiveCases = dac, //indeks
                        DeceasedToDate = dtd, //indeks + 2
                        FirstVaccineToDate = fvtd, //indeks + 3
                        SecondVaccineToDate = svtd //indeks + 4
                    };
                    index++;
                    listRegion.Add(tempRegion);
                });

                CasesDto temp = new CasesDto
                {
                    Date = currentDate,
                    Region = listRegion
                };

                listCasesDto.Add(temp);
            }

            return listCasesDto;

        }
    }
}
