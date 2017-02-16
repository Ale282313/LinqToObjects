using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinqPractice
{
	public static partial class Enumerator
	{
		public static IEnumerable<TResult> Repeat<TResult>(TResult element, int count)
		{
			if( count < 0)
			{
				throw new ArgumentOutOfRangeException("count is negative");
			}
			return RepeatImpl(element, count);
		}

		private static IEnumerable<TResult> RepeatImpl<TResult>(TResult element, int count)
		{
			for (int i = 0; i < count; i++)
			{
				yield return element;
			}
		}
	}
}
