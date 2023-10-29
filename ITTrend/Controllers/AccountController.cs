using ITTrend.Dto;
using ITTrend.Models;
using ITTrend.RepositoryPattern.CustomRepository.Interfaces;
using ITTrend.RepositoryPattern.CustomRepository.Repository;
using ITTrend.RepositoryPattern.UnitOfWork;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ITTrend.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AccountController:ControllerBase
    {

        private readonly IUnitOfWork _unitOfWork;
        private IUserRepository UserRepository => _unitOfWork.UserRepository;
        private IRoleRepository RoleRepository => _unitOfWork.RoleRepository;

 
        public AccountController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

       [HttpPost]
        public IActionResult Login(LoginReqDto loginReq)
        {
            var user =  UserRepository.Authenticate(loginReq.Email, loginReq.Password);
            if (user == null)
            {
                return Unauthorized();
            }

            var loginRes = new LoginReqDto();
            loginRes.Email = loginReq.Email;
            loginRes.Token = CreateJwt(user);
            return Ok(loginRes);

        }

        internal string CreateJwt(User user)
        {
            var secretKey = "SecretKey_1234567890_0987654321";

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));

            var claims = new List<Claim>()
        {
            new(ClaimTypes.Email, user.Email),
            new(ClaimTypes.NameIdentifier, user.Id.ToString())
        };
            var roleIds = user.UserRoles.Select(q => q.RoleId);
            var roleNames = RoleRepository.GetEntities().Where(q => roleIds.Contains(q.Id)).Select(q => q.Name);
            claims.AddRange(roleNames.Select(role => new Claim(ClaimTypes.Role, role)));

            var signingCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddMinutes(60),
                SigningCredentials = signingCredentials
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        [HttpPost]
        [Route("register - user")]
        public  IActionResult RegisterUser(UserRegisterDto dto)
        {
            if (UserRepository.UserAlreadyInDatabase(dto.Email) || string.IsNullOrEmpty(dto.Email))
                throw new ArgumentException("User already exists or user email is null, please try something else");
            var user = new User
            {
                FirstName=dto.FirstName,
                LastName=dto.LastName,
                Email = dto.Email,
                Password = dto.Password
            };
            UserRepository.Insert(user);
            _unitOfWork.Save();
            return Ok("register successfully");
        }
    }
}
