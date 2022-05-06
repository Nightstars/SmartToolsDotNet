using App.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace User.Core.Models
{
    public class AppUser : BaseModel
    {
        //
        // Summary:
        //     用户ID
        public string UserId
        {
            get;
            set;
        }

        //
        // Summary:
        //     用户手机号
        public string Mobile
        {
            get;
            set;
        }

        //
        // Summary:
        //     用户名称
        public string UserName
        {
            get;
            set;
        }

        //
        // Summary:
        //     管理员标识 0:普通用户 1:管理员
        public string AdminFlag
        {
            get;
            set;
        }

        //
        // Summary:
        //     是否为平台管理员
        public bool IsPlatformAdmin
        {
            get;
            set;
        }

        //
        // Summary:
        //     用户类型 0:货主 1:货代 2:报关行
        public string UserType
        {
            get;
            set;
        }

        //
        // Summary:
        //     邮箱地址
        public string EmailAddress
        {
            get;
            set;
        }

        //
        // Summary:
        //     用户锁定标识
        public string UserLocked
        {
            get;
            set;
        }

        //
        // Summary:
        //     是否为管理员
        public string IsAdmin
        {
            get;
            set;
        }

        //
        // Summary:
        //     当前企业信息
        public CorpInfo CurrentCorpInfo
        {
            get;
            set;
        }

        //
        // Summary:
        //     用户所属企业
        public CorpInfo UserCorpInfo
        {
            get;
            set;
        }

        //
        // Summary:
        //     附加信息
        public object Data
        {
            get;
            set;
        }

    }
}
