using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLitePCL;
using SqlSugar;

namespace Blog.Model.Entities
{
    public class BaseId
    {
        // 自增主键
        [SugarColumn(IsPrimaryKey = true, IsIdentity = true)]
        public int Id { get; set; }

    }
}
