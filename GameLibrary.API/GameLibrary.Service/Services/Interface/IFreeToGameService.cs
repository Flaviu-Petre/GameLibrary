using GameLibrary.Service.Dtos.FreeToGameApiDto;

namespace GameLibrary.Service.Services.Interface
{
    public interface IFreeToGameService
    {
        Task<IEnumerable<FreeToGameDto>> GetFreeGamesAsync();
    }
}
