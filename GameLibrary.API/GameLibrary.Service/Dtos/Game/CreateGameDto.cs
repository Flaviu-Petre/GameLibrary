using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameLibrary.Service.Dtos.Game
{
    public class CreateGameDto
    {
        public string? Title { get; set; }
        public DateTime ReleaseDate { get; set; }
        public string? Description { get; set; }
        public int? DeveloperId { get; set; }
        public int? PublisherId { get; set; }
        public int? PlatformId { get; set; }
        public ICollection<int> GenreIds { get; set; } = new List<int>();
    }
}
