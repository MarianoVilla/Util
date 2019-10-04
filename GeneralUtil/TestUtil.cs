using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace GeneralUtil
{
    public class ParamsComparer : Comparer<SqlParameter>
    {
        public override int Compare(SqlParameter x, SqlParameter y)
        {
            if (x.Value == null || y.Value == null)
                return 0;
            var NameCompare = x.ParameterName.CompareTo(y.ParameterName);
            if (NameCompare == 0 && x.Value.ToString() == y.Value.ToString())
                return 0;
            return -1;
        }
    }
    public class DynamicParams : DynamicParameters
    {
        public override bool Equals(object obj)
        {
            return CompareStrict(this, (DynamicParameters)obj);
        }
        public override int GetHashCode()
        {
            return this.ParameterNames.GetHashCode();
        }
        bool CompareStrict(DynamicParameters Me, DynamicParameters TheOther)
        {
            return Me.ParameterNames.All(xn =>
            {
                var ParamInX = Me.Get<dynamic>(xn);
                var ParamInY = TheOther.Get<dynamic>(xn);
                if (ParamInX == null)
                {
                    return (TheOther.Get<dynamic>(xn) == null);
                }
                return ParamInX.Equals(ParamInY);
            });
        }
    }
}
