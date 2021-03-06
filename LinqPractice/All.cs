﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinqPractice
{
	public static partial class Enumerable
	{
		public static bool All<TSource>(this IEnumerable<TSource> source, Func<TSource,bool> predicate)
		{
			if(source == null)
			{
				throw new ArgumentNullException("source is null");
			}
			if(predicate == null)
			{
				throw new ArgumentNullException("predicate is null");
			}
			foreach (TSource item in source)
			{
				if (!predicate(item))
				{
					return false;
				}
			}
			return true;
		}
	}
}
