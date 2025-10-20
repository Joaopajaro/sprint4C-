using Microsoft.AspNetCore.Mvc;
using StudentApi.Models;
using StudentApi.Services;

namespace StudentApi.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class ExternalController : ControllerBase
    {
        private readonly JokeService _jokeService;

        public ExternalController(JokeService jokeService)
        {
            _jokeService = jokeService;
        }

    
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
