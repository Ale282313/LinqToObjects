using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinqPractice
{
	public static partial class Enumerable
	{
		public static IEnumerable<TResult> GroupJoin<TOuter, TInner, TKey, TResult>(
			this IEnumerable<TOuter> outer,
			IEnumerable<TInner> inner,
			Func<TOuter, TKey> outerKeySelector,
			Func<TInner, TKey> innerKeySelector,
			Func<TOuter, IEnumerable<TInner>, TResult> resultSelector)
		{
			return outer.GroupJoin(inner, outerKeySelector, innerKeySelector, resultSelector, EqualityComparer<TKey>.Default);
		}

		public static IEnumerable<TResult> GroupJoin<TOuter, TInner, TKey, TResult>(
			this IEnumerable<TOuter> outer,
			IEnumerable<TInner> inner,
			Func<TOuter, TKey> outerKeySelector,
			Func<TInner, TKey> innerKeySelector,
			Func<TOuter, IEnumerable<TInner>, TResult> resultSelector,
			IEqualityComparer<TKey> comparer)
		{
			if (outer == null)
			{
				throw new ArgumentNullException("outer is null");
			}
			if (inner == null)
			{
				throw new ArgumentNullException("inner is null");
			}
			if (outerKeySelector == null)
			{
				throw new ArgumentNullException("outerKeySelector is null");
			}
			if (innerKeySelector == null)
			{
				throw new ArgumentNullException("innerKeySelector is null");
			}
			if (resultSelector == null)
			{
				throw new ArgumentNullException("resultSelector is null");
			}
			return GroupJoinImpl(outer, inner, outerKeySelector, innerKeySelector, resultSelector, comparer ?? EqualityComparer<TKey>.Default);
		}

		private static IEnumerable<TResult> GroupJoinImpl<TOuter, TInner, TKey, TResult>(
			IEnumerable<TOuter> outer,
			IEnumerable<TInner> inner,
			Func<TOuter, TKey> outerKeySelector,
			Func<TInner, TKey> innerKeySelector,
			Func<TOuter, IEnumerable<TInner>, TResult> resultSelector,
			IEqualityComparer<TKey> comparer)
		{
			var lookup = inner.ToLookupNoNullKeys(innerKeySelector, comparer);

			foreach (var outerElement in outer)
			{
				var key = outerKeySelector(outerElement);
				yield return resultSelector(outerElement, lookup[key]);
			}
		}
	}
}
