using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QueryFactory.Helpers
{
    public static class ArgsValidator
    {
        public static void ThrowIfNull(params object[] Args)
        {
            if (Args == null)
                throw new ArgumentNullException(nameof(Args));
            for(int i = 0; i < Args.Length; i++)
            {
                if (Args[i] == null)
                    throw new ArgumentNullException(i.ToString());
            }   
        }
        public static void ThrowIfNullOrWhiteSpace(params string[] Args)
        {
            if (Args == null)
                throw new ArgumentNullException(nameof(Args));
            for (int i = 0; i < Args.Length; i++)
            {
                if (string.IsNullOrWhiteSpace(Args[i]))
                    throw new ArgumentNullException(i.ToString());
            }
        }
    }
}
