using LinqPractice;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LinqTests
{
    [TestFixture]
    class ToLookupTests
    {
        [Test]
        public void SourceSequenceIsReadEagerly()
        {
            var source = new ThrowingEnumerable();
            Assert.Throws<InvalidOperationException>(() => source.ToLookup(x => x));
        }
        [Test]
        public void ChangesToSourceAfterToLookupAreNotNoticed()
        {
            List<string> source = new List<string> { "abc" };
            var lookup = source.ToLookup(x => x.Length);
            Assert.AreEqual(1, lookup.Count);
            source.Add("x");
            Assert.AreEqual(1, lookup.Count);
            source.Add("xyz");
            lookup[3].AssertSequenceEqual("abc");
        }
        [Test]
        public void LookupWithNoComparerOrElementSelector()
        {
            string[] source = { "abc", "def", "x", "y", "ghi", "z", "00" };
            var lookup = source.ToLookup(value => value.Length);
            lookup[3].AssertSequenceEqual("abc", "def", "ghi");
            lookup[1].AssertSequenceEqual("x", "y", "z");
            lookup[2].AssertSequenceEqual("00");

            Assert.AreEqual(3, lookup.Count);
            //unknown key return empty sequence
            lookup[100].AssertSequenceEqual();
        }
        [Test]
        public void LookupWithComparerButNoElementSelector()
        {
            string[] source = { "abc", "def", "ABC" };
            var lookup = source.ToLookup(value => value, StringComparer.OrdinalIgnoreCase);
            lookup["abc"].AssertSequenceEqual("abc", "ABC");
            lookup["def"].AssertSequenceEqual("def");
        }
        [Test]
        public void LookupWithNullComparerButNoElementSelector()
        {
            string[] source = { "abc", "def", "ABC" };
            var lookup = source.ToLookup(value => value, null);
            lookup["abc"].AssertSequenceEqual("abc");
            lookup["ABC"].AssertSequenceEqual("ABC");
            lookup["def"].AssertSequenceEqual("def");
        }
        [Test]
        public void LookupWithElementSelectorButNoComparer()
        {
            string[] source = { "abc", "def", "x", "y", "ghi", "z", "00" };
            // Use the length as the key selector, and the first character as the element
            var lookup = source.ToLookup(value => value.Length, value => value[0]);
            lookup[3].AssertSequenceEqual('a', 'd', 'g');
            lookup[1].AssertSequenceEqual('x', 'y', 'z');
            lookup[2].AssertSequenceEqual('0');
        }
        [Test]
        public void LookupWithComparerAndElementSelector()
        {
            var people = new[] {
                new { First = "Jon", Last = "Skeet" },
                new { First = "Tom", Last = "SKEET" }, // Note upper-cased name
                new { First = "Juni", Last = "Cortez" },
                new { First = "Holly", Last = "Skeet" },
                new { First = "Abbey", Last = "Bartlet" },
                new { First = "Carmen", Last = "Cortez" },
                new { First = "Jed", Last = "Bartlet" }
            };

            var lookup = people.ToLookup(p => p.Last, p => p.First, StringComparer.OrdinalIgnoreCase);
            lookup["Skeet"].AssertSequenceEqual("Jon", "Tom", "Holly");
            lookup["Cortez"].AssertSequenceEqual("Juni", "Carmen");
            //comparer used for lookups too
            lookup["BARTLET"].AssertSequenceEqual("Abbey", "Jed");

            lookup.Select(x => x.Key).AssertSequenceEqual("Skeet", "Cortez", "Bartlet");
        }
        [Test]
        public void FindNullKeyNonePresent()
        {
            string[] source = { "first", "second" };
            var lookup = source.ToLookup(x => x);
            lookup[null].AssertSequenceEqual();
        }
        [Test]
        public void FindByNullKeyWhenPresent()
        {
            string[] source = { "first", "null", "nothing", "second" };
            var lookup = source.ToLookup(x => x.StartsWith("n") ? null : x);
            lookup[null].AssertSequenceEqual("null", "nothing");
            lookup.Select(x => x.Key).AssertSequenceEqual("first", null, "second");
            Assert.AreEqual(3, lookup.Count);
        }
    }
}
