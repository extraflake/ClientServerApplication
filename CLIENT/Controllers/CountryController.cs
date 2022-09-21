using API.Models;
using CLIENT.Base;
using CLIENT.Repositories.Data;
using CLIENT.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace CLIENT.Controllers
{
    [Authorize]
    public class CountryController : BaseController<Country, CountryRepository>
    {
        public CountryController(CountryRepository repository) : base(repository)
        {

        }

        public IActionResult Index()
        {
            return View();
        }
    }
}



/*
 * {
 *  "result" : 200,
 *  "data" : [
 *      { .... },
 *      { .... }
 *  ]
 * }
 * 
 */