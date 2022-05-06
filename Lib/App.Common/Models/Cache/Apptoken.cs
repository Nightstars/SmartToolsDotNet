using App.Common.Models.Common;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Common.Models.Cache
{
    [SugarTable("APP_TOKEN")]
    public class AppToken : BaseEntity
    {
        [SugarColumn(ColumnName = "TOKEN")]
        public string Token { get; set; }

        [SugarColumn(ColumnName = "REFRESH_TOKEN")]
        public string RefreshToken { get; set; }

        [SugarColumn(ColumnName = "EXPIRE")]
        public string Expire { get; set; }

        [SugarColumn(ColumnName = "VERSION")]
        public string Version { get; set; }
    }
}
