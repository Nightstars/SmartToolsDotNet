using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Common.Models.Cache
{
    [SugarTable("APP_COMBO_DATASOURCE")]
    public class AppComboDatasource
    {
        [SugarColumn(ColumnName = "KEY")]
        public string Key { get; set; }

        [SugarColumn(ColumnName = "URL")]
        public string Url { get; set; }

        [SugarColumn(ColumnName = "VERSION")]
        public string Version { get; set; }
    }
}
