using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinqPractice
{
	public static partial class Enumerable
	{

		public static IEnumerable<TSource> Where<TSource>(
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
			return WhereImpl(source, predicate);
		}

		public static IEnumerable<TSource> WhereImpl<TSource>(
			this IEnumerable<TSource> source,
			Func<TSource, bool> predicate)
		{
			foreach (TSource item in source)
			{
				if (predicate(item))
				{
					yield return item;
				}
			}
		}

		public static IEnumerable<TSource> Where<TSource>(
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
			return WhereImpl(source, predicate);
		}

		public static IEnumerable<TSource> WhereImpl<TSource>(
			this IEnumerable<TSource> source,
			Func<TSource, int, bool> predicate)
		{
			int index = 0;
			foreach (TSource item in source)
			{
				if (predicate(item, index))
				{
					yield return item;
				}
				index++;
			}
		}

		//Where implemented using selectMany

	//	public static IEnumerable<TSource> Where<TSource>(
	//this IEnumerable<TSource> source,
	//Func<TSource, bool> predicate)
	//	{
	//		if (source == null)
	//		{
	//			throw new ArgumentNullException("source");
	//		}
	//		if (predicate == null)
	//		{
	//			throw new ArgumentNullException("predicate");
	//		}
	//		return source.SelectMany(x => Enumerable.Repeat(x, predicate(x) ? 1 : 0));
	//	}

	}
}
