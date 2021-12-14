using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Models.Request;
using Receipts_Server.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Receipts_Server.Controllers
{
    [ApiController]
    [Route("authorization")]
    public class AuthorizationController : Controller
    {
        private IUserAuthorizationService _userAuthorizationService;

        public AuthorizationController(IUserAuthorizationService userAuthorizationService)
        {
            _userAuthorizationService = userAuthorizationService;
        }

        [HttpPost]
        [Route("authenticate")]
        public IActionResult Authenticate(AuthenticateRequest model)
        {
            var response = _userAuthorizationService.Authenticate(model);

            if (response == null)
                return BadRequest(new { message = "Username or password is incorrect" });

            HttpContext.Response.Cookies.Append("currentOwner", response.OwnerId.ToString());
            return Ok(response);
        }

        [HttpPost]
        [Route("registration")]
        public IActionResult Registrate(OwnerRegisterData model)
        {
            var response = _userAuthorizationService.Register(model);

            if (response == null)
                return BadRequest(new { message = "Register is failed" });

            return Ok(response);
        }

        [HttpPut]
        [Route("exit")]
        public void Exit()
        {
            HttpContext.Response.Cookies.Delete("currentOwner");
        }
    }
}