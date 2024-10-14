using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Store.APIs.Errors;
using Store.APIs.Extensions;
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
        private readonly IMapper _mapper;
        private readonly UserManager<AppUser> _userManager;

        public AccountsController(IUserService userService,
                                  UserManager<AppUser> userManager,
                                  ITokenService tokenService,
                                  IMapper mapper)
        {
            _userService = userService;
            _tokenService = tokenService;
            _mapper = mapper;
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
        [Authorize]
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



        [HttpGet("Address")] // Get : /Api/Accounts/GetAddress
        [Authorize]

        public async Task<ActionResult<UserDto>> GetUserAddress()
        {
            var userEmail = User.FindFirstValue(ClaimTypes.Email);
            var user = await _userManager.FindByEmailWithAddressAsync(User);
            return Ok(_mapper.Map<AddressDto>(user.Address));
        }


        [HttpPut("Address")] // Put : /Api/Accounts/Address
        [Authorize]

        public async Task<ActionResult<UserDto>> UpdateUserAddress(AddressDto updatedAddress)
        {
            var user = await _userManager.FindByEmailWithAddressAsync(User);
            var mappedAddress = _mapper.Map<AddressDto,Address>(updatedAddress);

            mappedAddress.Id = user.Address.Id;
            user.Address = mappedAddress;

          var result=  await _userManager.UpdateAsync(user);

            if (!result.Succeeded)
            {
                return BadRequest(new ApiErrorResponse(400));
            }
            return Ok(updatedAddress);
        }




    }
}
