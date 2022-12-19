using Blog.IRepository;
using Blog.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Repository
{
    public class TypeInfoRepository:BaseRepository<TypeInfo>, ITypeInfoRepository
    {
    }
}
