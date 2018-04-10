﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.IdentityModel.Tokens;

namespace HResource.Common
{
    public class AuthOptions
    {
        public string Issuer {get; set; }
        public string Audience { get; set; }
        public string Key { get; set; }
        public int LifeTime { get; set; }
    }
}
