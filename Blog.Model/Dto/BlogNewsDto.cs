using Blog.Model.Entities;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Model.Dto
{
    public class BlogNewsDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public DateTime Time { get; set; }
        public int BrowseCount { get; set; }
        public int LikeCount { get; set; }
        public int TypeId { get; set; }
        public int WriteId { get; set; }
        public string TypeName { get; set; }
        public string WriterName { get; set; }
    }
}
