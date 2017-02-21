using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinqPractice
{
	public static partial class Enumerable
	{
		public static IEnumerable<TResult> Join<TOuter,TInner,TKey,TResult>(
			this IEnumerable<TOuter> outer,
			IEnumerable<TInner> inner,
			Func<TOuter,TKey> outerSelector,
			Func<TInner,TKey> innerSelector,
			Func<TOuter,TInner,TResult> resultSelector)
		{
			return Join(outer, inner, outerSelector, innerSelector, resultSelector, EqualityComparer<TKey>.Default);
		}
		public static IEnumerable<TResult> Join<TOuter,TInner,TKey,TResult>(
			this IEnumerable<TOuter> outer,
			IEnumerable<TInner> inner,
			Func<TOuter,TKey> outerSelector,
			Func<TInner,TKey> innerSelector,
			Func<TOuter,TInner,TResult> resultSelector,
			IEqualityComparer<TKey> comparer)
		{
			if(outer == null)
			{
				throw new ArgumentNullException("outer is null");
			}
			if (inner == null)
			{
				throw new ArgumentNullException("inner is null");
			}
			if (outerSelector == null)
			{
				throw new ArgumentNullException("outerSelector is null");
			}
			if(innerSelector == null)
			{
				throw new ArgumentNullException("innerSelector is null");
			}
			if(resultSelector == null)
			{
				throw new ArgumentNullException("resultSelector is null");
			}
			return JoinImpl(outer, inner, outerSelector, innerSelector, resultSelector, comparer ?? EqualityComparer<TKey>.Default);
		}

		private static IEnumerable<TResult> JoinImpl<TOuter,TInner,TKey,TResult>(
			 IEnumerable<TOuter> outer,
			IEnumerable<TInner> inner,
			Func<TOuter, TKey> outerKeySelector,
			Func<TInner, TKey> innerKeySelector,
			Func<TOuter, TInner, TResult> resultSelector,
			IEqualityComparer<TKey> comparer)
		{
			var lookup = inner.ToLookupNoNullKeys(innerKeySelector, comparer);
			foreach (var outerElement in outer)
			{
				var key = outerKeySelector(outerElement);
				foreach (var innerElement in lookup[key])
				{
					yield return resultSelector(outerElement, innerElement);
				}
			}
		}
	}
}
