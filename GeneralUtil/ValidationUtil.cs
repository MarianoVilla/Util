using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dager
{
    public  class ValidationUtil
    {


        //ToDo: sanity check.
        public IEnumerable<string> GetModelAnnotationErrors(object o)
        {
            return TypeDescriptor
                .GetProperties(o.GetType())
                .Cast<PropertyDescriptor>()
                .SelectMany(pd => pd.Attributes.OfType<ValidationAttribute>()
                                    .Where(va => !va.IsValid(pd.GetValue(o))))
                                    .Select(xx => xx.ErrorMessage);
        }
        //ToDo: sanity check.
        public List<IEnumerable<string>> GetModelAnnotationErrors(List<object> o)
        {
            List<IEnumerable<string>> Errors = new List<IEnumerable<string>>();
            o.ForEach(x => Errors.Add(TypeDescriptor
                .GetProperties(x.GetType())
                .Cast<PropertyDescriptor>()
                .SelectMany(pd => pd.Attributes.OfType<ValidationAttribute>()
                                    .Where(va => !va.IsValid(pd.GetValue(x))))
                                    .Select(xx => xx.ErrorMessage)));

            return Errors;
        }
    }
}
