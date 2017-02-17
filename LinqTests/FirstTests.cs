using LinqPractice;
using NUnit.Framework;
using System;
using System.Collections.Generic;
//using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinqTests
{
	[TestFixture]
	class FirstTests
	{
		[Test]
		public void NullSourcePredicateLess()
		{
			int[] numbers = null;
			Assert.Throws<ArgumentNullException>(() => numbers.First());
		}
		[Test]
		public void NullSourceWithPredicate()
		{
			int[] numbers = null;
			Assert.Throws<ArgumentNullException>(() => numbers.First(x => x > 0));
		}
		[Test]
		public void NullPredicate()
		{
			int[] numbers = { 1 };
			Assert.Throws<ArgumentNullException>(() => numbers.First(null));
		}
		[Test]
		public void EmptySequencePredicateLess()
		{
			int[] numbers = { };
			Assert.Throws<InvalidOperationException>(() => numbers.First());
		}
		[Test]
		public void EmptySequenceWithPredicate()
		{
			int[] numbers = { };
			Assert.Throws<InvalidOperationException>(() => numbers.First(x => x > 0));
		}
		[Test]
		public void SingleElementPredicateLess()
		{
			int[] numbers = { 1 };
			Assert.AreEqual(1, numbers.First());
		}
		[Test]
		public void SingleElementMatchPredicate()
		{
			int[] numbers = { 1 };
			Assert.AreEqual(1, numbers.First(x => x > 0));
		}
		[Test]
		public void SingleElementNotMatchPredicate()
		{
			int[] numbers = { 1 };
			Assert.Throws<InvalidOperationException>(() => numbers.First(x => x < 0));
		}
		[Test]
		public void MultipleElementsNoPredicate()
		{
			int[] numbers = { 1, 2, 3 };
			Assert.AreEqual(1, numbers.First());
		}
		[Test]
		public void MultipleElementsOneMatch()
		{
			int[] numbers = { 1, 2, 3 };
			Assert.AreEqual(2, numbers.First(x => x % 2 == 0));
		}
		[Test]
		public void MultipleElementsMultipleMatch()
		{
			int[] numbers = { 1, 2, 3, 4 };
			Assert.AreEqual(2, numbers.First(x => x % 2 == 0));
		}
		[Test]
		public void MultipleElementsNoMatch()
		{
			int[] numbers = { 1, 2, 3, 4 };
			Assert.Throws<InvalidOperationException>(() => numbers.First(x => x % 10 == 0));
		}
		[Test]
		public void ReturnAfterFindingFirstElementNoPredicate()
		{
			int[] numbers = { 2, 1, 0, 3 };
			var query = numbers.Select(x => 10 / x);
			//it should stop after checking the first element
			Assert.AreEqual(5, query.First());
		}
		[Test]
		public void ReturnAfterFirstElementWithPredicate()
		{
			int[] numbers = { 15, 1, 0, 4 };
			var query = numbers.Select(x => 10 / x);
			Assert.AreEqual(10, query.First(y => y > 5));
		}

	}
}
