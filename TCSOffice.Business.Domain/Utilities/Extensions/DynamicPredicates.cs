using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TCSOffice.Business.Domain.Utilities.Data;

namespace TCSOffice.Business.Domain.Utilities.Extensions
{
    public static class DynamicPredicates
    {
        /// <summary>
        /// This filters the collection and returns items that match the predicate from the collection.
        /// Only works for OPERATORS=CONTAINS,EQUALS and FIELDTYPES=INT,STRING at the moment
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="collection"></param>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public static IEnumerable<T> GetByPredicate<T>(this IEnumerable<T> collection, Predicate predicate)
        {
            var byPredicate = collection as IList<T> ?? collection.ToList();
            foreach (var item in byPredicate)
            {
                var value = item.GetPropertyValue(predicate.Name);
                var valueType = item.GetPropertyType(predicate.Name);

                //IF THE FIELD VALUE IS A STRING
                if (valueType.ToString().ToLower().Contains("string"))
                {
                    var valueToString = value.ToString();
                    var predicateValueToString = predicate.Value.ToString();
                    //i. equals
                    if (predicate.Operator == Operators.Equals && valueToString == predicateValueToString)
                    {
                        yield return item;
                    }
                    //ii. contains
                    if (predicate.Operator == Operators.Contains && valueToString.Contains(predicateValueToString))
                    {
                        yield return item;
                    }
                }

                //IF THE FIELD VALUE IS AN INT
                if (valueType.ToString().ToLower().Contains("int"))
                {
                    var valueToInt = Convert.ToInt32(value);
                    var predicateValueToInt = Convert.ToInt32(predicate.Value);
                    //i. equals
                    if (predicate.Operator == Operators.Equals && valueToInt == predicateValueToInt)
                    {
                        yield return item;
                    }
                }
            }
        }
    }
}
