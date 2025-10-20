using System.Net.Http.Json;
using StudentApi.Models;

namespace StudentApi.Services
{
    /// <summary>
    /// Provides a method for retrieving a random joke from a public API.  The
    /// HttpClient instance is injected by the framework and configured in
    /// Program.cs.  System.Net.Http.Json is used to deserialize the JSON
    /// response into the Joke model.
    /// </summary>
    public class JokeService
    {
        private readonly HttpClient _httpClient;

        public JokeService(HttpClient httpClient)
        {
            _httpClient = httpClient;
            // Set base address or default headers if needed.  In this simple
            // example we set no base address because we pass the full URI.
        }

        /// <summary>
        /// Calls an external API to retrieve a random joke.  Returns null if
        /// the request fails.  The API used is https://official-joke-api.appspot.com/random_joke.
        /// </summary>
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