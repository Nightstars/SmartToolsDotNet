using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Common.Models.Common
{
    public class BaseEntity
    {
        [SugarColumn(ColumnName = "SEQ_NO")]
        public string SeqNo { get; set; }

        [SugarColumn(ColumnName = "CREATE_TIME")]
        public string CreateTime { get; set; }

        [SugarColumn(ColumnName = "UPDATE_TIME")]
        public string UpdateTime { get; set; }
    }
}
