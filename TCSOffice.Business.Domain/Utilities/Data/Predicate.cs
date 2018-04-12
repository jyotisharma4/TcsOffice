using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TCSOffice.Business.Domain.Utilities.Data
{
    public class Predicate
    {
        public string Name { get; set; }
        public object Value { get; set; }
        public Operators Operator { get; set; }

        public Predicate(string fieldName, Operators operata, object value)
        {
            Name = fieldName;
            Value = value;
            Operator = operata;
        }
        public Predicate(string fieldName, string operata, object value)
        {
            Name = fieldName;
            Value = value;
            Operator =
                operata.ToLower().Contains("eq") ? Operators.Equals : //=
                operata.ToLower().Contains("g") ? Operators.GreaterThan : //gt
                operata.ToLower().Contains("l") ? Operators.LessThan : //lt
                operata.ToLower().Contains("c") ? Operators.Contains : //cn
                operata.ToLower().Contains("end") ? Operators.EndsWith : //ew
                operata.ToLower().Contains("start") ? Operators.StartsWith : //sw
                Operators.Equals;
        }
    }
}
