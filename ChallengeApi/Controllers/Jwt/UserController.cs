using ChallengeApi.Model;
using ChallengeApi.Models;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using SendGrid;
using SendGrid.Helpers.Mail;

namespace ChallengeApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class UserController : ControllerBase
    {
        private readonly Microsoft.AspNetCore.Identity.UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;

        private readonly IConfiguration _configuration;
        public UserController(Microsoft.AspNetCore.Identity.UserManager<User> userManager, SignInManager<User> signInManager,IConfiguration configuration)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _configuration = configuration;
        }
        //Register
        [HttpPost]

        [Route("api/[controller]/Register")]
        public async Task<IActionResult> Register(RegisterUser registerUser)
        {
           
            var aux = await _userManager.FindByNameAsync(registerUser.UserName);
            if (aux != null)
            {
                return StatusCode(StatusCodes.Status401Unauthorized, new
                {
                    Status = "Error",
                    Message = $"El usuario { registerUser.UserName} ya existe"
                });
            }
            var user = new User()
            {
                UserName = registerUser.UserName,
                Email = registerUser.Email,
                IsActive = true
            };
            var result = await _userManager.CreateAsync(user, registerUser.Password);
            if (result.Succeeded)
            {
                var ApiKey = _configuration["SendGridAPIKey"];
                var client = new SendGridClient(ApiKey + _configuration["SendGridAPIKey1"]);
                var from = new EmailAddress("santiagoezequielmartinez03@gmail.com");
                var to = new EmailAddress(registerUser.Email);
                var subject = "Welcome to Santiago Martinez API";
                var content = "Hi! I hope you enjoy the api. If you have a problem, please text me.";
                var Htmlcontent = "<strong>Hi! I hope you enjoy the api. <br> If you have a problem, please text me.</strong>";
                var msg = MailHelper.CreateSingleEmail(from, to, subject, content,Htmlcontent);
                await client.SendEmailAsync(msg);
                    }
            if (!result.Succeeded)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new
                {
                    Status = "Error",
                    Message = $"Error del servidor"
                });
            }
            return Ok(new{Status="Success"});
        }

        [HttpPost]
        [Route("api/[controller]/Login")]
        public async Task<IActionResult> Login(RequestLoginUser user)
        {
            var result = await _signInManager.PasswordSignInAsync(user.UserName,user.Password,false,false);
            if (result.Succeeded)
            {
                var currentUser = await _userManager.FindByNameAsync(user.UserName);
                if (currentUser.IsActive)
                {
                    var token = await GetToken(currentUser);

                    return Ok(token); 

                }
                
            }
            return StatusCode(StatusCodes.Status401Unauthorized, new
            {
                Status = "Error",
                Message = $"El usuario { user.UserName} no esta autorizado"
            }) ;
            


        }
        [HttpGet]
        [Route("api/[controller]/GetToken")]
        private async Task<RequestToken> GetToken(User currentUser)
        {
            var userRoles = await _userManager.GetRolesAsync(currentUser);
            var AuthClaims = new List<Claim>()
            {
                new Claim(ClaimTypes.Name,currentUser.UserName),
                new Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString())

            };
            AuthClaims.AddRange(userRoles.Select(x => new Claim(ClaimTypes.Role, x)));
            var AuthSignInKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("JuanRomanRiquelme"));
            var token = new JwtSecurityToken(
               issuer: "https://localhost:5001",
      audience: "https://localhost:5001",
      expires: DateTime.Now.AddHours(1),
      claims: AuthClaims,
      signingCredentials: new SigningCredentials(AuthSignInKey, SecurityAlgorithms.HmacSha256)
       );

            return new RequestToken
            {
                
         TokenCode = new JwtSecurityTokenHandler().WriteToken(token),
         ValidTo = token.ValidTo
};

               
            
        }
    }
}
 