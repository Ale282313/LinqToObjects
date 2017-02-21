using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinqPractice
{
	public static partial class Enumerable
	{
		public static List<TSource> ToList<TSource>(this IEnumerable<TSource> source)
		{
			if(source == null)
			{
				throw new ArgumentNullException("source is null");
			}
			return new List<TSource>(source);
		}
	}
}
