using System;
using System.Collections.Generic;
using System.Text;

namespace LittleLive.Core.Models
{
    public class JWTResource
    {
        public string SecretKey { get; set; }
        public string Issuer { get; set; }
        public string Audience { get; set; }

    }
}
