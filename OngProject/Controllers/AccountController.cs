﻿using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using OngProject.Core.Models;
using OngProject.Core.Models.DTOs;
using System.Linq;
using System.Threading.Tasks;

namespace OngProject.Controllers
{
    [Route("auth/")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<Users> _userManager;
        private readonly SignInManager<Users> _signInManager;
        private readonly IMapper _mapper;

        public AccountController(UserManager<Users> userManager, SignInManager<Users> signInManager, IMapper mapper) 
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _mapper = mapper;
        }

        /// <summary>
        /// Login de usuario registrado al sistema. Rol: admin | standard
        /// </summary>
        /// <returns>Token de acceso</returns>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST /auth/login
        ///     {
        ///         "email": "user@example.com",
        ///         "password": "Password@123"
        ///     }
        ///
        /// </remarks>
        /// <response code="200">Solicitud concretada con exito</response>
        /// <response code="401">Credenciales no válidas</response>
        [HttpPost("login")]
        public async Task<ActionResult<Users>> Login(LoginUserDTO login)
        {
            var user = _userManager.Users.SingleOrDefault(x => x.Email == login.Email);

            if (user == null) return Unauthorized("Username or password incorrect");

            var result = await _signInManager.CheckPasswordSignInAsync(user, login.Password, false);

            if (!result.Succeeded) return Unauthorized();

            return Ok(user);
        }

        /// <summary>
        /// Registro de nuevo usuario standard al sistema.
        /// </summary>
        /// <returns>JSON del usuario registrado</returns>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST /auth/register
        ///     {
        ///         "userName": "marita",
        ///         "firstName": "Marita",
        ///         "lastName": "Gómez",
        ///         "email": "maritagomez@gmail.com",
        ///         "passwordHash": "Password@123"
        ///     }
        ///
        /// </remarks>
        /// <response code="200">Solicitud concretada con exito</response>
        /// <response code="401">Credenciales no válidas</response>
        [HttpPost("register")]
        public async Task<ActionResult<Users>> Register([FromBody] UserDTO user)
        {
            if (UserExists(user.UserName)) 
                return BadRequest("UserName already taken");

            var newUser = new Users { 
                UserName = user.UserName, 
                Email = user.Email, 
                LastName= user.LastName, 
                FirstName = user.FirstName 
            };

            var result = await _userManager.CreateAsync(newUser, user.PasswordHash);

            if (!result.Succeeded) 
                return BadRequest(result.Errors);

            return Ok(user);
        }

        private bool UserExists(string userName)
        {
            return _userManager.Users.Any(u => u.UserName == userName);
        }
    }
}
