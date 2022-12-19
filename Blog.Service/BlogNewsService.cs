using Blog.IRepository;
using Blog.IService;
using Blog.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Service
{
    public class BlogNewsService : BaseService<BlogNews>, IBlogNewsService
    {
        private readonly IBlogNewsRepository _iBlogNewsRepository;

        public BlogNewsService(IBlogNewsRepository iBlogNewsRepository)
        {
            base._iBaseRepository = iBlogNewsRepository;
            _iBlogNewsRepository = iBlogNewsRepository;
        }
    }
}