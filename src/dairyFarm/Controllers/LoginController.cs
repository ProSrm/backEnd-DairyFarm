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
        //private readonly List<Login> _users = new List<Login>
        //{
        //    new Login { Email = "admin@dairyfarm.com", Password = "admin123", Authority = "admin" },
        //    new Login { Email = "user@dairyfarm.com", Password = "user123", Authority = "user" }
        //};

        [HttpPost]
        public async Task<IActionResult> Login( Login login)
        {
            if (login == null || string.IsNullOrEmpty(login.Email) || string.IsNullOrEmpty(login.Password))
            {
                return BadRequest("Email and Password are required.");
            }

            // Fetch the user from the database asynchronously
            var user = await _context.Login
                .FirstOrDefaultAsync(u => u.Email == login.Email && u.Password == login.Password);

            if (user == null)
            {
                return Unauthorized(new { message = "Invalid email or password." });
            }

            // Optional: Add login record if necessary (depending on your use case)
            // _context.Login.Add(login);
            // await _context.SaveChangesAsync();

            return Ok(new
            {
                email = user.Email,
                role = user.Authority,
                message = "Login successful"
            });
        }


    }
}