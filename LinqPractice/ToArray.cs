using System;
using System.Collections.Generic;

namespace LinqPractice
{
	public static partial class Enumerable
	{
		public static TSource[] ToArray<TSource>(this IEnumerable<TSource> source)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			ICollection<TSource> collection = source as ICollection<TSource>;
			if (collection != null)
			{
				TSource[] ret = new TSource[collection.Count];
				collection.CopyTo(ret, 0);
				return ret;
			}
			return new List<TSource>(source).ToArray();
		}
	}
}
