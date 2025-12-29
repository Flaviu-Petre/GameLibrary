using GameLibrary.Domain.Domains.Interface;
using GameLibrary.Repository.Repository.Interface;

namespace GameLibrary.Domain.Domains
{
    public class GenreDomain(IGenreRepository genreRepository) : IGenreDomain
    {
        private readonly IGenreRepository _genreRepository = genreRepository;

    }
}
