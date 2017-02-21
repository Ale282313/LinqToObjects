using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinqPractice
{
	public interface IGrouping<out TKey, out TElement> : IEnumerable<TElement>
	{
		TKey Key { get; }
	}
}
