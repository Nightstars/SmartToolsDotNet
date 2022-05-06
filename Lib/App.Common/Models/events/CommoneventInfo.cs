using App.Common.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Common.Models.events
{
    public class CommoneventInfo<T> where T : class
    {
        public MsgType msgtype { get; set; }

        public T data { get; set; }
    }
}
