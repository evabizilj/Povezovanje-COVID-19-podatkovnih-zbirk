
import {CREDENTIALS} from '~/constants.utils.js'

const cron = require("node-cron");
const axios = require("axios");
const port = 3000;

const BASE_URL = "https://coviddata.mysql.database.azure.com/api/Region";

app.listen(port, () => {
  console.log(`Success! Aplikacija se izvaja na portu: ${port}.`);
});

const authTrigger = async () => {
  await axios.basicAuth(`${BASE_URL}/auth`, {CREDENTIALS});
};

// Nastavimo CRON jobs, da se izvedejo vsakih 6 ur periodično.
// Vsi parametri so NULLABLE, torej niso potrebni za izvedbo zaledne procedure API-ja

// Vrne vse primere z informacijami o podatkih osnovanih na datumu in regiji
export cron.schedule("0 */6 * * *", const fetchCases = async (country, region, from, to) => {
  const response = await axios.get(`${BASE_URL}/cases?
  	country=${country}&
  	region=${region}&
  	from=${from}&
  	to=${to}
  	`);
  const data = response.data;
  return data;
});

// Vrne seznam vseh regij z vsoto aktivnih primerov v zadnjem tednu
export cron.schedule("0 */6 * * *", const fetchLastweek = async () => {
  const response = await axios.get(`${BASE_URL}/lastweek`);
  const data = response.data;
  return data;
});

// Vrne podatke o številu smrti, najvišje dnevno število smrti, število okuženih in število cep. v SLO
export cron.schedule("0 */6 * * *", const fetchVaccinationComparison = async (from1, from2, to1, to2) => {
  const response = await axios.get(`${BASE_URL}/vaccinationComparison?
  	fromDateWave1=${from1}&
  	fromDateWave2=${from2}&
  	toDateWave1=${to1}&
  	toDateWave2=${to2}
  	`);
  const data = response.data;
  return data;
});


// Vrne število umrlih, število cepljenih, število okuženih po starostnih skupinah v obdobju 2. in 4. vala
export cron.schedule("0 */6 * * *", const fetchAverageAgeOfDeath = async (from1, from2, to1, to2) => {
  const response = await axios.get(`${BASE_URL}/averageAgeOfDeath?
  	fromDateWave1=${from1}&
  	fromDateWave2=${from2}&
  	toDateWave1=${to1}&
  	toDateWave2=${to2}
  	`);
  const data = response.data;
  return data;
});
 
// Vrne 3 evropske države, kjer je precepljenost najvišja in 3, kjer je najnižja
export cron.schedule("0 */6 * * *", const fetchVaccinationHospitalisation = async () => {
  const response = await axios.get(`${BASE_URL}/vaccinationHospitalisation`);
  const data = response.data;
  return data;
});

 // Vrne št. okuženih, hospitalizacij na navadni in intenzivni negi ter št. smrti v obdobju DELTA variante
export cron.schedule("0 */6 * * *", const fetchVariantInfectionLevel = async () => {
  const response = await axios.get(`${BASE_URL}/variantInfectionLevel`);
  const data = response.data;
  return data;
});
 




