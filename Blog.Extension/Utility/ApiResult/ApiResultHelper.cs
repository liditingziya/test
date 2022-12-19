using SqlSugar;

namespace Blog.Extension.Utility.ApiResult
{
    public class ApiResultHelper
    {
        public static ApiResult Success(dynamic data)
        {
            return new ApiResult
            {
                Code = 200,
                Data = data,
                Message = "操作成功",
                Total = 0
            };
        }

        public static ApiResult Success(dynamic data, RefAsync<int> total)
        {
            return new ApiResult
            {
                Code = 200,
                Data = data,
                Message = "操作成功",
                Total = total
            };
        }

        public static ApiResult Error(string msg)
        {
            return new ApiResult
            {
                Code = 400,
                Data = null,
                Message = msg,
                Total = 0
            };
        }
    }
}
