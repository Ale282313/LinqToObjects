using LinqPractice;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LinqTests
{
	[TestFixture]
	class LongCountTests
	{
		[Test]
		public void NonCollectionCount()
		{
			Assert.AreEqual(5, Enumerable.Range(2, 5).LongCount());
		}

		[Test]
		public void GenericCollectionCount()
		{
			Assert.AreEqual(5, new List<int>(Enumerable.Range(2, 5)).LongCount());
		}

		[Test]
		public void NullSourceThrowsArgumentNullException()
		{
			IEnumerable<int> source = null;
			Assert.Throws<ArgumentNullException>(() => source.LongCount());
		}

		[Test]
		public void NullPredicateThrowsArgumentNullException()
		{
			Assert.Throws<ArgumentNullException>(() => new int[0].LongCount(null));
		}

		[Test]
		public void WithPredicateNullSource()
		{
			IEnumerable<int> source = null;
			Assert.Throws<ArgumentNullException>(() => source.LongCount(x => x == 1));
		}

		[Test]
		public void PredicateCount()
		{
			Assert.AreEqual(3, Enumerable.Range(2, 5).LongCount(x => x % 2 == 0));
		}
	}
}
