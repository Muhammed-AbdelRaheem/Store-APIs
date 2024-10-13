﻿using Microsoft.AspNetCore.Identity;
using Store.Core.Dtos.Auth;
using Store.Core.Entities.Identity;
using Store.Core.Servecies.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Services.Servecies
{
    public class UserService : IUserService
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManeger;
        private readonly ITokenService _tokenService;

        public UserService(UserManager<AppUser> userManager,
                           SignInManager<AppUser> signInManeger,
                           ITokenService tokenService)
        {
            _userManager = userManager;
            _signInManeger = signInManeger;
            _tokenService = tokenService;
        }


        public async Task<UserDto> LoginAsynce(LoginDto loginDto)
        {

            var user = await _userManager.FindByEmailAsync(loginDto.Email);

            if (user is null) return null;

            var result = await _signInManeger.CheckPasswordSignInAsync(user, loginDto.Password, false);

            if (!result.Succeeded) return null;
            return new UserDto()
            {
                Email = user.Email,
                DisplayName = user.DisplayName,
                Token = await _tokenService.CreateTokenAsync(user, _userManager)


            };
             
        }

        public Task<UserDto> RegisterAsynce(RegisterDto registerDto)
        {
            throw new NotImplementedException();
        }
    }
}
