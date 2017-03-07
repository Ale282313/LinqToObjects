using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinqPractice
{
	public static partial class Enumerable
	{
		public static IEnumerable<TSource> TakeWhile<TSource>(
			this IEnumerable<TSource> source,
			Func<TSource, bool> predicate)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source is null");
			}
			if (predicate == null)
			{
				throw new ArgumentNullException("predicate is null");
			}
			return TakeWhileImpl(source, predicate);
		}
		private static IEnumerable<TSource> TakeWhileImpl<TSource>(
			this IEnumerable<TSource> source,
			Func<TSource, bool> predicate)
		{
			foreach (TSource item in source)
			{
				if (!predicate(item))
				{
					yield break;
				}
				yield return item;
			}
		}
		public static IEnumerable<TSource> TakeWhile<TSource>(
			this IEnumerable<TSource> source,
			Func<TSource, int, bool> predicate)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source is null");
			}
			if (predicate == null)
			{
				throw new ArgumentNullException("predicate is null");
			}
			return TakeWhileImpl(source, predicate);
		}
		private static IEnumerable<TSource> TakeWhileImpl<TSource>(
			this IEnumerable<TSource> source,
			Func<TSource,int,bool> predicate)
		{
			int index = 0;
			foreach (TSource item in source)
			{
				if (!predicate(item, index))
				{
					yield break;
				}
				index++;
				yield return item;
			}
		}
	}
}
