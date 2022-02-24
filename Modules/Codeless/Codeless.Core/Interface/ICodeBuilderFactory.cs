using Codeless.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Codeless.Core.Interface
{
    public interface ICodeBuilderFactory
    {
        public ICodeBuilder GetInstance(List<DbTableInfo> ls,
			string rootnamespace,
			string modelName,
			string database,
			string tbname,
			string buildpath,
			string projtype,
			string outputtype,
			string primarykey,
			List<DbTableInfo> searchls,
			string area,
			string xmlpath,
			string viewtitle);
    }
}
