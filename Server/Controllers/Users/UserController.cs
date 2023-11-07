using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.IdentityModel.Tokens;
using Server.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Server.Controllers.Users
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private IConfiguration _config;

        public UserController(IConfiguration config)
        {
            _config = config;
        }
        [HttpPost("Login")]
        public IActionResult LoginUser(User userlogin)
        {
            User u = new User();
            u = getUser(userlogin.Email, userlogin.Password);
            // Kiểm tra tên đăng nhập và mật khẩu hợp lệ
            if (u != null)
            {
                var claims = new[]
            {
                new Claim(ClaimTypes.Name, u.Email)
                // Các claim khác có thể được thêm vào đây
            };

                // Tạo khóa bí mật từ secret key
                var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["JwtSettings:Key"]));
                var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

                // Tạo token
                var token = new JwtSecurityToken(
                    issuer: _config["JwtSettings:Issuer"],
                    audience: _config["JwtSettings:Audience"],
                    claims: claims,
                    expires: DateTime.UtcNow.AddHours(1),
                    signingCredentials: credentials
                );

                // Trả về token cho người dùng
                return Ok(
                   new JwtSecurityTokenHandler().WriteToken(token)
                );
            }
            return Unauthorized();
        }
      
        private User getUser(string email, string password)
        {
            using (prn231_finalprojectContext context = new prn231_finalprojectContext())
            {
                var data = context.Users.FirstOrDefault(c => c.Email.ToLower().Equals(email.ToLower()) && c.Password.Equals(password));
                return data;
            }
        }
        [HttpGet]
        [EnableQuery]
        //[Authorize]
        public IActionResult GetAllUser()
        {
            using (prn231_finalprojectContext context = new prn231_finalprojectContext())
            {
                var data = context.Users.ToList();
                return Ok(data);

            }
        }
    }
}
