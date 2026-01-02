using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameLibrary.Service.Dtos.Game
{
    public class GameDto
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public DateTime ReleaseDate { get; set; }
        public string? Description { get; set; }
        public string? Developer { get; set; }
        public string? Publisher { get; set; }
        public string? Platform { get; set; }
        public string? UserCount { get; set; }
        public ICollection<string> Genres { get; set; } = new List<string>();
    }
}
