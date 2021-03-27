using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace CannaBro.Models
{
    public class DataManager
    {
        public static List<StrainData> strains = new List<StrainData>();
        public static List<StrainData> favorites = new List<StrainData>();
<<<<<<< HEAD
        public static List<StrainData> recents = new List<StrainData>();
        public static List<FilterData> filters = new List<FilterData>();
=======
        public List<FilterData> filters = new List<FilterData>();
>>>>>>> 72e45dd82a36ec527d599372af22cba2e47786e1
        public List<string> temp = new List<string>();
        public List<NewsData> articles = new List<NewsData>();
        public List<StrainData> filteredList = new List<StrainData>();

        WebClient apiConnection = new WebClient();

        public async Task GetStrainsAsync()
        {
            // Clear lists to prevent duplicates on sign in.
            strains.Clear();
            favorites.Clear();
<<<<<<< HEAD
            recents.Clear();
            filters.Clear();
=======
>>>>>>> 72e45dd82a36ec527d599372af22cba2e47786e1

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
<<<<<<< HEAD
                    Name = result["Strain"].ToString().Replace("-", " "),
                    Race = result["Type"].ToString(),
                    Rating = double.Parse(result["Rating"].ToString()),
                    Star_1 = "starempty",
                    Star_2 = "starempty",
                    Star_3 = "starempty",
                    Star_4 = "starempty",
                    Star_5 = "starempty",
=======
                    Name = result["Strain"].ToString(),
                    Race = result["Type"].ToString(),
                    Rating = double.Parse(result["Rating"].ToString()),
>>>>>>> 72e45dd82a36ec527d599372af22cba2e47786e1
                    Effects = result["Effects"].ToString(),
                    Flavors = result["Flavor"].ToString(),
                    Description = result["Description"].ToString()
                };

                // Set race icon.
                switch (sd.Race)
                {
                    case "indica":
                        sd.RaceInitial = "i";
                        sd.RaceColor = Color.FromHex("FFF2CC");
                        break;

                    case "sativa":
                        sd.RaceInitial = "s";
                        sd.RaceColor = Color.FromHex("FDE0D9");
                        break;

                    case "hybrid":
                        sd.RaceInitial = "h";
                        sd.RaceColor = Color.FromHex("CAF4F4");
                        break;

                    default:
                        break;
                }

                // Set star rating.
                string[] stars = new string[] { sd.Star_1, sd.Star_2, sd.Star_3, sd.Star_4, sd.Star_5 };

                for (int i = 0; i < stars.Length; i++)
                {
                    if (sd.Rating >= i + 1)
                    {
                        switch (i)
                        {
                            case 0:
                                sd.Star_1 = "starfull";
                                break;
                            case 1:
                                sd.Star_2 = "starfull";
                                break;
                            case 2:
                                sd.Star_3 = "starfull";
                                break;
                            case 3:
                                sd.Star_4 = "starfull";
                                break;
                            case 4:
                                sd.Star_5 = "starfull";
                                break;
                            default:
                                break;
                        }
                    }
                    else if (sd.Rating >= i + 0.3)
                    {
                        switch (i)
                        {
                            case 0:
                                sd.Star_1 = "star.5";
                                break;
                            case 1:
                                sd.Star_2 = "star.5";
                                break;
                            case 2:
                                sd.Star_3 = "star.5";
                                break;
                            case 3:
                                sd.Star_4 = "star.5";
                                break;
                            case 4:
                                sd.Star_5 = "star.5";
                                break;
                            default:
                                break;
                        }
                    }
                }

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
<<<<<<< HEAD
                            Name = e,
                            Selected = false
=======
                            Name = e
>>>>>>> 72e45dd82a36ec527d599372af22cba2e47786e1
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
<<<<<<< HEAD
                            Name = f,
                            Selected = false
=======
                            Name = f
>>>>>>> 72e45dd82a36ec527d599372af22cba2e47786e1
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

<<<<<<< HEAD
                strain.FavFilename = inputInfo[0];
                strain.Favorited = true;
                favorites.Add(strain);
            }
        }

        public void SetRecents(string[] inputArray)
        {
            // Add recent straindata to list.
            foreach (string input in inputArray)
            {
                string[] inputInfo = input.Split(',');

                StrainData strain = strains.Where(i => i.Index == int.Parse(inputInfo[2])).FirstOrDefault();
                Console.WriteLine(strain.Name);

                strain.RecFilename = inputInfo[0];
                strain.DateViewed = DateTime.Parse(inputInfo[1]);
                recents.Add(strain);
=======
                strain.Filename = inputInfo[0];
                strain.Favorited = true;
                favorites.Add(strain);
>>>>>>> 72e45dd82a36ec527d599372af22cba2e47786e1
            }
        }
    }
}
