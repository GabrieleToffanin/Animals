using Animals.Core.Interfaces;
using Animals.Core.Models.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Animals.Core.Logic
{
    public class UserBusinessLogic : IUserBusinessLogic
    {
        private readonly IUserService _userService;

        public UserBusinessLogic(IUserService userService)
        {
            _userService = userService;
        }

        public async Task<string> AddRoleAsync(AddRoleModel model)
        {
            return await _userService.AddRoleAsync(model);
        }

        public async Task<AuthenticationModel> GetTokenAsync(TokenRequestModel model)
        {
            return await _userService.GetTokenAsync(model);
        }

        public async Task<string> RegisterUserAsync(RegisterModel model)
        {
            
            return await _userService.RegisterUserAsync(model);
        }
    }
}
