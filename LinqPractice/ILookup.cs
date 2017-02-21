using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinqPractice
{
	public interface ILookup<TKey, TElement> : IEnumerable<IGrouping<TKey, TElement>>
	{
		int Count { get; }
		IEnumerable<TElement> this[TKey key] { get; }
		bool Contains(TKey key);
	}
}