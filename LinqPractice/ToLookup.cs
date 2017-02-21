using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinqPractice
{
	public static partial class Enumerable
	{
		public static ILookup<TKey,TSource> ToLookup<TSource,TKey>(
			this IEnumerable<TSource> source,
			Func<TSource,TKey> keySelector)
		{
			return source.ToLookup(keySelector, element => element, EqualityComparer<TKey>.Default);
		}
		public static ILookup<TKey,TSource> ToLookup<TSource,TKey>(
			this IEnumerable<TSource> source,
			Func<TSource,TKey> keySelector,
			IEqualityComparer<TKey> comparer)
		{
			return source.ToLookup(keySelector, element => element, comparer);
		}
		public static ILookup<TKey,TElement> ToLookup<TSource,TKey,TElement>(
			this IEnumerable<TSource> source,
			Func<TSource,TKey> keySelector,
			Func<TSource,TElement> elementSelector)
		{
			return source.ToLookup(keySelector, elementSelector, EqualityComparer<TKey>.Default);
		}
		public static ILookup<TKey,TElement> ToLookup<TSource,TKey,TElement>(
			this IEnumerable<TSource> source,
			Func<TSource,TKey> keySelector,
			Func<TSource,TElement> elementSelector,
			IEqualityComparer<TKey> comparer)
		{
			if( source == null)
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
			Lookup<TKey, TElement> lookup = new Lookup<TKey, TElement>(comparer ?? EqualityComparer<TKey>.Default);
			foreach (TSource item in source)
			{
				TKey key = keySelector(item);
				TElement element = elementSelector(item);
				lookup.Add(key, element);
			}
			return lookup;
		}
		private static ILookup<TKey, TSource> ToLookupNoNullKeys<TSource, TKey>(
		   this IEnumerable<TSource> source,
		   Func<TSource, TKey> keySelector,
		   IEqualityComparer<TKey> comparer)
		{
			Lookup<TKey, TSource> lookup = new Lookup<TKey, TSource>(comparer ?? EqualityComparer<TKey>.Default);

			foreach (TSource item in source)
			{
				TKey key = keySelector(item);
				if (key != null)
				{
					lookup.Add(key, item);
				}
			}
			return lookup;
		}

	}
}
