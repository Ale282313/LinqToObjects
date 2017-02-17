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
	class ConcatTests
	{

		
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

		[Test]
		public void FisrtSequenceIsNotAccessedBeforeFirstUse()
		{
			IEnumerable<int> first = new ThrowingEnumerable();
			IEnumerable<int> second = new int[] { 1 };
			var query = first.Concat(second);

			using (var iterator = query.GetEnumerator())
			{
				//Now is used first
				Assert.Throws<InvalidOperationException>(() => iterator.MoveNext());
			}
		}

		[Test]
		public void SecondSequenceIsNotAccessedBeforeFirstUse()
		{
			IEnumerable<int> first = new int[] { 1 };
			IEnumerable<int> second = new ThrowingEnumerable();

			var query = first.Concat(second);

			using (var iterator = query.GetEnumerator())
			{
				Assert.IsTrue(iterator.MoveNext());
				Assert.AreEqual(1, iterator.Current);
				//now is used second
				Assert.Throws<InvalidOperationException>(() => iterator.MoveNext());
			}
		}

	}
}
