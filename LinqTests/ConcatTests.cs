using LinqPractice;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LinqTests
{
	[TestFixture]
	class ConcatTests
	{

		[Test]
		public void Concatenation()
		{
			IEnumerable<string> first = new string[] { "a" };
			IEnumerable<string> second = new string[] { "b" };
			Enumerator.Concat(first, second).AssertSequenceEqual("a","b");
		}
		[Test]
		public void SimpleConcatenation()
		{
			IEnumerable<string> first = new string[] { "a", "b" };
			IEnumerable<string> second = new string[] { "c", "d" };
			first.Concat(second).AssertSequenceEqual("a", "b", "c", "d");
		}

		[Test]
		public void NullFirstArgument()
		{
			IEnumerable<string> first = null;
			IEnumerable<string> second = new string[] { "a" };
			Assert.Throws<ArgumentNullException>(() => first.Concat(second));
		}

		[Test]
		public void NullSecondArgument()
		{
			IEnumerable<string> first = new string[] { "a" };
			IEnumerable<string> second = null;
			Assert.Throws<ArgumentNullException>(() => first.Concat(second));
		}

	}
}
