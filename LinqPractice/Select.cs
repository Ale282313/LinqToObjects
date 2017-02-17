using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinqPractice
{
	public static partial class Enumerable
	{

		public static IEnumerable<TResult> Select<TSource, TResult>(
			this IEnumerable<TSource> source,
			Func<TSource, TResult> selector)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source is null");
			}
			if (selector == null)
			{
				throw new ArgumentNullException("selector is null");
			}
			return SelectImpl(source, selector);
		}

		public static IEnumerable<TResult> SelectImpl<TSource, TResult>(
			this IEnumerable<TSource> source,
			Func<TSource, TResult> selector)
		{
			foreach (TSource item in source)
			{
				yield return selector(item);
			}
		}

		public static IEnumerable<TResult> Select<TSource, TResult>(
			this IEnumerable<TSource> source,
			Func<TSource, int, TResult> selector)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			if (selector == null)
			{
				throw new ArgumentNullException("selector");
			}
			return SelectImpl(source, selector);
		}

		private static IEnumerable<TResult> SelectImpl<TSource, TResult>(
			this IEnumerable<TSource> source,
			Func<TSource, int, TResult> selector)
		{
			int index = 0;
			foreach (TSource item in source)
			{
				yield return selector(item, index);
				index++;
			}
		}

		//Select implemented using selectMany

		//	public static IEnumerable<TResult> Select<TSource, TResult>(
		//this IEnumerable<TSource> source,
		//Func<TSource, TResult> selector)
		//	{
		//		if (source == null)
		//		{
		//			throw new ArgumentNullException("source");
		//		}
		//		if (selector == null)
		//		{
		//			throw new ArgumentNullException("selector");
		//		}
		//		return source.SelectMany(x => Enumerable.Repeat(selector(x), 1));
		//	}
	}
}
