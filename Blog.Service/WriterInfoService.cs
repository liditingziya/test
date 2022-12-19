using Blog.IRepository;
using Blog.IService;
using Blog.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Service
{
    public class WriterInfoService:BaseService<WriterInfo>, IWriterInfoService
    {
        private readonly IWriterInfoRepository _iWriterInfoRepository;

        public WriterInfoService(IWriterInfoRepository iWriteInfoRepository)
        {
            base._iBaseRepository = iWriteInfoRepository;
            _iWriterInfoRepository = iWriteInfoRepository;
        }
    }
}
