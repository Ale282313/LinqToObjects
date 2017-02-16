using LinqPractice;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LinqTests
{
	[TestFixture]
	class EmptyTests
	{
		[Test]
		public void EmptyContainsNoElements()
		{
			using( var empty= Enumerable.Empty<int>().GetEnumerator())
			{
				Assert.IsFalse(empty.MoveNext());
			}
		}

		[Test]
		public void EmptyIsASingletonPerElementType()
		{
			Assert.AreSame(Enumerable.Empty<int>(), Enumerable.Empty<int>());
			Assert.AreSame(Enumerable.Empty<long>(), Enumerable.Empty<long>());
			Assert.AreSame(Enumerable.Empty<string>(), Enumerable.Empty<string>());
			Assert.AreSame(Enumerable.Empty<object>(), Enumerable.Empty<object>());
			Assert.AreNotSame(Enumerable.Empty<int>(), Enumerable.Empty<long>());
			Assert.AreNotSame(Enumerable.Empty<object>(), Enumerable.Empty<string>());
		}
	}
}
