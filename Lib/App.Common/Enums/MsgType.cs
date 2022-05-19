using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Common.Enums
{
    public enum MsgType
    {
        login,//登录消息
        logout,//注销消息
        changeSite,//切换工厂
        clearLoginCache,//清除登录信息
        loginExpired,//登录过期
        rdp,//远程控制事件
        loadlog,//加载日志
        update, //更新事件
        logs, //日志事件
    }
}
