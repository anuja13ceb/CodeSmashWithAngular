using AutoMapper;
using CodeSmashWithAngular.Configurations;
using CodeSmashWithAngular.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Text.RegularExpressions;
using System.Text;
using CodeSmashWithAngular.Helpers;
using CodeSmashWithAngular.DatabaseContext;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using CodeSmashWithAngular.Dtos;
using Microsoft.AspNetCore.Authorization;

namespace CodeSmashWithAngular.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserUIController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ApplicationDbContext _dbContext;
        public UserUIController(IUnitOfWork unitOfWork, IMapper mapper, ApplicationDbContext dbContext) {

            this._unitOfWork = unitOfWork;
            this._dbContext= dbContext;
            this._mapper = mapper;
        }

        //[Authorize(Roles = "User")]
        [HttpGet]
        public  ActionResult GetAllUsers()
        {
            try
            {
                var userUIs = _unitOfWork.userUIRepository.All();
              

                if (userUIs.Count() == 0)
                    return StatusCode(StatusCodes.Status404NotFound, $"List of City is not available");
                //     throw new HttpStatusCodeException(HttpStatusCode.NotFound,
                //"Please check username or password");

                return StatusCode(StatusCodes.Status200OK, userUIs);
            }
            catch (Exception ex)
            {
                //throw ex;

                return StatusCode(StatusCodes.Status500InternalServerError, ex);
            }
        }


        [HttpPost("authenticate")]
        public async Task<IActionResult> Authenticate([FromBody] UserUI userObj)
        {
            if (userObj == null)
                return BadRequest();

            var user = _dbContext.UserUIs
                .FirstOrDefault(x => x.Username == userObj.Username);

            if (user == null)
                return NotFound(new { Message = "User not found!" });

            if (!PasswordHasher.VerifyPassword(userObj.Password, user.Password))
            {
                return BadRequest(new { Message = "Password is Incorrect" });
            }

            user.Token = CreateJwt(user);
            var newAccessToken = user.Token;
            //var newRefreshToken = CreateJwt(user);
            //user.RefreshToken = newRefreshToken;
            user.RefreshTokenExpiryTime = DateTime.Now.AddDays(5);
            _unitOfWork.SaveChanges();

            //return Ok(user.Token);
            return Ok(new TokenApiDto()
            {
                AccessToken = newAccessToken
               
            });
        }



        [HttpPost("register")]
        public IActionResult AddUser([FromBody] UserUI userObj)
        {
            if (userObj == null)
                return StatusCode(StatusCodes.Status404NotFound, $"User is not available"); ;

            
            if ( _unitOfWork.userUIRepository.CheckEmailExist(userObj.Email))
                return StatusCode(StatusCodes.Status404NotFound, $"Email Already Exist");

           if ( _unitOfWork.userUIRepository.CheckUsernameExist(userObj.Username))
                return BadRequest(new { Message = "Username Already Exist" });

            var passMessage = CheckPasswordStrength(userObj.Password);
            if (!string.IsNullOrEmpty(passMessage))
                return BadRequest(new { Message = passMessage.ToString() });

            userObj.Password = PasswordHasher.HashPassword(userObj.Password);
            userObj.Role = "User";
            userObj.Token = "";
            _unitOfWork.userUIRepository.Add(userObj);
            _unitOfWork.SaveChanges();
            return Ok(new
            {
                Status = 200,
                Message = "User Added!"
            });
        }

        private static string CheckPasswordStrength(string pass)
        {
            StringBuilder sb = new StringBuilder();
            if (pass.Length < 9)
                sb.Append("Minimum password length should be 8" + Environment.NewLine);
            if (!(Regex.IsMatch(pass, "[a-z]") && Regex.IsMatch(pass, "[A-Z]") && Regex.IsMatch(pass, "[0-9]")))
                sb.Append("Password should be AlphaNumeric" + Environment.NewLine);
            if (!Regex.IsMatch(pass, "[<,>,@,!,#,$,%,^,&,*,(,),_,+,\\[,\\],{,},?,:,;,|,',\\,.,/,~,`,-,=]"))
                sb.Append("Password should contain special charcter" + Environment.NewLine);
            return sb.ToString();
        }

        private string CreateJwt(UserUI user)
        {
            var jwtTokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes("This is the jwt key");
            var identity = new ClaimsIdentity(new Claim[]
            {
                new Claim(ClaimTypes.Role, user.Role),
                new Claim(ClaimTypes.Name,$"{user.Username}")
            });

            var credentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = identity,
                Expires = DateTime.Now.AddSeconds(10),
                SigningCredentials = credentials
            };
            var token = jwtTokenHandler.CreateToken(tokenDescriptor);
            return jwtTokenHandler.WriteToken(token);
        }


    }
}
