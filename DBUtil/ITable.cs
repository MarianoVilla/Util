using DBUtil.DA;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DBUtil
{
    public interface ITable
    {
        string IDColumn { get; set; }
    }
}
