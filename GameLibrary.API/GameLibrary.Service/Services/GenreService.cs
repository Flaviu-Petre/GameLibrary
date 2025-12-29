using GameLibrary.Domain.Domains.Interface;
using GameLibrary.Service.Services.Interface;

namespace GameLibrary.Service.Services
{
    public class GenreService(IGenreDomain genreDomain) : IGenreService
    {
        private readonly IGenreDomain _genreDomain = genreDomain;
    }
}
