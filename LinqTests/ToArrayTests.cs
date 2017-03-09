using LinqPractice;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using TestSupport;

namespace LinqTests
{
	[TestFixture]
	class ToArrayTests
	{
		[Test]
		public void resultIsIndependentOfSource()
		{
			List<string> source = new List<string>() { "xyz", "abc" };

			string[] result = Enumerable.ToArray(source);

			source[0] = "xxxx";
			Assert.AreEqual("xyz", result[0]);

			result[1] = "yyy";
			Assert.AreEqual("abc", source[1]);
		}
		[Test]
		public void sequenceIsEvaluatedEagerly()
		{
			int[] numbers = { 5, 3, 0 };
			var query = numbers.Select(x => 10 / x);
			Assert.Throws<DivideByZeroException>(() => query.ToArray());
		}
		[Test]
		public void NullSource()
		{
			IEnumerable<string> source = null;
			Assert.Throws<ArgumentNullException>(() => source.ToArray());
		}
		[Test]
		public void ConversionOfLazyEvaluatedSequence()
		{
			var range = Enumerable.Range(3, 3);
			var query = range.Select(x => x * 2);
			var list = query.ToArray();
			list.AssertSequenceEqual(6, 8, 10);
		}
		[Test]
		public void IcollectionOptimization()
		{
			var source = new NonEnumerableCollection<string> { "hello", "there" };
			var list = source.ToArray();
			list.AssertSequenceEqual("hello", "there");
		}
	}
}
