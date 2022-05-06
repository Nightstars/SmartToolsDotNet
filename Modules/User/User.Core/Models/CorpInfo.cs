using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace User.Core.Models
{
    public class CorpInfo
    {
        //
        // Summary:
        //     企业ID
        public string CorpId
        {
            get;
            set;
        }

        //
        // Summary:
        //     工厂代码
        public string Site
        {
            get;
            set;
        }

        //
        // Summary:
        //     企业名称
        public string CorpName
        {
            get;
            set;
        }

        //
        // Summary:
        //     企业10位海关编码
        public string CorpCode
        {
            get;
            set;
        }

        //
        // Summary:
        //     企业18位信用代码
        public string CorpSccCode
        {
            get;
            set;
        }

        //
        // Summary:
        //     企业10位检验检疫代码
        public string CorpCiqCode
        {
            get;
            set;
        }

        //
        // Summary:
        //     消费使用单位代码
        public string OwnerCode
        {
            get;
            set;
        }

        //
        // Summary:
        //     消费使用单位名称
        public string OwnerName
        {
            get;
            set;
        }

        //
        // Summary:
        //     消费使用单位CIQ代码
        public string OwnerCiqCode
        {
            get;
            set;
        }

        //
        // Summary:
        //     消费使用单位18位代码
        public string OwnerSccCode
        {
            get;
            set;
        }
    }
}
