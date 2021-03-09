using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace CannaBro.Models
{
    public class DataManager
    {
        public List<StrainData> resultData = new List<StrainData>();
        WebClient apiConnection = new WebClient();

        public async Task GetStrainsAsync()
        {
            string startAPI = "http://strainapi.evanbusse.com/IXujyIC/strains/search/flavor/pine"; // This will be split to allow for different types of searches in the final version.
            string apiString = await apiConnection.DownloadStringTaskAsync(startAPI);
            JArray jsonData = JArray.Parse(apiString);
            var jsonObjects = jsonData.OfType<JObject>().ToList();
            StrainData sd = new StrainData();

            // Loop through objects pulled from api and add to list.
            foreach (JObject result in jsonObjects)
            {
                sd = new StrainData()
                {
                    StrainName = result["name"].ToString()
                };

                // Add straindata object to list.
                resultData.Add(sd);
            }
        }
    }
}
