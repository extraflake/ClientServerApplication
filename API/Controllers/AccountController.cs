using API.Middleware;
using API.Repositories.Data;
using API.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        AccountRepository accountRepository;
        IJWTHandler JWT;

        public AccountController(AccountRepository accountRepository, IJWTHandler JWT)
        {
            this.accountRepository = accountRepository;
            this.JWT = JWT;
        }

        [HttpPost]
        [Route("Login")]
        public IActionResult Login(Login login)
        {
            // var data = accountRepository.Login(email, password);
            Account account = new Account()
            {
                Id = 1,
                Name = "Naufal Aji Wibowo",
                Email = "Naufal.Aji@mii.co.id",
            };
            var token = JWT.GenerateToken(account);
            return Ok(token);
        }

        [HttpPost]
        public IActionResult Register()
        {
            return Ok();
        }

        [HttpPost]
        public IActionResult ChangePassword()
        {
            return Ok();
        }

        [HttpPost]
        public IActionResult ForgotPassword()
        {
            return Ok();
        }
    }
}
