using LinqPractice;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LinqTests
{
	[TestFixture]
	class SelectManyTests
	{

		[Test]
		public void FlattenWithProjectionAndIndex()
		{
			int[] numbers = { 3, 5, 20, 15 };
			var query = numbers.SelectMany((x, index) => (x + index).ToString().ToCharArray(),
				(x, c) => x + ": " + c);
			// 3 => 3:3
			// 5 => 5:6
			// 20 => 20:2 , 20:2 (20+2=22)-> 20:2, 20:2
			// 15 => 15:1, 15:8 (15+3 =18)-> 15:1, 15:8
			query.AssertSequenceEqual("3: 3", "5: 6", "20: 2", "20: 2", "15: 1", "15: 8");
		}

		[Test]
		public void NullSourceThrowsNullArgumentException()
		{
			int[] numbers = null;
			Assert.Throws<ArgumentNullException>(() => numbers.SelectMany(x => x.ToString()));
		}

		[Test]
		public void SimpleFlatten()
		{
			string[] strings = { "aA", "B", "b" };
			var query = strings.SelectMany(x => x.ToString().ToLower());
			query.AssertSequenceEqual('a', 'a', 'b', 'b');
		}

		[Test]
		public void FlattenWithIndex()
		{
			int[] numbers = { 3, 5, 20 };
			var query = numbers.SelectMany((x, index) => (x + index).ToString().ToCharArray());
			query.AssertSequenceEqual('3', '6', '2', '2');
		}
		[Test]
		public void FlattenWithProjection()
		{
			int[] numbers = { 1, 24 };
			var query = numbers.SelectMany(x => x.ToString().ToCharArray(), (x, c) => x + ": " + c);
			query.AssertSequenceEqual("1: 1", "24: 2", "24: 4");
		}
	}
}
