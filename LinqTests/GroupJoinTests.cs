using LinqPractice;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LinqTests
{
	[TestFixture]
	class GroupJoinTests
	{
		[Test]
		public void ExecutionIsDeferrred()
		{
			var outer = new ThrowingEnumerable();
			var inner = new ThrowingEnumerable();
			outer.GroupJoin(inner, x => x, y => y, (x, y) => x + y.Count());
		}
		[Test]
		public void SimpleGroupJoin()
		{
			string[] outer = { "first", "second", "third" };
			string[] inner = { "essence", "offer", "eating", "psalm" };

			var query = outer.GroupJoin(inner,
										outerElement => outerElement[0],
										innerElement => innerElement[1],
										(outerElemenent, innerElements) => outerElemenent + ":" + String.Join(";", innerElements));
			query.AssertSequenceEqual("first:offer", "second:essence;psalm", "third:");
		}
		[Test]
		public void CustomComparer()
		{
			// We're going to match the start of the outer sequence item
			// with the end of the inner sequence item, in a case-insensitive manner
			string[] outer = { "ABCxxx", "abcyyy", "defzzz", "ghizzz" };
			string[] inner = { "000abc", "111gHi", "222333", "333AbC" };

			var query = outer.GroupJoin(inner,
								   outerElement => outerElement.Substring(0, 3),
								   innerElement => innerElement.Substring(3),
								   (outerElement, innerElements) => outerElement + ":" + String.Join(";", innerElements),
								   StringComparer.OrdinalIgnoreCase);
			// ABCxxx matches 000abc and 333AbC
			// abcyyy matches 000abc and 333AbC
			// defzzz doesn't match anything
			// ghizzz matches 111gHi
			query.AssertSequenceEqual("ABCxxx:000abc;333AbC", "abcyyy:000abc;333AbC", "defzzz:", "ghizzz:111gHi");
		}

		[Test]
		public void DifferentSourceTypes()
		{
			int[] outer = { 5, 3, 7, 4 };
			string[] inner = { "bee", "giraffe", "tiger", "badger", "ox", "cat", "dog" };

			var query = outer.GroupJoin(inner,
										outerElement => outerElement,
										innerElement => innerElement.Length,
										(outerElement, innerElement) => outerElement + ":" + string.Join(";", innerElement));

			query.AssertSequenceEqual("5:tiger", "3:bee;cat;dog", "7:giraffe", "4:");
		}
		[Test]
		public void NullKeys()
		{
			string[] outer = { "first", null, "second" };
			string[] inner = { "first", "null", "nothing" };
			var query = outer.GroupJoin(inner,
								   outerElement => outerElement,
								   innerElement => innerElement.StartsWith("n") ? null : innerElement,
								   (outerElement, innerElements) => outerElement + ":" + String.Join(";", innerElements));
			// No matches for the null outer key
			query.AssertSequenceEqual("first:first", ":", "second:");
		}

	}
}
