using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinqPractice
{
	public static partial class Enumerable
	{
		public static IEnumerable<TSource> DefaultIfEmpty<TSource>(
			this IEnumerable<TSource> source)
		{
			return source.DefaultIfEmpty(default(TSource));
		}
		public static IEnumerable<TSource> DefaultIfEmpty<TSource>(
			this IEnumerable<TSource> source, TSource defaultValue)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source is null");
			}
			return DefaultIfEmptyImpl(source, defaultValue);
		}
		private static IEnumerable<TSource> DefaultIfEmptyImpl<TSource>(
			IEnumerable<TSource> source, TSource defaultValue)
		{
			bool foundAny = false;
			foreach (TSource item in source)
			{
				yield return item;
				foundAny = true;
			}
			if (!foundAny)
			{
				yield return defaultValue;
			}
		}
	}
}
