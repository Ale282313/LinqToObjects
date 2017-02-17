using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinqTests
{
	[TestFixture]
	class LastOrDefaultTests
	{
		[Test]
		public void NullSourceWithoutPredicate()
		{
			int[] source = null;
			Assert.Throws<ArgumentNullException>(() => source.LastOrDefault());
		}

		[Test]
		public void NullSourceWithPredicate()
		{
			int[] source = null;
			Assert.Throws<ArgumentNullException>(() => source.LastOrDefault(x => x > 3));
		}

		[Test]
		public void NullPredicate()
		{
			var source = new LinkedList<int>(new int[] { 1, 3, 5 });
			Assert.Throws<ArgumentNullException>(() => source.LastOrDefault(null));
		}

		[Test]
		public void EmptySequenceWithoutPredicate()
		{
			var source = new LinkedList<int>();
			Assert.AreEqual(0, source.LastOrDefault());
		}

		[Test]
		public void SingleElementSequenceWithoutPredicate()
		{
			var source = new LinkedList<int>(new int[] { 5 });
			Assert.AreEqual(5, source.LastOrDefault());
		}

		[Test]
		public void MultipleElementSequenceWithoutPredicate()
		{
			var source = new LinkedList<int>(new int[] { 5, 10 });
			Assert.AreEqual(10, source.LastOrDefault());
		}

		[Test]
		public void EmptySequenceWithPredicate()
		{
			var source = new LinkedList<int>();
			Assert.AreEqual(0, source.LastOrDefault(x => x > 3));
		}

		[Test]
		public void SingleElementSequenceWithMatchingPredicate()
		{
			var source = new LinkedList<int>(new int[] { 5 });
			Assert.AreEqual(5, source.LastOrDefault(x => x > 3));
		}

		[Test]
		public void SingleElementSequenceWithNonMatchingPredicate()
		{
			var source = new LinkedList<int>(new int[] { 2 });
			Assert.AreEqual(0, source.LastOrDefault(x => x > 3));
		}

		[Test]
		public void MultipleElementSequenceWithNoPredicateMatches()
		{
			var source = new LinkedList<int>(new int[] { 1, 2, 2, 1 });
			Assert.AreEqual(0, source.LastOrDefault(x => x > 3));
		}

		[Test]
		public void MultipleElementSequenceWithSinglePredicateMatch()
		{
			var source = new LinkedList<int>(new int[] { 1, 2, 5, 2, 1 });
			Assert.AreEqual(5, source.LastOrDefault(x => x > 3));
		}

		[Test]
		public void MultipleElementSequenceWithMultiplePredicateMatches()
		{
			var source = new LinkedList<int>(new int[] { 1, 2, 5, 10, 2, 1 });
			Assert.AreEqual(10, source.LastOrDefault(x => x > 3));
		}
	}
}
