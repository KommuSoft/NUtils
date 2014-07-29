//
//  ListUtils.cs
//
//  Author:
//       Willem Van Onsem <vanonsem.willem@gmail.com>
//
//  Copyright (c) 2014 Willem Van Onsem
//
//  This program is free software: you can redistribute it and/or modify
//  it under the terms of the GNU General Public License as published by
//  the Free Software Foundation, either version 3 of the License, or
//  (at your option) any later version.
//
//  This program is distributed in the hope that it will be useful,
//  but WITHOUT ANY WARRANTY; without even the implied warranty of
//  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//  GNU General Public License for more details.
//
//  You should have received a copy of the GNU General Public License
//  along with this program.  If not, see <http://www.gnu.org/licenses/>.
using System;
using System.Collections.Generic;
using NUtils.Abstract;

namespace NUtils.Collections {
	/// <summary>
	/// A set of utility functions applicable on <see cref="T:IList`1"/> instances.
	/// </summary>
	public static class ListUtils {

		#region Search methods
		/// <summary>
		/// Search an object in the given ordered <paramref name="list"/> based on the given <paramref name="key"/> and
		/// the <see cref="comparer"/> that also determined the order of the given <paramref name="list"/>.
		/// </summary>
		/// <returns>The index of the found object, or the bitwise negation of the index at which an object
		/// that maps on the given <paramref name="key"/> can be inserted.</returns>
		/// <param name="list">The given ordered list on which the binary search takes place.</param>
		/// <param name="key">The given key that determines which object is searched for.</param>
		/// <param name="comparer">The given <see cref="IExpandComparer`2"/> that determines how the objects
		/// in the given <paramref name="list"/> are ordered and guides the search.</param>
		/// <typeparam name="TKey">The type of <paramref name="key"/> that is used for the search.</typeparam>
		/// <typeparam name="TTarget">The type of objects in the given <paramref name="list"/>.</typeparam>
		public static int BinarySearch<TKey,TTarget> (this IList<TTarget> list, TKey key, IExpandComparer<TKey,TTarget> comparer) {
			int i0 = 0x00, i2 = list.Count - 0x01, i1, res;
			do {
				i1 = (i0 + i2) >> 0x01;
				res = comparer.Compare (key, list [i1]);
				if (res < 0x00) {
					i2 = i1 - 0x01;
				} else if (res > 0x00) {
					i0 = i1 + 0x01;
				} else {
					return i1;
				}
			} while(i0 <= i2);
			return ~i0;
		}
		#endregion
	}
}

