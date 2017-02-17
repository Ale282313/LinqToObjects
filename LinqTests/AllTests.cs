using LinqPractice;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LinqTests
{
	[TestFixture]
	class AllTests
	{
		[Test]
		public void NullSourceThrowsNullArgumentException()
		{
			int[] numbers = null;
			Assert.Throws<ArgumentNullException>(() => numbers.All(x=>x%2==0));
		}
		[Test]
		public void NullPredicateThrowsNullArgumentException()
		{
			int[] numbers = { 1, 2 };
			Assert.Throws<ArgumentNullException>(() => numbers.All(null));
		}

		[Test]
		public void EmptySequence()
		{
			int[] numbers = { };
			Assert.IsTrue(numbers.All(x => x > 0));
		}

		[Test]
		public void SomeElementsMatchThePredicate()
		{
			int[] numbers = { 1, 2, -1, -2, 0 };
			Assert.IsFalse(numbers.All(x => x > 0));
		}
		[Test]
		public void AllElementsMatchThePrediacte()
		{
			int[] numbers = { 1, 2, 3 };
			Assert.IsTrue(numbers.All(x => x > 0));
		}
		[Test]
		public void NoneMatchThePredicate()
		{
			int[] numbers = { 1, 2, 3 };
			Assert.IsFalse(numbers.All(x => x < 0));
		}
		[Test]
		public void SequenceIsNotEvaluatedAfterFirstNonMatch()
		{
			int[] numbers = { 1, 2, 10, 0, 3, 4 };
			var query = numbers.Select(x => 10/x);
			//it won't evaluate 10/0 which should throw an exception
			Assert.IsFalse(query.All(y => y > 2));
		}

	}
}
