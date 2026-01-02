using GameLibrary.Service.Dtos.FreeToGameApiDto;
using GameLibrary.Service.Services.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace GameLibrary.Service.Services
{
    public class FreeToGameService(HttpClient httpClient) : IFreeToGameService
    {
        private readonly HttpClient _httpClient = httpClient;

        public async Task<IEnumerable<FreeToGameDto>> GetFreeGamesAsync()
        {
            var response = await _httpClient.GetFromJsonAsync<IEnumerable<FreeToGameDto>>("games");
            return response ?? Enumerable.Empty<FreeToGameDto>();
        }
    }
}
