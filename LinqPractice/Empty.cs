using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinqPractice
{
	public static partial class Enumerable
	{
		public static IEnumerable<TResult> Empty<TResult>()
		{
			return EmptyHolder<TResult>.Array;
		}
		private static class EmptyHolder<T>
		{
			internal static readonly T[] Array = new T[0];
		}
	}
}
