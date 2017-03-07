using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinqPractice
{
	public static partial class Enumerable
	{
		public static IEnumerable<TSource> Take<TSource>(
			this IEnumerable<TSource> source,
			int count)
		{
			if(source == null)
			{
				throw new ArgumentNullException("source is null");
			}
			return TakeImpl(source, count);
		}
		private static IEnumerable<TSource> TakeImpl<TSource>(
			this IEnumerable<TSource> source,
			int count)
		{
			using (IEnumerator<TSource> iterator = source.GetEnumerator())
			{
				for(int i=0;i<count && iterator.MoveNext(); i++)
				{
					yield return iterator.Current;
				}
			}
		}

	}
}
