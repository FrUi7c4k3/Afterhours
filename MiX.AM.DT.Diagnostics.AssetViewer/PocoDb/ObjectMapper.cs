using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace PocoDb
{
	internal class ObjectMapper
	{
		internal static IEnumerable<T> Map<T>(DataTable table)
			where T : new()
		{
			List<T> list = new List<T>();

			for (int rowIndex = 0; rowIndex < table.Rows.Count; rowIndex++)
			{
				T obj = new T();

				foreach (var property in typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance | BindingFlags.NonPublic))
				{
					string colName = property.Name;
					SetProperty<T>(obj, property, table.Rows[rowIndex][colName]);
				}

				list.Add(obj);
			}

			return list;
		}

		private static void SetProperty<T>(T obj, PropertyInfo property, object value)
		{
			try
			{
				property.SetValue(obj, value);
			}
			catch
			{
				property.SetValue(obj, Convert(value, property.PropertyType));
			}
		}

		private static object Convert(object dbValue, Type targetType)
		{
			TypeConverter c = TypeDescriptor.GetConverter(dbValue);
			if (c != null && c.CanConvertTo(targetType))
			{
				return c.ConvertTo(dbValue, targetType);
			}

			return null;
		}
	}
}
