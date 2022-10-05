using AsyncStreamsApi.Model;
using AsyncStreamsApi.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace AsyncStreamsApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MovieController : ControllerBase
    {
        private readonly IMovieRepository _movieRepository;

        public MovieController(IMovieRepository movieRepository)
        {
            _movieRepository = movieRepository;
        }

        [HttpGet]
        public IAsyncEnumerable<Movie> GetAsync()
        {
            return _movieRepository.GetAsync();
        }
    }
}
