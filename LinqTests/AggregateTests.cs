using LinqPractice;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LinqTests
{
	[TestFixture]
	class AggregateTests
	{
		[Test]
		public void NullSourceUnseeded()
		{
			int[] source = null;
			Assert.Throws<ArgumentNullException>(() => source.Aggregate((x, y) => x + y));
		}
		[Test]
		public void NullFuncUnseeded()
		{
			int[] source = { 1, 2, 3 };
			Assert.Throws<ArgumentNullException>(() => source.Aggregate(null));
		}
		[Test]
		public void UnseededAggregation()
		{
			int[] source = { 1, 4, 5 };
			Assert.AreEqual(17, source.Aggregate((current, value) => current * 2 + value));
		}

		[Test]
		public void NullSourceSedded()
		{
			int[] source = null;
			Assert.Throws<ArgumentNullException>(() => source.Aggregate(3, (x, y) => x + y));
		}

		[Test]
		public void NullFuncSeeded()
		{
			int[] source = { 1, 3 };
			Assert.Throws<ArgumentNullException>(() => source.Aggregate(5, null));
		}
		[Test]
		public void NullSourceSeededWithResultSelector()
		{
			int[] source = null;
			Assert.Throws<ArgumentNullException>(() => source.Aggregate(3, (x, y) => x + y, result => result.ToString()));
		}
		[Test]
		public void NullFuncSeededWithResultSelector()
		{
			int[] source = { 1, 3 };
			Assert.Throws<ArgumentNullException>(() => source.Aggregate(5, null, result => result.ToString()));
		}
		[Test]
		public void NullProjectionSeededWithResultSelector()
		{
			int[] source = { 1, 3 };
			Func<int, string> resultSelector = null;
			Assert.Throws<ArgumentNullException>(() => source.Aggregate(5, (x, y) => x + y, resultSelector));
		}
		[Test]
		public void EmptySequenceUnseeded()
		{
			int[] source = { };
			Assert.Throws<InvalidOperationException>(() => source.Aggregate((x, y) => x + y));
		}
		[Test]
		public void EmptySequenceSeeded()
		{
			int[] source = { };
			Assert.AreEqual(5, source.Aggregate(5, (x, y) => x + y));
		}
		[Test]
		public void EmptySequenceSeededWithResultSelector()
		{
			int[] source = { };
			Assert.AreEqual("5", source.Aggregate(5, (x, y) => x + y, x => x.ToString()));
		}
		[Test]
		public void FirstElementOfInputIsUsedAsSeedForUnseededOverload()
		{
			int[] source = { 5, 3, 2 };
			Assert.AreEqual(30, source.Aggregate((acc, value) => acc * value));
		}

		[Test]
		public void SeededAggregationWithResultSelector()
		{
			int[] source = { 1, 4, 5 };
			int seed = 5;
			Func<int, int, int> func = (current, value) => current * 2 + value;
			Func<int, string> resultSelector = result => result.ToString();
			//First: 5(seed) *2 +1(element) =11
			//Second: 11(result from fisrt)*2 +4(element) = 26
			//Third: 26*2+5 = 57
			//Apply toString projection : 57.ToString()=> "57"
			Assert.AreEqual("57", source.Aggregate(seed, func, resultSelector));
		}

		[Test]
		public void DifferentSourceAndAccumulatorTypes()
		{
			int largeValue = 2000000000;
			int[] source = { largeValue, largeValue, largeValue };
			long sum = source.Aggregate(0L, (acc, value) => acc + value);
			Assert.AreEqual(6000000000L, sum);
			Assert.IsTrue(sum > int.MaxValue);
		}
	}
}
