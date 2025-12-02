using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameLibrary.Core.Dtos.Common
{
    internal class PlatformDto
    {
        public string? Name { get; set; }
        public string? Manufacturer { get; set; }
        public List<GameDto>? Games { get; set; }
    }
}
