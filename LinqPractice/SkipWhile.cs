using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinqPractice
{
	public static partial class Enumerable
	{
		public static IEnumerable<TSource> SkipWhile<TSource>(
			this IEnumerable<TSource> source,
			Func<TSource,bool> predicate)
		{
			if (source == null)
			{
				throw new ArgumentNullException("Source is null");
			}
			if( predicate == null)
			{
				throw new ArgumentNullException("predicate is null");
			}
			return SkipWhileImpl(source, predicate);
		}
		private static IEnumerable<TSource> SkipWhileImpl<TSource>(
			this IEnumerable<TSource> source,
			Func<TSource,bool> predicate)
		{
			using(IEnumerator<TSource> iterator = source.GetEnumerator())
			{
				while (iterator.MoveNext())
				{
					TSource item = iterator.Current;
					if (!predicate(item))
					{
						yield return item;
						break;
					}
				}
				while (iterator.MoveNext())
				{
					yield return iterator.Current;
				}
			}
		}
		public static IEnumerable<TSource> SkipWhile<TSource>(
			this IEnumerable<TSource> source,
			Func<TSource,int,bool> predicate)
		{
			if(source == null)
			{
				throw new ArgumentNullException("source is null");
			}
			if(predicate == null)
			{
				throw new ArgumentNullException("predicate is null");
			}
			return SkipWhileImpl(source, predicate);
		}
		private static IEnumerable<TSource> SkipWhileImpl<TSource>(
			this IEnumerable<TSource> source,
			Func<TSource,int,bool> predicate)
		{
			using (IEnumerator<TSource> iterator=source.GetEnumerator())
			{
				int index = 0;
				while (iterator.MoveNext())
				{
					TSource item = iterator.Current;
					if (!predicate(item, index))
					{
						yield return item;
						break;
					}
					index++;
				}
				while (iterator.MoveNext())
				{
					yield return iterator.Current;
				}
			}
		}
	}
}
