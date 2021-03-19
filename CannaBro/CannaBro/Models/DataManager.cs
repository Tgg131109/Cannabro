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
        public static List<StrainData> strains = new List<StrainData>();
        public static List<StrainData> favorites = new List<StrainData>();
        public List<FilterData> filters = new List<FilterData>();
        public List<string> temp = new List<string>();
        public List<NewsData> articles = new List<NewsData>();
        public List<StrainData> filteredList = new List<StrainData>();

        WebClient apiConnection = new WebClient();

        public async Task GetStrainsAsync()
        {
            // Clear lists to prevent duplicates on sign in.
            strains.Clear();
            favorites.Clear();

            string strainAPI = "https://medizen-ds.herokuapp.com/strains";
            string apiString = await apiConnection.DownloadStringTaskAsync(strainAPI);
            JArray jsonData = JArray.Parse(apiString);
            var jsonObjects = jsonData.OfType<JObject>().ToList();
            _ = new StrainData();

            // Loop through objects pulled from api and add to list.
            foreach (JObject result in jsonObjects)
            {
                // Create a new strain data object.
                StrainData sd = new StrainData()
                {
                    Index = int.Parse(result["index"].ToString()),
                    Name = result["Strain"].ToString(),
                    Race = result["Type"].ToString(),
                    Rating = double.Parse(result["Rating"].ToString()),
                    Effects = result["Effects"].ToString(),
                    Flavors = result["Flavor"].ToString(),
                    Description = result["Description"].ToString()
                };

                // Add straindata object to list.
                strains.Add(sd);

                // Split effects into an array.
                string[] strainEffects = result["Effects"].ToString().Split(',');

                // Loop through effects and add to list if new.
                foreach (string e in strainEffects)
                {
                    // Action if effect is not in list.
                    if (!temp.Contains(e))
                    {
                        // Add to temp list for further comparisons.
                        temp.Add(e);

                        // Create a new filter data object.
                        FilterData ed = new FilterData()
                        {
                            Type = "Effect",
                            Name = e
                        };

                        // Add filterdata object to list
                        filters.Add(ed);
                    }                  
                }

                // Split flavors into an array.
                string[] strainFlavors = result["Flavor"].ToString().Split(',');

                // Loop through flavors and add to list if new.
                foreach (string f in strainFlavors)
                {
                    // Action if flavor is not in list.
                    if (!temp.Contains(f))
                    {
                        // Add to temp list for further comparisons.
                        temp.Add(f);

                        // Create a new flavor data object.
                        FilterData fd = new FilterData()
                        {
                            Type = "Flavor",
                            Name = f
                        };

                        // Add filterdata object to list.
                        filters.Add(fd);
                    }
                }
            }
        }

        public async Task GetNewsAsync()
        {
            string startAPI = "https://newsapi.org/v2/everything?q=cannabis&pagesize=10&apiKey=7b30261f68c645af8995058bda703f34";
            string apiString = await apiConnection.DownloadStringTaskAsync(startAPI);
            JObject jsonData = JObject.Parse(apiString);
            _ = new NewsData();

            // Loop through objects pulled from api and add to list.
            foreach (JObject result in jsonData["articles"])
            {
                string title = result["title"].ToString();

                // Shorten long article headlines.
                if (result["title"].ToString().Length > 110)
                {
                    title = $"{result["title"].ToString().Substring(0, 110)}...";
                }

                NewsData newsData = new NewsData()
                {
                    Title = title,
                    Author = result["author"].ToString(),
                    Source = result["source"]["name"].ToString(),
                    Image = result["urlToImage"].ToString(),
                    Date = DateTime.Parse(result["publishedAt"].ToString()).ToString("d"),
                    URL = result["url"].ToString()
                };

                // Add news data object to list.
                articles.Add(newsData);
            }
        }

        public async Task FilterStrains(string filters, List<StrainData> inputList)
        {
            string filterAPI = "https://medizen-ds.herokuapp.com/rec/20/" + $"{filters}";
            string apiString = await apiConnection.DownloadStringTaskAsync(filterAPI);
            JArray jsonData = JArray.Parse(apiString);
            var jsonObjects = jsonData.OfType<JObject>().ToList();

            foreach(int result in jsonData)
            {
                StrainData strain = inputList.Where(i => i.Index == int.Parse(result.ToString())).FirstOrDefault();
                Console.WriteLine(strain.Name);

                filteredList.Add(strain);
            }
        }

        public void SetFavorites(string[] inputArray)
        {
            // Add favorite straindata to list.
            foreach (string input in inputArray)
            {
                string[] inputInfo = input.Split(',');

                StrainData strain = strains.Where(i => i.Index == int.Parse(inputInfo[1])).FirstOrDefault();
                Console.WriteLine(strain.Name);

                strain.Filename = inputInfo[0];
                strain.Favorited = true;
                favorites.Add(strain);
            }
        }
    }
}
