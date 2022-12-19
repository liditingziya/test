using Blog.Extension.Utility._MD5;
using Blog.Extension.Utility.ApiResult;
using Blog.IService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Blog.WebAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AuthorizeController : ControllerBase
    {
        private readonly IWriterInfoService _iWriterInfoService;

        public AuthorizeController(IWriterInfoService iWriterInfoService)
        {
            _iWriterInfoService = iWriterInfoService;
        }

        [HttpPost]
        public async Task<ApiResult> Login(string userName, string password)
        {
            // 加密
            string pwd = MD5Helper.MD5Encrypt32(password);
            var writer = await _iWriterInfoService.FindAsync(c => c.UserName == userName && c.UserPwd == pwd);
            if (writer == null) return ApiResultHelper.Error("登录失败");
            var claims = new Claim[]
            {
                new Claim(ClaimTypes.Name, writer.Name),
                new Claim("Id", writer.Id.ToString()),
                new Claim("UserName", writer.UserName)
                // 随便解密 不放敏感数据
            };
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa")); // key 至少16位
            var token = new JwtSecurityToken(
                issuer: "http://localhost:6060",
                audience: "http://localhost:6060",
                claims: claims,
                notBefore: DateTime.Now,
                expires: DateTime.Now.AddHours(1),
                signingCredentials: new SigningCredentials(key, SecurityAlgorithms.HmacSha256)
                );
            var jwtToken = new JwtSecurityTokenHandler().WriteToken(token);
            return ApiResultHelper.Success(jwtToken);
        }
    }
}
