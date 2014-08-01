//
//  CollectionBase.cs
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
	/// A basic implementation of <see cref="T:ICollection`1"/> interface.
	/// </summary>
	/// <typeparam name='TElement'>
	/// The type of elements stored in the <see cref="T:ICollection`1"/>.
	/// </typeparam>
	public abstract class CollectionBase<TElement> : EnumerableBase<TElement>, ICollection<TElement> {
		
		#region ICollection implementation
		/// <summary>
		/// Gets the number of elements contained in the <see cref="T:ICollection`1" />.
		/// </summary>
		/// <value>The number of elements contained in the <see cref="T:ICollection`1" />.</value>
		public abstract int Count {
			get;
		}

		/// <summary>
		/// Gets a value indicating whether the <see cref="T:ICollection`1" /> is read-only.
		/// </summary>
		/// <value><c>true</c> if the <see cref="T:ICollection`1" /> is read-only;
		/// otherwise, <c>false</c>.</value>
		/// <remarks>
		/// <para>By default, this value is <c>false</c>.</para>
		/// </remarks>
		public virtual bool IsReadOnly {
			get {
				return false;
			}
		}
		#endregion
		#region Constructors
		/// <summary>
		/// Initializes a new instance of the <see cref="T:CollectionBase`1"/> class.
		/// </summary>
		protected CollectionBase () {
		}
		#endregion
		#region ICollection implementation
		/// <summary>
		/// Adds an object to the end of the <see cref="T:ICollection`1"/>.
		/// </summary>
		/// <returns>The <see cref="T:ICollection`1"/> index at which the <paramref name="value"/> has been added.</returns>
		/// <param name="item">The <see cref="T:System.Object" /> to be added to the end of the <see cref="T:ICollection`1" />.</param>
		/// <exception cref="T:System.NotSupportedException"> The <see cref="T:ICollection`1"/> is read-only.</exception>
		/// <exception cref="T:NotSupportedException">The <see cref="T:ICollection`1"/> has a fixed size.</exception>
		public abstract void Add (TElement item);

		/// <summary>
		/// Removes all items from the <see cref="T:System.Collections.IList" />. This implementation always throws <see cref="T:System.NotSupportedException" />.
		/// </summary>
		/// <exception cref="T:System.NotSupportedException"> The <see cref="T:ICollection`1"/> is read-only.</exception>
		/// <exception cref="T:NotSupportedException">The <see cref="T:ICollection`1"/> has a fixed size.</exception>
		public abstract void Clear ();

		/// <summary>
		/// Determines whether an element is in the <see cref="T:ICollection`1"/>.
		/// </summary>
		/// <returns>true if <paramref name="item"/> is found in the <see cref="T:ICollection`1"/>; otherwise,
		/// false.</returns>
		/// <param name="item">The object to locate in this <see cref="T:ICollection`1"/>. The element to locate can
		/// be null for reference types.</param>
		public virtual bool Contains (TElement item) {
			foreach (TElement e in this) {
				if (Object.Equals (item, e)) {
					return true;
				}
			}
			return false;
		}

		/// <summary>
		/// Copies the elements of the collection to a compatible one-dimensional array, starting at the specified index of the target array.
		/// </summary>
		/// <param name="array">The one-dimensional array that is the destination of the elements copied from the current collection.</param>
		/// <param name="arrayIndex">The index in <paramref name="array" /> at which copying begins, optional, by default <c>0</c>. </param>
		/// <remarks>
		/// <para>If the array is not long enough, copying will stop at the end of the array.</para>
		/// <para>If the index is less than one, copying will begin at the first element of the array.</para>
		/// <para>If the given <paramref name="array"/> is not effective, nothing happens.</para>
		/// </remarks>
		public virtual void CopyTo (TElement[] array, int arrayIndex = 0x00) {
			if (array != null) {
				int i = Math.Max (arrayIndex, 0x00);
				int n = array.Length;
				foreach (TElement e in this) {
					if (i < n) {
						array [i++] = e;
					}
				}
			}
		}

		/// <summary>
		/// Removes the first occurrence of an item from the current collection.
		/// </summary>
		/// <param name="item">The object to remove from the <see cref="T:ICollection`1"/>.</param>
		/// <returns><c>true</c> if an object was removed from the <see cref="T:ICollection`1"/>; otherwise <c>false</c>.</returns>
		/// <exception cref="T:System.NotSupportedException"> The <see cref="T:ICollection`1"/> is read-only.</exception>
		/// <exception cref="T:NotSupportedException">The <see cref="T:ICollection`1"/> has a fixed size.</exception>
		public abstract bool Remove (TElement item);
		#endregion
	}
}

