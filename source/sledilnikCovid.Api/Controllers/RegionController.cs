using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using sledilnikCovid.Application.Contracts;
using sledilnikCovid.Core.Models;

namespace sledilnikCovid.Api.Controllers
{   
    [Produces("application/json")]
    [Route("api")]
    [ApiController]
    public class RegionController : ControllerBase // Region because we have Europe and Slovenia datasets
    {

        private readonly IRegionService _regionService;

        public RegionController(
            IRegionService regionService
            )
        {
            _regionService = regionService;
        }

        /// <summary>
        /// Vrne vse primere z informacijami o podatkih osnovanih na datumu in regiji
        /// </summary>
        /// <param name="region"></param>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <returns>Vrne seznam primerov</returns>
        /// <response code="200">Vrne seznam primerov glede na željeno regijo v dani državi</response>
        /// <response code="400">Vrne izjemo glede na neustrezne vhodne podatke uporabnika</response>
        [HttpGet]
        [Route("slovenia/cases")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<List<CasesDto>>> GetCases(string? country, string? region, DateTime? from, DateTime? to)
        {
            //collect syntax and semantic errors
            var validRegions = new List<string> {"lj", "ce", "kr", "nm", "kk", "kp", "mb", "ms", "ng", "po", "sg", "za"};
            var exceptionStack = new List<string>();
            DateTime dDate;

            if (region != null && !validRegions.Contains(region))
                exceptionStack.Add("Invalid region.");

            if (from > to)
                exceptionStack.Add("'From' date cannot be later than 'To' date");

            //evaluate syntax and semantic errors
            if (exceptionStack.Count == 0)
            {
                var data = await _regionService.FetchDataCases(region, from, to);
                return Ok(data);
            }
            else {
                return BadRequest(exceptionStack);
            }

        }

        /// <summary>
        /// Vrne seznam vseh regij z vsoto aktivnih primerov v zadnjem tednu
        /// </summary>
        /// <returns>Seznam danih vsot glede na regijo</returns>
        /// <response code="200">Vrne seznam regij z njihovo korespondečno vsoto</response>
        /// <response code="400">Vrne izjemo glede na neustrezne vhodne podatke uporabnika</response>
        [HttpGet]
        [Route("slovenia/lastweek")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<List<LastweekDto>>> GetLastweek()
        {
            var data = await _regionService.FetchDataLastWeek();
            return Ok(data);
        }

        /// <summary>
        /// Vrne podatke o številu smrti, najvišje dnevno število smrti, število okuženih (pozitivnih) in število cepljenih v Sloveniji v obdobju analiziranega 2. in 4. vala
        /// </summary>
        /// <returns>Objekt VaccinationComparisonDto</returns>
        /// <response code="200">Vrne ustrezne podatke</response>
        /// <response code="400">Vrne izjemo glede na neustrezne vhodne podatke uporabnika</response>
        [HttpGet]
        [Route("slovenia/vaccinationComparison")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<List<VaccinationComparisonDto>>> GetVaccinationComparison(DateTime fromDateWave1, DateTime fromDateWave2, DateTime toDateWave1, DateTime toDateWave2)
        { 
            var data = await _regionService.FetchVaccinationComparison(fromDateWave1, fromDateWave2, toDateWave1, toDateWave2);
            return Ok(data);
        }

        /// <summary>
        /// Vrne število umrlih, število cepljenih, število okuženh (pozitivnih) po starostnih skupinah v obdobju analiziranega 2. in 4. vala
        /// </summary>
        /// <returns>Seznam objektovAverageAgeOfDeathDto</returns>
        /// <response code="200">Vrne ustrezne podatke</response>
        /// <response code="400">Vrne izjemo glede na neustrezne vhodne podatke uporabnika</response>
        [HttpGet]
        [Route("slovenia/averageAgeOfDeath")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<List<AverageAgeOfDeathDto>>> Get(DateTime fromDateWave1, DateTime fromDateWave2, DateTime toDateWave1, DateTime toDateWave2)
        {
            var data = await _regionService.FetchAverageAgeOfDeath(fromDateWave1, fromDateWave2, toDateWave1, toDateWave2);
            return Ok(data);
        }

        /// <summary>
        /// Vrne število okuženih, hospitalizacij na navadni in intenzivni negi ter število smrti v obdobju, ko je prevladovala originalna in delta različica
        /// </summary>
        /// <returns>Seznam objektov VaccinationHospitalisation</returns>
        /// <response code="200">Vrne ustrezne podatke</response>
        /// <response code="400">Vrne izjemo glede na neustrezne vhodne podatke uporabnika</response>
        [HttpGet]
        [Route("slovenia/variantInfectionLevel")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<List<VariantDto>>> GetVariantInfectionLevel()
        {
            var data = await _regionService.FetchVariantInfectionLevel(); ;
            return Ok(data);
        }


        /// <summary>
        /// Vrne 3 evropske države, kjer je precepljenost prebivalstva najvišja in 3 evropske države, kjer je precepljenost najnižja ter pri le-teh vrne delež hospitalizacij na intenzivni negi
        /// </summary>
        /// <returns>Seznam objektov VaccinationHospitalisation</returns>
        /// <response code="200">Vrne ustrezne podatke</response>
        /// <response code="400">Vrne izjemo glede na neustrezne vhodne podatke uporabnika</response>
        [HttpGet]
        [Route("europe/vaccinationHospitalisation")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<List<VaccinationHospitalisationDto>>> GetVaccinationHospitalisation()
        {
            var data = await _regionService.FetchVaccinationHospitalisation(); ;
            return Ok(data);
        }

    }
}
