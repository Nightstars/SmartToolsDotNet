using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Common.Models
{
    public class BaseModel
    {
        /// <summary>
        /// 错误信息
        /// </summary>
        public string ErrorMessage { get; set; }

        public bool IsError { get; set; } = true;
    }
}
