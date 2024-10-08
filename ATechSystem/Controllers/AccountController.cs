using ATechSystem.DTOS;
using ATechSystem.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ATechSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IConfiguration configuration;

        public AccountController(UserManager<ApplicationUser> userManager,IConfiguration configuration)
        {
            this.userManager = userManager;
            this.configuration = configuration;
        }
        #region Register
        [HttpPost("Register")]  // POST api/Account/Register
        public async Task<IActionResult> Register(UserDTO userFromRequest)
        {
            if (ModelState.IsValid)
            {
                //Save DB
                ApplicationUser user = new ApplicationUser();
                user.UserName = userFromRequest.UserName;
                user.Email = userFromRequest.Email;
                user.PhoneNumber = userFromRequest.Mobile;

                var result= await userManager.CreateAsync(user,userFromRequest.Password);
                if (result.Succeeded)
                {
                    return Ok(result);
                }
                foreach (var item in result.Errors)
                {
                    ModelState.AddModelError("Password", item.Description);
                }
            }
            return BadRequest(ModelState);
        }
        #endregion

        #region Login
        [HttpPost("Login")] // POST api/Account/Login
        public async Task<IActionResult> Login(LoginDTO userFromRequest)
        {
            if (ModelState.IsValid)
            {
                //check 
                var ApplicationUserDB=await userManager.FindByNameAsync(userFromRequest.UserName);
                if (ApplicationUserDB != null)
                {
                    var checkPassword = await userManager.CheckPasswordAsync(ApplicationUserDB, userFromRequest.Password);
                    if (checkPassword)
                    {
                        //Generate Token

                        //handel Claims For User 
                        List<Claim> UserClaims = new List<Claim>();

                        //Token Generate Guid id and Will Change randmly When Create 
                        UserClaims.Add(new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()));

                        UserClaims.Add(new Claim(ClaimTypes.NameIdentifier, ApplicationUserDB.Id));
                        UserClaims.Add(new Claim(ClaimTypes.Name, ApplicationUserDB.UserName));
                        
                        //Handel User Roles 
                        var userRoles = await userManager.GetRolesAsync(ApplicationUserDB);
                        foreach (var roleName in userRoles)
                        {
                            UserClaims.Add(new Claim(ClaimTypes.Role, roleName));
                        }

                        //Create signingCredentials
                        var SignInKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT:SecritKey"]));
                        SigningCredentials signingCredential = new SigningCredentials
                              (SignInKey, SecurityAlgorithms.HmacSha256);

                        //Design Token
                        JwtSecurityToken MyToken = new JwtSecurityToken(
                            issuer: configuration["JWT:IssuerIp"],
                            audience: configuration["JWT:AudienceIp"],
                            expires:DateTime.UtcNow.AddHours(1),
                            claims: UserClaims,
                            signingCredentials: signingCredential

                        );

                        //Generate Token
                        return Ok(new
                        {
                            token=new JwtSecurityTokenHandler().WriteToken(MyToken),
                            Exception= DateTime.UtcNow.AddHours(1)
                        });

                    }
                    else
                    {
                        ModelState.AddModelError("UserName", "UserName or Password is inValid");
                    }
                }
                else
                {
                    return NotFound("UserName Not Found");
                }
              
                    
            }
            return BadRequest(ModelState);
        }
        #endregion



        #region ResetPasswored

        #endregion

        #region Confirmantion Mail

        #endregion
    }
}
