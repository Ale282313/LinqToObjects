using NUnit.Framework;
using System;
using System.Collections.Generic;
//using System.Linq;
using LinqPractice;
using System.Text;
using System.Threading.Tasks;

namespace LinqTests
{
	[TestFixture]
	class SelectTests
	{

		[Test]
		public void NullSourceThrowsNullArgumentException()
		{
			IEnumerable<int> source = null;
			Assert.Throws<ArgumentNullException>(() => source.Select(x => x + 1));
		}

		[Test]
		public void NullPredicateThrowsNullArgumentException()
		{
			int[] source = { 1, 3, 7, 9, 10 };
			Func<int, bool> predicate = null;
			Assert.Throws<ArgumentNullException>(() => source.Select(predicate));
		}

		[Test]
		public void WithIndexNullSourceThrowsNullArgumentException()
		{
			IEnumerable<int> source = null;
			Assert.Throws<ArgumentNullException>(() => source.Select((x, index) => x + index));
		}

		[Test]
		public void WithIndexNullPredicateThrowsNullArgumentException()
		{
			int[] source = { 1, 3, 7, 9, 10 };
			Func<int, int, bool> predicate = null;
			Assert.Throws<ArgumentNullException>(() => source.Select(predicate));
		}

		[Test]
		public void SimpleProjectionToDifferentType()
		{
			int[] source = { 1, 5, 2 };
			var result = source.Select(x => x.ToString());
			result.AssertSequenceEqual("1", "5", "2");
		}

		[Test]
		public void SideEffectsInProjection()
		{
			int[] source = new int[3];
			int count = 0;
			var query = source.Select(x => count++);
			query.AssertSequenceEqual(0, 1, 2);
			query.AssertSequenceEqual(3, 4, 5);
			count = 10;
			query.AssertSequenceEqual(10, 11, 12);
		}

		[Test]
		public void WhereAndSelect()
		{
			int[] source = { 1, 3, 4, 2, 8, 1 };
			var result = from x in source
						 where x < 4
						 select x * 2;
			result.AssertSequenceEqual(2, 6, 4, 2);
		}

		[Test]
		public void SimpleProjectionQuery()
		{
			int[] source = { 1, 2, 5 };
			var result = from x in source
						 select x * 2;
			result.AssertSequenceEqual(2, 4, 10);
		}

		[Test]
		public void WithIndexSimpleProjectionQuery()
		{
			int[] source = { 1, 2, 3 };
			var result = source.Select((x, index) => x + index);
			result.AssertSequenceEqual(1, 3, 5);

		}
		[Test]
		public void ExecutionIsDeferred()
		{
			ThrowingEnumerable.AssertDeferred(src => src.Select(x => x * 2));
		}


	}
}
