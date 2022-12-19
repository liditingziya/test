using Blog.IService;
using Blog.Model.Entities;
using Blog.Extension.Utility.ApiResult;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using SqlSugar;
using AutoMapper;
using Blog.Model.Dto;

namespace Blog.WebAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class BlogNewsController : ControllerBase
    {
        private readonly IBlogNewsService _iBlogNewsService;
        public BlogNewsController(IBlogNewsService iBlogNewsService)
        {
            _iBlogNewsService = iBlogNewsService;
        }

        [HttpGet]
        public async Task<ActionResult<ApiResult>> GetBlogNews()
        {
            var data = await _iBlogNewsService.QueryAsync();
            if (data.Count == 0) return ApiResultHelper.Error("没有更过的文章");
            return ApiResultHelper.Success(data);
        }

        [HttpGet]
        public async Task<ApiResult> GetNewBlogNewsPage([FromServices]IMapper iMapper, int page, int size)
        {
            RefAsync<int> total = 0;

            var blog = await _iBlogNewsService.QueryAsync(page, size, total);
            try
            {
                var blogDto = iMapper.Map<List<BlogNewsDto>>(blog);
                return ApiResultHelper.Success(blogDto, total);
            }
            catch (Exception)
            {
                return ApiResultHelper.Error("映射错误");
            }
            
        }

        [HttpPost]
        [Authorize]
        public async Task<ActionResult<ApiResult>> CreateBlog(string title, string content, int typeid)
        {
            // 数据验证
            var blog = new BlogNews
            {
                BrowseCount = 0,
                Content = content,
                Title = title,
                Time = DateTime.Now,
                TypeId = typeid,
                WriteId = Convert.ToInt32(User.FindFirst("Id").Value)
            };

            bool flag = await _iBlogNewsService.CreateAsync(blog);
            if (!flag) return ApiResultHelper.Error("添加失败");
            return ApiResultHelper.Success("添加成功");
        }

        [HttpDelete]
        [Authorize]
        public async Task<ActionResult<ApiResult>> DeleteById(int id)
        {
            bool flag = await _iBlogNewsService.DeleteAsync(id);
            if (!flag) return ApiResultHelper.Error("删除失败");
            return ApiResultHelper.Success("删除成功");
        }

        [HttpPut]
        [Authorize]
        public async Task<ActionResult<ApiResult>> Edit(int id,string title, string content, int typeid)
        {
            var blog = await _iBlogNewsService.FindAsync(id);
            // 判断blog空
            if (blog == null) return ApiResultHelper.Error("无法找到该文章");
            blog.Title = title;
            blog.TypeId = typeid;
            blog.Content = content;
            bool flag  = await _iBlogNewsService.EditAsync(blog);
            if (!flag) return ApiResultHelper.Error("修改失败");
            return ApiResultHelper.Success("修改成功");
        }
    }
}
