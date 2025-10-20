using Microsoft.AspNetCore.Mvc;
using StudentApi.Models;
using StudentApi.Services;

namespace StudentApi.Controllers
{
    /// <summary>
    /// Demonstrates how to call an external API from within our own API.  This
    /// controller uses the JokeService to fetch a random joke.  In a real
    /// application you might call other services such as weather forecasts or
    /// news feeds.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class ExternalController : ControllerBase
    {
        private readonly JokeService _jokeService;

        public ExternalController(JokeService jokeService)
        {
            _jokeService = jokeService;
        }

        /// <summary>
        /// Returns a random joke from an external public API.  If the joke could
        /// not be fetched an HTTP 502 Bad Gateway status is returned.
        /// </summary>
        [HttpGet("joke")]
        public async Task<ActionResult<Joke>> GetRandomJoke()
        {
            var joke = await _jokeService.GetRandomJokeAsync();
            if (joke is null)
            {
                return StatusCode(StatusCodes.Status502BadGateway, "Falha ao obter piada da API externa.");
            }
            return Ok(joke);
        }
    }
}