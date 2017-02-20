using LinqPractice;
using NUnit.Framework;
using System;
using System.Collections.Generic;

using System.Text;
using System.Threading.Tasks;

namespace LinqTests
{
	[TestFixture]
	class IntersectTests
	{
		[Test]
		public void NullFirstWithoutComparer()
		{
			string[] first = null;
			string[] second = { };
			Assert.Throws<ArgumentNullException>(() => first.Intersect(second));
		}
		[Test]
		public void NullSecondWithoutComparer()
		{
			string[] first = { };
			string[] second = null;
			Assert.Throws<ArgumentNullException>(() => first.Intersect(second));
		}
		[Test]
		public void NullFirstWithComparer()
		{
			string[] first = null;
			string[] second = { };
			Assert.Throws<ArgumentNullException>(() => first.Intersect(second, StringComparer.Ordinal));
		}
		[Test]
		public void NullSecondWithComparer()
		{
			string[] first = { };
			string[] second = null;
			Assert.Throws<ArgumentNullException>(() => first.Intersect(second, StringComparer.Ordinal));
		}
		[Test]
		public void NoComparerSpecified()
		{
			string[] first = { "A", "a", "b", "c", "b" };
			string[] second = { "b", "a", "d", "a" };
			first.Intersect(second).AssertSequenceEqual("a", "b");
		}
		[Test]
		public void NullComparerSpecified()
		{
			string[] first = { "A", "a", "b", "c", "b" };
			string[] second = { "b", "a", "d", "a" };
			first.Intersect(second, null).AssertSequenceEqual("a", "b");
		}
		[Test]
		public void CaseInsensitiveComparerSpecified()
		{
			string[] first = { "A", "a", "b", "c", "b" };
			string[] second = { "b", "a", "d", "a" };
			first.Intersect(second, StringComparer.OrdinalIgnoreCase).AssertSequenceEqual("A", "b");
		}
		[Test]
		public void NoSequenceUsedBeforeIteration()
		{
			var first = new ThrowingEnumerable();
			var second = new ThrowingEnumerable();
			var query = first.Intersect(second);
			using (var iterator = query.GetEnumerator())
			{ }
		}
		[Test]
		public void SecodSequenceReadFullyOnFirstResultIteration()
		{
			int[] first = { 1 };
			var secondQuery = new[] { 10, 2, 0 }.Select(x => 10 / x);

			var query = first.Intersect(secondQuery);
			using (var iterator = query.GetEnumerator())
			{
				Assert.Throws<DivideByZeroException>(() => iterator.MoveNext());
			}
		}
		[Test]
		public void FirstSequenceOnlyReadAsResultsAreRead()
		{
			var firstQuery = new[] { 10, 2, 0, 2 }.Select(x => 10 / x);
			int[] second = { 1 };
			var query = firstQuery.Intersect(second);
			using( var iterator = query.GetEnumerator())
			{
				Assert.IsTrue(iterator.MoveNext());
				Assert.AreEqual(1, iterator.Current);
				// getting second result requires reading from the input sequence until the 0 division
				Assert.Throws<DivideByZeroException>(() => iterator.MoveNext());
			}
		}
	}
}
