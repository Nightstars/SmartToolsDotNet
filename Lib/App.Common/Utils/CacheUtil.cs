using App.Common.Cache;
using App.Common.Db;
using App.Common.Interface;
using App.Common.Models.Cache;
using App.Common.Status;
using Microsoft.Data.Sqlite;
using Microsoft.Extensions.Caching.Memory;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Common.Utils
{
    public class CacheUtil : ICacheUtil
    {
        #region 初始化     
        protected readonly SqlSugarClient Db;

        public CacheUtil()
        {
            Db = DbUtil.GetInstance(GetSqliteConnectionStringBuilder(), DbType.Sqlite);
        }

        #endregion

        #region GetSqliteConnectionStringBuilder
        public static string GetSqliteConnectionStringBuilder()
        {
            return $@"Data Source={AppDomain.CurrentDomain.BaseDirectory}app_data\cache.db;Cache=Shared;";
        }
        #endregion

        #region 获取服务器地址
        /// <summary>
        /// 获取服务器地址
        /// </summary>
        /// <returns></returns>
        public List<AppServer> GetAppServer()
        {
            return Db.Queryable<AppServer>().ToList();
        }
        #endregion

        #region 获取用户信息
        /// <summary>
        /// 获取用户信息
        /// </summary>
        /// <returns></returns>
        public Appuser GetAppuser()
        {
            return Db.Queryable<Appuser>()//.Where(x => x.Version == Cache.Cache.appCache.Get<string>(CacheKeys.CurrentCmsVersion))
                .First();
        }
        #endregion

        #region 获取凭证
        /// <summary>
        /// 获取凭证
        /// </summary>
        /// <returns></returns>
        public AppToken GetToken()
        {
            return Db.Queryable<AppToken>().Where(x => x.Version == Cache.Cache.appCache.Get(CacheKeys.CurrentCmsVersion) as string ).First();
        }
        #endregion

        #region 设置用户信息
        /// <summary>
        /// 设置用户信息
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public bool SetAppuser(Appuser user)
        {
            //Db.DbMaintenance.TruncateTable<Appuser>();
            var rs = Db.Deleteable<Appuser>().Where(x => x.SeqNo != user.SeqNo && x.Version == Cache.Cache.appCache.Get(CacheKeys.CurrentCmsVersion) as string).ExecuteCommand();
            return Db.Insertable<Appuser>(user).ExecuteCommand() > 0;
        }
        #endregion

        #region 设置凭证
        /// <summary>
        /// 设置凭证
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        public bool SetToken(AppToken token)
        {
            var rs = Db.Deleteable<AppToken>().Where(x => x.SeqNo != token.SeqNo && x.Version == Cache.Cache.appCache.Get<string>(CacheKeys.CurrentCmsVersion)) .ExecuteCommand();
            return Db.Insertable<AppToken>(token).ExecuteCommand() > 0;

        }
        #endregion

        #region 清除登录缓存
        public bool ClearLoginCache()
        {
            var rs = Db.Deleteable<Appuser>().ExecuteCommand() > 0;
            rs = Db.Deleteable<AppToken>().ExecuteCommand() > 0;
            return rs;
        }
        #endregion

        #region 获取下拉数据源
        public List<AppComboDatasource> GetComboDatasources()
        {
            var db = DbUtil.GetInstance(@"Database=SPECIAL_SMARTTOOLSDOTNET;Server=192.168.2.52,59550\\V6_4;User ID = sa; Password = Dbwork2022;", DbType.SqlServer);
            return db.Queryable<AppComboDatasource>().Distinct().ToList();
            //return Db.Queryable<AppComboDatasource>().ToList();
        }
        #endregion
    }
}