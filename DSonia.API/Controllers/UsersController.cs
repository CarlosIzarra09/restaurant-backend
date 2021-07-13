using DSonia.API.Domain.Services;
using DSonia.API.Domain.Services.Communication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DSonia.API.Controllers
{

    [Authorize] //any users who will want access to this endpoint must be authenticaded
    [ApiController]
    [Route("/api/[controller]")]
    public class UsersController : ControllerBase
    {
        private IUserService _userService;
        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var users = _userService.GetAll();
            return Ok(users);
        }

        [AllowAnonymous]//This endpoint is an exception
        [HttpPost("authenticate")]
        public IActionResult Authenticate([FromBody] AuthenticationRequest request)
        {
            var response = _userService.Authenticate(request);
            if (response == null)
                return BadRequest(new { message = "Invalid Username or Password" });
            return Ok(response);
        }

        [AllowAnonymous]
        [HttpPost]
        public IActionResult Register(RegisterRequest request)
        {
            _userService.Register(request);
            return Ok(new { message = "Registration successful" });
        }
    }
}
