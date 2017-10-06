using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//
// Used in ToDataTable converter
//
using System.Data;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.ComponentModel;

namespace mmMovieCollection.Model
{
    public static class AllExtensions
    {

        /// <summary>
        /// Convert IEnumerable to DataTable
        /// </summary>
        public static DataTable ToDataTable<T>(this List<T> items)
        {
            var tb = new DataTable(typeof(T).Name);
            PropertyInfo[] props = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);
            foreach (var prop in props)
            {
                Type propType = prop.PropertyType;
                if (propType.IsGenericType && propType.GetGenericTypeDefinition().Equals(typeof(Nullable<>)))
                    propType = new NullableConverter(propType).UnderlyingType;
                tb.Columns.Add(prop.Name, prop.PropertyType);
            }
            foreach (var item in items)
            {
                var values = new object[props.Length];
                for (var i = 0; i < props.Length; i++)
                    values[i] = props[i].GetValue(item, null);
                tb.Rows.Add(values);
            }
            return tb;
        }
    }
}
