using Blog.IRepository;
using Blog.IService;
using Blog.Repository;
using Blog.Service;
using Microsoft.Extensions.DependencyInjection;

namespace Blog.Extension
{
    public static class IOCExtend
    {
        public static IServiceCollection AddCustomIOC(this IServiceCollection services)
        {
            services.AddScoped<IBlogNewsRepository, BlogNewsRepository>();
            services.AddScoped<ITypeInfoRepository, TypeInfoRepository>();
            services.AddScoped<IWriterInfoRepository, WriterInfoRepository>();
            services.AddScoped<IBlogNewsService, BlogNewsService>();
            services.AddScoped<ITypeInfoService, TypeInfoService>();
            services.AddScoped<IWriterInfoService, WriterInfoService>();
            return services;
        }
    }
}
