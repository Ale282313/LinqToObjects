using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinqTests
{
	[TestFixture]
	class CountTests
	{
		[Test]
		public void NonCollectionCount()
		{
			Assert.AreEqual(5, Enumerable.Range(2, 5).Count());
		}

		[Test]
		public void GenericCollectionCount()
		{
			Assert.AreEqual(5, new List<int>(Enumerable.Range(2, 5)).Count());
		}

		[Test]
		public void NullSourceThrowsArgumentNullException()
		{
			IEnumerable<int> source = null;
			Assert.Throws<ArgumentNullException>(() => source.Count());
		}

		[Test]
		public void NullPredicateThrowsArgumentNullException()
		{
			Assert.Throws<ArgumentNullException>(() => new int[0].Count(null));
		}

		[Test]
		public void WithPredicateNullSource()
		{
			IEnumerable<int> source = null;
			Assert.Throws<ArgumentNullException>(() => source.Count(x => x == 1));
		}

		[Test]
		public void PredicateCount()
		{
			Assert.AreEqual(3, Enumerable.Range(2, 5).Count(x => x % 2 == 0));
		}
	}
}
