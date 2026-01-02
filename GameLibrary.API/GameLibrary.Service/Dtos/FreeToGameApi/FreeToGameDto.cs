using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameLibrary.Service.Dtos.FreeToGameApiDto
{
    public class FreeToGameDto
    {
        public string Title { get; set; } = string.Empty;
        public string Short_Description { get; set; } = string.Empty;
        public string Game_Url { get; set; } = string.Empty;
        public string Genre { get; set; } = string.Empty;
        public string Platform { get; set; } = string.Empty;
        public string Publisher { get; set; } = string.Empty;
        public string Developer { get; set; } = string.Empty;
        public string Release_Date { get; set; } = string.Empty;
    }
}
