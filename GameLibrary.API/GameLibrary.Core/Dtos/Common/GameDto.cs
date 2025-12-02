using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameLibrary.Core.Dtos.Common
{
    public class GameDto
    {
        public string? Title { get; set; }
        public DateTime ReleaseDate { get; set; }
        public string? Description { get; set; }
        public List<UserDto>? Users { get; set; }
        public DeveloperDto? Developer { get; set; }
        public List<GenreDto>? Genres { get; set; }
        public PublisherDto? Publisher { get; set; }
    }
}