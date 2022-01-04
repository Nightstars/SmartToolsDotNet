using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodelessModule.Models
{
    public class DbTableInfo
    {
        /// <summary>
        /// 表名
        /// </summary>
        public string tableName { get; set; }

        /// <summary>
        /// 字段列名
        /// </summary>
        public string columnName { get; set; }

        /// <summary>
        /// 字段备注
        /// </summary>
        public string columnDescription { get; set; }

        /// <summary>
        /// 字段类型
        /// </summary>
        public string type { get; set; }

        /// <summary>
        /// 字段长度
        /// </summary>
        public int length { get; set; }

        [SugarColumn(IsIgnore = true)]
        public string displayName {
            get { return $"{columnName} {columnDescription}"; }
        }
    }
}
