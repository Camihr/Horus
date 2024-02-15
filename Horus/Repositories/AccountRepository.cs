using Horus.Models;
using Horus.Resources;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Horus.Repositories
{
    public class AccountRepository
    {
        public async Task<ModelResponse<User>> SignIn(UserLogin request)
        {
            var jsonData = JsonConvert.SerializeObject(request);

            var content = new StringContent(jsonData, Encoding.UTF8, "application/json");

            var url = AppSettings.ResourceManager.GetString("ApiUrl") + AppSettings.ResourceManager.GetString("LoginEndPoint");

            using (var httpClient = new HttpClient())
            {
                try
                {
                    var response = await httpClient.PostAsync(url, content);

                    if (response.IsSuccessStatusCode)
                    {
                        var jsonResponse = await response.Content.ReadAsStringAsync();

                        var user = JsonConvert.DeserializeObject<User>(jsonResponse);

                        return new ModelResponse<User>
                        {
                            Data = user,
                            IsOk = true
                        };
                    }
                    else
                    {
                        return new ModelResponse<User>
                        {
                            Data = null,
                            IsOk = false,
                            Message = "El email y/o la contraseña son inválidas"
                        };
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error haciendo login: {ex}");

                    return new ModelResponse<User>
                    {
                        Data = null,
                        IsOk = false,
                        Message = "Ocurrió un error inesperado"
                    };
                }
            }
        }
    }
}
