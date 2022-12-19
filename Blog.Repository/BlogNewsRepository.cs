using Blog.IRepository;
using Blog.Model.Entities;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Repository
{
    public class BlogNewsRepository:BaseRepository<BlogNews>, IBlogNewsRepository
    {
        public async override Task<List<BlogNews>> QueryAsync()
        {
            return await base.Context.Queryable<BlogNews>()
                .Mapper(c=> c.TypeInfo, c=> c.TypeId, c => c.TypeInfo.Id)
                .Mapper(c => c.WriteInfo, c => c.WriteId, c=> c.WriteInfo.Id)
                .ToListAsync();
        }
        public override async Task<List<BlogNews>> QueryAsync(int page, int size, RefAsync<int> total)
        {
            return await base.Context.Queryable<BlogNews>()
                .Mapper(c => c.WriteInfo, c => c.WriteId, c => c.WriteInfo.Id)
                .Mapper(c => c.TypeInfo, c => c.TypeId, c => c.TypeInfo.Id)
                .ToPageListAsync(page, size, total);
        }
        public override async Task<List<BlogNews>> QueryAsync(Expression<Func<BlogNews, bool>> func, int page, int size, RefAsync<int> total)
        {
            return await base.Context.Queryable<BlogNews>()
            .Where(func)
            .Mapper(c=> c.WriteInfo, c=> c.WriteId, c => c.WriteInfo.Id)
            .Mapper(c => c.TypeInfo, c => c.TypeId, c => c.TypeInfo.Id)
            .ToPageListAsync(page, size, total);
        }

    }
}
