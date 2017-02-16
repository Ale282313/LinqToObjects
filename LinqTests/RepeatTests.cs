using LinqPractice;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
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
			Enumerator.Repeat(a, 3).AssertSequenceEqual("a", "a", "a");
		}

		[Test]
		public void RepeatAnEmptySequence()
		{
			int number = 1;
			Enumerator.Repeat(number, 0).AssertSequenceEqual();
		}

		[Test]
		public void RepeatNullSequence()
		{
			string number = null;
			Enumerator.Repeat(number, 2).AssertSequenceEqual(null, null);
		}

		[Test]
		public void CheckNegativeCount()
		{
			Assert.Throws<ArgumentOutOfRangeException>(() => Enumerator.Repeat("a", -1));
		}
	}
}
