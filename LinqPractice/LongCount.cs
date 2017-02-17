using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinqPractice
{
	public static partial class Enumerable
	{

		public static long LongCount<TSource>(this IEnumerable<TSource> source)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source is null");
			}

			//Optimization for ICollection<T>
			ICollection<TSource> genericCollection = source as ICollection<TSource>;
			if (genericCollection != null)
			{
				return genericCollection.Count;
			}

			//Optinization for ICollection
			ICollection nonGenericCollection = source as ICollection;
			if (nonGenericCollection != null)
			{
				return nonGenericCollection.Count;
			}
			checked
			{
				long count = 0;
				using (var iterator = source.GetEnumerator())
				{
					while (iterator.MoveNext())
					{
						count++;
					}
				}
				return count;
			}

		}
		public static long LongCount<TSource>(this IEnumerable<TSource> source,
			Func<TSource, bool> predicate)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source is null");
			}
			if (predicate == null)
			{
				throw new ArgumentNullException("predicate id null");
			}
			checked
			{
				long count = 0;
				foreach (TSource item in source)
				{
					if (predicate(item))
					{
						count++;
					}
				}
				return count;
			}
		}
	}
}
