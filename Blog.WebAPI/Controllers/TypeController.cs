using Blog.IService;
using Blog.Model.Entities;
using Blog.Extension.Utility.ApiResult;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.DirectoryServices.ActiveDirectory;

namespace Blog.WebAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class TypeController : ControllerBase
    {
        private readonly ITypeInfoService _iTypeInfoService;

        public TypeController(ITypeInfoService iTypeInfoService)
        {
            _iTypeInfoService = iTypeInfoService;
        }

        [HttpGet]
        public async Task<ActionResult<ApiResult>> GetTypes()
        {
            var types = await _iTypeInfoService.QueryAsync();
            if (types.Count == 0) return ApiResultHelper.Error("没有更多类型");
            return ApiResultHelper.Success(types);
        }

        [HttpPost]
        public async Task<ActionResult<ApiResult>> CreateType(string name)
        {
            if (string.IsNullOrEmpty(name)) return ApiResultHelper.Error("类型名不能为空");
            var type = new TypeInfo
            {
                Name = name
            };
            bool flag = await _iTypeInfoService.CreateAsync(type);
            if (!flag) return ApiResultHelper.Error("添加失败");
            return ApiResultHelper.Success(type);
        }

        [HttpPut]
        public async Task<ActionResult<ApiResult>> EditType(int id, string name)
        {
            var type = await _iTypeInfoService.FindAsync(id);
            if (type == null) return ApiResultHelper.Error("没有找到该类型");
            type.Name = name;
            bool flag = await _iTypeInfoService.EditAsync(type);
            if (!flag) return ApiResultHelper.Error("修改失败");
            return ApiResultHelper.Success(type);
        }


        [HttpDelete]
        public async Task<ActionResult<ApiResult>> DeleteType(int id)
        {
            bool flag = await _iTypeInfoService.DeleteAsync(id);
            if (!flag) return ApiResultHelper.Error("删除失败");
            return ApiResultHelper.Success("删除成功");
        }
    }
}
