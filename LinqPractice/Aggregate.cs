using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinqPractice
{
	public static partial class Enumerable
	{
		public static TSource Aggregate<TSource>(
			this IEnumerable<TSource> source,
			Func<TSource, TSource, TSource> func)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source is null");
			}
			if (func == null)
			{
				throw new ArgumentNullException("func is null");
			}
			using (IEnumerator<TSource> iterator = source.GetEnumerator())
			{
				if (!iterator.MoveNext())
				{
					throw new InvalidOperationException("Source sequence is empty");
				}
				TSource current = iterator.Current;
				while (iterator.MoveNext())
				{
					current = func(current, iterator.Current);
				}
				return current;
			}
		}

		public static TAccumulate Aggregate<TSource, TAccumulate>(
			this IEnumerable<TSource> source,
			TAccumulate seed,
			Func<TAccumulate, TSource, TAccumulate> func)
		{
			return source.Aggregate(seed, func, x => x);
		}
		public static TResult Aggregate<TSource, TAccumulate, TResult>(
			this IEnumerable<TSource> source,
			TAccumulate seed,
			Func<TAccumulate, TSource, TAccumulate> func,
			Func<TAccumulate, TResult> resultSelector)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source is null");
			}
			if (func == null)
			{
				throw new ArgumentNullException("func is null");
			}
			if (resultSelector == null)
			{
				throw new ArgumentNullException("resultSelector is null");
			}
			TAccumulate current = seed;
			foreach (TSource item in source)
			{
				current = func(current, item);
			}
			return resultSelector(current);
		}
	}
}
