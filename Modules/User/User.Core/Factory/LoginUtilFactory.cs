using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using User.Core.Utils;

namespace User.Core.Factory
{
    public class LoginUtilFactory
    {
        public LoginUtil GetInstance()
        {
            return new LoginUtil();
        }
    }
}
