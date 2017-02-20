using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinqPractice
{
	public static partial  class Enumerable
	{
		public static IEnumerable<TSource> Union<TSource>(
			this IEnumerable<TSource> first,
			IEnumerable<TSource> second)
		{
			return Union(first, second, EqualityComparer<TSource>.Default);
		}
		public static IEnumerable<TSource> Union<TSource>(
			this IEnumerable<TSource> first,
			IEnumerable<TSource> second,
			IEqualityComparer<TSource> comparer)
		{
			if(first == null)
			{
				throw new ArgumentNullException("first is null");
			}
			if(second == null)
			{
				throw new ArgumentNullException("second is null");
			}
			return UnionImpl(first, second, comparer ?? EqualityComparer<TSource>.Default);
		}

		private static IEnumerable<TSource> UnionImpl<TSource>(IEnumerable<TSource> first, IEnumerable<TSource> second,IEqualityComparer<TSource> comparer)
		{
			HashSet < TSource > seenElements= new HashSet<TSource>(comparer);
			foreach (TSource item in first)
			{
				if (seenElements.Add(item))
				{
					yield return item;
				}
			}
			foreach (TSource item in second)
			{
				if (seenElements.Add(item))
				{
					yield return item;
				}
			}
		}
	}
}
