using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyFirstWebApplication1.Options
{
    public class JwtSettings
    {
        public string Secret { get; set; }

        public System.TimeSpan TokenLifetime { get; set; }
    }
}
