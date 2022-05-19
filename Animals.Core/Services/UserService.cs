using Animals.Core.Interfaces;
using Animals.Core.Models.User;
using Animals.Core.Settings;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Animals.Core.AuthConstants;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;

namespace Animals.Core.Services
{
    /// <summary>
    /// UserService class is responsible of managing UserData in API Context exposes methods 
    /// for JWT related Authentication
    /// </summary>
    public class UserService : IUserService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly JWT _jwt;

        public UserService(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, IOptions<JWT> jwt)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _jwt = jwt.Value;
        }

        /// <summary>
        /// Adds a role to already registered User
        /// This method must be possibily used by the Api-Developer with CreatorRole
        /// </summary>
        /// <param name="model">AddRoleModel request for Registered User</param>
        /// <returns>Operation Result Message</returns>
        public async ValueTask<string> AddRoleAsync(AddRoleModel model)
        {
            var user = await _userManager.FindByEmailAsync(model.Email);
            if(user == null)
            {
                return $"No Accounts Registered with {model.Email}";
            }
            if (await _userManager.CheckPasswordAsync(user, model.Password))
            {
                var roleExists = Enum.GetNames(typeof(Authorization.Roles))
                    .Any(x => x.ToLower() == model?.Role?.ToLower());
                if (roleExists)
                {
                    var validRole = Enum.GetValues(typeof(Authorization.Roles))
                                              .Cast<Authorization.Roles>()
                                              .Where(x => x.ToString().ToLower() == model.Role.ToLower()).FirstOrDefault();
                    await _userManager.AddToRoleAsync(user, validRole.ToString());
                    return $"Added {model.Role} to user {model.Email}.";
                }
                return $"Role {model.Role} not found.";
            }
            return $"Incorrect Credentials for user {user.Email}.";
        }

        /// <summary>
        /// This method has to be called during Log-In phase, 
        /// it provides JWT Bearer Token, so the Client-User 
        /// can insert the token in Header
        /// </summary>
        /// <param name="model">Token Request Model, LoginPhase</param>
        /// <returns>Return an authenticationModel with JTW Bearer Token in <paramref name="authenticationModel"/>.<paramref name="Token"/> as string</returns>
        public async ValueTask<AuthenticationModel> GetTokenAsync(TokenRequestModel model)
        {
            var authenticationModel = new AuthenticationModel();
            var user = await _userManager.FindByEmailAsync(model.Email);

            if(user == null)
            {
                authenticationModel.IsAuthenticated = false;
                authenticationModel.Message = $"No accounts registered with {model.Email}";
                return authenticationModel;
            }

            if(await _userManager.CheckPasswordAsync(user, model.Password))
            {
                //Creates JWT Token 
                authenticationModel.IsAuthenticated = true;
                JwtSecurityToken jwtSecurityToken = await CreateJwtToken(user);
                authenticationModel.Token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);
                //Sets values to AuthenticationModel 
                authenticationModel.Email = user.Email;
                authenticationModel.UserName = user.UserName;
                var rolesList = await _userManager.GetRolesAsync(user).ConfigureAwait(false);
                authenticationModel.Roles = rolesList.ToList();

                return authenticationModel;
            }
            authenticationModel.IsAuthenticated = false;
            authenticationModel.Message = $"Incorrect Credentials for user {user.Email}.";
            return authenticationModel;
        }

        /// <summary>
        /// Method who allows to register and save user to DB
        /// </summary>
        /// <param name="model">Register model asks providing via HttpGet body required informations for registering</param>
        /// <returns>String result</returns>
        public async ValueTask<string> RegisterUserAsync(RegisterModel model)
        {
            var user = new ApplicationUser
            {
                UserName = model.Username,
                Email = model.Email,
                FirstName = model.FirstName,
                LastName = model.LastName
            };

            var userWithSameEmail = await _userManager.FindByEmailAsync(model.Email);
            if (userWithSameEmail == null)
            {
                var result = await _userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                    await _userManager.AddToRoleAsync(user, Authorization.default_role.ToString());
                return $"User registered with username {user.UserName}";
            }
            else return $"Email {user.Email} already exists";
        }

        /// <summary>
        /// Sets JWT token claims and updates if expiration date is arrived 
        /// </summary>
        /// <param name="user">Logging user</param>
        /// <returns>JWT Token for current loggin in user</returns>
        private async ValueTask<JwtSecurityToken> CreateJwtToken(ApplicationUser user)
        {
            var userClaims = await _userManager.GetClaimsAsync(user);
            var roles = await _userManager.GetRolesAsync(user);

            var roleClaims = new List<Claim>();

            foreach (var item in roles)
                roleClaims.Add(new Claim("roles", item));

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim("uid", user.Id)
            }
            .Union(userClaims)
            .Union(roleClaims);

            var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwt.Key));
            var signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);

            var jwtSecurityToken = new JwtSecurityToken(
                issuer: _jwt.Issuer,
                audience: _jwt.Audience,
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(_jwt.DurationInMinutes),
                signingCredentials: signingCredentials);

            return jwtSecurityToken;
        }
    }
}
