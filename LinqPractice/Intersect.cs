using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinqPractice
{
	public static partial class Enumerable
	{
		public static IEnumerable<TSource> Intersect<TSource>(
			this IEnumerable<TSource> first,
			IEnumerable<TSource> second)
		{
			return Intersect(first, second, EqualityComparer<TSource>.Default);
		}
		public static IEnumerable<TSource> Intersect<TSource>(
			this IEnumerable<TSource> first,
			IEnumerable<TSource> second,
			IEqualityComparer<TSource> comparer)
		{
			if( first == null)
			{
				throw new ArgumentNullException("first is null");
			}
			if( second == null)
			{
				throw new ArgumentNullException("second id null");
			}
			return IntersectImpl(first, second, comparer ?? EqualityComparer<TSource>.Default);
		}
		private static IEnumerable<TSource> IntersectImpl<TSource>(
			IEnumerable<TSource> first,
			IEnumerable<TSource> second,
			IEqualityComparer<TSource> comparer)
		{
			HashSet<TSource> potentialElements = new HashSet<TSource>(second, comparer);
			foreach (TSource item in first)
			{
				if (potentialElements.Remove(item))
				{
					yield return item;
				}
			}
		}
	}
}
