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
    public class TypeInfoService:BaseService<TypeInfo>, ITypeInfoService
    {
        private readonly ITypeInfoRepository _iTypeInfoRepository;

        public TypeInfoService(ITypeInfoRepository iTypeInfoRepository)
        {
            base._iBaseRepository = iTypeInfoRepository;
            _iTypeInfoRepository = iTypeInfoRepository;
        }
    }
}
