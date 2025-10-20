using System.Net.Http.Json;
using StudentApi.Models;

namespace StudentApi.Services
{

    public class JokeService
    {
        private readonly HttpClient _httpClient;

        public JokeService(HttpClient httpClient)
        {
            _httpClient = httpClient;
          
        }

     
        public async Task<Joke?> GetRandomJokeAsync()
        {
            try
            {
                var joke = await _httpClient.GetFromJsonAsync<Joke>("https://official-joke-api.appspot.com/random_joke");
                return joke;
            }
            catch
            {
                // In a real application you would log the exception here.
                return null;
            }
        }
    }
}
