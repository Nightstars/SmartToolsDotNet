using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Common.Cache
{
    public static class Cache
    {
        public static IMemoryCache appCache = new MemoryCache(Options.Create(new MemoryCacheOptions()));
    }
}
