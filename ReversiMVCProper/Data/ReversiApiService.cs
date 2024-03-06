using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Newtonsoft.Json;
using ReversiMVCProper.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Text.Json.Serialization;

namespace ReversiMVCProper.Data
{
    public class ReversiApiService
    {
        private readonly HttpClient httpClient;

        public ReversiApiService()
        {
            httpClient = new()
            {
                BaseAddress = new Uri("http://localhost:5000/")
            };
        }

        public List<Spel> GetAllOpenSpellen()
        {
            List<Spel> spellen = new();
            var answer = httpClient.GetAsync("/api/spel/").Result;
            var answerconverted = answer.Content.ReadAsStringAsync().Result;
            var deser = JsonConvert.DeserializeObject<List<Spel>>(answerconverted);
            if (deser == null)
                return spellen;
            foreach(var item in deser)
            {
                if (item == null) 
                    continue;
                spellen.Add(item);
            }
            return spellen;
        }
        public Spel GetSpel(int id)
        {
            var answer = httpClient.GetAsync($"/api/spel/{id}").Result;
            var answerconverted = answer.Content.ReadAsStringAsync().Result;
            
	    if (answerconverted == "Spel bestaat niet!")
		return null;
	    var spel = JsonConvert.DeserializeObject<Spel>(answerconverted);
            return spel;
        }

        public Spel GetSpelFromSpeler(string id)
        {
            try
            {
                var answer = httpClient.GetAsync($"/api/spel/speler/spelertoken?spelertoken={id}").Result;
                if (!answer.IsSuccessStatusCode)
                    return null;
                var answerconverted = answer.Content.ReadAsStringAsync().Result;
                Spel spel = JsonConvert.DeserializeObject<Spel>(answerconverted);
                return spel;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public Spel CreateSpel(string spelerToken, string omschrijving)
        {
            FormUrlEncodedContent formContent = new(new[]
            {
                new KeyValuePair<string, string>("spelerToken", spelerToken),
                new KeyValuePair<string, string>("omschrijving", omschrijving)
            });

            HttpResponseMessage answer = httpClient.PostAsync($"/api/spel?spelerToken={spelerToken}&spelOmschrijving={omschrijving}", formContent).Result;
            var answerconverted = answer.Content.ReadAsStringAsync().Result;
            var spel = JsonConvert.DeserializeObject<Spel>(answerconverted);

            return spel;
        }

        public bool JoinSpel(string spelerToken, int id)
        {
            Spel spel = GetSpel(id);
            if (spelerToken == "")
                return false;
            if (spel == null)
                return false;
            if (spel.Speler1Token == spelerToken)
                return false;
            if (spel.Speler2Token != "")
                return false;
            HttpResponseMessage answer = httpClient.GetAsync($"/api/spel/join?spelerToken={spelerToken}&id={id}").Result;
            return true;
        }
        public void EndGame(int id)
        {
            _ = httpClient.GetAsync($"/api/spel/end?id={id}");
        }
    }
}
