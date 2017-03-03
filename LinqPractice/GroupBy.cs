using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinqPractice
{
	public static partial class Enumerable
	{
		public static IEnumerable<IGrouping<TKey, TSource>> GroupBy<TSource, TKey>(
			this IEnumerable<TSource> source,
			Func<TSource, TKey> keySelector)
		{
			return source.GroupBy(keySelector, x => x, EqualityComparer<TKey>.Default);
		}

		public static IEnumerable<IGrouping<TKey,TSource>> GroupBy<TKey,TSource>(
			this IEnumerable<TSource> source,
			Func<TSource,TKey> keySelector,
			IEqualityComparer<TKey> comparer)
		{
			return source.GroupBy(keySelector, x => x, comparer);
		}

		public static IEnumerable<IGrouping<TKey,TElement>> GroupBy<TSource,TKey,TElement>(
			this IEnumerable<TSource> source,
			Func<TSource,TKey> keySelector,
			Func<TSource,TElement> elementSelector)
		{
			return source.GroupBy(keySelector, elementSelector, EqualityComparer<TKey>.Default);
		}

		public static IEnumerable<IGrouping<TKey, TElement>> GroupBy<TSource, TKey, TElement>(
			this IEnumerable<TSource> source,
			Func<TSource, TKey> keySelector,
			Func<TSource, TElement> elementSelector,
			IEqualityComparer<TKey> comparer)
		{
			if(source == null)
			{
				throw new ArgumentNullException("source is null");
			}
			if(keySelector == null)
			{
				throw new ArgumentNullException("keySelector is null");
			}
			if(elementSelector == null)
			{
				throw new ArgumentNullException("elementSelector is null");
			}
			return GroupByImpl(source, keySelector, elementSelector, comparer ?? EqualityComparer<TKey>.Default);
		}
		
		private static IEnumerable<IGrouping<TKey,TElement>> GroupByImpl<TSource,TKey,TElement>(
			IEnumerable<TSource> source,
			Func<TSource,TKey> keySelector,
			Func<TSource,TElement> elementSelector,
			IEqualityComparer<TKey> comparer)
		{
			var lookup = source.ToLookup(keySelector, elementSelector, comparer);
			foreach (var result in lookup)
			{
				yield return result;
			}
		}

		public static IEnumerable<TResult> GroupBy<TSource,TKey,TResult>(
			this IEnumerable<TSource> source,
			Func<TSource,TKey> keySelector,
			Func<TKey,IEnumerable<TSource>,TResult> resultSelector)
		{
			return source.GroupBy(keySelector, x => x, resultSelector, EqualityComparer<TKey>.Default);
		}

		public static IEnumerable<TResult> GroupBy<TSource, TKey, TResult>(
			this IEnumerable<TSource> source,
			Func<TSource, TKey> keySelector,
			Func<TKey, IEnumerable<TSource>, TResult> resultSelector,
			IEqualityComparer<TKey> comparer)
		{
			return source.GroupBy(keySelector, x => x, resultSelector, comparer);
		}

		public static IEnumerable<TResult> GroupBy<TSource, TKey, TElement, TResult>(
			this IEnumerable<TSource> source,
			Func<TSource, TKey> keySelector,
			Func<TSource, TElement> elementSelector,
			Func<TKey, IEnumerable<TElement>, TResult> resultSelector)
		{
			return source.GroupBy(keySelector, elementSelector, resultSelector, EqualityComparer<TKey>.Default);
		}

		public static IEnumerable<TResult> GroupBy<TSource, TKey, TElement, TResult>(
			this IEnumerable<TSource> source,
			Func<TSource, TKey> keySelector,
			Func<TSource, TElement> elementSelector,
			Func<TKey, IEnumerable<TElement>, TResult> resultSelector,
			IEqualityComparer<TKey> comparer)
		{
			if (resultSelector == null)
			{
				throw new ArgumentNullException("resultSelector");
			}
			// Let the other GroupBy overload do the rest of the argument validation
			return source.GroupBy(keySelector, elementSelector, comparer)
						 .Select(group => resultSelector(group.Key, group));
		}
	}
}
