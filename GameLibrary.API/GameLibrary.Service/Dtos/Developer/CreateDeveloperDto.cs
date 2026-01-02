using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameLibrary.Service.Dtos.Developer
{
    public class CreateDeveloperDto
    {
        public string? Name { get; set; }
        public string? Country { get; set; }
        public DateTime FoundedDate { get; set; }
    }
}
