using APIService.UserLogin;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using APIService.Models;
using APIService.DTOs;
using System.Threading.Tasks;
using APIService.Services;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Authorization;
using APIService.Interfaces;

namespace APIService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly JwtSettings _jwtSettings;
        private readonly IUserService _userService;

        public UserController(JwtSettings jwtSettings, IUserService userService)
        {
            _jwtSettings = jwtSettings;
            _userService = userService;
        }

        [HttpGet]
        public ActionResult<List<User>> GetUsers()
        {
            return _userService.GetUsers();
        }

        [HttpGet("{id:length(24)}")]
        public ActionResult<User> GetUser(string id)
        {
            var user = _userService.GetUser(id);

            //if (user == null)
            //{
            //    return NotFound();
            //}

            return Ok(user);
        }

        [HttpPost]
        public async Task <ActionResult<User>> Create(User user)
        {
            await _userService.CreateUser(user);
            return CreatedAtAction(nameof(GetUser), new { id = user.Id }, user);
        }


        [Route("authenticate")]
        [HttpPost]
        public ActionResult Login([FromBody] User user)
        {
            var token = _userService.Authenticate(user.UserName, user.Password);

            if(token == null)
            {
                return Unauthorized();
            }

            return Ok(new { token, user });
        }
    }
}
