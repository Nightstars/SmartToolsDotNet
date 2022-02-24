using Codeless.Core.Db;
using Codeless.Core.Models;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DbTableInfo = Codeless.Core.Models.DbTableInfo;

namespace Codeless.Core.Services
{
    public class CodeLessService
    {
        #region initialize
        private readonly SqlSugarClient _db;
        public CodeLessService(string connectString)
        {
            _db = DbUtil.GetInstance(connectString);
        }
        #endregion

        #region GetDatabase
        /// <summary>
        /// GetDatabase
        /// </summary>
        /// <returns></returns>
        public List<DataBaseInfo> GetDatabase()
        {
            return _db.Queryable<DataBaseInfo>().AS("MASTER.sys.SYSDATABASES").Where(x => x.dbid > 4).OrderBy(x => x.name).ToList();
        }
        #endregion

        #region GetDbTable
        /// <summary>
        /// GetDbTable
        /// </summary>
        /// <param name="database"></param>
        /// <returns></returns>
        public List<DbTable> GetDbTable(string database)
        {
            return _db.Queryable<DbTable>().AS($"{database}.sys.tables").OrderBy(x => x.name).ToList();
        }
        #endregion

        #region GetDbTableInfo
        /// <summary>
        /// GetDbTableInfo
        /// </summary>
        /// <param name="database"></param>
        /// <param name="table"></param>
        /// <returns></returns>
        public List<DbTableInfo> GetDbTableInfo(string database,string table)
        {
            string sql = $@"use {database}
                            SELECT
							( CASE WHEN a.colorder= 1 THEN d.name ELSE NULL END ) tableName,
							a.colorder, --列字段序号
							a.name columnName,--列名
							( CASE WHEN COLUMNPROPERTY( a.id, a.name, 'IsIdentity' ) = 1 THEN '1' ELSE '0' END ) IsIdentity,
							(
							CASE
									WHEN (
									SELECT COUNT
										( * ) 
									FROM
										sysobjects 
									WHERE
										(
											name IN (
											SELECT
												name 
											FROM
												sysindexes 
											WHERE
												( id = a.id ) 
												AND (
													indid IN (
													SELECT
														indid 
													FROM
														sysindexkeys 
													WHERE
														( id = a.id ) 
														AND ( colid IN ( SELECT colid FROM syscolumns WHERE ( id = a.id ) AND ( name = a.name ) ) ) 
													) 
												) 
											) 
										) 
										AND ( xtype = 'PK' ) 
										) > 0 THEN
										'1' ELSE '0' 
									END 
									) IsPK, --是否是主键
									b.name type, --类型
									a.length size, --占用空间（字节）
									COLUMNPROPERTY( a.id, a.name, 'PRECISION' ) AS length, --长度
									isnull( COLUMNPROPERTY( a.id, a.name, 'Scale' ), 0 ) AS Decimalplaces,
								( CASE WHEN a.isnullable= 1 THEN '1' ELSE '0' END ) AllowNull,--是否允许为空
							isnull( e.text, '' ) defaultvalue, --默认值
							isnull( g.[value], ' ' ) AS columnDescription --列名备注 
						FROM
							syscolumns a
							LEFT JOIN systypes b ON a.xtype= b.xusertype
							INNER JOIN sysobjects d ON a.id= d.id 
							AND d.xtype= 'U' 
							AND d.name<> 'dtproperties'
							LEFT JOIN syscomments e ON a.cdefault= e.id
							LEFT JOIN sys.extended_properties g ON a.id= g.major_id 
							AND a.colid= g.minor_id
							LEFT JOIN sys.extended_properties f ON d.id= f.class 
							AND f.minor_id= 0 
						WHERE
							d.name= '{table}' 
						ORDER BY
							a.id,
							a.colorder
						";
            return _db.Ado.SqlQuery<DbTableInfo>(sql);
        }
        #endregion
    }
}
