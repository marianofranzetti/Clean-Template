using CleanArchitecture.Application.Constants;
using CleanArchitecture.Application.Contracts.Identity;
using CleanArchitecture.Application.Models.Identity;
using CleanArchitecture.Identity.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace CleanArchitecture.Identity.Services
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<ApplicationuUser> _userManager;
        private readonly SignInManager<ApplicationuUser> _signInManager;
        private readonly JwtSettings _jwtSettings;

        public AuthService(UserManager<ApplicationuUser> userManager, SignInManager<ApplicationuUser> signInManager,
          IOptions<JwtSettings> jwtSettings)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _jwtSettings = jwtSettings.Value;
        }

        public async Task<AuthResponse> Login(AuthRequest request)
        {
            var user = await _userManager.FindByEmailAsync(request.Email);

            if (user is null)
            {
                throw new Exception($"El usuario con el email {request.Email} no existe");
            }

            var result = await _signInManager.PasswordSignInAsync
                (user.UserName!, request.Password, false, lockoutOnFailure: false);

            if (!result.Succeeded)
            {
                throw new Exception($"Las credenciales son incorrectas");
            }

            var token = await GenerateToken(user);

            return new AuthResponse()
            {
                Id = user.Id,
                Token = new JwtSecurityTokenHandler().WriteToken(token),
                Email = user.Email!,
                UserName = user.UserName!
            };
        }

        public async Task<RegistrationsResponse> Register(RegistrationRequest request)
        {
            var existingUser = await _userManager.FindByNameAsync(request.Username);

            if (existingUser is not null)
            {
                throw new Exception($"El username ingresado ya existe");
            }

            var existingEmail = await _userManager.FindByEmailAsync(request.Email);

            if (existingUser is not null)
            {
                throw new Exception($"El email ingresado ya existe");
            }

            var user = new ApplicationuUser
            {
                Email = request.Email,
                Nombre = request.Nombre,
                Apellidos = request.Apellidos,
                UserName = request.Username,
                EmailConfirmed = true
            };

            var result = _userManager.CreateAsync(user, request.Password);

            if (result.Result.Succeeded)
            {
                await _userManager.AddToRoleAsync(user, "Operator");
                var token = await GenerateToken(user);

                return new RegistrationsResponse()
                {
                    Email = request.Email,
                    Token = new JwtSecurityTokenHandler().WriteToken(token),
                    UserId = user.Id,
                    UserName = user.UserName!
                };
            }

            throw new Exception($"{result.Result.Errors}");

        }

        private async Task<JwtSecurityToken> GenerateToken(ApplicationuUser user)
        {
            var userClaims = await _userManager.GetClaimsAsync(user);
            var roles = await _userManager.GetRolesAsync(user);
            var roleClaims = new List<Claim>();

            /*foreach (var role in roles)
            {
                roleClaims.Add(new Claim(ClaimTypes.Role, role));
            }*/

            roleClaims.AddRange(roles.Select(role => new Claim(ClaimTypes.Role, role)));

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.UserName!),
                new Claim(JwtRegisteredClaimNames.Email, user.Email!),
                new Claim(CustomClaimTypes.Uid, user.Id!),
            }.Union(userClaims).Union(roleClaims);

            var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Key));
            var signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);

            var jwtSecurityToken = new JwtSecurityToken(
                    issuer: _jwtSettings.Issuer,
                    audience: _jwtSettings.Audience,
                    claims: claims,
                    expires: DateTime.Now.AddMinutes(_jwtSettings.DurationInMinutes),
                    signingCredentials: signingCredentials);

            return jwtSecurityToken;
        }

    }
}
