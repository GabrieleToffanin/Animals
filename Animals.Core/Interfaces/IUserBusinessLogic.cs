using Animals.Core.Models.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Animals.Core.Interfaces
{
    public interface IUserBusinessLogic
    {
        ValueTask<string> RegisterUserAsync(RegisterModel model);
        ValueTask<AuthenticationModel> GetTokenAsync(TokenRequestModel model);
        ValueTask<string> AddRoleAsync(AddRoleModel model);
    }
}
