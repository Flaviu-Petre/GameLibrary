using GameLibrary.Infrastructure.Config.Models;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameLibrary.Infrastructure.Config
{
    public class AppConfig
    {
        public static ConnectionStringsSettings? ConnectionStrings { get; set; }
        public static void Init(IConfiguration configuration)
        {
            Configure(configuration);
        }
        private static void Configure(IConfiguration configuration)
        {
            ConnectionStrings = configuration.GetSection("ConnectionStrings").Get<ConnectionStringsSettings>();
        }
    }
}
