//
//  EnumeratorUtils.cs
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

namespace NUtils.Functional {

	/// <summary>
	/// A utility class that provides utility functions for <see cref="IEumerator`1"/> instances.
	/// </summary>
	public static class EnumeratorUtils {

		#region Copying
		/// <summary>
		/// Copies the elements of the <see cref="T:IEumerator`1"/> to a compatible one-dimensional array, starting at the specified index of the target array.
		/// </summary>
		/// <param name="source">The <see cref="T:IEnumerator`1"/> that provides the elements to copy.</param>
		/// <param name="array">The one-dimensional array that is the destination of the elements copied from the current collection.</param>
		/// <param name="index">The index in <paramref name="array"/> at which copying begins.</param>
		/// <exception cref="T:ArgumentNullException"><paramref name="source" /> is null.</exception>
		/// <exception cref="T:ArgumentNullException"><paramref name="array" /> is null.</exception>
		/// <exception cref="T:ArgumentOutOfRangeException"><paramref name="index" /> is less than the lower bound of <paramref name="array" />. </exception>
		/// <exception cref="T:ArgumentException"><paramref name="index" /> is equal to or greater than the length of <paramref name="array" />.</exception>
		/// <exception cref="T:ArgumentException">The number of elements in the collection is greater than the available space from <paramref name="index" /> to the end of the destination <paramref name="array" />.</exception>
		public static void CopyTo<T> (this IEnumerator<T> source, T[] array, int index) {
			if (source != null) {
				if (array != null) {
					if (index >= 0x00) {
						int n = array.Length;
						if (index < n) {
							bool nxt = source.MoveNext ();
							while (nxt && index < n) {
								array [index++] = source.Current;
								nxt = source.MoveNext ();
							}
							if (nxt) {
								throw new ArgumentException ("The number of elements to copy is greater than the available space from the index to the end of the array", "array");
							}
						} else {
							throw new ArgumentException ("The index is greater than or equal to the length of the array.", "index");
						}
					} else {
						throw new ArgumentOutOfRangeException ("index", "The index is less than zero.");
					}
				} else {
					throw new ArgumentNullException ("array");
				}
			} else {
				throw new ArgumentNullException ("source");
			}
		}
		#endregion
	}
}

