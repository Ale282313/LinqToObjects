
using LinqPractice;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LinqTests
{
	[TestFixture]
	class UnionTests
	{
		[Test]
		public void NullFirstWithoutComparer()
		{
			string[] first = null;
			string[] second = { };
			Assert.Throws<ArgumentNullException>(() => first.Union(second));
			
		}
		[Test]
		public void NullSecondWithoutComparer()
		{
			string[] first = { };
			string[] second = null;
			Assert.Throws<ArgumentNullException>(() => first.Union(second));
		}
		[Test]
		public void NullFirstWithComparer()
		{
			string[] first = null;
			string[] second = { };
			Assert.Throws<ArgumentNullException>(() => first.Union(second, StringComparer.Ordinal));
		}
		[Test]
		public void NullSecondWithComparer()
		{
			string[] first = { };
			string[] second = null;
			Assert.Throws<ArgumentNullException>(() => first.Union(second, StringComparer.Ordinal));
		}
		[Test]
		public void UnionWithoutComparer()
		{
			string[] first = { "a", "b", "B", "c", "b" };
			string[] second = { "d", "e", "d", "a" };
			first.Union(second).AssertSequenceEqual("a", "b", "B", "c", "d", "e");
		}
		[Test]
		public void UnionWithComparerNull()
		{
			string[] first = { "a", "b", "B", "c", "b" };
			string[] second = { "d", "e", "d", "a" };
			first.Union(second, null).AssertSequenceEqual("a", "b", "B", "c", "d", "e");
		}
		[Test]
		public void UnionWithCaseInsensitiveComparer()
		{
			string[] first = { "a", "b", "B", "c", "b" };
			string[] second = { "d", "e", "d", "a" };
			first.Union(second, StringComparer.OrdinalIgnoreCase).AssertSequenceEqual("a", "b", "c", "d", "e");
		}
		[Test]
		public void UnionWithEmptyFirstSequence()
		{
			string[] first = { };
			string[] second = { "d", "e", "d" };
			first.Union(second).AssertSequenceEqual("d", "e");
		}
		[Test]
		public void UnionWithEmptySecondSequence()
		{
			string[] first = { "a", "b", "B", "c", "b" };
			string[] second = { };
			first.Union(second).AssertSequenceEqual("a", "b", "B", "c");
		}

		[Test]
		public void UnionWithTwoEmptySequences()
		{
			string[] first = { };
			string[] second = { };
			first.Union(second).AssertSequenceEqual();
		}
		[Test]
		public void FirstSequenceIsNotUsedUntilQueryIsIterated()
		{
			var first = new ThrowingEnumerable();
			int[] second = { 2 };
			var query = first.Union(second);
			using (var iterator = query.GetEnumerator())
			{
				Assert.Throws<InvalidOperationException>(() => iterator.MoveNext());
			}
		}
		[Test]
		public void SecondSequenceIsNotUSedUntilFirstIsExhausted()
		{
			int[] first = { 3, 5, 3 };
			var second = new ThrowingEnumerable();
			using (var iterator = first.Union(second).GetEnumerator())
			{
				Assert.IsTrue(iterator.MoveNext());
				Assert.AreEqual(3, iterator.Current);
				Assert.IsTrue(iterator.MoveNext());
				Assert.AreEqual(5, iterator.Current);

				Assert.Throws<InvalidOperationException>(() => iterator.MoveNext());
			}
		}
	}
}
