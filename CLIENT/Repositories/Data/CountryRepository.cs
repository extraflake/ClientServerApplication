﻿using API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CLIENT.Repositories.Data
{
    public class CountryRepository : GeneralRepository<Country>
    {
        public CountryRepository(string request = "Country/") : base(request)
        {

        }
    }
}
