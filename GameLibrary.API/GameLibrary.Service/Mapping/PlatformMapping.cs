using GameLibrary.Service.Dtos.Platform;
using GameLibrary.Entity.Entities;

namespace GameLibrary.Service.Mapping
{
    public static class PlatformMapping
    {
        public static PlatformDto ToDto(this Platform platform)
        {
            return new PlatformDto
            {
                Id = platform.Id,
                Name = platform.Name,
                Manufacturer = platform.Manufacturer,
                ReleaseYear = platform.ReleaseYear
            };
        }

        public static Platform ToEntity(this CreatePlatformDto platformDto)
        {
            return new Platform
            {
                Name = platformDto.Name,
                Manufacturer = platformDto.Manufacturer,
                ReleaseYear = platformDto.ReleaseYear
            };
        }

        public static Platform ToEntity(this UpdatePlatformDto platformDto)
        {
            return new Platform
            {
                Name = platformDto.Name,
                Manufacturer = platformDto.Manufacturer,
                ReleaseYear = platformDto.ReleaseYear
            };
        }

    }
}
