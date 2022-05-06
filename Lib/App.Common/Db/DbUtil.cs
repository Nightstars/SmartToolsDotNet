using Newtonsoft.Json;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Common.Db
{
    public class DbUtil
    {
        //创建SqlSugarClient
        public static SqlSugarClient GetInstance(string connectionString,DbType dbtype = DbType.SqlServer, bool debug = false)
        {
            //创建数据库对象
            SqlSugarClient db = new SqlSugarClient(new ConnectionConfig()
            {
                ConnectionString = connectionString,//连接符字串
                DbType = dbtype,
                IsAutoCloseConnection = true
            });

            if (debug)
            {
                //添加Sql打印事件
                db.Aop.OnLogExecuting = (sql, pars) =>
                {
                    Console.WriteLine($"sql语句: {sql}, 参数: {JsonConvert.SerializeObject(pars)} ");
                };
            }

            return db;
        }
    }
}
