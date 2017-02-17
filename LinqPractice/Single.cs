using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinqPractice
{
	public static partial class Enumerable
	{
		public static TSource Single<TSource>(
			this IEnumerable<TSource> source)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			using (IEnumerator<TSource> iterator = source.GetEnumerator())
			{
				if (!iterator.MoveNext())
				{
					throw new InvalidOperationException("Sequence was empty");
				}
				TSource ret = iterator.Current;
				if (iterator.MoveNext())
				{
					throw new InvalidOperationException("Sequence contained multiple elements");
				}
				return ret;
			}
		}

		public static TSource Single<TSource>(
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

			TSource ret = default(TSource);
			bool foundAny = false;
			foreach (TSource item in source)
			{
				if (predicate(item))
				{
					if (foundAny)
					{
						throw new InvalidOperationException("Sequence contained multiple matching elements");
					}
					foundAny = true;
					ret = item;
				}
			}
			if (!foundAny)
			{
				throw new InvalidOperationException("No items matched the predicate");
			}
			return ret;
		}
	}
}
