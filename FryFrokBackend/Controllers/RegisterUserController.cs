using FryFrokBackend.DataBase;
using FryFrokBackend.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FryFrokBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegisterUserController : ControllerBase
    {
        private readonly AppDBContext _dbContext;

        public RegisterUserController (AppDBContext dbContext)
        {
            _dbContext = dbContext;
        }
        [HttpPost("RegisterUser")]
        public async Task<IActionResult> Register(RegisterLoginDTO registerUser)
        {
            if (registerUser.Password != registerUser.ConfirmPassword)
                return BadRequest("Password Not Match");

            var ExistUser = await _dbContext.Register.FirstOrDefaultAsync(u => u.Email == registerUser.Email);
            if (ExistUser != null)
            {
                return BadRequest("User Already Exist");
            }

            var newUser = new RegisterUser
            {
                Name = registerUser.Name,
                Email = registerUser.Email,
                Password = registerUser.Password,
            };
            _dbContext.Add(newUser);
            await _dbContext.SaveChangesAsync();

            return Ok("User Register Successfully");
        }

        [HttpPost("LoginUser")]
        public async Task<IActionResult> Login([FromBody]LoginDto logindto )
        {
            var user = await _dbContext.Register.FirstOrDefaultAsync(u => u.Email == logindto.Email && u.Password == logindto.Password);
            if(user==null)
            {
                return Unauthorized("Invalid Cradentional");
            }
            return Ok("User Login Successfully");
        }
    }
}
