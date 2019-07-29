using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DBUtil.DA
{
    public class NoIDColumnException : Exception
    {
        public NoIDColumnException(string message) : base(message) {  }
    }

}
