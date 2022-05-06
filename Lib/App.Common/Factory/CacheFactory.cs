using App.Common.Interface;
using App.Common.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Common.Factory
{
    public class CacheFactory : ICacheFactory
    {
        public ICacheUtil GetInstance()
        {
            return new CacheUtil();
        }
    }
}
