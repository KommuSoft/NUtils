//
//  CollectionUtils.cs
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

namespace NUtils.Collections {

	/// <summary>
	/// A set of utility functions for <see cref="T:ICollection`1"/> instances.
	/// </summary>
	public static class CollectionUtils {

		#region Mutliple operations at once
		/// <summary>
		/// Add all the given <paramref name="items"/> to the given 
		/// </summary>
		/// <param name="collection">The <see cref="T:ICollection`1"/> instance to which the items are added.</param>
		/// <param name="items">The given <see cref="T:IEnumerable`1"/> of items to add to the <paramref name="collection"/>.</param>
		/// <typeparam name="T">The type of elements stored in the collection and the type of the elements to be added.</typeparam>
		/// <remarks>
		/// <para>If <paramref name="collection"/> or <paramref name="items"/> are not effective, nothing happens.</para>
		/// </remarks>
		public static void AddAll<T> (this ICollection<T> collection, IEnumerable<T> items) {
			if (collection != null && items != null) {
				foreach (T item in items) {
					collection.Add (item);
				}
			}
		}
		#endregion
	}
}

