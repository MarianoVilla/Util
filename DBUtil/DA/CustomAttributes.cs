using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DBUtil.DA
{
    public sealed class Selectable : Attribute { }
    public sealed class Insertable : Attribute { }
    public sealed class ID : Attribute { }
}
