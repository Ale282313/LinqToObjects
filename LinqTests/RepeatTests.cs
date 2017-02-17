using LinqPractice;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LinqTests
{
	[TestFixture]
	class RepeatTests
	{

		[Test]
		public void RepeatAString3Times()
		{
			string a = "a";
			Enumerable.Repeat(a, 3).AssertSequenceEqual("a", "a", "a");
		}

		[Test]
		public void RepeatAnEmptySequence()
		{
			int number = 1;
			Enumerable.Repeat(number, 0).AssertSequenceEqual();
		}

		[Test]
		public void RepeatNullSequence()
		{
			string number = null;
			Enumerable.Repeat(number, 2).AssertSequenceEqual(null, null);
		}

		[Test]
		public void CheckNegativeCount()
		{
			Assert.Throws<ArgumentOutOfRangeException>(() => Enumerable.Repeat("a", -1));
		}
	}
}
