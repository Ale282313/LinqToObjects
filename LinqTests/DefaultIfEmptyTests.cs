using LinqPractice;
using NUnit.Framework;
using System;
using System.Collections.Generic;

using System.Text;
using System.Threading.Tasks;

namespace LinqTests
{
	[TestFixture]
	class DefaultIfEmptyTests
	{

		[Test]
		public void NullSourceWithoutDefaultValueThrowsNullArgumentException()
		{
			int[] numbers = null;
			Assert.Throws<ArgumentNullException>(() => numbers.DefaultIfEmpty());
		}
		[Test]
		public void NullSourceWithDefaultValueThrowsNullArgumentException()
		{
			int[] numbers = null;
			Assert.Throws<ArgumentNullException>(() => numbers.DefaultIfEmpty(0));
		}
		[Test]
		public void NoDefaultValueSpecifiedEmptyInputSequence()
		{
			int[] numbers = {  };
			numbers.DefaultIfEmpty().AssertSequenceEqual(0);
		}

		[Test]
		public void DefaultValueSpecifiedEmptyInputSequence()
		{
			int[] numbers = { };
			numbers.DefaultIfEmpty(3).AssertSequenceEqual(3);
		}

		[Test]
		public void NoDefaultValueSpecifiedNonEmptyInputSequence()
		{
			int[] numbers = { 1 };
			numbers.DefaultIfEmpty().AssertSequenceEqual(1);
		}
		[Test]
		public void DefaultValueSpecifiedNonEmptyInputSequence()
		{
			int[] numbers = { 1 };
			numbers.DefaultIfEmpty(3).AssertSequenceEqual(1);
		}
	}

}
