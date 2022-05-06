using App.Common.Models.Common;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Common.Models.Cache
{
    [SugarTable("APP_SERVER_INFO")]
    public class AppServer : BaseEntity
    {
        [SugarColumn(ColumnName = "VERSION")]
        public string Version { get; set; }

        [SugarColumn(ColumnName = "HOST")]
        public string Host { get; set; }
    }
}
