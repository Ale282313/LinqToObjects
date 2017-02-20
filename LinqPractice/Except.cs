using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinqPractice
{
	public static partial class Enumerable
	{
		public static IEnumerable<TSource> Except<TSource>(
		   this IEnumerable<TSource> first,
		   IEnumerable<TSource> second)
		{
			return Except(first, second, EqualityComparer<TSource>.Default);
		}

		public static IEnumerable<TSource> Except<TSource>(
			this IEnumerable<TSource> first,
			IEnumerable<TSource> second,
			IEqualityComparer<TSource> comparer)
		{
			if (first == null)
			{
				throw new ArgumentNullException("first");
			}
			if (second == null)
			{
				throw new ArgumentNullException("second");
			}
			return ExceptImpl(first, second, comparer ?? EqualityComparer<TSource>.Default);
		}
		private static IEnumerable<TSource> ExceptImpl<TSource>(
			IEnumerable<TSource> first,
			IEnumerable<TSource> second,
			IEqualityComparer<TSource> comparer)
		{
			HashSet<TSource> bannedElements = new HashSet<TSource>(second, comparer);
			foreach (TSource item in first)
			{
				if (bannedElements.Add(item))
				{
					yield return item;
				}
			}
		}
	}
}
