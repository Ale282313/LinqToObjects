using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinqPractice
{
	public static partial class Enumerable
	{
		public static TSource FirstOrDefault<TSource>(this IEnumerable<TSource> source)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source is null");
			}
			using (IEnumerator<TSource> iterator = source.GetEnumerator())
			{
				return iterator.MoveNext() ? iterator.Current : default(TSource);
			}
		}
		public static TSource FirstOrDefault<TSource>(
			this IEnumerable<TSource> source,
			Func<TSource, bool> predicate)
		{
			foreach (TSource item in source)
			{
				if (predicate(item))
				{
					return item;
				}
			}
			return default(TSource);
		}

		//FIRSTOrDEFAULT implemented using DefaultIfEmpty

	//	public static TSource FirstOrDefault<TSource>(
	//this IEnumerable<TSource> source)
	//	{
	//		return source.DefaultIfEmpty().First();
	//	}
	//	public static TSource FirstOrDefault<TSource>(
	//		this IEnumerable<TSource> source,
	//		Func<TSource, bool> predicate)
	//	{
	//		// Can’t just use source.DefaultIfEmpty().First(predicate) 
	//		return source.Where(predicate).DefaultIfEmpty().First();
	//	}
	}
}
