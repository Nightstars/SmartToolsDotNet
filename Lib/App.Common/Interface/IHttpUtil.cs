using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Common.Interface
{
    public interface IHttpUtil<Result> where Result : class
    {
        //post 请求
        public Task<Result> PostAsync(string url, object param);

        public Task<Stream> PostStreamAsync(string url, object param);

        //验证token
        public Task<bool> ValidateTokenAsync();
    }
}
