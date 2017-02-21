using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinqPractice
{
	internal sealed class Grouping<TKey,TElement>:IGrouping<TKey,TElement>,IList<TElement>
	{
		private readonly TKey key;
		private readonly List<TElement> list;
		internal Grouping(TKey key)
		{
			this.key = key;
			this.list = new List<TElement>();
		}
		public TKey Key { get { return key; } }

		public IEnumerator<TElement> GetEnumerator()
		{
			return list.GetEnumerator();
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return GetEnumerator();
		}
		internal void Add(TElement item)
		{
			list.Add(item);
		}

		public int IndexOf(TElement item)
		{
			return list.IndexOf(item);
		}

		void IList<TElement>.Insert(int index, TElement item)
		{
			throw new NotSupportedException();
		}

		void IList<TElement>.RemoveAt(int index)
		{
			throw new NotSupportedException();
		}

		public TElement this[int index]
		{
			get { return list[index]; }
			set { throw new NotSupportedException(); }
		}

		void ICollection<TElement>.Add(TElement item)
		{
			throw new NotSupportedException();
		}

		void ICollection<TElement>.Clear()
		{
			throw new NotSupportedException();
		}

		public bool Contains(TElement item)
		{
			return list.Contains(item);
		}

		public void CopyTo(TElement[] array, int arrayIndex)
		{
			list.CopyTo(array, arrayIndex);
		}

		public int Count
		{
			get { return list.Count; }
		}

		public bool IsReadOnly
		{
			get { return true; }
		}

		bool ICollection<TElement>.Remove(TElement item)
		{
			throw new NotSupportedException();
		}


	}
}
