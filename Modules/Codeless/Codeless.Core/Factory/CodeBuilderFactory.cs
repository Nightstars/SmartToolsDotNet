using Codeless.Core.Interface;
using Codeless.Core.Models;
using Codeless.Core.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Codeless.Core.Factory
{
    public class CodeBuilderFactory : ICodeBuilderFactory
    {

        public ICodeBuilder GetInstance(List<DbTableInfo> ls, string rootnamespace, string modelName, string database, string tbname, string buildpath, string projtype, string outputtype, string primarykey, List<DbTableInfo> searchls, string area, string xmlpath, string viewtitle)
        {
            return new CodeBuilder(ls, rootnamespace, modelName, database, tbname, buildpath, projtype, outputtype, primarykey, searchls, area, xmlpath, viewtitle);
        }
    }
}
