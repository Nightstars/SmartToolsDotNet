using App.Common.Models.Common;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Common.Models.Cache
{
    [SugarTable("APP_USER_INFO")]
    public class Appuser : BaseEntity
    {
        [SugarColumn(ColumnName = "USER_ID")]
        public string UserId { get; set; }

        [SugarColumn(ColumnName = "USER_NAME")]
        public string UserName { get; set; }


        [SugarColumn(ColumnName = "USER_EMAIL")]
        public string UserEmail { get; set; }


        [SugarColumn(ColumnName = "CROP_ID")]
        public string CropId { get; set; }


        [SugarColumn(ColumnName = "CROP_NAME")]
        public string CropName { get; set; }


        [SugarColumn(ColumnName = "CROP_CODE")]
        public string CropCode { get; set; }


        [SugarColumn(ColumnName = "SITE")]
        public string Site { get; set; }

        [SugarColumn(ColumnName = "VERSION")]
        public string Version { get; set; }

    }
}
