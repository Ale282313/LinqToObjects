using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinqPractice
{
	public static partial class Enumerable
	{
		public static TSource LastOrDefault<TSource>(
			this IEnumerable<TSource> source)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}

			IList<TSource> list = source as IList<TSource>;
			if (list != null)
			{
				return list.Count == 0 ? default(TSource) : list[list.Count - 1];
			}

			TSource last = default(TSource);
			foreach (TSource item in source)
			{
				last = item;
			}
			return last;
		}

		public static TSource LastOrDefault<TSource>(
			this IEnumerable<TSource> source,
			Func<TSource, bool> predicate)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			if (predicate == null)
			{
				throw new ArgumentNullException("predicate");
			}
			TSource last = default(TSource);
			foreach (TSource item in source)
			{
				if (predicate(item))
				{
					last = item;
				}
			}
			return last;
		}
	}
}
