using AutoMapper;
using CodeSmashWithAngular.Configurations;
using CodeSmashWithAngular.Dtos;
using CodeSmashWithAngular.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace CodeSmashWithAngular.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork uow;
        public AccountController(IUnitOfWork unitOfWork,IMapper mapper)
        {
            uow = unitOfWork;
            _mapper= mapper;
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginReqDto loginReq)
        {
            var user = await uow.userRepository.Authenticate(loginReq.Username,loginReq.Password);
            if (user == null)
            {
                return Unauthorized();
            }
            var login = _mapper.Map<LoginResDto>(user);
           login.Token = CreateToken(user);

            return Ok(login);
        }
        private string CreateToken(User user)
        {
            var tokenhandler = new JwtSecurityTokenHandler();
            var tokenkey = Encoding.ASCII.GetBytes("This is the jwt key"); 
            var tokenDescriptor = new SecurityTokenDescriptor()
            {
                Subject = new System.Security.Claims.ClaimsIdentity(
            new Claim[]
            {
            new Claim(ClaimTypes.Name, user.Username),
            new Claim(ClaimTypes.NameIdentifier,user.Id.ToString())
            }),
                Expires = DateTime.UtcNow.AddHours(1),
                SigningCredentials = new SigningCredentials(
            new SymmetricSecurityKey(tokenkey), SecurityAlgorithms.HmacSha384Signature)
            };
            var token = tokenhandler.CreateToken(tokenDescriptor);
            return tokenhandler.WriteToken(token);
        }
    }
}
