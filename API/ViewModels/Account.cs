using API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.ViewModels
{
    public class Account
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public List<Role> Roles { get; set; }
    }
}
