using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Common.Models.Common
{
    public class CommonResult<T> where T : class
    {
        public string Code { get; set; }

        public string Success { get; set; }

        public string Message { get; set; }

        public T Data { get; set; }
    }
}
