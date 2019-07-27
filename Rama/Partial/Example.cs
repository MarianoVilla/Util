using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rama
{
    partial class Example<TFirst, TSecond> : IEquatable<string> where TFirst : class
    {
        public bool Equals(string other)
        {
            return false;
        }
    }
}
