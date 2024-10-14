using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Store.APIs.Errors;
using Store.Core.Dtos.Auth;
using Store.Core.Entities.Identity;
using Store.Core.Servecies.Contract;
using System.Security.Claims;

namespace Store.APIs.Controllers
{


    public class AccountsController : BaseApiController
    {
        private readonly IUserService _userService;
        private readonly ITokenService _tokenService;
        private readonly UserManager<AppUser> _userManager;

        public AccountsController(IUserService userService,
                                  UserManager<AppUser> userManager,
                                  ITokenService tokenService)
        {
            _userService = userService;
            _tokenService = tokenService;
            _userManager = userManager;
        }


        [HttpPost("Login")] // POST : /Api/Accounts/Login
        public async Task<ActionResult<UserDto>> LoginAsync(LoginDto loginDto)
        {

            var user = await _userService.LoginAsync(loginDto);

            if (user is null) return Unauthorized(new ApiErrorResponse(StatusCodes.Status401Unauthorized));

            return Ok(user);
        }



        [HttpPost("Register")] // POST : /Api/Accounts/Register
        public async Task<ActionResult<UserDto>> Register(RegisterDto registerDto)
        {

            var user = await _userService.RegisterAsync(registerDto);

            if (user is null) return BadRequest(new ApiErrorResponse(StatusCodes.Status400BadRequest));

            return Ok(user);
        }



        [HttpGet("GetCurrentUser")] // Get : /Api/Accounts/GetCurrentUser

        public async Task<ActionResult<UserDto>> GetCurrentUser()
        {
            var userEmail = User.FindFirstValue(ClaimTypes.Email);
            if (userEmail is null) return BadRequest(new ApiErrorResponse(StatusCodes.Status400BadRequest));

            var user =await _userManager.FindByEmailAsync(userEmail);
            if (user is null) return BadRequest(new ApiErrorResponse(StatusCodes.Status400BadRequest));

            return Ok(new UserDto()
            {
                DisplayName = user.DisplayName,
                Email = user.Email,
                Token = await _tokenService.CreateTokenAsync(user, _userManager)


            });
        }



    }
}
