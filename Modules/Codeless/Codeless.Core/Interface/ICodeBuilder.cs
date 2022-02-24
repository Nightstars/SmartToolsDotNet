using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Codeless.Core.Interface
{
    public interface ICodeBuilder
    {
        /// <summary>
        /// 生成实体
        /// </summary>
        public ICodeBuilder BuildModel();

        /// <summary>
        /// 生成SearchModel
        /// </summary>
        public ICodeBuilder BuildSearchModel();

        /// <summary>
        /// 生成Service
        /// </summary>
        public ICodeBuilder BuildService();

        /// <summary>
        /// 生成Controller
        /// </summary>
        public ICodeBuilder BuildController();

        /// <summary>
        /// 生成视图页面
        /// </summary>
        public ICodeBuilder BuildViews();

    }
}
