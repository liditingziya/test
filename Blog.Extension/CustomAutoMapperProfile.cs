using System;
using System.Collections.Generic;
using System.DirectoryServices;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Blog.Model.Dto;
using Blog.Model.Entities;

namespace Blog.Extension
{
    public class CustomAutoMapperProfile:Profile
    {
        public CustomAutoMapperProfile()
        {
            base.CreateMap<WriterInfo, WriterInfoDto>();
            base.CreateMap<BlogNews, BlogNewsDto>()
                .ForMember(dest => dest.TypeName, sourse => sourse.MapFrom(src => src.TypeInfo.Name))
                .ForMember(dest => dest.WriterName, sourse => sourse.MapFrom(src => src.WriteInfo.Name));  
        }
    }
}
