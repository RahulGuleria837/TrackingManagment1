using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Win32;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using TrackingManagment.Identity;
using TrackingManagment.LoginViewModel;
using TrackingManagment.ViewModel;

namespace TrackingManagment.Services
{
    public class UserServices : IUserService
    {

        private readonly UserManager<ApplicationUser> _manager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly ApplicationDbContext _context;
        private  JwtSettings _appSettings;


        public UserServices(UserManager<ApplicationUser> manager, SignInManager<ApplicationUser> signInManager,
            ApplicationDbContext context, IOptions<JwtSettings> appSettings)
        {
            _manager = manager;
            _signInManager = signInManager;
            _context = context;
            _appSettings = appSettings.Value;

        }



        //To Check if the user is unique 
        public async Task<bool> IsUnique(string Email)
        {
            var checkUserInDb = await _manager.FindByNameAsync(Email);
            if (checkUserInDb == null) { return false; }

            return true;
        }


        //To login user
        public async Task<ApplicationUser> AuhthenticateUser(LoginView login)
        {
            var checkUserInDb = await _manager.FindByNameAsync(login.UserName);
            if (checkUserInDb == null) { return null; }
            var checkLogin = await _signInManager.CheckPasswordSignInAsync(checkUserInDb,login.Password,false);
            var applicationUser=_context.Users.FirstOrDefault(A=>A.UserName==login.UserName);
            if (!checkLogin.Succeeded) return null;
            var secret = _appSettings.SecretKey;
            var token = CreateToken(applicationUser);
            return checkUserInDb;


        }

        


        //To Register the new user
        public async Task<bool> Register(RegisterView register)
            
        {
            if(register == null) return false;

            ApplicationUser user = new ApplicationUser()
            {
                UserName = register.UserName,
                PasswordHash = register.Password,
                Email = register.Email
            };

            var newUser = await _manager.CreateAsync(user,user.PasswordHash);
            if (!newUser.Succeeded) return false;
            return true;

        }

        public ApplicationUser CreateToken(ApplicationUser? register)
        {
            var jwtsettings = _appSettings.SecretKey;
            var jwtsettingsMin = _appSettings.ExpirationMinutes;

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(jwtsettings);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, register.Id.ToString()),
                    // Add additional claims as needed
                }),
                Expires = DateTime.UtcNow.AddMinutes(jwtsettingsMin),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            register.Token = tokenHandler.WriteToken(token);

            return register;
        }
    }
   

}
