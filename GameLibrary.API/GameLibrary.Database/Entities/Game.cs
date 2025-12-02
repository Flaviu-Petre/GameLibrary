using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameLibrary.Database.Entities
{
    public class Game : BaseEntity
    {
        public string? Title { get; set; }
        public DateTime ReleaseDate { get; set; }
        public string? Description { get; set; }
        public List<User>? Users { get; set; }
        public Developer? Developer { get; set; }
        public List<Genre>? Genres { get; set; }
        public Publisher? Publisher { get; set; }
        public Platform? Platform { get; set; }
    }
}
