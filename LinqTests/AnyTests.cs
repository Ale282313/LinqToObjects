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
	class AnyTests
	{
		[Test]
		public void NullSourceThrowsNullArgumentException()
		{
			int[] numbers = null;
			Assert.Throws<ArgumentNullException>(() => numbers.Any(x => x > 10));
		}
		[Test]
		public void NullPredicateThrowsNullArgumentException()
		{
			int[] numbers = { 1, 2 };
			Assert.Throws<ArgumentNullException>(() => numbers.Any(null));
		}
		[Test]
		public void EmptySequence()
		{
			int[] numbers = { };
			Assert.IsFalse(numbers.Any(x => x > 0));
		}
		[Test]
		public void PredicateLessAnyReturnsTrueForNonEmptySequence()
		{
			int[] numbers = { 1 };
			Assert.IsTrue(numbers.Any());
		}
		[Test]
		public void AllElementDoNotMatch()
		{
			int[] numbers = { -1, -2 };
			Assert.IsFalse(numbers.Any(x => x > 0));
		}
		[Test]
		public void SomeElementsMatchThePredicate()
		{
			int[] numbers = { 1, 2, 3, 4 };
			Assert.IsTrue(numbers.Any(x => x % 2 == 0));
		}
		[Test]
		public void SequenceIsNotEvaluatedAfterFirstMatch()
		{
			int[] numbers = { 10, 2, 0, 3 };
			var query = numbers.Select(x => 10 / x);
			Assert.IsTrue(query.Any(y => y > 2));
		}

	}
}
