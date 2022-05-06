using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Common.Models.Common
{
    public class TokenInfo
    {
        /// <summary>
        /// 
        /// </summary>
        public string id_token { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string access_token { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public double expires_in { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string token_type { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string refresh_token { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string scope { get; set; }
    }
}
