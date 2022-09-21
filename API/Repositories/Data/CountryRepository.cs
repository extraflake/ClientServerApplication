using API.Context;
using API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Repositories.Data
{
    public class CountryRepository : GeneralRepository<Country, int>
    {
        MyContext myContext;

        public CountryRepository(MyContext myContext) : base(myContext)
        {
            this.myContext = myContext;
        }

        public Country GetByName(string name)
        {
            var data = myContext.Countries.SingleOrDefault(x => x.Name.Equals(name));
            return data;
        }
    }
}
