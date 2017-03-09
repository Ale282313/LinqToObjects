using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinqPractice
{
	public static partial class Enumerable
	{
		public static Dictionary<TKey, TSource> ToDictionary<TSource, TKey>(
			this IEnumerable<TSource> source,
			Func<TSource, TKey> keySelector)
		{
			return source.ToDictionary(keySelector, x => x, EqualityComparer<TKey>.Default);
		}

		public static Dictionary<TKey, TElement> ToDictionary<TSource, TKey, TElement>(
			this IEnumerable<TSource> source,
			Func<TSource, TKey> keySelector,
			Func<TSource, TElement> elementSelector)
		{
			return source.ToDictionary(keySelector, elementSelector, EqualityComparer<TKey>.Default);
		}

		public static Dictionary<TKey, TSource> ToDictionary<TSource, TKey>(
			this IEnumerable<TSource> source,
			Func<TSource, TKey> keySelector,
			IEqualityComparer<TKey> comparer)
		{
			return source.ToDictionary(keySelector, x => x, comparer);
		}

		public static Dictionary<TKey, TElement> ToDictionary<TSource, TKey, TElement>(
			this IEnumerable<TSource> source,
			Func<TSource, TKey> keySelector,
			Func<TSource, TElement> elementSelector,
			IEqualityComparer<TKey> comparer)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			if (keySelector == null)
			{
				throw new ArgumentNullException("keySelector");
			}
			if (elementSelector == null)
			{
				throw new ArgumentNullException("elementSelector");
			}
			comparer = comparer ?? EqualityComparer<TKey>.Default;
			ICollection<TSource> list = source as ICollection<TSource>;
			var ret = list == null ? new Dictionary<TKey, TElement>(comparer)
								   : new Dictionary<TKey, TElement>(list.Count, comparer);
			foreach (TSource item in source)
			{
				ret.Add(keySelector(item), elementSelector(item));
			}
			return ret;
		}
	}
}
