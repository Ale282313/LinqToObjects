using LinqPractice;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LinqTests
{
	[TestFixture]
	class GroupByTests
	{
		[Test]
		public void ExecutionIsPartiallyDeferred()
		{
			new ThrowingEnumerable().GroupBy(x => x);
		}
		[Test]
		public void SequenceIsReadFullyBeforeFirstResultReturned()
		{
			int[] numbers = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 0 };
			var query = numbers.Select(x => 10 / x);
			var groups = query.GroupBy(x => x);
			Assert.Throws<DivideByZeroException>(() =>
			{
				using (var iterator = groups.GetEnumerator())
				{
					iterator.MoveNext();
				}
			});
		}
		[Test]
		public void SimpleGrouping()
		{
			string[] source = { "abc", "hello", "def", "there", "four" };
			var groups = source.GroupBy(x => x.Length);
			var list = groups.ToList();
			Assert.AreEqual(3, list.Count);

			list[0].AssertSequenceEqual("abc", "def");
			Assert.AreEqual(3, list[0].Key);
			list[1].AssertSequenceEqual("hello", "there");
			Assert.AreEqual(5, list[1].Key);
			list[2].AssertSequenceEqual("four");
			Assert.AreEqual(4, list[2].Key);
		}
		[Test]
		public void GroupByWithElementProjection()
		{
			string[] source = { "abc", "hello", "def", "there", "four" };
			var groups = source.GroupBy(x => x.Length, x => x[0]);

			var list = groups.ToList();
			Assert.AreEqual(3, list.Count);

			list[0].AssertSequenceEqual('a','d');
			Assert.AreEqual(3, list[0].Key);

			list[1].AssertSequenceEqual('h', 't');
			Assert.AreEqual(5, list[1].Key);

			list[2].AssertSequenceEqual('f');
			Assert.AreEqual(4, list[2].Key);
		}
		[Test]
		public void GroupByWithElementProjectionAndCollectionProjection()
		{
			string[] source = { "abc", "hello", "def", "there", "four" };
			var groups = source.GroupBy(x => x.Length,
										x => x[0],
										(key, values) => key + ":" + string.Join(";", values));
			groups.AssertSequenceEqual("3:a;d", "5:h;t", "4:f");
		}
		[Test]
		public void ChangesToSourceAreIgnoredInWhileIteratingOverResultsAfterFirstElementRetrieved()
		{
			var source = new List<string> { "a", "b", "c", "def" };
			var groups = source.GroupBy(x => x.Length);
			using( var iterator = groups.GetEnumerator())
			{
				Assert.IsTrue(iterator.MoveNext());
				iterator.Current.AssertSequenceEqual("a", "b", "c");

				source.Add("ghi");
				Assert.IsTrue(iterator.MoveNext());
				iterator.Current.AssertSequenceEqual("def");

				Assert.IsFalse(iterator.MoveNext());
			}

			using(var iterator = groups.GetEnumerator())
			{
				Assert.IsTrue(iterator.MoveNext());
				iterator.Current.AssertSequenceEqual("a", "b", "c");

				Assert.IsTrue(iterator.MoveNext());
				iterator.Current.AssertSequenceEqual("def", "ghi");
			}
		}
		[Test]
		public void NullKeys()
		{
			string[] source = { "first", "null", "nothing", "second" };
			var groups = source.GroupBy(x => x.StartsWith("n") ? null : x,
										(key, values) => key + ":" + string.Join(";", values));
			groups.AssertSequenceEqual("first:first", ":null;nothing", "second:second");
		}
	}
}
