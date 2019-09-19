using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QueryFactory.Queries.BaseAndInterfaces
{
    public interface  IRunnableQuery
    {
        ISqlQuery Query { get; set; }
    }
}
