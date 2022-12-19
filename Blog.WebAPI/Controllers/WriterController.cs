using Blog.IService;
using Blog.Model.Entities;
using Blog.Extension.Utility._MD5;
using Blog.Extension.Utility.ApiResult;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using AutoMapper;
using Blog.Model.Dto;

namespace Blog.WebAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class WriterController : ControllerBase
    {
        private readonly IWriterInfoService _iWriterInfoService;

        public WriterController(IWriterInfoService iWriterInfoService)
        {
            _iWriterInfoService = iWriterInfoService;
        }


        [AllowAnonymous]
        [HttpGet]
        public async Task<ApiResult> Find([FromServices]IMapper iMapper, int id)
        {
            var writer  = await _iWriterInfoService.FindAsync(id);
            var writerDto = iMapper.Map<WriterInfoDto>(writer);
            return ApiResultHelper.Success(writerDto);
        }

        [HttpPost]
        public async Task<ActionResult<ApiResult>> Create(string name, string userName, string userPassword)
        {
            // 数据校验 
            var writer = new WriterInfo
            {
                Name = name,
                UserName = userName,
                // 加密密码
                UserPwd = MD5Helper.MD5Encrypt32(userPassword)
            };
            // 判断是否存在相同的账号
            var oldwriter = await _iWriterInfoService.FindAsync(c=>c.UserName == userName);
            if (oldwriter != null) return ApiResultHelper.Error("账号已存在");

            bool flag = await _iWriterInfoService.CreateAsync(writer);
            if (!flag) return ApiResultHelper.Error("添加失败");
            return ApiResultHelper.Success(writer);
        }

        [HttpPut]
        public async Task<ApiResult> Edit(string name)
        {
            int id = Convert.ToInt32(User.FindFirst("Id").Value);
            var writer = await _iWriterInfoService.FindAsync(id);
            writer.Name = name;
            bool flag = await _iWriterInfoService.EditAsync(writer);
            if (!flag) return ApiResultHelper.Error("修改失败");
            return ApiResultHelper.Success("修改成功");
        }
    }
}
