using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinqPractice
{
	public static partial class Enumerable
	{
		public static IEnumerable<TSource> Skip<TSource>(
			this IEnumerable<TSource> source,
			int count)
		{
			if(source == null)
			{
				throw new ArgumentNullException("source is null");
			}
			if (count <= 0)
				return source;
			return SkipImpl(source, count);
		}
		private static IEnumerable<TSource> SkipImpl<TSource>(
			this IEnumerable<TSource> source,
			int count)
		{
			using(IEnumerator<TSource> iterator = source.GetEnumerator())
			{
				for(int i = 0; i < count; i++)
				{
					if (!iterator.MoveNext())
					{
						yield break;
					}
				}
				while (iterator.MoveNext())
				{
					yield return iterator.Current;
				}
			}

		}
	}
}
