using System;
using System.Threading.Tasks;
using System.Net.Http;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace WebAPIClient
{
    class Peter
    {
        [JsonProperty("gender")]
        public string Gender { get; set; }

        [JsonProperty("count")]
        public int Count { get; set; }

        [JsonProperty("probability")]
        public double Probability { get; set; }
    }
    
    class Program
    {
        private static readonly HttpClient client = new HttpClient();

        static async Task Main(string[] args)
        {
            await ProcessRepo();
        }

        private static async Task ProcessRepo()
        {
            while (true)
            {
                try
                {
                    Console.WriteLine("Type Peter to reveal their gender. If not, press enter to end program.");
                    var singleName = Console.ReadLine();

                    if (string.IsNullOrEmpty(singleName))
                    {
                        break;
                    }

                    var names = await client.GetAsync("https://api.genderize.io/?name=peter" + singleName.ToLower());
                    var nameresult = await names.Content.ReadAsStringAsync();

                    var genderize = JsonConvert.DeserializeObject<Peter>(nameresult);

                    Console.WriteLine("~~~~~");
                    Console.WriteLine("Gender: " + genderize.Gender);
                    Console.WriteLine("Count: " + genderize.Count);
                    Console.WriteLine("Probability: " + genderize.Probability);
                    Console.WriteLine("~~~~~");
                }

                catch (Exception)
                {
                    Console.WriteLine("Error has occured. Please enter the correct name.");
                }
            }
        }
    }

}