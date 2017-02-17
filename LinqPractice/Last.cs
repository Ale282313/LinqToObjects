using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinqPractice
{
	public static partial class Enumerable
	{
		public static TSource Last<TSource>(
			this IEnumerable<TSource> source)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}

			IList<TSource> list = source as IList<TSource>;
			if (list != null)
			{
				if (list.Count == 0)
				{
					throw new InvalidOperationException("Sequence was empty");
				}
				return list[list.Count - 1];
			}

			using (IEnumerator<TSource> iterator = source.GetEnumerator())
			{
				if (!iterator.MoveNext())
				{
					throw new InvalidOperationException("Sequence was empty");
				}
				TSource last = iterator.Current;
				while (iterator.MoveNext())
				{
					last = iterator.Current;
				}
				return last;
			}
		}

		public static TSource Last<TSource>(
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
			bool foundAny = false;
			TSource last = default(TSource);
			foreach (TSource item in source)
			{
				if (predicate(item))
				{
					foundAny = true;
					last = item;
				}
			}
			if (!foundAny)
			{
				throw new InvalidOperationException("No items matched the predicate");
			}
			return last;
		}
	}
}
