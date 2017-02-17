using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinqPractice
{
	public static partial class Enumerator
	{
		public static IEnumerable<TSource> Concat<TSource>(
			this IEnumerable<TSource> first, IEnumerable<TSource> second)
		{
			if (first == null)
			{
				throw new ArgumentNullException("the first sequence is null");
			}
			if (second == null)
			{
				throw new ArgumentNullException("the second sequence in null");
			}
			return ConcatImpl(first, second);
		}
		private static IEnumerable<TSource> ConcatImpl<TSource>(IEnumerable<TSource> first,
			IEnumerable<TSource> second)
		{
			foreach (TSource item in first)
			{
				yield return item;
			}
			//avoid hanging onto a reference we don't really need
			first = null;
			foreach (TSource item in second)
			{
				yield return item;
			}

		}

		//concat implemented using selectMany
	//	public static IEnumerable<TSource> Concat<TSource>(
	//this IEnumerable<TSource> first,
	//IEnumerable<TSource> second)
	//	{
	//		if (first == null)
	//		{
	//			throw new ArgumentNullException("first");
	//		}
	//		if (second == null)
	//		{
	//			throw new ArgumentNullException("second");
	//		}
	//		return new[] { first, second }.SelectMany(x => x);
	//	}
	}
}
