using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Horus.Models;
using Newtonsoft.Json;
using Horus.Resources;
using Xamarin.Essentials;

namespace Horus.Repositories
{
    public class ChallengesRepository
    {
        public async Task<ModelResponse<List<Challenge>>> GetChallenges()
        {
            var url = AppSettings.ResourceManager.GetString("ApiUrl") + AppSettings.ResourceManager.GetString("ChallengesEndPoint");

            using (var httpClient = new HttpClient())
            {
                try
                {
                    var token = await SecureStorage.GetAsync("Token");

                    var user = JsonConvert.DeserializeObject<User>(token);

                    httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue(user.AuthorizationToken);

                    var response = await httpClient.GetAsync(url);

                    if (response.IsSuccessStatusCode)
                    {
                        var jsonResponse = await response.Content.ReadAsStringAsync();

                        var chanllenges = JsonConvert.DeserializeObject<List<Challenge>>(jsonResponse);

                        return new ModelResponse<List<Challenge>>()
                        {
                            IsOk = true,
                            Data = chanllenges
                        };
                    }
                    else
                    {
                        return new ModelResponse<List<Challenge>>() 
                        {
                            IsOk = false,
                            Data = new List<Challenge>(),
                            Message = "No se encontraron retos"
                        };
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error solicitando retos: {ex}");
                    
                    return new ModelResponse<List<Challenge>>()
                    {
                        IsOk = false,
                        Message = "Ocurrió un error inesperado"
                    };
                }
            }
        }
    }
}
