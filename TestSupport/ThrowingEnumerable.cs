using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using NUnit.Framework;

namespace LinqTests
{
	/// <summary>
	/// Class to help for deferred execution tests: it throw an exception
	/// if GetEnumerator is called.
	/// </summary>
	public sealed class ThrowingEnumerable : IEnumerable<int>
	{
		public IEnumerator<int> GetEnumerator()
		{
			throw new InvalidOperationException();
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return GetEnumerator();
		}

		/// <summary>
		/// Check that the given function uses deferred execution.
		/// A "spiked" source is given to the function: the function
		/// call itself shouldn't throw an exception. However, using
		/// the result (by calling GetEnumerator() then MoveNext() on it) *should*
		/// throw InvalidOperationException.
		/// </summary>
		public static void AssertDeferred<T>(
			Func<IEnumerable<int>, IEnumerable<T>> deferredFunction)
		{
			ThrowingEnumerable source = new ThrowingEnumerable();
			var result = deferredFunction(source);
			using (var iterator = result.GetEnumerator())
			{
				Assert.Throws<InvalidOperationException>(() => iterator.MoveNext());
			}
		}
	}
}
