using LinqPractice;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LinqTests
{
	[TestFixture]
	class JoinTests
	{
		[Test]
		public void ExecutinIsDeferred()
		{
			var outer = new ThrowingEnumerable();
			var inner = new ThrowingEnumerable();
			outer.Join(inner, x => x, y => y, (x, y) => x + y);
		}
		[Test]
		public void OuterSequenceIsStreamed()
		{
			var outer = new[] { 10, 0, 2 }.Select(x => 10 / x);
			var inner = new[] { 1, 2, 3 };
			var query = outer.Join(inner, x => x, y => y, (x, y) => x + y);

			using (var iterator = query.GetEnumerator())
			{
				Assert.IsTrue(iterator.MoveNext());
				Assert.AreEqual(2, iterator.Current);
				Assert.Throws<DivideByZeroException>(() => iterator.MoveNext());
			}
		}
		[Test]
		public void InnerSequenceIsBuffered()
		{
			var outer = new[] { 1, 2, 3 };
			var inner = new[] { 10, 0, 2 }.Select(x => 10 / x);
			var query = outer.Join(inner, x => x, y => y, (x, y) => x + y);
			using (var iterator = query.GetEnumerator())
			{
				Assert.Throws<DivideByZeroException>(() => iterator.MoveNext());
			}
		}
		[Test]
		public void SimpleJoin()
		{
			string[] outer = { "first", "second", "third" };
			string[] inner = { "essence", "offer", "eating", "psalm" };
			var query = outer.Join(inner,
				outerElement => outerElement[0],
				innerElement => innerElement[1],
				(outerElement, innerElement) => outerElement + " " + innerElement);
			query.AssertSequenceEqual("first offer", "second essence", "second psalm");
		}
		[Test]
		public void CustomComparer()
		{
			// We're going to match the start of the outer sequence item
			// with the end of the inner sequence item, in a case-insensitive manner
			string[] outer = { "ABCxxx", "abcyyy", "defzzz", "ghizzz" };
			string[] inner = { "000abc", "111gHi", "222333" };

			var query = outer.Join(inner,
				outerElement => outerElement.Substring(0, 3),
				innerElement => innerElement.Substring(3),
				(outerElement, innerElement) => outerElement + ":" + innerElement,
				StringComparer.OrdinalIgnoreCase);

			query.AssertSequenceEqual("ABCxxx:000abc", "abcyyy:000abc", "ghizzz:111gHi");
		}
		[Test]
		public void DifferentSourceTypes()
		{
			int[] outer = { 5, 3, 7 };
			string[] inner = { "bee", "giraffe", "tiger", "badger", "ox", "cat", "dog" };

			var query = outer.Join(inner,
				outerElement => outerElement,
				innerElement => innerElement.Length,
				(outerElement, innerElement) => outerElement + ":" + innerElement);
			query.AssertSequenceEqual("5:tiger", "3:bee", "3:cat", "3:dog", "7:giraffe");
		}
		[Test]
		public void NullKeys()
		{
			string[] outer = { "first", "null", "nothing", "second" };
			string[] inner = { "nuff", "second" };

			var query = outer.Join(inner,
				outerElement => outerElement.StartsWith("n") ? null : outerElement,
				innerElement => innerElement.StartsWith("n") ? null : innerElement,
				(outerElement, innerElement) => outerElement + ":" + innerElement);

			query.AssertSequenceEqual("second:second");
		}
	}
}
