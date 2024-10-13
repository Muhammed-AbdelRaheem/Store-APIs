using Store.Core.Dtos.Auth;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Core.Servecies.Contract
{
    public interface IUserService
    {
        Task<UserDto> LoginAsynce(LoginDto loginDto);
        Task<UserDto> RegisterAsynce(RegisterDto registerDto);


    }
}
