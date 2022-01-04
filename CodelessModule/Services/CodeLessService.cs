using CodelessModule.Db;
using CodelessModule.Models;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DbTableInfo = CodelessModule.Models.DbTableInfo;

namespace CodelessModule.Services
{
    class CodeLessService
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
                            select
                            A.name as tableName, --表名
                            B.name as columnName, --列名
                            C.value as columnDescription, --列名备注
                            T.type,--类型
                            T.length--长度
                            from sys.tables A
                            inner join sys.columns B on B.object_id = A.object_id
                            left join sys.extended_properties C on C.major_id = B.object_id and C.minor_id = B.column_id
                            inner join (SELECT syscolumns.name AS name,systypes.name AS type,syscolumns.length AS length 
                             FROM syscolumns INNER JOIN systypes ON systypes.xtype=syscolumns.xtype
                             WHERE id=(SELECT id FROM sysobjects WHERE  name='{table}' and systypes.name<> 'sysname')
                            )T on T.name=B.name
                            where A.name = '{table}'";
            return _db.Ado.SqlQuery<DbTableInfo>(sql);
        }
        #endregion
    }
}
