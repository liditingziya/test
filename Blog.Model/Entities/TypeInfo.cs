using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using SqlSugar;

namespace Blog.Model.Entities
{
    public class TypeInfo:BaseId
    {
        [SugarColumn(ColumnDataType = "nvarchar(30)")]
        public string Name { get; set; }
    }
}
