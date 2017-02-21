using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LinqPractice
{
	internal sealed class Lookup<TKey, TElement> : ILookup<TKey, TElement>
	{
		private readonly NullKeyFriendlyDictionary<TKey, Grouping<TKey, TElement>> map;
		private readonly List<TKey> keys;

		internal Lookup(IEqualityComparer<TKey> comparer)
		{
			map = new NullKeyFriendlyDictionary<TKey, Grouping<TKey, TElement>>(comparer);
			keys = new List<TKey>();
		}

		internal void Add(TKey key, TElement element)
		{
			Grouping<TKey, TElement> group;
			if (!map.TryGetValue(key, out group))
			{
				group = new Grouping<TKey, TElement>(key);
				map[key] = group;
				keys.Add(key);
			}
			group.Add(element);
		}

		public int Count
		{
			get { return map.Count; }
		}

		public IEnumerable<TElement> this[TKey key]
		{
			get
			{
				Grouping<TKey, TElement> group;
				if (!map.TryGetValue(key, out group))
				{
					return Enumerable.Empty<TElement>();
				}
				return group;
			}
		}

		public bool Contains(TKey key)
		{
			return map.ContainsKey(key);
		}

		public IEnumerator<IGrouping<TKey, TElement>> GetEnumerator()
		{
			return keys.Select<TKey, IGrouping<TKey, TElement>>(key => map[key])
					   .GetEnumerator();
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return GetEnumerator();
		}
	}
}
