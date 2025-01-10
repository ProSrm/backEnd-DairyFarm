using dairyFarm.DbContexts;
using dairyFarm.Entity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace dairyFarm.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly DFdbContext _context;

        public LoginController(DFdbContext context)
        {
            _context = context;
        }       

        [HttpPost]
        public async Task<IActionResult> Login([FromBody] Login login)
        {
            if (login == null || string.IsNullOrEmpty(login.Email) || string.IsNullOrEmpty(login.Password))
            {
                return BadRequest(new { message = "Email and Password are required." });
            }

            var user = await _context.Login
                .FirstOrDefaultAsync(u => u.Email == login.Email && u.Password == login.Password);

            if (user == null)
            {
                return Unauthorized(new { message = "Invalid email or password." });
            }

            return Ok(new
            {
                email = user.Email,
                role = user.Authority,
                message = "Login successful"
            });
        }
    }
}