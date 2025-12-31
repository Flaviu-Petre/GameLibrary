using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameLibrary.Service.Dtos.Publisher
{
    public class CreatePublisherDto
    {
        public string? Name { get; set; }
        public string? Country { get; set; }
        public string? Website { get; set; }
    }
}
