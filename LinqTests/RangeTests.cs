
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
	class RangeTests
	{

		[Test]
		public void NegativeCount()
		{
			Assert.Throws<ArgumentOutOfRangeException>(() => Enumerable.Range(10, -1));

		}

		[Test]
		public void CountTooLarge()
		{
			Assert.Throws<ArgumentOutOfRangeException>(() => Enumerable.Range(int.MaxValue, 2));
			Assert.Throws<ArgumentOutOfRangeException>(() => Enumerable.Range(2, int.MaxValue));
		}

		[Test]
		public void ValidRange()
		{
			Enumerable.Range(5, 3).AssertSequenceEqual(5, 6, 7);
		}

		[Test]
		public void NegativeStart()
		{
			Enumerable.Range(-2, 5).AssertSequenceEqual(-2, -1, 0, 1, 2);
		}

		[Test]
		public void EmptyRange()
		{
			Enumerable.Range(2, 0).AssertSequenceEqual();
		}
	}
}
